﻿//==================================================================
//Author: Dave Hardin, Upperbay Systems LLC
//Author URL: https://upperbay.com
//License: MIT
//Date: 2001-2024
//Description: 
//Notes:
//==================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Xml;

using Upperbay.Core.Logging;

namespace Upperbay.Core.Library
{
    /// <summary>
    /// 
    /// </summary>
    static public class MyAppConfig
    {
        static private bool isConfigured = false;
        static private Dictionary<string, Dictionary<string, string>> _clusterParameters = new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentName"></param>
        /// <returns></returns>
        static public bool SetMyAppConfig(string agentName)
        {
            //// Specify agent config settings at runtime.
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.File = "config\\" + agentName + ".app.config";
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            ConfigurationManager.GetSection("appSettings");
            InitClusterParameters();
            isConfigured = true;

            return true;
        }



        /// <summary>
        /// AppConfig overrides for hardcoding app.config file parameters.
        /// Defaults to App.Config file
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        static public string GetParameter(string parameter)
        {
            if (!isConfigured)
            {
                Log2.Error("MyAppConfig is NOT configured: {0}", parameter);
                return null;
            }

            string val = "";
            bool isInternalParameter = true;

            switch (parameter)
            {
                //System Parameters
                //case "ClusterName": val = "VOLTA_BETA3"; break;
                case "ClusterName": val = "VOLTA_BETA2_HIVE"; break;
                case "ClusterVersion": val = "0.6.9"; break;
                //
                case "Period": val = "5000"; break;
                case "AIGameEnable": val = "true"; break;
                //MQTT Local Parameters
                case "MqttLocalEnable": val = "true"; break;
                case "MqttLocalIpAddress": val = "localhost"; break;
                case "MqttLocalPort": val = "1883"; break;
                case "MqttLocalLoginName": val = ""; break;//Dave
                case "MqttLocalPassword": val = ""; break;//Dave
                //MQTT Remote Parameters
                case "MqttRemoteEnable": val = "true"; break;
                case "MqttRemoteIpAddress": val = "192.168.0.114"; break;
                case "MqttRemotePort":val = "1883";break;
                case "MqttRemoteLoginName": val = "";break;
                case "MqttRemotePassword":val = "";break;
                //MQTT Cloud Parameters
                case "MqttCloudEnable": val = "true"; break;
                //Thingspeak Parameters
                case "ThingSpeakReferenceServerURL":val = "https://api.thingspeak.com/channels/"; break;
                case "WattsMirrorAtCampDavid": val = "channelID=1111475,fieldID=1,writeKey=QN271S8WTAKN3P8S"; break;
                case "SoilMoistureAtCampDavid": val = "channelID=369308,fieldID=1,readKey=SEQU2LPFM2JEFWUR"; break;
                case "LowSoilMoistureAtCampDavid": val = "channelID=369308,fieldID=6,writeKey=INDFAI1JLG6LE4F8"; break;
                case "ThingSpeakWriteTag1": val = "channelID=5252525,fieldID=1,writeKey=kjhhk"; break;
                case "CalcAveragesFromRawData": val = "true"; break;
                //Database Parameters
                case "hasODBCDatabase":val = "true";break;
                case "ODBCConnectionString":val = "DSN=C2CSYSTEM_LOC"; break;
                //Enable Internal Executive Assistants
                case "hasEventAssistant":val = "true";break;
                case "hasGameEventAssistant":val = "true";break;
                case "hasEtherReadAccessorAssistant":val = "false";break;
                case "hasEtherWriteAccessorAssistant":val = "false";break;
                case "hasSimulatorAssistant":val = "true";break;
                case "hasCacheAssistant":val = "true";break;
                case "hasMqttPublisherAssistant":val = "true";break;
                case "hasMqttSubscriberAssistant": val = "true"; break;
                case "hasSmartthingsPublisherAssistant": val = "true"; break;
                case "hasCloudMqttPublisherAssistant":val = "true";break;
                case "hasCloudMqttSecureSubscriberAssistant":val = "true";break;
                case "hasThingSpeakReadAccessorAssistant":val = "true";break;
                case "hasThingSpeakWriteAccessorAssistant":val = "true";break;
                case "hasManualInputAssistant":val = "true";break;
                case "hasWeatherServiceAssistant":val = "false";break;
                //Ancillary Parameters
                case "Voice":val = "Microsoft Zira Desktop";break;
                case "CacheDataFile":val = "DataVariableCache.txt";break;
                case "ReloadCacheOnStart":val = "true";break;
                case "PublishingDelay": val = "1"; break;
                case "LMPEnable": val = "true"; break;
                case "LMPLowThreshold": val = "40"; break;
                case "LMPHighThreshold": val = "70"; break;
                // Weather  Parameters
                case "ZipCode": val = "19958"; break;
                case "Location": val = "Boggs Lane"; break;
                case "WeatherStationID": val = "KTAN"; break;
                case "WeatherAlertZone": val = "DEZ004"; break;
                //TEST Parameters
                case "EthereumOracleName":val = "HawksNest";break;
                case "MaxEthereumSlices": val = "30"; break;
                case "enableCommunity": val = "false"; break;
                case "enableCluster": val = "false"; break;
                case "SystemBrand": val = "Upperbay Systems"; break;
                case "SystemName": val = "CurrenForCarbon"; break;

                default:
                    // from app.config file
                    isInternalParameter = false;
                    try
                    {
                        ConfigurationManager.RefreshSection("appSettings");
                        ConfigurationManager.GetSection("appSettings");
                        val = ConfigurationManager.AppSettings[parameter];
                        if ((val == "") || (val == null))
                        {
                            string value = Environment.GetEnvironmentVariable(parameter);
                            if (value != null)
                            {
                                Log2.Debug("CONFIGURATION PARAMETER IN ENVIRONMENT: {0}", parameter);
                                return value;
                            }
                            else
                            {
                                Log2.Error("CONFIGURATION PARAMETER NOT FOUND: {0}", parameter);
                                return null;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Log2.Error("CONFIGURATION PARAMETER NOT FOUND: {0}", parameter);
                        return null;
                    }
                    break;
            }

            // check if the config parameter is overidden in config file
            if (isInternalParameter)
            {
                try
                {
                    ConfigurationManager.RefreshSection("appSettings");
                    ConfigurationManager.GetSection("appSettings");
                    string fileval = ConfigurationManager.AppSettings[parameter];
                    if ((fileval == "") || (fileval == null))
                    {
                        // normal
                    }
                    else
                    {
                        Log2.Debug("INTERNAL CONFIGURATION PARAMETER OVERRIDDEN IN CONFIG FILE: {0} = {1}", parameter, fileval);
                        val = fileval;
                    }
                }
                catch (Exception)
                {
                    //normal
                }
            }

            return val;
        }

        /// <summary>
        /// 
        /// </summary>
        static public void InitClusterParameters()
        {

            try
            {
                string cluster;

                cluster = "VOLTA_BETA2_HIVE";
                Dictionary<string, string> VOLTA_BETA2_HIVE = new Dictionary<string, string>();
                //VOLTA_BETA2_HIVE.Add("EthereumContractAddress", "0xaB5B2Cf8D5ba4e58Dd7c1aE3799d6c1a642d2E6a");
                //VOLTA_BETA2_HIVE.Add("RemoteEthereumServerURL", "http://www.mauiview.com:8545");
                VOLTA_BETA2_HIVE.Add("EthereumChainId", "73799");
                //VOLTA_BETA2_HIVE.Add("EthereumClusterKey", "0x0949a8d20891952dbc52ec59a2aaf36dcd97b5a114103ba4c949fdc0652a2a7f");
                //VOLTA_BETA2_HIVE.Add("EthereumClusterAddress", "0x47b03cb6a335A15a87Fb63AE295add5aFB0539ed");
                //VOLTA_BETA2_HIVE.Add("MqttCloudSecureLoginName", "pearlygates");
                //VOLTA_BETA2_HIVE.Add("MqttCloudSecurePassword", "!Pearlygates87!");
                        //VOLTA_BETA2_HIVE.Add("MqttCloudSecureIpAddress", "broker.hivemq.com");
                        //VOLTA_BETA2_HIVE.Add("MqttCloudSecurePort", "1883");
                        //VOLTA_BETA2_HIVE.Add("MqttCloudIpAddress", "broker.hivemq.com");
                        //VOLTA_BETA2_HIVE.Add("MqttCloudPort", "1883");
                //VOLTA_BETA2_HIVE.Add("MqttCloudSecureIpAddress", "9c41862892f042dea951b2f348b676ec.s1.eu.hivemq.cloud");
                VOLTA_BETA2_HIVE.Add("MqttCloudSecurePort", "8883");
                //VOLTA_BETA2_HIVE.Add("MqttCloudIpAddress", "9c41862892f042dea951b2f348b676ec.s1.eu.hivemq.cloud");
                VOLTA_BETA2_HIVE.Add("MqttCloudPort", "8883");
                //VOLTA_BETA2_HIVE.Add("SMSAccountName", "davidhardin2");
                //VOLTA_BETA2_HIVE.Add("SMSAccountKey", "eWymWS3pHCqvewP8NqKdc2DnvCABDE");
                VOLTA_BETA2_HIVE.Add("LMPRTO", "PJM");
                //VOLTA_BETA2_HIVE.Add("LMPKey", "312249d38ae6410bbd6ea56f8343eef8");
                VOLTA_BETA2_HIVE.Add("LMPNode", "49955");
                _clusterParameters.Add(cluster, VOLTA_BETA2_HIVE);

                cluster = "VOLTA_BETA3";
                Dictionary<string, string> VOLTA_BETA3 = new Dictionary<string, string>();
                VOLTA_BETA3.Add("EthereumContractAddress", "0xB99b29a8b95Bd776ef204AF35075E1f44F48aA1E");
                //VOLTA_BETA3.Add("RemoteEthereumServerURL", "http://www.mauiview.com:8545");
                VOLTA_BETA3.Add("EthereumChainId", "73799");
                //VOLTA_BETA3.Add("EthereumClusterKey", "0x0949a8d20891952dbc52ec59a2aaf36dcd97b5a114103ba4c949fdc0652a2a7f");
                //VOLTA_BETA3.Add("EthereumClusterAddress", "0x47b03cb6a335A15a87Fb63AE295add5aFB0539ed");
                //VOLTA_BETA3.Add("MqttCloudSecureLoginName", "pearlygates");
                //VOLTA_BETA3.Add("MqttCloudSecurePassword", "!Pearlygates87!");
                //VOLTA_BETA3.Add("MqttCloudSecureIpAddress", "9c41862892f042dea951b2f348b676ec.s1.eu.hivemq.cloud");
                VOLTA_BETA3.Add("MqttCloudSecurePort", "8883");
                //VOLTA_BETA3.Add("MqttCloudIpAddress", "9c41862892f042dea951b2f348b676ec.s1.eu.hivemq.cloud");
                VOLTA_BETA3.Add("MqttCloudPort", "8883");
                //VOLTA_BETA3.Add("SMSAccountName", "davidhardin2");
                //VOLTA_BETA3.Add("SMSAccountKey", "eWymWS3pHCqvewP8NqKdc2DnvCABDE");
                VOLTA_BETA3.Add("LMPRTO", "PJM");
                //VOLTA_BETA3.Add("LMPKey", "312249d38ae6410bbd6ea56f8343eef8");
                VOLTA_BETA3.Add("LMPNode", "49955");
                _clusterParameters.Add(cluster, VOLTA_BETA3);

                cluster = "VOLTA_BETA4";
                Dictionary<string, string> VOLTA_BETA4 = new Dictionary<string, string>();
                VOLTA_BETA4.Add("EthereumContractAddress", "0x5B8C0a8914c8ebA00dA4328ba5F02d719D3e053c");
                VOLTA_BETA4.Add("RemoteEthereumServerURL", "http://www.mauiview.com:8545");
                VOLTA_BETA4.Add("EthereumChainId", "73799");
                //VOLTA_BETA4.Add("EthereumClusterKey", "0x0949a8d20891952dbc52ec59a2aaf36dcd97b5a114103ba4c949fdc0652a2a7f");
                //VOLTA_BETA4.Add("EthereumClusterAddress", "0x47b03cb6a335A15a87Fb63AE295add5aFB0539ed");
                //VOLTA_BETA4.Add("MqttCloudSecureLoginName", "pearlygates");
                //VOLTA_BETA4.Add("MqttCloudSecurePassword", "!Pearlygates87!");
                //VOLTA_BETA4.Add("MqttCloudSecureIpAddress", "9c41862892f042dea951b2f348b676ec.s1.eu.hivemq.cloud");
                VOLTA_BETA4.Add("MqttCloudSecurePort", "8883");
                //VOLTA_BETA4.Add("MqttCloudIpAddress", "9c41862892f042dea951b2f348b676ec.s1.eu.hivemq.cloud");
                VOLTA_BETA4.Add("MqttCloudPort", "8883");
                //VOLTA_BETA4.Add("SMSAccountName", "davidhardin2");
                //VOLTA_BETA4.Add("SMSAccountKey", "eWymWS3pHCqvewP8NqKdc2DnvCABDE");
                VOLTA_BETA4.Add("LMPRTO", "PJM");
                //VOLTA_BETA4.Add("LMPKey", "312249d38ae6410bbd6ea56f8343eef8");
                VOLTA_BETA4.Add("LMPNode", "49955");
                _clusterParameters.Add(cluster, VOLTA_BETA4);



                cluster = "VOLTA_BETA5";
                Dictionary<string, string> VOLTA_BETA5 = new Dictionary<string, string>();
                VOLTA_BETA5.Add("EthereumContractAddress", "0x36228475Bf4ed21ACb0EEBbF943192d2d657ce5A");
                VOLTA_BETA5.Add("RemoteEthereumServerURL", "http://www.mauiview.com:8545");
                VOLTA_BETA5.Add("EthereumChainId", "73799");
                //VOLTA_BETA5.Add("EthereumClusterKey", "0x0949a8d20891952dbc52ec59a2aaf36dcd97b5a114103ba4c949fdc0652a2a7f");
                //VOLTA_BETA5.Add("EthereumClusterAddress", "0x47b03cb6a335A15a87Fb63AE295add5aFB0539ed");
                //VOLTA_BETA5.Add("MqttCloudSecureLoginName", "pearlygates");
                //VOLTA_BETA5.Add("MqttCloudSecurePassword", "!Pearlygates87!");
                //VOLTA_BETA5.Add("MqttCloudSecureIpAddress", "spectacular-waiter.cloudmqtt.com");
                VOLTA_BETA5.Add("MqttCloudSecurePort", "8883");
                //VOLTA_BETA5.Add("MqttCloudIpAddress", "9c41862892f042dea951b2f348b676ec.s1.eu.hivemq.cloud");
                VOLTA_BETA5.Add("MqttCloudPort", "8883");
                //VOLTA_BETA5.Add("SMSAccountName", "davidhardin2");
                //VOLTA_BETA5.Add("SMSAccountKey", "eWymWS3pHCqvewP8NqKdc2DnvCABDE");
                VOLTA_BETA5.Add("LMPRTO", "PJM");
                //VOLTA_BETA5.Add("LMPKey", "312249d38ae6410bbd6ea56f8343eef8");
                VOLTA_BETA5.Add("LMPNode", "49955");
                _clusterParameters.Add(cluster, VOLTA_BETA5);


           
                cluster = "VOLTA_BETA6";
                Dictionary<string, string> VOLTA_BETA6 = new Dictionary<string, string>();
                VOLTA_BETA6.Add("EthereumContractAddress", "0xb1066c5A5f580d3BCFb415B9862De886ab716D55");
                VOLTA_BETA6.Add("RemoteEthereumServerURL", "http://www.mauiview.com:8545");
                VOLTA_BETA6.Add("EthereumChainId", "73799");
                //VOLTA_BETA6.Add("EthereumClusterKey", "0x0949a8d20891952dbc52ec59a2aaf36dcd97b5a114103ba4c949fdc0652a2a7f");
                //VOLTA_BETA6.Add("EthereumClusterAddress", "0x47b03cb6a335A15a87Fb63AE295add5aFB0539ed");
                //VOLTA_BETA6.Add("MqttCloudSecureLoginName", "pearlygates");
                //VOLTA_BETA6.Add("MqttCloudSecurePassword", "!Pearlygates87!");
                VOLTA_BETA6.Add("MqttCloudSecurePort", "8883");
                //VOLTA_BETA6.Add("MqttCloudIpAddress", "spectacular-waiter.cloudmqtt.com");
                VOLTA_BETA6.Add("MqttCloudPort", "8883");
                //VOLTA_BETA6.Add("SMSAccountName", "davidhardin2");
                //VOLTA_BETA6.Add("SMSAccountKey", "eWymWS3pHCqvewP8NqKdc2DnvCABDE");
                VOLTA_BETA6.Add("LMPRTO", "PJM");
                //VOLTA_BETA6.Add("LMPKey", "312249d38ae6410bbd6ea56f8343eef8");
                VOLTA_BETA6.Add("LMPNode", "49955");
                _clusterParameters.Add(cluster, VOLTA_BETA6);

                cluster = "VOLTA_BETA7";
                Dictionary<string, string> VOLTA_BETA7 = new Dictionary<string, string>();
                VOLTA_BETA7.Add("EthereumContractAddress", "0x1fE012B3664914725a4CB86952341fF9fe84319E");
                VOLTA_BETA7.Add("RemoteEthereumServerURL", "http://www.mauiview.com:8545");
                VOLTA_BETA7.Add("EthereumChainId", "73799");
                //VOLTA_BETA7.Add("EthereumClusterKey", "0x0949a8d20891952dbc52ec59a2aaf36dcd97b5a114103ba4c949fdc0652a2a7f");
                //VOLTA_BETA7.Add("EthereumClusterAddress", "0x47b03cb6a335A15a87Fb63AE295add5aFB0539ed");
                //VOLTA_BETA7.Add("MqttCloudSecureLoginName", "pearlygates");
                //VOLTA_BETA7.Add("MqttCloudSecurePassword", "!Pearlygates87!");
                //VOLTA_BETA7.Add("MqttCloudSecureLoginName", "pearlygates");
                //VOLTA_BETA7.Add("MqttCloudSecurePassword", "pearlygates");
                //VOLTA_BETA7.Add("MqttCloudSecureIpAddress", "spectacular-waiter.cloudmqtt.com");
                VOLTA_BETA7.Add("MqttCloudSecurePort", "8883");
                //VOLTA_BETA7.Add("MqttCloudIpAddress", "spectacular-waiter.cloudmqtt.com");
                VOLTA_BETA7.Add("MqttCloudPort", "8883");
                //VOLTA_BETA7.Add("SMSAccountName", "davidhardin2");
                //VOLTA_BETA7.Add("SMSAccountKey", "eWymWS3pHCqvewP8NqKdc2DnvCABDE");
                VOLTA_BETA7.Add("LMPRTO", "PJM");
                //VOLTA_BETA7.Add("LMPKey", "312249d38ae6410bbd6ea56f8343eef8");
                VOLTA_BETA7.Add("LMPNode", "49955");
                _clusterParameters.Add(cluster, VOLTA_BETA7);


                cluster = "EWC_BETA1";
                Dictionary<string, string> EWC_BETA1 = new Dictionary<string, string>();
                EWC_BETA1.Add("EthereumContractAddress", "0x27229C971BAf53C3A9Be9e9e2a6eB64498aeE139");
                EWC_BETA1.Add("RemoteEthereumServerURL", "https://rpc.energyweb.org");
                EWC_BETA1.Add("EthereumChainId", "0xf6");
                EWC_BETA1.Add("EthereumClusterKey", "0x0949a8d20891952dbc52ec59a2xxxxxxxxxxxx52a2a7f");
                EWC_BETA1.Add("EthereumClusterAddress", "0x47b03cb6a335A15a87FbxxxxxxxxxxxaFB0539ed");
                EWC_BETA1.Add("MqttCloudSecureLoginName", "pearlygates");
                EWC_BETA1.Add("MqttCloudSecurePassword", "!Pearlyxxxxxx7!");
                EWC_BETA1.Add("MqttCloudIpAddress", "spectacular-waiter.cloudmqtt.com");
                EWC_BETA1.Add("MqttCloudPort", "1883");
                EWC_BETA1.Add("SMSAccountName", "davidhardin2");
                EWC_BETA1.Add("SMSAccountKey", "eWymWSxxxxxxxxxxNqKdc2DnvCABDE");
                EWC_BETA1.Add("LMPRTO", "PJM");
                EWC_BETA1.Add("LMPKey", "312249d38ae6410bbxxxxxxxf8");
                EWC_BETA1.Add("LMPNode", "49955");
                _clusterParameters.Add(cluster, EWC_BETA1);
             
            }
            catch (Exception ex)
            {
                Log2.Error("InitClusterParameters {0}", ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        static public string GetClusterParameter(string cluster,string parameter)
        {
            if (!isConfigured)
            {
                Log2.Error("MyAppConfig is NOT configured: {0}", parameter);
                return null;
            }

            string val;

            try
            {
                Dictionary<string, string> myClusterDict;
                if (_clusterParameters.TryGetValue(cluster, out myClusterDict))
                {
                    if (myClusterDict.TryGetValue(parameter, out val))
                    {
                        //Log2.Info("GetClusterParameter FOUND {0}:{1}", cluster, parameter);
                        return val;
                    }
                    else
                    {
                        // from app.config file
                        Log2.Error("GetClusterParameter NOT FOUND {0}:{1}", cluster, parameter);
                        try
                        {
                            ConfigurationManager.RefreshSection("appSettings");
                            ConfigurationManager.GetSection("appSettings");
                            val = ConfigurationManager.AppSettings[parameter];
                            if (val == null)
                            {
                                string value = Environment.GetEnvironmentVariable(parameter);
                                if (value != null)
                                {
                                    Log2.Debug("CLUSTER CONFIGURATION PARAMETER IN ENVIRONMENT: {0} {1}", cluster, parameter);
                                    return value;
                                }
                                else
                                {
                                    Log2.Error("CLUSTER CONFIGURATION PARAMETER NOT FOUND: {0} {1}", cluster, parameter);
                                    return null;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            Log2.Error("CLUSTER CONFIGURATION PARAMETER NOT FOUND IN APPSETTINGS: {0} {1}", cluster, parameter);
                            return null;
                        }
                        return val;
                    }
                }
                else
                {
                    Log2.Error("GetClusterParameter NOT FOUND {0}:{1}", cluster, parameter);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log2.Error("GetClusterParameter NOT FOUND {0}:{1}", cluster, parameter);
                Log2.Error("GetClusterParameter {0}", ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        static public void AppConfigAddNewParms()
        {
            try
            {
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.File = "config\\ClusterAgent.app.config";
                ConfigurationManager.RefreshSection("appSettings");
                ConfigurationManager.GetSection("appSettings");

                if (ConfigurationManager.AppSettings["NewParm1"] == null)
                {
                    AppConfigAddParm("NewParm1", "NewParmValue1");
                    Log2.Info("GetParm NewParm1 = {0}", MyAppConfig.GetParameter("NewParm1"));
                }
                if (ConfigurationManager.AppSettings["NewParm2"] == null)
                {
                    AppConfigAddParm("NewParm2", "NewParmValue2");
                    Log2.Info("GetParm NewParm2 = {0}", MyAppConfig.GetParameter("NewParm2"));
                }
            }
            catch (Exception ex)
            {
                Log2.Error("AppConfigAddParms: EXCEPTION = " + ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        static public void AppConfigAddParm(string key, string value)
        {
            try
            {
                //System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                //config.AppSettings.File = "config\\ClusterAgent.app.config";
                //config.AppSettings.Settings.Add(key, value);
                //config.Save(ConfigurationSaveMode.Modified, true);
                //ConfigurationManager.RefreshSection("appSettings");

                var xmlDoc = new XmlDocument();
                string currentDirPath = Directory.GetCurrentDirectory();
                string fileName = currentDirPath + "\\config\\ClusterAgent.app.config";
                xmlDoc.Load(fileName);
                XmlNode node = xmlDoc.SelectSingleNode("//appSettings");
                XmlElement elem;
                elem = xmlDoc.CreateElement("add");
                elem.SetAttribute("key", key);
                elem.SetAttribute("value", value);
                node.AppendChild(elem);
                xmlDoc.Save(fileName);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception ex)
            {
                Log2.Error("AppConfigAddParm: EXCEPTION = " + ex.ToString());
            }
        }



        /// <summary>
        /// 
        /// </summary>
        static public bool SyncAppConfig()
        {
            try
            {
                string currentDirPath = Directory.GetCurrentDirectory();
                DirectoryInfo currentDirInfo = new DirectoryInfo(currentDirPath);
                string currentDirName = currentDirInfo.Name;
                Log2.Trace("SyncAppConfig:Current Dir Name = " + currentDirName);
                string baseServiceName = currentDirName; //"e.g. CurrentForCarbon"

                DirectoryInfo parentDirInfo = Directory.GetParent(".");
                string parentDir = parentDirInfo.FullName;
                Log2.Trace("SyncAppConfig:Parent Dir Name = " + parentDir);

                string restoreDir = parentDir + "\\" + baseServiceName;
                string restoreFile = restoreDir + "\\config\\ClusterAgent.app.config";
                Log2.Trace("SyncAppConfig:SourceFile Name = " + restoreFile);

                string saveDir = parentDir + "\\Configurations\\";
                if (!Directory.Exists(saveDir))
                {
                    //The below code will create a folder if the folder does not exists.            
                    DirectoryInfo folder = Directory.CreateDirectory(saveDir);
                }
                string saveFile = saveDir + baseServiceName + "_ClusterAgent.app.config";
                   
                Log2.Trace("SyncAppConfig:TargetFile Name = " + saveFile);

                if ((File.Exists(restoreFile)) && (File.Exists(saveFile)))
                {
                    Log2.Debug("AppConfig files are already synced");
                    //both files exist so don't do anything
                }
                else if ((!File.Exists(restoreFile)) && (File.Exists(saveFile)))
                {
                    Log2.Debug("SyncAppConfig:TargetFile Name = " + saveFile);
                    File.Copy(saveFile, restoreFile, true);
                }
                else if ((File.Exists(restoreFile)) && (!File.Exists(saveFile)))
                {
                    Log2.Debug("SyncAppConfig:TargetFile Name = " + saveFile);
                    File.Copy(restoreFile, saveFile, true);
                }
                else if ((!File.Exists(restoreFile)) && (!File.Exists(saveFile)))
                {
                    Log2.Debug("AppConfig File is MISSING!!! -> " + restoreFile);
                    return false;
                }
                
                return true;
            }
            catch (Exception ex)
            {
                Log2.Error("SyncAppConfig: EXCEPTION = " + ex.ToString());
                return true;
            }
        }
    }
}
