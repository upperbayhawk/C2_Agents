from openai import OpenAI
import sys

#USE Openai Playground to delete
#API doesnt work!!
# and delete files

agent_name = "UberGeek"


if len(sys.argv) > 1:
    agent_name = sys.argv[1]
    print(f"First argument: {agent_name}")
else:
    print("No argument provided")


client = OpenAI()
    #OpenAI.api_key = os.getenv('OPENAI_API_KEY')
    #OpenAI.api_key = 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

 # Open the file and read its contents
print("Listing all assistants")
my_assistants = client.beta.assistants.list(
    order="desc",
    limit="20",
)


print("assistants")
for item in my_assistants.data:
    print(item)

    #substring = "UberGeek"
    substring = agent_name
    print(agent_name)
    # Check if substring exists in text
    if substring in str(item):
        print("Agent found!")
        my_assistant = item
        #my_assistant.id = item.id
        response = client.beta.assistants.delete(agent_name)

        print(response)
        print("BYE")
        break
    else:
        print("Substring not found.")

print("Agent deleted")


