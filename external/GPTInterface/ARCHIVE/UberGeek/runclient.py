import asyncio
import sys
import os

import paho.mqtt.client as mqtt
from concurrent.futures import ThreadPoolExecutor
import arrow
import logging

agent_name = "UberGeek"
agent_output_file = "client_output.txt"
agent_log_file = "client_log.txt"

# MQTT Broker settings
MQTT_BROKER = "localhost"
MQTT_PORT = 1883
#MQTT_TOPIC = "DataVariable/All"
MQTT_TOPIC_SEND = "openai/assistant/" + agent_name + "/ToAgent"
MQTT_TOPIC_RECEIVE = "openai/assistant/" + agent_name + "/FromAgent"
OUTPUT_SEPARATOR = "///////////////////////////////"

#-----------------------------------------------------

# Configure logging
logging.basicConfig(filename=agent_log_file, encoding='utf-8',level=logging.INFO)
logger = logging.getLogger(__name__)

# MQTT Callbacks
def on_connect(client, userdata, flags, rc):
    print(f"Connected with result code {rc}")

def on_message(client, userdata, msg):
    print(f"Received message '{msg.payload.decode()}' on topic '{msg.topic}'")
    input = str(msg.payload.decode())
    if msg.topic == MQTT_TOPIC_RECEIVE:
        print ("from agent" + msg.topic)
        process_message(input)
        logger.info("Assistant: " + input)
    else:
        print ("to agent" + msg.topic)
        logger.info("User: " + input)

def process_message(input_message):
    print ("incoming placeholder")
    print (input_message)
    
# Asynchronous function to read from stdin
async def async_input(prompt):
    with ThreadPoolExecutor(1) as executor:
        return await asyncio.get_event_loop().run_in_executor(executor, input, prompt)


# Main async function
async def main():
 # Set up MQTT client
    clientMQ = mqtt.Client()
    clientMQ.on_connect = on_connect
    clientMQ.on_message = on_message
    clientMQ.connect(MQTT_BROKER, MQTT_PORT, 60)
    clientMQ.subscribe(MQTT_TOPIC_RECEIVE)
    clientMQ.subscribe(MQTT_TOPIC_SEND)
    
    logger.info(OUTPUT_SEPARATOR)
    logger.info("Connected to MQTT")
      
    # Start the MQTT client loop in a separate thread
    loop = asyncio.get_running_loop()
    await loop.run_in_executor(None, clientMQ.loop_start)

    try:
        while True:
            user_input = await async_input("Enter message (or type 'x' to quit): ")
            if user_input.lower() == 'x':
                break

            print(user_input)
            clientMQ.publish(MQTT_TOPIC_SEND, user_input)
            
    finally:
        clientMQ.loop_stop()
        clientMQ.disconnect()

if __name__ == "__main__":

    if len(sys.argv) > 1:
        agent_name = sys.argv[1]
        print(f"Agent Name: {agent_name}")
    else:
        print("No argument provided")

    asyncio.run(main())