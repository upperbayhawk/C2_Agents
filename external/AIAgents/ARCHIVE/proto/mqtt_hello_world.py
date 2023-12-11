import json
import logging
from mqtt_interface import MQTTInterface

# Configure logging
logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)

def main():
    try:
        # Initialize MQTT Interface
        mqtt_interface = MQTTInterface("localhost", 1883, "test/topic")

        # Connect to MQTT Broker
        mqtt_interface.connect()
        logger.info("Connected to MQTT Broker")

        # Subscribe to Topic
        mqtt_interface.subscribe()
        logger.info("Subscribed to topic")

        # Send Message
        mqtt_interface.send(json.dumps({"message": "hell yes"}))
        logger.info("Message sent")

        # Keep the script running to listen for incoming messages
        input("Press Enter to stop...\n")

        # Disconnect
        mqtt_interface.disconnect()
        logger.info("Disconnected from MQTT Broker")

    except Exception as e:
        logger.error(f"An error occurred: {e}")

if __name__ == "__main__":
    main()