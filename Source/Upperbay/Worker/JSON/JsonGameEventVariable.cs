﻿// Copyright (C) Upperbay Systems, LLC - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Dave Hardin <dave@upperbay.com>, 2001-2020

using Newtonsoft.Json;

// Assemblies needed for Agentness
using Upperbay.Core.Logging;
using Upperbay.Agent.Interfaces;


namespace Upperbay.Worker.JSON
{
    public class JsonGameEventVariable
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datavar"></param>
        /// <returns></returns>
        public string GameEventVariable2Json(GameEventVariable datavar)
        {
            string output = JsonConvert.SerializeObject(datavar);
            Log2.Trace("GameEventVariable2Json: {0}", output);
            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public GameEventVariable Json2GameEventVariable(string jsonString)
        {
            GameEventVariable deserializedData = JsonConvert.DeserializeObject<GameEventVariable>(jsonString);
            Log2.Trace("Json2GameEventVariable: {0} {1}", 
                deserializedData.GameName,
                deserializedData.GameId); 
            return deserializedData;
        }
    }
}// End Namespace
