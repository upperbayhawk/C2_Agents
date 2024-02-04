// Copyright (C) Upperbay Systems, LLC - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Dave Hardin <dave@upperbay.com>, 2001-2020

using Newtonsoft.Json;

// Assemblies needed for Agentness
using Upperbay.Core.Logging;
using Upperbay.Agent.Interfaces;


namespace Upperbay.Worker.JSON
{
    public class JsonGridPeakDetected
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="datavar"></param>
        /// <returns></returns>
        public string GridPeakDetected2Json(GridPeakDetectedObject datavar)
        {
            string output = JsonConvert.SerializeObject(datavar);
            Log2.Debug("GridPeakDetected2Json: {0}", output);
            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public GridPeakDetectedObject Json2GridPeakDetected(string jsonString)
        {
            GridPeakDetectedObject deserializedData = JsonConvert.DeserializeObject<GridPeakDetectedObject>(jsonString);
            Log2.Debug("Json2GridPeakDetected: {0} {1}", 
                deserializedData.agent_name,
                deserializedData.type_name); 
            return deserializedData;
        }
    }
}// End Namespace
