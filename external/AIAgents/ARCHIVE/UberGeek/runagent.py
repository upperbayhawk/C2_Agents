import asyncio
import sys
import os

import paho.mqtt.client as mqtt
from concurrent.futures import ThreadPoolExecutor
from openai import OpenAI
import arrow
import logging

agent_name = "UberGeek"
agent_output_file = "output.txt"
agent_log_file = "log.txt"
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

# MQTT Callbacks
def on_connect(client, userdata, flags, rc):
    print(f"Connected with result code {rc}")
    #clientMQ.subscribe(MQTT_TOPIC)

def on_message(client, userdata, msg):
    print(f"Received message '{msg.payload.decode()}' on topic '{msg.topic}'")
   #print(f" ")
   #pass

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
    print("connect")
    clientMQ.subscribe(MQTT_TOPIC_RECEIVE)
    clientMQ.subscribe(MQTT_TOPIC_SEND)

    client = OpenAI()
    #OpenAI.api_key = os.getenv('OPENAI_API_KEY')
    #OpenAI.api_key = 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

    with open(agent_output_file, "a", encoding="utf-8") as file:
        file.write("\n" + OUTPUT_SEPARATOR + "\n\n")
    
    #-------------------------------------------------------
    print("Listing all Agents")
    my_assistants = client.beta.assistants.list(
        order="desc",
        limit="20",
    )

    print("Find My Agent")
    for item in my_assistants.data:
        # Check if substring exists in text
        if agent_name in str(item):
            print("Agent found!" + agent_name)
            my_assistant = item
            logger.info("Agent Found")
            print(str(my_assistant))
            break
        else:
            print("Agent not found.")

        #my_assistant = client.beta.assistants.retrieve(assistant_name)
        
    #---------------------------------------------

    # Step 2: Create a Thread
    my_thread = client.beta.threads.create()
    print(f"This is the thread object: {my_thread} \n")

    # Open the file and read its contents
    with open(agent_init_file, 'r', encoding='utf-8') as init_file:
        init_file_contents = init_file.read()

    # file_contents now holds the contents of the file as a string
    print(init_file_contents)

    my_thread_message = client.beta.threads.messages.create(
        thread_id=my_thread.id,
        role="user",
        content=init_file_contents,
    )
    print(f"This is the message object: {my_thread_message} \n")
#---------------------------------------------

    with open(agent_instructions_file, 'r', encoding='utf-8') as instruction_file:
        instruct__file_contents = instruction_file.read()

    # Step 4: Run the Assistant
    my_run = client.beta.threads.runs.create(
    thread_id=my_thread.id,
    assistant_id=my_assistant.id,
    instructions=instruct__file_contents
    )

    print(f"This is the run object: {my_run} \n")
    
    while my_run.status != "completed":
        keep_retrieving_run = client.beta.threads.runs.retrieve(
            thread_id=my_thread.id,
            run_id=my_run.id
        )
        #print(f"Run status: {keep_retrieving_run.status}")

        if keep_retrieving_run.status == "completed":
            print("\n")
            break

    # Step 6: Retrieve the Messages added by the Assistant to the Thread
    all_messages = client.beta.threads.messages.list(
    thread_id=my_thread.id
    )

    print("------------------------------------------------------------ \n")

    print(f"User: {my_thread_message.content[0].text.value}")
    print(f"Assistant: {all_messages.data[0].content[0].text.value}")

    with open(agent_output_file, "a", encoding="utf-8") as file:
       file.write(str(my_thread_message.content[0].text.value + "\n"))
       file.write(str(all_messages.data[0].content[0].text.value+ "\n"))


    # Start the MQTT client loop in a separate thread
    loop = asyncio.get_running_loop()
    await loop.run_in_executor(None, clientMQ.loop_start)

    try:
        while True:
            user_input = await async_input("Enter message (or type 'x' to quit): ")
            if user_input.lower() == 'x':
                break

            print(user_input)

            utc= arrow.utcnow()
            local=utc.to('US/Eastern')
            print ("Time " + str(local) + "\n")

            clientMQ.publish(MQTT_TOPIC_SEND, user_input)
            logger.info("User: " + user_input)

            my_thread_message = client.beta.threads.messages.create(
                    thread_id=my_thread.id,
                    role="user",
                    content=user_input
            )
            print(f"This is the message object: {my_thread_message} \n")

            # Step 4: Run the Assistant
            my_run = client.beta.threads.runs.create(
            thread_id=my_thread.id,
            assistant_id=my_assistant.id,
            instructions=instruct__file_contents
            )

            print(f"This is the run object: {my_run} \n")
            
            while my_run.status != "completed":
                keep_retrieving_run = client.beta.threads.runs.retrieve(
                    thread_id=my_thread.id,
                    run_id=my_run.id
                )
                #print(f"Run status: {keep_retrieving_run.status}")

                if keep_retrieving_run.status == "completed":
                    print("\n")
                    break
            # Step 6: Retrieve the Messages added by the Assistant to the Thread
            all_messages = client.beta.threads.messages.list(
            thread_id=my_thread.id
            )
            print("------------------------------------------------------------ \n")

            print(f"User: {my_thread_message.content[0].text.value}")
            print(f"Assistant: {all_messages.data[0].content[0].text.value}")

            with open(agent_output_file, "a", encoding="utf-8") as file:
                file.write(str(my_thread_message.content[0].text.value + "\n"))
                file.write(str(all_messages.data[0].content[0].text.value + "\n"))

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