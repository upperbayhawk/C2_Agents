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
    public class JsonGamePlayerConfidential
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resultvar"></param>
        /// <returns></returns>
        public string GamePlayerConfidential2Json(GamePlayerConfidential resultvar)
        {
            string output = JsonConvert.SerializeObject(resultvar);
            Log2.Trace("GamePlayerConfidential2Json: {0}", output);
            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public GamePlayerConfidential Json2GamePlayerConfidential(string jsonString)
        {
            GamePlayerConfidential deserializedData = JsonConvert.DeserializeObject<GamePlayerConfidential>(jsonString);
            Log2.Trace("Json2GamePlayerConfidential: {0} {1}", 
                deserializedData.GamePlayerName,
                deserializedData.GamePlayerId); 
            return deserializedData;
        }
    }
}// End Namespace
