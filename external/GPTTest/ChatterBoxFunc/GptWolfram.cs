// Copyright (C) Upperbay Systems, LLC - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Dave Hardin <dave@upperbay.com>, 2001-2020

using System;
using System.Threading.Tasks;
using Genbox.WolframAlpha;
using Genbox.WolframAlpha.Enums;
using Genbox.WolframAlpha.Objects;
using Genbox.WolframAlpha.Requests;
using Genbox.WolframAlpha.Responses;
using Microsoft.SqlServer.Server;

namespace ChatterBox
{
    public static class GptWolfram
    {
        //public GptWolfram() 
        //{ 
        
        
        //}

        private const string _appId = "E3YRPU-957KGL8RVU"; //ChatterBox

        public static async Task<string> compute_regression1(string data)
        {
            try
            {
                //Create the client.
                WolframAlphaClient client = new WolframAlphaClient(_appId);

                //FullResultRequest request = new FullResultRequest("100 digits of pi");
                //
                //ShortAnswerRequest request = new ShortAnswerRequest("100 digits of pi");


                //string wolfyPrompt = "100 digits of pi";
                string wolfyPrompt = "Calculate the linear regression of " + data;
                //string wolfyPrompt = "LinearModelFit[{1,2},{2,3},{3,4},{4,5}][\"BestFitParameters\"]";

                //string wolfyPrompt = "Calculate the LinearModelFit of {1,2},{2,3},{3,4},{4,5}";
                Console.WriteLine("Wolfy Prompt: " + wolfyPrompt);

                ShortAnswerRequest request = new ShortAnswerRequest(wolfyPrompt);


                //request.Formats = Genbox.WolframAlpha.Enums.Format.Plaintext;

                //We start a new query.
                //FullResultResponse results = await client.FullResultAsync(request).ConfigureAwait(false);
                string results = await client.ShortAnswerAsync(request).ConfigureAwait(false);
                //string results = await client.ShortAnswerAsync(request).ConfigureAwait(false);

                //Here we output the Wolfram|Alpha results.
                //if (results.IsError)
                //    Console.WriteLine("Woops, where was an error: " + results.ErrorDetails.Message);

                Console.WriteLine("Results: "+ results);

                //Results are split into "pods" that contain information. Those pods can have SubPods.
                //foreach (Pod pod in results.Pods)
                //{
                //    Console.WriteLine(pod.Title + ":");

                //    foreach (SubPod subPod in pod.SubPods)
                //    {
                //        if (string.IsNullOrEmpty(subPod.Plaintext))
                //            Console.WriteLine("<Cannot output in console>");
                //        else
                //            Console.WriteLine(subPod.Plaintext);
                //    }

                //    Console.WriteLine();
                //}
                return results;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "ERROR";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<string> compute_regression(string data)
        {
            try
            {
                //Create the client.
                WolframAlphaClient client = new WolframAlphaClient(_appId);

                string wolfyPrompt = "Calculate the slope of the best fit straight " + data;
                //Console.WriteLine("Wolfy Prompt: " + wolfyPrompt);
                //FullResultRequest request = new FullResultRequest(wolfyPrompt);

                //string wolfyPrompt = "Calculate the slope of the LinearModelFit of " + data;

                //string wolfyPrompt = "LinearModelFit[{1,2},{2,3},{3,4},{4,5}][\"BestFitParameters\"]";

                Console.WriteLine("Wolfy Prompt: " + wolfyPrompt);
                FullResultRequest request = new FullResultRequest(wolfyPrompt);



                //                FullResultRequest request = new FullResultRequest("100 digits of pi");
                request.Formats = Genbox.WolframAlpha.Enums.Format.Plaintext;

                //We start a new query.
                FullResultResponse results = await client.FullResultAsync(request).ConfigureAwait(false);
                
                //Here we output the Wolfram|Alpha results.
                if (results.IsError)
                    Console.WriteLine("Woops, where was an error: " + results.ErrorDetails.Message);

                Console.WriteLine();

                //Results are split into "pods" that contain information. Those pods can have SubPods.
                foreach (Pod pod in results.Pods)
                {
                    Console.WriteLine(pod.Title + ":");

                    foreach (SubPod subPod in pod.SubPods)
                    {
                        if (string.IsNullOrEmpty(subPod.Plaintext))
                            Console.WriteLine("<Cannot output in console>");
                        else
                            Console.WriteLine(subPod.Plaintext);
                    }

                    Console.WriteLine();
                }
                return "OK";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "ERROR";
            }
        }
    }
}

