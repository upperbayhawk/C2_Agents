# xfunctionlib.py

# Copyright (C) Upperbay Systems, LLC - All Rights Reserved
# Unauthorized copying of this file, via any medium is strictly prohibited
# Proprietary and confidential
# Written by Dave Hardin <dave@upperbay.com>, 2023

import paho.mqtt.client as mqtt
from logbook import Logger, FileHandler
from xnetworklib import XNetwork


class XFunction:
    def __init__(self):
        self.mqtt_broker_host = "localhost"  # Replace with your MQTT broker host
        self.mqtt_broker_port = 1883  # Replace with your MQTT broker port
        self.log = Logger("xfunction")
        self.log.debug("Hello From Below")

        self.base_topic = "openai/xfunctions/MathWizard"

    def sendAlarmSignalToNetworkNode(self, network_node, message):
        xnet = XNetwork(self.mqtt_broker_host, self.mqtt_broker_port)
        xnet.connect_to_broker()
        while xnet.connected == False:
            pass
        topic = self.base_topic + network_node
        xnet.send_mqtt_event(topic, message)
        xnet.disconnect_from_broker()               
        self.log.debug("sendAlarmSignalToNetworkNode: " + message + " to " + topic)
        return "OK"

    def sendControlSignalToNetworkNode(self, network_node, message):
        xnet = XNetwork(self.mqtt_broker_host, self.mqtt_broker_port)
        xnet.connect_to_broker()
        while xnet.connected == False:
            pass
        
        topic = self.base_topic + network_node
        xnet.send_mqtt_event(topic, message)
        xnet.disconnect_from_broker()               
        self.log.debug("sendControlSignalToNetworkNode: " + message + " to " + topic)
        return "OK"

    def sendNoticeSignalToNetworkNode(self, network_node, message):
        xnet = XNetwork(self.mqtt_broker_host, self.mqtt_broker_port)
        xnet.connect_to_broker()
        while xnet.connected == False:
            pass
        
        topic = self.base_topic + network_node
        xnet.send_mqtt_event(topic, message)
        xnet.disconnect_from_broker()               
        self.log.debug("sendNoticeSignalToNetworkNode: " + message + " to " + topic)
        return "OK"

    def getNickname(self, location):
        self.log.debug("getNickname: " + location)
        return "SandyPlace"

    def getNickname1(self, location):
        self.log.debug("getNickname: " + location)
        return "SandyPlace1"

    def getNickname2(self, location):
        self.log.debug("getNickname: " + location)
        return "SandyPlace2"

    def getNickname3(self, location):
        self.log.debug("getNickname: " + location)
        return "SandyPlace3"
