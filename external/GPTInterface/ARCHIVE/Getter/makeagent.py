from openai import OpenAI
import json
import logging

agent_name = "Getter"
# These files are uploaded when agent is born
agent_base_file = "interop.pdf"
agent_data_file = "guide.pdf"
agent_instructions_file = 'Instructions.txt'
agent_log_file = agent_name + "_log.txt"

#agent_tools=[{"type": "code_interpreter"},{"type":"retrieval"}],
agent_tools=[{"type": "retrieval"}]
#agent_tools=[{"type": "code_interpreter"}]
agent_model="gpt-4-1106-preview"

#-----------------------------------------------------

# Configure logging
logging.basicConfig(filename=agent_log_file, encoding='utf-8',level=logging.INFO)
logger = logging.getLogger(__name__)

client = OpenAI()
#OpenAI.api_key = os.getenv('OPENAI_API_KEY')
#OpenAI.api_key = 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


AgentBase = client.files.create(
    file=open(agent_base_file, "rb"),
    purpose='assistants'
)
logging.info(agent_base_file + "Uploaded")

AgentData = client.files.create(
    file=open(agent_data_file, "rb"),
    purpose='assistants'
)

# Upload more files with an "assistants" purpose
#file = client.files.create(
#file=open("assistant_data.csv", "rb"),
#purpose='assistants'
#)

#backfile = client.files.create(
#file=open("data1.csv", "rb"),
#purpose='assistants'
#)

# Open the file and read its contents
with open(agent_instructions_file, 'r', encoding='utf-8') as instructions_file:
    instructions = instructions_file.read()

# file_contents now holds the contents of the file as a string
print(instructions)

# Create an assistant using the file ID
my_assistant = client.beta.assistants.create(
instructions=instructions,
name=agent_name,
#model="gpt-4-1106-preview",
model=agent_model,
#model="gpt-4",
#tools=[{"type": "code_interpreter"},{"type":"retrieval"}],
#tools=[{"type": "retrieval"}]
#tools=[{"type": "code_interpreter"}],
tools=agent_tools,
#file_ids=[file.id,bizfile.id,backfile.id]
file_ids=[AgentBase.id,AgentData.id]
#file_ids=[AgentBase.id]
)

my_assistants = client.beta.assistants.list(
    order="desc",
    limit="20",
)

print("All Assistants")
print(my_assistants.data)
print("New Assistant")
print(my_assistant)
