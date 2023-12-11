# config.py

# Copyright (C) Upperbay Systems, LLC - All Rights Reserved
# Unauthorized copying of this file, via any medium is strictly prohibited
# Proprietary and confidential
# Written by Dave Hardin <dave@upperbay.com>, 2023

import logbook

agent_name = "WeatherMan-1-0-0"
# MQTT Broker settings
MQTT_BROKER = "localhost"
MQTT_PORT = 1883
mqtt_base_topic = "openai/assistant/"
MQTT_TOPIC_TOASSISTANT = mqtt_base_topic + agent_name + "/ToAssistant"
MQTT_TOPIC_TOCLIENT = mqtt_base_topic + agent_name + "/ToClient"
#
MQTT_TOPIC_COMMAND = mqtt_base_topic + agent_name + "/CommandCenter"
MQTT_TOPIC_CONTROL = mqtt_base_topic + agent_name + "/ControlPanel"
MQTT_TOPIC_ALARM = mqtt_base_topic + agent_name + "/AlarmPanel"
MQTT_TOPIC_NOTICE = mqtt_base_topic + agent_name + "/NoticePanel"
MQTT_TOPIC_ALERT = mqtt_base_topic + agent_name + "/AlertPanel"
#
MQTT_TOPIC_DATAFEED = mqtt_base_topic + agent_name + "/DataFeed"
# Logger level
logging_level = logbook.DEBUG