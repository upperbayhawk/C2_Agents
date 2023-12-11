from concurrent.futures import ThreadPoolExecutor
import asyncio
import os
from openai import OpenAI

async def repeated_task():
    # This is the task that you want to repeat
    print("Task executed")

async def loop_every_x_seconds(x):
    while True:
        await repeated_task()
        await asyncio.sleep(x)

def StartAgent:
    client = OpenAI()
    #OpenAI.api_key = os.getenv('OPENAI_API_KEY')
    #OpenAI.api_key = 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

    # Step 1: Create an Assistant
    my_assistant = client.beta.assistants.create(
        model="gpt-4",
        instructions="You are a personal math tutor. When asked a question, write and run Python code to answer the question.",
        name="Math Tutor",
        tools=[{"type": "code_interpreter"}],
    )
    print(f"This is the assistant object: {my_assistant} \n")

    # Step 2: Create a Thread
    my_thread = client.beta.threads.create()
    print(f"This is the thread object: {my_thread} \n")


async def async_input(prompt: str) -> str:
    with ThreadPoolExecutor(1, "AsyncInputPool") as executor:
        return await asyncio.get_event_loop().run_in_executor(executor, input, prompt)

def RunAgent:

# Step 3: Add a Message to a Thread
my_thread_message = client.beta.threads.messages.create(
  thread_id=my_thread.id,
  role="user",
  content="I need to solve the equation `3x + 6969 = 14`. Can you help me?",
)
print(f"This is the message object: {my_thread_message} \n")

# Step 4: Run the Assistant
my_run = client.beta.threads.runs.create(
  thread_id=my_thread.id,
  assistant_id=my_assistant.id,
  instructions="Please address the user as Rok Benko."
)
print(f"This is the run object: {my_run} \n")

# Step 5: Periodically retrieve the Run to check on its status to see if it has moved to completed
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


async def main():
    while True:
        user_input = await async_input("Enter your text (or type 'exit' to quit): ")
        if user_input.lower() == 'exit':
            break
        print(f"You entered: {user_input}")


if __name__ == "__main__":
    interval = 5  # Set the interval (in seconds)

    # Run the asynchronous loop
    asyncio.run(loop_every_x_seconds(interval))

if __name__ == "__main__":
    asyncio.run(main())
