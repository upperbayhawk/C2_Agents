// Copyright (C) Upperbay Systems, LLC - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Dave Hardin <dave@upperbay.com>, 2001-2020

using System;
using System.Text;

using Serilog;
using Serilog.Core;
using Serilog.Events;

using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using System.Threading;
using MQTTnet.Client.Options;
using Newtonsoft.Json;

namespace OadrVenXLib
{
    public class MqttLocalDriver
    {
        private static IManagedMqttClient mqttLocal;

        #region Methods
        static MqttLocalDriver()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mqttURI"></param>
        /// <param name="mqttUser"></param>
        /// <param name="mqttPassword"></param>
        /// <param name="mqttPort"></param>
        /// <returns></returns>
        public static bool MqttInitializeAsync(string mqttURI,
                                                string mqttUser,
                                                string mqttPassword,
                                                int mqttPort)
        {
            string clientId = Guid.NewGuid().ToString();
           
            string mymqttURI = mqttURI;
            string mymqttUser = mqttUser;
            string mymqttPassword = mqttPassword;
            int mymqttPort = mqttPort;
                      
            Log.Debug("Local MqttInitializeAsync Entered");

            // Setup and start a managed MQTT client.
            var myLivingWill = new MqttApplicationMessage()
            {
                Topic = "status",
                Payload = Encoding.UTF8.GetBytes("server/disconnect"),
                Retain = true
            };

            var options = new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(480))
                .WithMaxPendingMessages(20)
                .WithClientOptions(new MqttClientOptionsBuilder()
                    .WithClientId(clientId)
                    .WithCredentials(mymqttUser, mymqttPassword)
                    .WithTcpServer(mymqttURI, mymqttPort)
                     //.WithTls()
                     .WithCommunicationTimeout(TimeSpan.FromSeconds(15))
                     // .WithAuthentication()
                     .WithKeepAlivePeriod(TimeSpan.FromSeconds(120))
                     .WithRequestProblemInformation(true)
                     .WithCleanSession(true)
                     .WithWillMessage(myLivingWill)
                     .WithProtocolVersion(MQTTnet.Formatter.MqttProtocolVersion.V311)
                    .Build())
                .Build();

            mqttLocal = new MqttFactory().CreateManagedMqttClient();

           Log.Verbose("Local MqttFactory Called");

            mqttLocal.UseConnectedHandler (e =>
            {
               Log.Debug("Local Connected successfully with MQTT Broker: {0}", e);
            });

            mqttLocal.UseDisconnectedHandler(e =>
            {
                Log.Error("Local Disconnected from MQTT Broker: {0}", 
                                                          e.Exception);
            });

            mqttLocal.UseApplicationMessageReceivedHandler(e =>
            {
                string receivedTopic = e.ApplicationMessage.Topic;
                try
                {
                    if (string.IsNullOrWhiteSpace(receivedTopic) == false)
                    {
                        
                        if (receivedTopic.StartsWith("CREATEGAME"))
                        {
                            //Check if round trip
                            string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                            Log.Debug("Local Topic: {0}. Message Received: {1}", receivedTopic, payload);
                            OadrData data = JsonConvert.DeserializeObject<OadrData>(payload);


                        }

                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message, ex);
                }
            });


            mqttLocal.StartAsync(options);
            int counter = 0;
            while (mqttLocal.IsConnected == false)
            {
                if (counter > 20)
                {
                    Log.Error("Local Mqtt Startup Failed!: {0}", counter.ToString());
                    break;
                }
                counter++;
                Thread.Sleep(1000);
            }

            Log.Verbose("Local MqttInitializeAsync Completed");

            return true;
        }


        /// <summary>
        /// Publish Message.
        /// </summary>
        /// <param name="topic">Topic.</param>
        /// <param name="payload">Payload.</param>
        /// <param name="retainFlag">Retain flag.</param>
        /// <param name="qos">Quality of Service.</param>
        /// <returns>Task.</returns>

        public static bool MqttPublishAsync(string topic, 
                                        string payload, 
                                        bool retainFlag = false,
                                        int qos = 2)
        {
            Log.Verbose("Local MqttPublishAsync Entered");

            mqttLocal.PublishAsync(new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(payload)
            .WithQualityOfServiceLevel((MQTTnet.Protocol.MqttQualityOfServiceLevel)qos)
            .WithRetainFlag(retainFlag)
            .Build());

            Log.Verbose("Local MqttPublishAsync Completed");

            return true;
        }


        /// <summary>
        /// Subscribe topic.
        /// </summary>
        /// <param name="topic">Topic.</param>
        /// <param name="qos">Quality of Service.</param>
        /// <returns>Task.</returns>
        public static bool MqttSubscribeAsync(string topic, int qos = 2)
        {

            Log.Verbose("Local MqttSubscribeAsync Entered");

            MqttTopicFilter MqttTopic = new MqttTopicFilter();
            MqttTopic.Topic = topic;
            MqttTopic.QualityOfServiceLevel = (MQTTnet.Protocol.MqttQualityOfServiceLevel)qos;
            mqttLocal.SubscribeAsync(MqttTopic);

            Log.Verbose("Local MqttSubscribeAsync Completed");
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool MqttStopAsync()
        {
            int counter = 0;
            if (mqttLocal != null)
            {
                mqttLocal.StopAsync();
                while (mqttLocal.IsConnected == true)
                {
                    if (counter > 20)
                    {
                        Log.Error("Local Mqtt Stop Failed!: {0}", counter.ToString());
                        break;
                    }
                    counter++;
                    Thread.Sleep(1000);
                }
            }
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsConnected()
        {
            bool bit = false;
            if (mqttLocal != null)
            {
                bit = mqttLocal.IsConnected;
            }
            return bit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsStarted()
        {
            bool bit = false;
            if(mqttLocal != null)
            {
                bit = mqttLocal.IsStarted;
            }
            return bit;
        }

    }
    #endregion
}
