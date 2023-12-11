import paho.mqtt.client as mqtt

class MQTTInterface:
    def __init__(self, broker, port, topic):
        self.client = mqtt.Client()
        self.broker = broker
        self.port = port
        self.topic = "DataVariable/All"
        self.client.on_message = self.on_message

    def connect(self):
        try:
            self.client.connect(self.broker, self.port, 60)
            self.client.loop_start()
        except Exception as e:
            raise ConnectionError(f"Error connecting to MQTT Broker: {e}")

    def send(self, message):
        try:
            self.client.publish(self.topic, message)
        except Exception as e:
            raise IOError(f"Error sending message: {e}")

    def subscribe(self):
        try:
            self.client.subscribe(self.topic)
        except Exception as e:
            raise IOError(f"Error subscribing to topic: {e}")

    def on_message(self, client, userdata, message):
        print(f"Received message '{message.payload.decode()}' on topic '{message.topic}'")

    def disconnect(self):
        self.client.loop_stop()
        self.client.disconnect()