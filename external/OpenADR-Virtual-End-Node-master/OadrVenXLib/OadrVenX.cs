// Copyright (C) Upperbay Systems, LLC - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Dave Hardin <dave@upperbay.com>, 2001-2020

using System;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Newtonsoft.Json;
using MQTTnet;

namespace OadrVenXLib
{
  
    public class OadrVenX
    {
        // Constructor
        public OadrVenX()
        {
            // Initialization code, if any, can be placed here
        }

        // Init method
        public void Init()
        {
            // Initialization logic, if any, can be placed here
            string logfilename = String.Format("logs\\OadrVenX_log.txt");

            Log.Logger = new LoggerConfiguration()
                  .MinimumLevel.Verbose()
                  .WriteTo.File(logfilename)
                  .CreateLogger();
        }

        // OnOadrEvent method
        public void OnOadrEvent(string groupName, string signalName, string signalType, string signalId, string dateTime, string duration)
        {
            // Handle the Oadr event here
            // ...
            Log.Information("OnOadrEvent");

            Log.Information("groupName = " + groupName);
            Log.Information("signalName = " + signalName);
            Log.Information("signalType = " + signalType);
            Log.Information("signalId = " + signalId);
            Log.Information("dateTime = " + dateTime);
            Log.Information("duration = " + duration);

            OadrData data = new OadrData()
            {
                GroupName = groupName,
                SignalName = signalName,
                SignalType = signalType,
                SignalId = signalId,
                DateTime = dateTime,
                Duration = duration
            };

            string jsonData = JsonConvert.SerializeObject(data);
            Log.Information(jsonData);


            MqttLocalDriver.MqttInitializeAsync("localhost", "", "", 1883);
            //MqttLocalDriver.MqttSubscribeAsync("creategame");
            MqttLocalDriver.MqttPublishAsync("CREATEGAME", jsonData);

            Log.CloseAndFlush();

        }
    }
}
