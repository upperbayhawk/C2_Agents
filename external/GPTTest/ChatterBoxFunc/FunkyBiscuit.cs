// Copyright (C) Upperbay Systems, LLC - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Dave Hardin <dave@upperbay.com>, 2001-2020

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatterBox
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    public static class FunkyBiscuit
    {

        public static string get_location(string location)
        {

            Console.WriteLine("from Maui " + location);

            return "Lewes";

        }

        public static async Task<string> compute_regression(string data)
        {
            try
            {
                string result = "OK";
                //GptWolfram compute = new GptWolfram();
                //            string result = GptWolfram.compute_regression(data);
                Console.WriteLine("Calling Wolfy");
                result = await GptWolfram.compute_regression(data);
                Console.WriteLine("Funky: " + result);
                return result;
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Error: " + ex.Message);
                return "ERROR";
            }
        }

        public static async Task<string> compute_regression2 (string data)
        {
            try
            {
                string result = "OK";
                result =GptMathNet.compute_regression(data);
                Console.WriteLine("Funky Math: " + result);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return "ERROR";
            }
        }






        public static string talk(string data)
        {
            GptSpeak compute = new GptSpeak();
            compute.Talk(data);
            return "OK";
        }
    }

}
