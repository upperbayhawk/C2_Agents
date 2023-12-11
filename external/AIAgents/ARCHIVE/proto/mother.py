import asyncio
import paho.mqtt.client as mqtt
from concurrent.futures import ThreadPoolExecutor
import os
from openai import OpenAI
import arrow



# MQTT Broker settings
MQTT_BROKER = "localhost"
MQTT_PORT = 1883
#MQTT_TOPIC = "DataVariable/All"
MQTT_TOPIC = "topic/junk"



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
    clientMQ.subscribe(MQTT_TOPIC)

    client = OpenAI()
    #OpenAI.api_key = os.getenv('OPENAI_API_KEY')
    #OpenAI.api_key = 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




    # Upload a file with an "assistants" purpose
    file = client.files.create(
    file=open("data.txt", "rb"),
    purpose='assistants'
    )

    # Define the path to your file
    system_file_path = 'system_prompt.txt'
   

    # Open the file and read its contents
    with open(system_file_path, 'r', encoding='utf-8') as system_file:
        system_file_contents = system_file.read()

    # file_contents now holds the contents of the file as a string
    print(system_file_contents)

    user_file_path = 'user_prompt.txt'

    # Open the file and read its contents
    with open(user_file_path, 'r', encoding='utf-8') as user_file:
        user_file_contents = user_file.read()

    # file_contents now holds the contents of the file as a string
    print(user_file_contents)

    # Create an assistant using the file ID
    my_assistant = client.beta.assistants.create(
    instructions=system_file_contents,
    name='Uber Geek',
    model="gpt-4-1106-preview",
    #tools=[{"type": "code_interpreter"},{"type":"retrieval"}],
    #tools=[{"type": "retrieval"}]
    tools=[{"type": "code_interpreter"}],
    file_ids=[file.id]
    )
    # Step 1: Create an Assistant
    #my_assistant = client.beta.assistants.create(
     #   model="gpt-4",
      #  instructions="You are a personal math tutor. When asked a question, write and run Python code to answer the question.",
       # name="Math Tutor",
        #tools=[{"type": "code_interpreter"}],
    #)
    print(f"This is the assistant object: {my_assistant} \n")

    my_assistants = client.beta.assistants.list(
        order="desc",
        limit="20",
    )
    print(my_assistants.data)

    # Step 2: Create a Thread
    my_thread = client.beta.threads.create()
    print(f"This is the thread object: {my_thread} \n")

#-------------------------------------------------------
    my_thread_message = client.beta.threads.messages.create(
        thread_id=my_thread.id,
        role="user",
        content=user_file_contents,
    )
    print(f"This is the message object: {my_thread_message} \n")



    # Step 4: Run the Assistant
    my_run = client.beta.threads.runs.create(
    thread_id=my_thread.id,
    assistant_id=my_assistant.id,
    instructions="Please address the user as Rok Benko."
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



    # Start the MQTT client loop in a separate thread
    loop = asyncio.get_running_loop()
    await loop.run_in_executor(None, clientMQ.loop_start)

    try:
        while True:
            user_input = await async_input("Enter message (or type 'x' to quit): ")
            if user_input.lower() == 'x':
                response = client.beta.assistants.delete(my_assistant)
                break

            print(user_input)

            utc= arrow.utcnow()
            local=utc.to('US/Eastern')
            print (local)


            clientMQ.publish(MQTT_TOPIC, user_input)

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
            instructions="Please address the user as Rok Benko."
            )

            print(f"This is the run object: {my_run} \n")
            
            while my_run.status != "completed":
                keep_retrieving_run = client.beta.threads.runs.retrieve(
                    thread_id=my_thread.id,
                    run_id=my_run.id
                )
                print(f"Run status: {keep_retrieving_run.status}")

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

    finally:
        clientMQ.loop_stop()
        clientMQ.disconnect()

if __name__ == "__main__":
    asyncio.run(main())