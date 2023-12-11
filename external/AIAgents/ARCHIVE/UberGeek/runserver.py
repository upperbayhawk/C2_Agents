import asyncio
import sys
import os

import paho.mqtt.client as mqtt
from concurrent.futures import ThreadPoolExecutor
from openai import OpenAI
import arrow
import logging
from openailib import openailib_instance

agent_name = "UberGeek"
agent_output_file = "server_output.txt"
agent_log_file = "server_log.txt"
agent_init_file = 'InitPrompt.txt'
agent_instructions_file = "Instructions.txt"

# MQTT Broker settings
MQTT_BROKER = "localhost"
MQTT_PORT = 1883
#MQTT_TOPIC = "DataVariable/All"
MQTT_TOPIC_SEND = "openai/assistant/" + agent_name + "/FromAgent"
MQTT_TOPIC_RECEIVE = "openai/assistant/" + agent_name + "/ToAgent"
OUTPUT_SEPARATOR = "///////////////////////////////"

#-----------------------------------------------------

# Configure logging
logging.basicConfig(filename=agent_log_file, encoding='utf-8',level=logging.INFO)
logger = logging.getLogger(__name__)

clientMQ = mqtt.Client()

# MQTT Callbacks
def on_connect(client, userdata, flags, rc):
    print(f"Connected with result code {rc}")
    #clientMQ.subscribe(MQTT_TOPIC)

def on_message(client, userdata, msg):
    print(f"Received message '{msg.payload.decode()}' on topic '{msg.topic}'")
    input = str(msg.payload.decode())
    if msg.topic == MQTT_TOPIC_RECEIVE:
        print("To agent: " + msg.topic)
        process_message(input)
    else:
        print("To client: " + msg.topic + " " + openailib_instance.last_message)
           

# Asynchronous function to read from stdin
async def async_input(prompt):
    with ThreadPoolExecutor(1) as executor:
        return await asyncio.get_event_loop().run_in_executor(executor, input, prompt)

def process_message(input_message):
    print ("processing")
    openailib_instance.run(input_message)
    clientMQ.publish(MQTT_TOPIC_SEND, openailib_instance.last_message)

#//////////////////////////////////////////////////////
# Main async function
async def main():
 # Set up MQTT client
    
    clientMQ.on_connect = on_connect
    clientMQ.on_message = on_message
    clientMQ.connect(MQTT_BROKER, MQTT_PORT, 60)
    print("connect")
    clientMQ.subscribe(MQTT_TOPIC_RECEIVE)
    clientMQ.subscribe(MQTT_TOPIC_SEND)

    #OpenAI.api_key = os.getenv('OPENAI_API_KEY')
    #OpenAI.api_key = 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

    with open(agent_output_file, "a", encoding="utf-8") as file:
        file.write("\n" + OUTPUT_SEPARATOR + "\n\n")
    
    #-------------------------------------------------------

   
    # Start the MQTT client loop in a separate thread
    loop = asyncio.get_running_loop()
    await loop.run_in_executor(None, clientMQ.loop_start)

    try:
        while True:
            user_input = await async_input("Enter message (or type 'x' to quit): ")
            if user_input.lower() == 'x':
                openailib_instance.close()
                break

            print(user_input)

            utc= arrow.utcnow()
            local=utc.to('US/Eastern')
            print ("Time " + str(local) + "\n")

            clientMQ.publish(MQTT_TOPIC_SEND, user_input)
            logger.info("User: " + user_input)

            process_message(user_input)


    finally:
        clientMQ.loop_stop()
        clientMQ.disconnect()

if __name__ == "__main__":

    if len(sys.argv) > 1:
        agent_name = sys.argv[1]
        print(f"Agent Name: {agent_name}")
    else:
        print("No argument provided")
        
    # Initialize the OpenAILib
    openailib_instance.initialize(agent_name,agent_init_file,agent_instructions_file,agent_output_file)

    asyncio.run(main())