import json
import yaml
#from yaml import SafeLoader

with open('1_oadr3.0.1.yaml', 'r') as file:
    configuration = yaml.safe_load(file)

with open('oadr3.0.1.json', 'w') as json_file:
    json.dump(configuration, json_file)
    
output = json.dumps(json.load(open('oadr3.0.1.json')), indent=2)
print(output)



#with open("1_oadr3.0.1.yaml","r") as f:
   # yaml_string = f.read()
#python_dict=yaml.load(yaml_string, Loader=SafeLoader)
#file=open("oadr3.0.1.json","w")
#json.dumps(python_dict,file)
#file.close()
#print("JSON file saved")