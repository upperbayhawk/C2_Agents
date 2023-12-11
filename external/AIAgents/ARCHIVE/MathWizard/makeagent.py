# makeagent.py

# Copyright (C) Upperbay Systems, LLC - All Rights Reserved
# Unauthorized copying of this file, via any medium is strictly prohibited
# Proprietary and confidential
# Written by Dave Hardin <dave@upperbay.com>, 2023

import json
import logging
from openai import OpenAI
from logbook import Logger, FileHandler


agent_name = "MathWizard"
# These files are uploaded when agent is born
agent_base_file = "./data/AgentBase.txt"
agent_data_file = "./data/AgentData.csv"

agent_instructions_file = "./data/Instructions.txt"
agent_model="gpt-4-1106-preview"
#agent_tools=[{"type": "code_interpreter"},{"type":"retrieval"}],
#agent_tools=[{"type": "retrieval"}]
#agent_tools=[{"type": "code_interpreter"}]
# code_interpreter with function callbacks

agent_tools=[{"type": "code_interpreter"},{
      "type": "function",
    "function": {
      "name": "sendAlarmSignalToNetworkNode",
      "description": "Send an alarm signal to a destination network node.",
      "parameters": {
        "type": "object",
        "properties": {
          "network_node": {"type": "string", "description": "The name of a node on the network. Network node names include AlarmPanel, ControlPanel, CommandCenter."},
          "message": {"type": "string", "description": "The contents of the alarm signal message."},
        },
        "required": ["network_node", "message"]
      }
    }
  }, {
    "type": "function",
    "function": {
      "name": "sendControlSignalToNetworkNode",
      "description": "Send a control signal to a destination network node.",
      "parameters": {
        "type": "object",
        "properties": {
          "network_node": {"type": "string", "description": "The name of a node on the network. Network node names include AlarmPanel, ControlPanel, CommandCenter."},
          "message": {"type": "string", "description": "The contents of the control signal message."},
        },
        "required": ["network_node", "message"]
      }
    } 
  },{
    "type": "function",
    "function": {
      "name": "sendNoticeSignalToNetworkNode",
      "description": "Send a notice signal to a destination network node.",
      "parameters": {
        "type": "object",
        "properties": {
          "network_node": {"type": "string", "description": "The name of a node on the network. Network node names include AlarmPanel, ControlPanel, CommandCenter."},
          "message": {"type": "string", "description": "The contents of the notice signal message."},
        },
        "required": ["network_node", "message"]
      }
    } 
  },{
    "type": "function",
    "function": {
      "name": "getNickname",
      "description": "Get the nickname of a city",
      "parameters": {
        "type": "object",
        "properties": {
          "location": {"type": "string", "description": "The city and state e.g. San Francisco, CA"},
        },
        "required": ["location"]
      }
    } 
  },{
    "type": "function",
    "function": {
      "name": "getNickname1",
      "description": "Get the nickname of a city",
      "parameters": {
        "type": "object",
        "properties": {
          "location": {"type": "string", "description": "The city and state e.g. San Francisco, CA"},
        },
        "required": ["location"]
      }
    } 
  },{
    "type": "function",
    "function": {
      "name": "getNickname2",
      "description": "Get the nickname of a city",
      "parameters": {
        "type": "object",
        "properties": {
          "location": {"type": "string", "description": "The city and state e.g. San Francisco, CA"},
        },
        "required": ["location"]
      }
    } 
  },{
    "type": "function",
    "function": {
      "name": "getNickname3",
      "description": "Get the nickname of a city",
      "parameters": {
        "type": "object",
        "properties": {
          "location": {"type": "string", "description": "The city and state e.g. San Francisco, CA"},
        },
        "required": ["location"]
      }
    } 
  }]


#-----------------------------------------------------
# Configure logging
agent_log_file = ".\\logs\\" + agent_name + "_log.txt"

FileHandler(agent_log_file).push_application()
log = Logger("makeagent")

log.info("Hello From Below")

client = OpenAI()

#-----------------------------------------------------
# Upload files for Retieval Agent
# Must add file ids to create below
#-----------------------------------------------------

#AgentBase = client.files.create(
#    file=open(agent_base_file, "rb"),
#    purpose='assistants'
#)

#AgentData = client.files.create(
#    file=open(agent_data_file, "rb"),
#    purpose='assistants'
#)

#file = client.files.create(
#file=open("assistant_data.csv", "rb"),
#purpose='assistants'
#)

#backfile = client.files.create(
#file=open("data1.csv", "rb"),
#purpose='assistants'
#)
#-----------------------------------------------------

# Add instructions to agent
with open(agent_instructions_file, 'r', encoding='utf-8') as instructions_file:
    instructions = instructions_file.read()
print(instructions)

# Create an assistant
my_assistant = client.beta.assistants.create(
    instructions=instructions,
    name=agent_name,
    #model="gpt-4-1106-preview",
    model=agent_model,
    #model="gpt-4",
    tools=agent_tools
    #tools=[{"type": "code_interpreter"},{"type":"retrieval"}],
    #tools=[{"type": "retrieval"}]
    #tools=[{"type": "code_interpreter"}],
    # File IDS here
    #file_ids=[file.id,bizfile.id,backfile.id]
    #file_ids=[AgentBase.id,AgentData.id]
)

#my_assistants = client.beta.assistants.list(
#    order="desc",
#    limit="20",
#)
#print("All Assistants")
#print(my_assistants.data)

print("New Assistant")
print(my_assistant)

print("\nAgent " + agent_name + " created.")
