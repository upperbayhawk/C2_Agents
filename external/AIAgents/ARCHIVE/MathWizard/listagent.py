# listagent.py

# Copyright (C) Upperbay Systems, LLC - All Rights Reserved
# Unauthorized copying of this file, via any medium is strictly prohibited
# Proprietary and confidential
# Written by Dave Hardin <dave@upperbay.com>, 2023

from openai import OpenAI
from logbook import Logger, FileHandler

FileHandler(agent_log_file).push_application()
log = Logger("runclient")
log.info("Hello From Below")

client = OpenAI()

my_assistants = client.beta.assistants.list(
    order="desc",
    limit="20",
)

print("All Assistants")

for agent in my_assistants:
    print("Assistant: " + str(agent) + "\n")