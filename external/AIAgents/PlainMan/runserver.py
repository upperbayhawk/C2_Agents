# runserver.py

# Copyright (C) Upperbay Systems, LLC - All Rights Reserved
# Unauthorized copying of this file, via any medium is strictly prohibited
# Proprietary and confidential
# Written by Dave Hardin <dave@upperbay.com>, 2023

"""
Main proxy server for the OpenAI Assistant
"""
import asyncio
import sys
import os
import queue
import threading
#import logging
import logbook
import json

import arrow
import paho.mqtt.client as mqtt
from concurrent.futures import ThreadPoolExecutor
from openai import OpenAI

from xcachelib import data_cache_instance
from openailib import openailib_instance
import config

agent_name = config.agent_name

logging_level = config.logging_level

agent_output_file = ".\\logs\\server_output.txt"
agent_log_file = ".\\logs\\server_log.txt"
agent_init_prompt_file = ".\\data\\InitialPrompt.txt"
agent_init_instructions_file = ".\\data\\InitialInstructions.txt"

MQTT_BROKER = config.MQTT_BROKER
MQTT_PORT = config.MQTT_PORT
MQTT_TOPIC_TOASSISTANT = config.MQTT_TOPIC_TOASSISTANT
MQTT_TOPIC_TOCLIENT = config.MQTT_TOPIC_TOCLIENT
MQTT_TOPIC_DATAFEED = config.MQTT_TOPIC_DATAFEED
SEPARATOR = "======================"

#-----------------------------------------------------

# Configure logging
#logging.basicConfig(filename=agent_log_file, encoding='utf-8',level=logging.INFO)
#logger = logging.getLogger(__name__)

logbook.FileHandler(agent_log_file,level=logging_level).push_application()
log = logbook.Logger("runserver")
log.debug("Hello From Below: " + agent_name)
#-----------------------------------------------------
in_queue = queue.Queue()
datafeed_queue = queue.Queue()
clientMQ = mqtt.Client()

# MQTT Callbacks
def on_connect(client, userdata, flags, rc):
    log.debug(f"Connected with result code {rc}")

def on_message(client, userdata, msg):
    log.debug(f"Received message '{msg.payload.decode()}' on topic '{msg.topic}'")
    pipe_input = str(msg.payload.decode())
    if msg.topic == MQTT_TOPIC_TOASSISTANT:
        log.debug("To Assistant Inbound: " + msg.topic + " " + pipe_input)
        print("To Assistant Inbound: " + msg.topic + " " + pipe_input)
        process_incoming_message(pipe_input)
    elif msg.topic == MQTT_TOPIC_TOCLIENT:
        log.debug("To Client Outbound: " + msg.topic + " " + pipe_input)
        print("To Client Outbound: " + msg.topic + " " + pipe_input)
        process_outgoing_message(pipe_input)
        pass
    elif msg.topic == MQTT_TOPIC_DATAFEED:
        log.debug("To DataFeed: " + msg.topic + " " + pipe_input)
        print("To DataFeed: " + msg.topic + " " + pipe_input)
        process_datafeed_message(pipe_input)
        pass

# Asynchronous function to read from stdin
async def async_input(prompt):
    with ThreadPoolExecutor(1) as executor:
        return await asyncio.get_event_loop().run_in_executor(executor, input, prompt)

# Message Processors
def process_incoming_message(message):
    log.trace("Processing Incoming: " + message)
    in_queue.put(message)

def process_outgoing_message(message):
    log.trace("Processing Outgoing: " + message)
    #out_queue.put(message)

def process_local_message(message):
    log.trace("Processing Local: " + message)
    in_queue.put(message)

def process_datafeed_message(message):
    log.trace("Processing DataFeed: " + message)
    datafeed_queue.put(message)

def datafeed_worker_thread():
    while True:
        message = datafeed_queue.get()
        if message is None:
            break
        data = json.loads(message)
        tagname = data["name"]
        tagvalue = data["value"]
        tagstatus = data["status"]
        data_cache_instance.write(tagname, tagvalue,tagstatus)
        value =  data_cache_instance.read(tagname)
        log.debug("value from cache = "  + value)

# Main worker thread
def main_worker_thread():
    while True:
        try:
            message = in_queue.get()
            if message is None:
                break
            msg_return = openailib_instance.run(message)
            if msg_return == "OK":
                last_message = openailib_instance.last_message
                if last_message != "NULL":
                    log.debug("Finished and publishing results to client: " + last_message)

                    clientMQ.publish(MQTT_TOPIC_TOCLIENT, last_message)
                    log.debug("last_message: " + last_message)
                else:
                    log.error("WORKER ERROR. Last message is null.")
            else:
                log.error("WORKER ERROR PROCESSING INPUT")
        except Exception as ex:
            log.error("Exception " + str(ex))
            print ("Exception " + str(ex))
#-------------------------------------------------------

# Main async function
async def main():
    # Set up MQTT client
    clientMQ.on_connect = on_connect
    clientMQ.on_message = on_message
    clientMQ.connect(MQTT_BROKER, MQTT_PORT, 60)
    clientMQ.subscribe(MQTT_TOPIC_TOCLIENT)
    clientMQ.subscribe(MQTT_TOPIC_TOASSISTANT)
    clientMQ.subscribe(MQTT_TOPIC_DATAFEED)

    with open(agent_output_file, "a", encoding="utf-8") as file:
        file.write("\n" + SEPARATOR + "\n\n")
    
    # Start the MQTT client loop in a separate thread
    executer2 = ThreadPoolExecutor(max_workers=10)
    loop = asyncio.get_running_loop()
    await loop.run_in_executor(executer2, clientMQ.loop_start)

    # Start worker in separate thread
    my_main_thread = threading.Thread(target=main_worker_thread)
    my_main_thread.daemon = True
    my_main_thread.start()
    
    # Start worker in separate thread
    my_datafeed_thread = threading.Thread(target=datafeed_worker_thread)
    my_datafeed_thread.daemon = True
    my_datafeed_thread.start()

    try:
        openailib_instance.initialize(agent_name,agent_init_prompt_file,agent_init_instructions_file,agent_output_file)
    except Exception as ex:
        log.error("Exception: " + str(ex))
        print ("Exception: " + str(ex))
   
    try:
        while True:
            user_input = await async_input("Enter message (or type 'x' to quit): ")
            if user_input.lower() == 'x':
                openailib_instance.close()
                break

            print(user_input)
            log.debug(user_input)

            utc= arrow.utcnow()
            local=utc.to('US/Eastern')
            print ("Time " + str(local) + "\n")

            process_local_message(user_input)

    finally:
        clientMQ.loop_stop()
        clientMQ.disconnect()

if __name__ == "__main__":

    if len(sys.argv) > 1:
        agent_name = sys.argv[1]
    else:
        pass
    
    print(f"Agent Name: {agent_name}")        
    asyncio.run(main())