# runclient.py

# Copyright (C) Upperbay Systems, LLC - All Rights Reserved
# Unauthorized copying of this file, via any medium is strictly prohibited
# Proprietary and confidential
# Written by Dave Hardin <dave@upperbay.com>, 2023

import asyncio
import sys
import os

import paho.mqtt.client as mqtt
from concurrent.futures import ThreadPoolExecutor
import arrow
from logbook import Logger, FileHandler
import logbook

agent_name = "Funky"
agent_output_file = ".\\data\\client_output.txt"
agent_log_file = ".\\logs\\client_log.txt"

# MQTT Broker settings
MQTT_BROKER = "localhost"
MQTT_PORT = 1883
#MQTT_TOPIC = "DataVariable/All"
MQTT_TOPIC_TOASSISTANT = "openai/assistant/" + agent_name + "/ToAssistant"
MQTT_TOPIC_TOCLIENT = "openai/assistant/" + agent_name + "/ToClient"
SEPARATOR = "========================="

#-----------------------------------------------------

FileHandler(agent_log_file,level=logbook.DEBUG).push_application()
log = Logger("runclient", 0)
log.debug("Hello From Below")
#-----------------------------------------------------

# MQTT Callbacks
def on_connect(client, userdata, flags, rc):
    log.debug(f"Connected with result code {rc}")

def on_message(client, userdata, msg):
    log.debug(f"Received message '{msg.payload.decode()}' on topic '{msg.topic}'")
    input = str(msg.payload.decode())
    if msg.topic == MQTT_TOPIC_TOCLIENT:
        print(input)
        log.debug("From Assistant: " + input)

    elif msg.topic == MQTT_TOPIC_TOASSISTANT:
        log.debug("To Assistant: " + input)
        print("To Assistant: " + input)
    
# Asynchronous function to read from stdin
async def async_input(prompt):
    with ThreadPoolExecutor(1) as executor:
        return await asyncio.get_event_loop().run_in_executor(executor, input, prompt)

#-----------------------------------------------------

# Main async function

async def main():
 # Set up MQTT client
    clientMQ = mqtt.Client()
    clientMQ.on_connect = on_connect
    clientMQ.on_message = on_message
    clientMQ.connect(MQTT_BROKER, MQTT_PORT, 60)
    clientMQ.subscribe(MQTT_TOPIC_TOCLIENT)
    clientMQ.subscribe(MQTT_TOPIC_TOASSISTANT)
    
    log.debug(SEPARATOR)
    log.debug("Connected to MQTT")
      
    # Start the MQTT client loop in a separate thread
    executer2 = ThreadPoolExecutor(max_workers=3)
    loop = asyncio.get_running_loop()
    await loop.run_in_executor(executer2, clientMQ.loop_start)

    try:
        while True:
            user_input = await async_input("Enter message (or type 'x' to quit): \n")
            if user_input.lower() == 'x':
                break

            #print(user_input)
            clientMQ.publish(MQTT_TOPIC_TOASSISTANT, user_input)
            
    finally:
        clientMQ.loop_stop()
        clientMQ.disconnect()

if __name__ == "__main__":

    if len(sys.argv) > 1:
        agent_name = sys.argv[1]
        print(f"Assistant Name: {agent_name}")
    else:
        #print("No argument provided")
        pass

    asyncio.run(main())