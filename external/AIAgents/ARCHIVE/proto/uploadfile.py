from openai import OpenAI
import sys


client = OpenAI()


data_file = "data1.csv"
#data_file1 = "assistant_data.csv"

if len(sys.argv) > 1:
    assistant_name = sys.argv[1]
    print(f"First argument: {assistant_name}")

    file = client.files.create(
        file=open("data1.csv", "rb"),
        purpose='assistants'
    )

   
    print("Listing all assistants")
    my_assistants = client.beta.assistants.list(
        order="desc",
        limit="20",
    )
    print("assistants")
    for item in my_assistants.data:
        print(item)

        #substring = "UberGeek"
        substring = assistant_name

        # Check if substring exists in text
        if substring in str(item):
            print("Substring found!")
            my_assistant = item
            #my_assistant.id = item.id
            break
        else:
            print("Substring not found.")

#    my_assistant = client.beta.assistants.retrieve(assistant_name)
    #print(my_assistant)
    
    
    print(my_assistants)

    my_assistant = client.beta.assistants.retrieve(assistant_name)


    assistant_files = client.beta.assistants.files.list(
        assistant_id=my_assistant.id
    )

    print(assistant_files)




   # assistant_file = client.beta.assistants.files.create(
#    assistant_id=my_assistant.id, 
 #       file_id="data1.csv"
  #  )
   # print(assistant_file)

   

    my_updated_assistant = client.beta.assistants.update(
        assistant_name,
        file_ids=[data_file],
    )


else:
    print("No argument provided")
