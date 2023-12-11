﻿// Copyright (C) Upperbay Systems, LLC - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Dave Hardin <dave@upperbay.com>, 2001-2020

using System;
using System.Text;
using Upperbay.Core.Logging;
using Upperbay.Agent.Interfaces;
using Upperbay.Worker.Hasher;
using Upperbay.Agent.ColonyMatrix;
using Upperbay.Worker.JSON;


using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using System.Threading;
using MQTTnet.Client.Options;

namespace Upperbay.Worker.MQTT
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
                      
            Log2.Trace("Local MqttInitializeAsync Entered");

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

            Log2.Trace("Local MqttFactory Called");

            mqttLocal.UseConnectedHandler (e =>
            {
               Log2.Debug("Local Connected successfully with MQTT Broker: {0}", e);
            });

            mqttLocal.UseDisconnectedHandler(e =>
            {
                Log2.Error("Local Disconnected from MQTT Broker: {0}", 
                                                          e.Exception);
            });

            mqttLocal.UseApplicationMessageReceivedHandler(e =>
            {
                string receivedTopic = e.ApplicationMessage.Topic;
                try
                {
                    if (string.IsNullOrWhiteSpace(receivedTopic) == false)
                    {
                        if (receivedTopic.Equals(TOPICS.DATAVARIABLE_TOPIC))
                        {
                            JsonDataVariable jdv = new JsonDataVariable();
                            DataVariable dv = new DataVariable();
                            string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                            Log2.Trace("Local Topic: {0}. Message Received: {1}", receivedTopic, payload);
                            //TODO: Send Data to TankFarm!!!!
                            //DataHasher hasher = new DataHasher();
                            //int hashCode = hasher.HashDataJson(payload);
                            //place holder for now
                            DataHasher dataHasher = new DataHasher();
                            Int32 hash = payload.GetHashCode();
                            Log2.Trace("Local Payload Hash {0}", hash.ToString());
                            dv = jdv.Json2DataVariable(payload);
                            DataVariableCache.PutObject(dv.ExternalName, dv, (int)hash);
                            Log2.Trace("Local DV Updated {0} {1}", dv.ExternalName, hash);
                            Log2.Trace("Local Now Dumping");

                            //DataVariableCache.DumpCache();
                        }
                        else if (receivedTopic.Equals(TOPICS.COMMAND_TOPIC))
                        {
                            //CHANGE TO MESSAGE CACHE!
                            //JsonDataVariable jdv = new JsonDataVariable();
                            //DataVariable dv = new DataVariable();
                            //string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                            //Log2.Trace("Topic: {0}. Message Received: {1}", receivedTopic, payload);
                            ////TODO: Send Data to TankFarm!!!!
                            ////DataHasher hasher = new DataHasher();
                            ////int hashCode = hasher.HashDataJson(payload);
                            //Int32 hash = payload.GetHashCode();
                            //Log2.Trace("Payload Hash {0}", hash.ToString());
                            //dv = jdv.Json2DataVariable(payload);
                            //DataVariableCache.PutObject(dv.ExternalName, dv, (int)hash);
                            //Log2.Trace("DV Updated {0} {1}", dv.ExternalName, hash);
                            //Log2.Trace("Now Dumping");

                            //DataVariableCache.DumpCache();
                        }
                        else if (receivedTopic.Equals(TOPICS.EVENT_TOPIC))
                        {
                            
                            JsonEventVariable jev = new JsonEventVariable();
                            EventVariable ev = new EventVariable();
                            string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                            Log2.Trace("Local Topic: {0}. Message Received: {1}", receivedTopic, payload);
                            ev = jev.Json2EventVariable(payload);
                            EventVariableCache.WriteEventQueue(ev);
                            Log2.Trace("Local EVENT Queued {0} {1}", ev.EventName,ev.EventType);
                            Log2.Trace("Local Now Dumping");
                            EventVariableCache.DumpQueue();
                        }
                        else if (receivedTopic.StartsWith("smartthings"))
                        {
                            JsonDataVariable jdv = new JsonDataVariable();
                            DataVariable dv = new DataVariable();
                            string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                            Log2.Debug("Local Topic: {0}. Message Received: {1}", receivedTopic, payload);
                            //TODO: Send Data to TankFarm!!!!
                            //DataHasher hasher = new DataHasher();
                            //int hashCode = hasher.HashDataJson(payload);
                            //place holder for now
                            //DataHasher dataHasher = new DataHasher();
                            //Int32 hash = payload.GetHashCode();
                            //Log2.Trace("Local Payload Hash {0}", hash.ToString());
                            //dv = jdv.Json2DataVariable(payload);
                            //DataVariableCache.PutObject(dv.ExternalName, dv, (int)hash);
                            //Log2.Trace("Local DV Updated {0} {1}", dv.ExternalName, hash);
                            //Log2.Trace("Local Now Dumping");
                        }
                        else if (receivedTopic.StartsWith("CREATEGAME"))
                        {
                            string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                            Log2.Debug("Local Topic: {0}. Message Received: {1}", receivedTopic, payload);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log2.Error(ex.Message, ex);
                }
            });


            mqttLocal.StartAsync(options);
            int counter = 0;
            while (mqttLocal.IsConnected == false)
            {
                if (counter > 20)
                {
                    Log2.Error("Local Mqtt Startup Failed!: {0}", counter.ToString());
                    break;
                }
                counter++;
                Thread.Sleep(1000);
            }

            Log2.Trace("Local MqttInitializeAsync Completed");

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
            Log2.Trace("Local MqttPublishAsync Entered");

            mqttLocal.PublishAsync(new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(payload)
            .WithQualityOfServiceLevel((MQTTnet.Protocol.MqttQualityOfServiceLevel)qos)
            .WithRetainFlag(retainFlag)
            .Build());

            Log2.Trace("Local MqttPublishAsync Completed");

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

            Log2.Trace("Local MqttSubscribeAsync Entered");

            MqttTopicFilter MqttTopic = new MqttTopicFilter();
            MqttTopic.Topic = topic;
            MqttTopic.QualityOfServiceLevel = (MQTTnet.Protocol.MqttQualityOfServiceLevel)qos;
            mqttLocal.SubscribeAsync(MqttTopic);

            Log2.Trace("Local MqttSubscribeAsync Completed");
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool MqttStopAsync()
        {
            int counter = 0;
            mqttLocal.StopAsync();
            while (mqttLocal.IsConnected == true)
            {
                if (counter > 20)
                {
                    Log2.Error("Local Mqtt Stop Failed!: {0}", counter.ToString());
                    break;
                }
                counter++;
                Thread.Sleep(1000);
            }
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsConnected()
        {
            bool bit = mqttLocal.IsConnected;
            return bit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsStarted()
        {
            bool bit = mqttLocal.IsStarted;
            return bit;
        }

    }
    #endregion
}
