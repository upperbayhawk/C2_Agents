# openailib.py
from openai import OpenAI

class OpenAILib:
   
    def __init__(self):
        self.is_initialized = False
        self.client = OpenAI()     
        self.OUTPUT_SEPARATOR = "///////////////////////////////"

    def initialize(self,agent_name,agent_init_file,agent_instructions_file,agent_output_file):
        if not self.is_initialized:
            print("Initializing OpenAILib...")
            # Add initialization code for your AI library here

            self.agent_name = agent_name
            self.agent_init_file = agent_init_file
            self.agent_instructions_file = agent_instructions_file
            self.agent_output_file = agent_output_file

            with open(self.agent_output_file, "a", encoding="utf-8") as file:
                file.write("\n" + self.OUTPUT_SEPARATOR + "\n\n")

            print("Listing all Agents")
            self.my_assistants = self.client.beta.assistants.list(
                order="desc",
                limit="20",
            )

            print("Find My Agent")
            for item in self.my_assistants.data:
                # Check if substring exists in text
                if agent_name in str(item):
                    print("Agent found!" + agent_name)
                    self.my_assistant = item
                    #logger.info("Agent Found")
                    break
                else:
                    print("Agent not found.")

                #self.my_assistant = self.client.beta.assistants.retrieve(me_assistant)
                #print(self.my_assistant)

            #---------------------------------------------

            # Step 2: Create a Thread
            self.my_thread = self.client.beta.threads.create()
            print(f"This is the thread object: {self.my_thread} \n")

            # Open the file and read its contents
            with open(self.agent_init_file, 'r', encoding='utf-8') as init_file:
                self.init_file_contents = init_file.read()

            # file_contents now holds the contents of the file as a string
            print(self.init_file_contents)

            my_thread_message = self.client.beta.threads.messages.create(
                thread_id=self.my_thread.id,
                role="user",
                content=self.init_file_contents,
            )
            print(f"This is the message object: {my_thread_message} \n")
        #---------------------------------------------

            with open(self.agent_instructions_file, 'r', encoding='utf-8') as instruction_file:
                self.instruct__file_contents = instruction_file.read()

            # Step 4: Run the Assistant
            my_run = self.client.beta.threads.runs.create(
                thread_id=self.my_thread.id,
                assistant_id=self.my_assistant.id,
                instructions=self.instruct__file_contents
            )

            print(f"This is the run object: {my_run} \n")
            
            while my_run.status != "completed":
                keep_retrieving_run = self.client.beta.threads.runs.retrieve(
                    thread_id=self.my_thread.id,
                    run_id=my_run.id
                )
                #print(f"Run status: {keep_retrieving_run.status}")

                if keep_retrieving_run.status == "completed":
                    print("\n")
                    break

            # Step 6: Retrieve the Messages added by the Assistant to the Thread
            all_messages = self.client.beta.threads.messages.list(
                thread_id=self.my_thread.id
            )

            print("------------------------------------------------------------ \n")

            print(f"User: {my_thread_message.content[0].text.value}")
            print(f"Assistant: {all_messages.data[0].content[0].text.value}")

            with open(self.agent_output_file, "a", encoding="utf-8") as file:
                file.write(str(my_thread_message.content[0].text.value + "\n"))
                file.write(str(all_messages.data[0].content[0].text.value+ "\n"))

            self.is_initialized = True
        else:
            print("OpenAILib is already initialized.")

    def run(self, input_message):
        if self.is_initialized:
            print("Running OpenAILib...")
            # Add code to execute AI-related tasks here
            print ("processing")
            print (input_message)
            user_input = input_message
            my_thread_message = self.client.beta.threads.messages.create(
                            thread_id=self.my_thread.id,
                            role="user",
                            content=user_input
            )
            print(f"This is the message object: {my_thread_message} \n")

            # Step 4: Run the Assistant
            my_run = self.client.beta.threads.runs.create(
            thread_id=self.my_thread.id,
            assistant_id=self.my_assistant.id,
            instructions=self.instruct__file_contents
            )

            print(f"This is the run object: {my_run} \n")
            
            while my_run.status != "completed":
                keep_retrieving_run = self.client.beta.threads.runs.retrieve(
                    thread_id=self.my_thread.id,
                    run_id=my_run.id
            )
            #print(f"Run status: {keep_retrieving_run.status}")

                if keep_retrieving_run.status == "completed":
                    print("\n")
                    break


            # Step 6: Retrieve the Messages added by the Assistant to the Thread
            all_messages = self.client.beta.threads.messages.list(
            thread_id=self.my_thread.id
            )
            print("------------------------------------------------------------ \n")

            print(f"User: {my_thread_message.content[0].text.value}")
            print(f"Assistant: {all_messages.data[0].content[0].text.value}")

            with open(self.agent_output_file, "a", encoding="utf-8") as file:
                file.write(str(my_thread_message.content[0].text.value + "\n"))
                file.write(str(all_messages.data[0].content[0].text.value + "\n"))

            self.last_message = str(all_messages.data[0].content[0].text.value)
        else:
            print("OpenAILib is not initialized. Please initialize it first.")

    def close(self):
        if self.is_initialized:
            print("Closing OpenAILib...")
            # Add code to clean up and close your AI library here
            self.is_initialized = False
        else:
            print("OpenAILib is not initialized. No need to close it.")

# Instantiate OpenAILib class to make it a singleton instance
openailib_instance = OpenAILib()