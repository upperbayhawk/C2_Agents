# runserver.py

# Copyright (C) Upperbay Systems, LLC - All Rights Reserved
# Unauthorized copying of this file, via any medium is strictly prohibited
# Proprietary and confidential
# Written by Dave Hardin <dave@upperbay.com>, 2023

import asyncio
import sys
import os
import queue
import threading

from openailib import openailib_instance
import paho.mqtt.client as mqtt
from concurrent.futures import ThreadPoolExecutor
from openai import OpenAI
import arrow
#import logging
import logbook
from logbook import Logger, FileHandler

logging_level = logbook.DEBUG

agent_name = "Funky"
agent_output_file = ".\\logs\\server_output.txt"
agent_log_file = ".\\logs\\server_log.txt"
agent_init_file = ".\\data\\InitPrompt.txt"
agent_instructions_file = ".\\data\\Instructions.txt"

# MQTT Broker settings
MQTT_BROKER = "localhost"
MQTT_PORT = 1883
#MQTT_TOPIC = "DataVariable/All"
MQTT_TOPIC_TOCLIENT = "openai/assistant/" + agent_name + "/ToClient"
MQTT_TOPIC_TOASSISTANT = "openai/assistant/" + agent_name + "/ToAssistant"
SEPARATOR = "======================"

#-----------------------------------------------------

# Configure logging
#logging.basicConfig(filename=agent_log_file, encoding='utf-8',level=logging.INFO)
#logger = logging.getLogger(__name__)

FileHandler(agent_log_file,level=logging_level).push_application()
log = Logger("runserver")
#log.info("Hello From Below")
#-----------------------------------------------------
in_queue = queue.Queue()
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
        process_message(pipe_input)
    elif msg.topic == MQTT_TOPIC_TOCLIENT:
        log.debug("To Client Outbound: " + msg.topic + " " + pipe_input)
        print("To Client Outbound: " + msg.topic + " " + pipe_input)
        pass

# Asynchronous function to read from stdin
async def async_input(prompt):
    with ThreadPoolExecutor(1) as executor:
        return await asyncio.get_event_loop().run_in_executor(executor, input, prompt)

def process_message(message):
    log.debug("Processing: " + message)
    in_queue.put(message)
 
def worker_thread():
    while True:
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
                log.error("WORKER ERROR")
        else:
            log.error("WORKER ERROR PROCESSING INPUT")  
#-------------------------------------------------------

# Main async function
async def main():
 # Set up MQTT client
   
    clientMQ.on_connect = on_connect
    clientMQ.on_message = on_message
    clientMQ.connect(MQTT_BROKER, MQTT_PORT, 60)
    clientMQ.subscribe(MQTT_TOPIC_TOCLIENT)
    clientMQ.subscribe(MQTT_TOPIC_TOASSISTANT)

    with open(agent_output_file, "a", encoding="utf-8") as file:
        file.write("\n" + SEPARATOR + "\n\n")
    
    # Start the MQTT client loop in a separate thread
    executer2 = ThreadPoolExecutor(max_workers=10)
    loop = asyncio.get_running_loop()
    await loop.run_in_executor(executer2, clientMQ.loop_start)

    # Start worker in separate thread
    my_thread = threading.Thread(target=worker_thread)
    my_thread.daemon = True
    my_thread.start()
    
    openailib_instance.initialize(agent_name,agent_init_file,agent_instructions_file,agent_output_file)
   
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

            clientMQ.publish(MQTT_TOPIC_TOCLIENT, user_input)
            process_message(user_input)

    finally:
        clientMQ.loop_stop()
        clientMQ.disconnect()

if __name__ == "__main__":

    if len(sys.argv) > 1:
        agent_name = sys.argv[1]
        print(f"Agent Name: {agent_name}")
    else:
        #print("No argument provided")
        pass
        
    asyncio.run(main())