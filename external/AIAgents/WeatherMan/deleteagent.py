# deleteagent.py

# Copyright (C) Upperbay Systems, LLC - All Rights Reserved
# Unauthorized copying of this file, via any medium is strictly prohibited
# Proprietary and confidential
# Written by Dave Hardin <dave@upperbay.com>, 2023

from openai import OpenAI
import logbook
import sys

import config

agent_name = config.agent_name

# API doesnt work!!
# MUST DELETE FILES TOO

if len(sys.argv) > 1:
    agent_name = sys.argv[1]
    print(f"Agent Name: {agent_name}")

client = OpenAI()

#print("Listing all assistants")
my_assistants = client.beta.assistants.list(
    order="desc",
    limit="20",
)

print("Assistants")
for item in my_assistants.data:
    print(item)
    substring = agent_name
    # Check if substring exists in text
    if substring in str(item):
        print("Agent found!")
        #my_assistant = item
        response = client.beta.assistants.delete(agent_name)
        print(response)
        print("Agent deleted\n")
        break
    else:
        print("Assistant not found.")

print("BYE")


