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
using System.Collections;
using System.Diagnostics;

namespace ChatterBox
{
    public class ChatGptFunc
    {
        private readonly HttpClient _httpClient;
        private string _url = "https://api.openai.com/v1/chat/completions";
        //private string _apiKey = "Bearer " + "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";
        private string _apiKey = "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";
        private string _model = Models.Gpt_3_5_Turbo_0613;
        //private string _model = Models.Gpt_4_0613;
        public ChatGptFunc()
        {
           
        }

        public async Task<string> SendMessageToChatGptAsync(string userMessage, string systemPrompt)
        {

            Console.WriteLine("usermessage="+userMessage);

            List<ChatMessage> myMessages = new List<ChatMessage>
                {
                    ChatMessage.FromSystem(systemPrompt),
                    //ChatMessage.FromSystem("You are a helpful assistant. Talk to Dave when you have anything to say."),
                    //ChatMessage.FromUser("Where did the sailing happen?"),

                    ChatMessage.FromUser(userMessage),
                    //ChatMessage.FromAssistant("Tell Dave that The Los Angeles Dodgers won the World Series in 2020."),
                    //ChatMessage.FromUser("Where was it played?"),
                };


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
            functionDefinitionBuilder.AddParameter("location", "string", "the main input", null, true);
            FunctionDefinition functionDefinition = functionDefinitionBuilder.Build();

            string functionName1 = "compute_regression";
            string functionDesc1 = "Compute linearRegression";
            FunctionDefinitionBuilder functionDefinitionBuilder1 = new FunctionDefinitionBuilder(functionName1, functionDesc1);
            functionDefinitionBuilder1.AddParameter("data", "string", "the main input", null, true);
            FunctionDefinition functionDefinition1 = functionDefinitionBuilder1.Build();

            string functionName2 = "talk";
            string functionDesc2 = "Talk to Dave";
            FunctionDefinitionBuilder functionDefinitionBuilder2 = new FunctionDefinitionBuilder(functionName2, functionDesc2);
            functionDefinitionBuilder2.AddParameter("data", "string", "text to talk", null, true);
            FunctionDefinition functionDefinition2 = functionDefinitionBuilder2.Build();

            List<FunctionDefinition> myFunctions = new List<FunctionDefinition>
                {
                    functionDefinition,
                    functionDefinition1,
                    functionDefinition2,
                };


            Console.WriteLine(functionDefinition.Name);
            Console.WriteLine(functionDefinition.Description);
            //Console.WriteLine(functionDefinition.Parameters);
            string myFunctionName = null;
            string myParamName = null;
            string myParamValue = null;

            Console.WriteLine("PASS1");
            try
            {


                var completionResult = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
                {
                    Messages = myMessages,
                    Functions = myFunctions,
                    Model = _model,
                    MaxTokens = 50//optional
                });
                if (completionResult.Successful)
                {
                    Console.WriteLine("content= " + completionResult.Choices.First().Message.Content);
                    myFunctionName = completionResult.Choices.First().Message.FunctionCall.Name;
                    Console.WriteLine("function call name=" + completionResult.Choices.First().Message.FunctionCall.Name);
                    Console.WriteLine("function call args=" + completionResult.Choices.First().Message.FunctionCall.Arguments);

                    Dictionary<string, object> dic = completionResult.Choices.First().Message.FunctionCall.ParseArguments();
                    Console.WriteLine("Parsing good");

                    foreach (KeyValuePair<string, object> entry in dic)
                    {
                        Console.WriteLine("parm= " + entry.Key);// do something with entry.Value or entry.Key
                        Console.WriteLine("Value= " + entry.Value);
                        myParamName = entry.Key;
                        myParamValue = (string)entry.Value.ToString();
                    }
                    //Console.WriteLine(completionResult.Choices.First().Message.FunctionCall.ParseArguments());
                    myFunctionName = completionResult.Choices.First().Message.FunctionCall.Name;

                }
                else
                {
                    Console.WriteLine("ERROR");
                    Console.WriteLine(completionResult.Error);
                }
            }
            catch (Exception ex )
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            //--------------------------------------------------------

            string myresult = null;
            try
            {
                //var parameterTypes = new Type[] { typeof(string), typeof(string) };
                
                Type staticClassType = typeof(FunkyBiscuit);

                MethodInfo myStaticMethod = staticClassType.GetMethod(myFunctionName);

                //var myInstance = (UBI)Activator.CreateInstance(type);
//                object[] parameters = new object[] { myParamValue };

                object[] parameters = new object[] { myParamValue };

                //myresult = (string)myStaticMethod.Invoke(null, parameters);
                Task task = (Task) myStaticMethod.Invoke(null, parameters);
                task.Wait();
                myresult = ((dynamic)task).Result;
                Console.WriteLine("result= "+myresult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //var staticMethod = type.GetMethod("StaticMethod");
            //staticMethod.Invoke(null, null);

            Console.WriteLine("PASS2");
            Console.WriteLine("returning function output");
            //myMessages.Add(ChatMessage.FromFunction("MAUI", functionName));
            myMessages.Add(ChatMessage.FromFunction(myresult, functionName));

            //--------------------------------------------------------
            // Send function response

            var completionResult1 = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = myMessages,
                Functions = myFunctions,
                Model = _model,
                MaxTokens = 50//optional
            });
            if (completionResult1.Successful)
            {
                Console.WriteLine("content= "+completionResult1.Choices.First().Message.Content);
            }
            else
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(completionResult1.Error);
            }
            //--------------------------------------------------------

            Console.WriteLine("PASS3");

            Console.WriteLine("returning LLM response");

            var completionResult2 = await openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = myMessages,
                Functions = myFunctions,
                Model = _model,
                MaxTokens = 50//optional
            });
            if (completionResult2.Successful)
            {
                Console.WriteLine("content= "+completionResult2.Choices.First().Message.Content);

                //Console.WriteLine("name=" + completionResult2.Choices.First().Message.FunctionCall.Name);
                //Console.WriteLine(completionResult.Choices.First().Message.FunctionCall.ParseArguments());

            }
            else
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(completionResult2.Error);
            }




            return null;
        }

       



    }

  }
