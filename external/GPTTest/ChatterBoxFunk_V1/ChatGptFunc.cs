// Copyright (C) Upperbay Systems, LLC - All Rights Reserved
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// Written by Dave Hardin <dave@upperbay.com>, 2001-2020

// Services/ChatGptService.cs
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenAI.Managers;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
using OpenAI;
using System.Linq;
using System.Reflection;

namespace ChatterBox
{
    public class ChatGptFunc
    {
        private readonly HttpClient _httpClient;
        private string _url = "https://api.openai.com/v1/chat/completions";
        private string _apiKey = "Bearer " + "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";

        public ChatGptFunc()
        {
           
        }

        public async Task<string> SendMessageToChatGptAsync(string userMessage)
        {

            var openAiService = new OpenAIService(new OpenAiOptions()
            {
                //ApiKey = Environment.GetEnvironmentVariable(_apiKey)
                ApiKey = _apiKey

            }); ;

            //string functionName = "calc_linear_regression";
            //string functionDesc = "Calculate the linear regression of a time-series of data";
            //FunctionDefinitionBuilder functionDefinitionBuilder = new FunctionDefinitionBuilder(functionName, functionDesc);
            //functionDefinitionBuilder.AddParameter("input", "string", "the main input",null,true);
            //FunctionDefinition functionDefinition = functionDefinitionBuilder.Build();

            string functionName = "get_location";
            string functionDesc = "Get the location of an event";
            FunctionDefinitionBuilder functionDefinitionBuilder = new FunctionDefinitionBuilder(functionName, functionDesc);
            functionDefinitionBuilder.AddParameter("input", "string", "the main input", null, true);
            FunctionDefinition functionDefinition = functionDefinitionBuilder.Build();



            var completionResult = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
                {
                    ChatMessage.FromSystem("You are a helpful assistant."),
                    ChatMessage.FromUser("Who won the world series in 2020?"),
                    ChatMessage.FromAssistant("The Los Angeles Dodgers won the World Series in 2020."),
                    ChatMessage.FromUser("Where was it played?")
                },
                Functions = new List<FunctionDefinition>
                {
                    functionDefinition
                },
                Model = Models.Gpt_4,
                MaxTokens = 50//optional
            }) ;
            if (completionResult.Successful)
            {
                Console.WriteLine(completionResult.Choices.First().Message.Content);
                Console.WriteLine(completionResult.Choices.First().Message.FunctionCall.Name);
                Console.WriteLine(completionResult.Choices.First().Message.FunctionCall.ParseArguments());

            }
            else
            {
                Console.WriteLine("ERROR");
            }
            //--------------------------------------------------------

            //var type = Type.GetType("ExternalType");
            //var myMethod = type.GetMethod("MyMethod");
            //var initiatedObject = (ExternalType)Activator.CreateInstance(type);
            //myMethod.Invoke(initiatedObject, null);


            //var staticMethod = type.GetMethod("StaticMethod");
            //staticMethod.Invoke(null, null);


            //--------------------------------------------------------
            // Send function response
            var completionResult1 = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
                {
                    ChatMessage.FromFunction("Lewes, Delaware"),
                },
                Model = Models.Gpt_4,
                MaxTokens = 50//optional
            });
            if (completionResult.Successful)
            {
                Console.WriteLine(completionResult.Choices.First().Message.Content);
            }

            return null;
        }







    }

  }
