// Copyright (C) Upperbay Systems, LLC - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Dave Hardin <dave@upperbay.com>, 2001-2020

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Upperbay.Core.Logging;


namespace Upperbay.Worker.EtherAccess
{
   

     public static class GamePlayerStatistics
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gamePlayerID"></param>
        /// <returns></returns>
        public static bool InitPlayer(string gamePlayerID)
        {
            yearToDateC2CAwards = new YearToDatePlayerAwards(gamePlayerID);
            return true;
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool CalcPlayerGameStatistics()
        {
            Log2.Debug("CalcPlayerGameStatistics");
            try
            {
                yearToDateC2CAwards.LogAllPointsAndValues();

                string myFilePath = "logs\\MyGameYearToDate.csv";
                if (File.Exists(myFilePath))
                    File.Delete(myFilePath);
                yearToDateC2CAwards.LogMyPointsAndValuesToFile(myFilePath);
            }
            catch (Exception ex)
            {
                Log2.Error("{0}", ex.Message);
            }


            return true;
        }

       
        public static YearToDatePlayerAwards yearToDateC2CAwards = null;

    }
}
