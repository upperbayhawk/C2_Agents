﻿using System;
using System.Threading.Tasks;
using Genbox.WolframAlpha.Enums;
using Genbox.WolframAlpha.Objects;
using Genbox.WolframAlpha.Requests;
using Genbox.WolframAlpha.Responses;

namespace Genbox.WolframAlpha.Examples
{
    public class Program
    {
        private const string _appId = "INSERT APP ID HERE";

        private static async Task Main(string[] args)
        {
            //Create the client.
            WolframAlphaClient client = new WolframAlphaClient(_appId);

            FullResultRequest request = new FullResultRequest("100 digits of pi");
            request.Formats = Format.Plaintext;

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
        }
    }
}