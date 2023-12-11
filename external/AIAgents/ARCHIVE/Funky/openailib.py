# openailib.py

# Copyright (C) Upperbay Systems, LLC - All Rights Reserved
# Unauthorized copying of this file, via any medium is strictly prohibited
# Proprietary and confidential
# Written by Dave Hardin <dave@upperbay.com>, 2023

import logging
import json
from openai import OpenAI
from logbook import Logger, FileHandler
from xfunctionlib import XFunction


class OpenAILib:
   
    def __init__(self):
        self.is_initialized = False
        self.client = OpenAI()     
        self.SEPARATOR = "======================="
        
      
    def initialize(self,agent_name,agent_init_file,agent_instructions_file,agent_output_file):
        if not self.is_initialized:
            # Add initialization code

            #FileHandler(agent_log_file).push_application()
            self.log = Logger("openailib", 0)
            self.log.debug("Initializing OpenAILib...")

            self.agent_name = agent_name
            self.agent_init_file = agent_init_file
            self.agent_instructions_file = agent_instructions_file
            self.agent_output_file = agent_output_file

            # Configure logging
           # self.logger = logging.getLogger("openailib")

            with open(self.agent_output_file, "a", encoding="utf-8") as file:
                file.write("\n" + self.SEPARATOR + "\n\n")

            print("Getting all Agents")
            self.my_assistants = self.client.beta.assistants.list(
                order="desc",
                limit="20",
            )

            agent_found = False
            print("Find My Agent")
            for item in self.my_assistants.data:
                # Check if substring exists in text
                if agent_name in str(item):
                    print("Agent found!: " + agent_name)
                    self.my_assistant = item
                    agent_found = True
                    break

            if agent_found == False:
                print("Agent not found!" + agent_name)
                return

            # This should also work
            #self.my_assistant = self.client.beta.assistants.retrieve(agent_name)
            #print(self.my_assistant)
                                   
            #---------------------------------------------

            # Step 2: Create a Thread
            self.my_thread = self.client.beta.threads.create()
            self.log.debug(f"Thread Object: {self.my_thread} \n")

            # Open the initialization file and read its contents
            with open(self.agent_init_file, 'r', encoding='utf-8') as init_file:
                self.init_file_contents = init_file.read()
            self.log.debug(self.init_file_contents)

            my_thread_message = self.client.beta.threads.messages.create(
                thread_id=self.my_thread.id,
                role="user",
                content=self.init_file_contents,
            )
            self.log.debug(f"Message Object: {my_thread_message} \n")
        #---------------------------------------------

            # Open the instructions file and read its contents
            with open(self.agent_instructions_file, 'r', encoding='utf-8') as instruction_file:
                self.instruct__file_contents = instruction_file.read()

            # Step 4: Run the Assistant
            my_run = self.client.beta.threads.runs.create(
                thread_id=self.my_thread.id,
                assistant_id=self.my_assistant.id,
                instructions=self.instruct__file_contents
            )

            self.log.debug(f"Run Object: {my_run} \n")
            
            while my_run.status != "completed":
                keep_retrieving_run = self.client.beta.threads.runs.retrieve(
                    thread_id=self.my_thread.id,
                    run_id=my_run.id
                )
                #print(f"Run status: {keep_retrieving_run.status}")

                if keep_retrieving_run.status == "completed":
                    print("\n")
                    break

            print("-------------------------------------------- \n")

            # Process outputs

            # Step 6: Retrieve the Messages added by the Assistant to the Thread
            all_messages = self.client.beta.threads.messages.list(
                thread_id=self.my_thread.id
            )

            user_msg = my_thread_message.content[0].text.value
            assist_msg = all_messages.data[0].content[0].text.value

            print(f"User: {user_msg}")
            print(f"Assistant: {assist_msg}")

            self.log.debug(f"User: {user_msg}")
            self.log.debug(f"Assistant: {assist_msg}")

            with open(self.agent_output_file, "a", encoding="utf-8") as file:
                file.write(str(my_thread_message.content[0].text.value + "\n"))
                file.write(str(all_messages.data[0].content[0].text.value+ "\n"))

            self.is_initialized = True
        else:
            self.log.debug("OpenAILib is already initialized.")

    def run(self, input_message):
        if self.is_initialized:
            #print("Running OpenAILib...")
            # Add code to execute AI-related tasks here
            #print ("processing")
            self.log.debug (input_message)
            self.last_message = "NULL"
            my_thread_message = self.client.beta.threads.messages.create(
                            thread_id=self.my_thread.id,
                            role="user",
                            content=input_message
            )
            self.log.debug(f"Message Object: {my_thread_message} \n")

            # Step 4: Run the Assistant
            my_run = self.client.beta.threads.runs.create(
            thread_id=self.my_thread.id,
            assistant_id=self.my_assistant.id,
            instructions=self.instruct__file_contents
            )
            self.log.debug(f"Run Object: {my_run} \n")
            
            # Process Function Callbacks           
            internal_function = False
            while my_run.status != "completed":
                keep_retrieving_run = self.client.beta.threads.runs.retrieve(
                    thread_id=self.my_thread.id,
                    run_id=my_run.id
                )
                #print(f"Run status: {keep_retrieving_run.status}")

                if keep_retrieving_run.status == "completed":
                    print("\n")
                    break

                if keep_retrieving_run.status == "requires_action":
                        #call function and return
                        
                        tool_returns=[]
                        for tool_call in keep_retrieving_run.required_action.submit_tool_outputs.tool_calls:
                            function_name = tool_call.function.name
                            print(function_name)
                            self.log.debug(function_name)
                            arguments = tool_call.function.arguments
                            self.log.debug(arguments)
                            print(arguments)
                           
                            #call function1
                            #////////////////////////////////////////////////////////
                            if function_name == "sendAlarmSignalToNetworkNode":
                                try:
                                    my_args = json.loads(arguments)
                                    print("From Json location = " + my_args["network_node"])
                                    network_node = my_args["network_node"]
                                    message = my_args["message"]
                                    return_value = "ERROR"
                                    try:
                                        xfunc = XFunction()
                                        return_value = xfunc.sendAlarmSignalToNetworkNode(network_node,message)
                                    except Exception as e1:
                                        self.log.error("FUNCTION ERROR: {e1}: " + function_name)

                                    tool_return = json.loads('{"tool_call_id": "hello", "output": "return_value"}')

                                    tool_return["tool_call_id"] = tool_call.id
                                    tool_return["output"] = return_value

                                    print("function: " + function_name + " = " + tool_return["output"])
                                    tool_returns.append(tool_return)

                                except Exception as e:
                                    self.log.error("FUNCTION WRAPPER ERROR: {e}:" + function_name)
                           
                            #////////////////////////////////////////////////////////
                            if function_name == "sendControlSignalToNetworkNode":
                                try:
                                    my_args = json.loads(arguments)
                                    print("From Json location = " + my_args["network_node"])
                                    network_node = my_args["network_node"]
                                    message = my_args["message"]
                                    return_value = "ERROR"
                                    try:
                                        xfunc = XFunction()
                                        return_value = xfunc.sendControlSignalToNetworkNode(network_node,message)
                                    except Exception as e1:
                                        self.log.error("FUNCTION ERROR: {e1}: " + function_name)

                                    tool_return = json.loads('{"tool_call_id": "hello", "output": "return_value"}')

                                    tool_return["tool_call_id"] = tool_call.id
                                    tool_return["output"] = return_value

                                    print("function: " + function_name + " = " + tool_return["output"])
                                    tool_returns.append(tool_return)

                                except Exception as e:
                                    self.log.error("FUNCTION WRAPPER ERROR: {e}:" + function_name)
                            #////////////////////////////////////////////////////////
                            if function_name == "sendNoticeSignalToNetworkNode":
                                try:
                                    my_args = json.loads(arguments)
                                    print("From Json location = " + my_args["network_node"])
                                    network_node = my_args["network_node"]
                                    message = my_args["message"]
                                    return_value = "ERROR"
                                    try:
                                        xfunc = XFunction()
                                        return_value = xfunc.sendNoticeSignalToNetworkNode(network_node,message)
                                    except Exception as e1:
                                        self.log.error("FUNCTION ERROR: {e1}: " + function_name)

                                    tool_return = json.loads('{"tool_call_id": "hello", "output": "return_value"}')

                                    tool_return["tool_call_id"] = tool_call.id
                                    tool_return["output"] = return_value

                                    print("function: " + function_name + " = " + tool_return["output"])
                                    tool_returns.append(tool_return)

                                except Exception as e:
                                    self.log.error("FUNCTION WRAPPER ERROR: {e}:" + function_name)
                            #////////////////////////////////////////////////////////
                            if function_name == "getNickname":
                                try:
                                    my_args = json.loads(arguments)
                                    print("From Json location = " + my_args["location"])
                                    location = my_args["location"]
                                    return_value = "ERROR"
                                    try:
                                        xfunc = XFunction()
                                        return_value = xfunc.getNickname(location)
                                    except Exception as e1:
                                        self.log.error("FUNCTION ERROR: {e1}: " + function_name)

                                    tool_return = json.loads('{"tool_call_id": "hello", "output": "return_value"}')
                                    
                                    tool_return["tool_call_id"] = tool_call.id
                                    tool_return["output"] = return_value

                                    print("function: " + function_name + " = " + tool_return["output"])
                                    tool_returns.append(tool_return)

                                except Exception as e:
                                    self.log.error("FUNCTION WRAPPER ERROR: {e}:" + function_name)
                            #////////////////////////////////////////////////////////
                            if function_name == "getNickname1":
                                try:
                                    my_args = json.loads(arguments)
                                    print("From Json location = " + my_args["location"])
                                    location = my_args["location"]
                                    return_value = "ERROR"
                                    try:
                                        xfunc = XFunction()
                                        return_value = xfunc.getNickname1(location)
                                    except Exception as e1:
                                        self.log.error("FUNCTION ERROR: {e1}: " + function_name)

                                    tool_return = json.loads('{"tool_call_id": "hello", "output": "return_value"}')
                                    
                                    tool_return["tool_call_id"] = tool_call.id
                                    tool_return["output"] = return_value

                                    print("function: " + function_name + " = " + tool_return["output"])
                                    tool_returns.append(tool_return)

                                except Exception as e:
                                    self.log.error("FUNCTION WRAPPER ERROR: {e}:" + function_name)
                            #////////////////////////////////////////////////////////
                            if function_name == "getNickname2":
                                try:
                                    my_args = json.loads(arguments)
                                    print("From Json location = " + my_args["location"])
                                    location = my_args["location"]
                                    return_value = "ERROR"
                                    try:
                                        xfunc = XFunction()
                                        return_value = xfunc.getNickname2(location)
                                    except Exception as e1:
                                        self.log.error("FUNCTION ERROR: {e1}: " + function_name)

                                    tool_return = json.loads('{"tool_call_id": "hello", "output": "return_value"}')
                                    
                                    tool_return["tool_call_id"] = tool_call.id
                                    tool_return["output"] = return_value

                                    print("function: " + function_name + " = " + tool_return["output"])
                                    tool_returns.append(tool_return)

                                except Exception as e:
                                    self.log.error("FUNCTION WRAPPER ERROR: {e}:" + function_name)
                            #////////////////////////////////////////////////////////
                            if function_name == "getNickname3":
                                try:
                                    my_args = json.loads(arguments)
                                    print("From Json location = " + my_args["location"])
                                    location = my_args["location"]
                                    return_value = "ERROR"
                                    try:
                                        xfunc = XFunction()
                                        return_value = xfunc.getNickname3(location)
                                    except Exception as e1:
                                        self.log.error("FUNCTION ERROR: {e1}: " + function_name)

                                    tool_return = json.loads('{"tool_call_id": "hello", "output": "return_value"}')
                                    
                                    tool_return["tool_call_id"] = tool_call.id
                                    tool_return["output"] = return_value

                                    print("function: " + function_name + " = " + tool_return["output"])
                                    tool_returns.append(tool_return)

                                except Exception as e:
                                    self.log.error("FUNCTION WRAPPER ERROR: {e}:" + function_name)

                            #////////////////////////////////////////////////////////
                            
                        run = self.client.beta.threads.runs.submit_tool_outputs(
                            thread_id=self.my_thread.id,
                            run_id=my_run.id,
                            tool_outputs= tool_returns
                            #[
                            #    {
                            #        "tool_call_id": tool_call.id,
                            #        "output": return_value
                            #    }
                            #    ]
                            )
                    
            print("------------------------------------------------------------ \n")

            # Step 6: Retrieve the Messages added by the Assistant to the Thread
            all_messages = self.client.beta.threads.messages.list(
                thread_id=self.my_thread.id
            )

            user_msg = my_thread_message.content[0].text.value
            assist_msg = all_messages.data[0].content[0].text.value

            print(f"User: {user_msg}")
            print(f"Assistant: {assist_msg}")

            self.log.debug(f"User: {user_msg}")
            self.log.debug(f"Assistant: {assist_msg}")


            with open(self.agent_output_file, "a", encoding="utf-8") as file:
                file.write(str(user_msg + "\n"))
                file.write(str(assist_msg + "\n"))

            self.last_message = str(assist_msg)
            self.log.debug("last_message: " + self.last_message)

            return "OK"
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