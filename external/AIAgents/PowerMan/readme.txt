Packages needed

install python3
https://www.python.org/downloads/

install pip
https://pip.pypa.io/en/stable/installation/

pip install openai
pip install logbook
    OpenAI API uses the standard logger and spews messages so I needed a different logger.
pip install smartthings
pip install arrow

pip install playsound==1.2.2
pip install cachetools
---------------------------------------------

Log Levels, Trace, debug, info

---------------------------------------------

MQTT messages
openai/assistant/agentname/

ToClient
ToAssistant
DataFeed
alarms
Alerts
Command
Control
Notices


From Volttron for consideration
• alerts - Base topic for alerts published by agents and subsystems, such as agent health alerts
• analysis - Base topic for analytics being used with building data
• config - Base topic for managing agent configuration
• devices - Base topic for data being published by drivers
• datalogger - Base topic for agents wishing to record time series data
• heartbeat - Topic for publishing periodic “heartbeat” or “keep-alive”
• market - Base topics for market agent communication
• record - Base topic for agents to record data in an arbitrary format
• weather - Base topic for polling publishes of weather service agents



