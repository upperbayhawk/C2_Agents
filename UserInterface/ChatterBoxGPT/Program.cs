using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Threading;

using Upperbay.Core.Logging;
using Upperbay.Core.Library;
using Upperbay.Worker.LMP;
using System.Net.NetworkInformation;


namespace ChatterBoxGPT
{

   class Program
    { 
       static async Task Main(string[] args)
       {
            int sleepMinutes = 0;

            MyAppConfig.SetMyAppConfig("ClusterAgent");
            //string traceMode = MyAppConfig.GetParameter("TraceMode");
            string traceMode = "trace";
            Log2.LogInit("ChatterBoxGPT", "ClusterAgent", traceMode);
            Log2.Info("DebugLevel = " + traceMode);


            if (args == null)
            {
                
            }
            else
            {
                // Step 2: print length, and loop over all arguments.
                Log2.Info("args length is " + args.Length);
                
                for (int i = 0; i < args.Length; i++)
                {
                    string argument = args[i];
                    Log2.Info("args index " + i);
                    Log2.Info(" is [" + argument + "]");
                    sleepMinutes = int.Parse(argument);
                }
            }

            DateTime originalDateTime = DateTime.Now;

            MQTTPipe.MqttInitializeAsync("192.168.0.131",
                                             "",
                                             "",
                                             1883);
            MQTTPipe.MqttSubscribeAsync();

            bool exit = false;
            string userInput = null;

           
           // Console.Clear();
            Console.WriteLine("Select PJM dataset to be fed to GPT");
            Console.WriteLine("1. PJMLoadForecastSevenDay");
            Console.WriteLine("2. PJMFiveMinLoadForecast");
            Console.WriteLine("3. PJMUnverifiedFiveMinLmp");
            Console.WriteLine("4. PJMHourLoadMetered");
            Console.WriteLine("5. PJMHourLoadPrelim");
            Console.WriteLine("6. PJMInstLoad");
            Console.WriteLine("7. PJMOperationsSummary");
            Console.WriteLine("8. Exit");

            Console.Write("Enter your choice (1-8): ");

            userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    // Code for Option 3
                    Console.WriteLine("PJMLoadForecastSevenDay selected.");
                    break;
                case "2":
                    // Code for Option 1
                    Console.WriteLine("PJMFiveMinLoadForecast selected.");
                    break;
                case "3":
                    // Code for Option 3
                    Console.WriteLine("PJMUnverifiedFiveMinLmp selected.");
                    break;
                case "4":
                    // Code for Option 3
                    Console.WriteLine("PJMHourLoadMetered selected.");
                    break;
                case "5":
                    // Code for Option 3
                    Console.WriteLine("PJMHourLoadPrelim selected.");
                    break;
                case "6":
                    // Code for Option 3
                    Console.WriteLine("PJMInstLoad selected.");
                    break;
                case "7":
                    // Code for Option 3
                    Console.WriteLine("PJMOperationsSummary selected.");
                    break;
                case "8":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice, bye, bye.");
                    exit = true;
                    break;
            }
           
            if (exit) return;

            while (true)
            {
                DateTime startTime = DateTime.Now;

                //PJMRealTimeLMP pjmRealTimeLMP = new PJMRealTimeLMP();
                //pjmRealTimeLMP.GetPJMRealTimeLMPHistory(".\\GptPromptDataReal.txt");

                //PJMRealTimeRTLoad pjmRealTimeRTLoad = new PJMRealTimeRTLoad();
                //pjmRealTimeRTLoad.GetPJMRealTimeLoadForecastHistory(".\\GptPromptDataReal.txt");
                ///////////////////////////////////////////

                Console.WriteLine("Getting Grid Data");


                if (userInput == "1")
                {

                    PJMLoadForecastSevenDay pJMLoadForecastSevenDay = new PJMLoadForecastSevenDay();
                    string json = pJMLoadForecastSevenDay.GetJson("RTO_COMBINED", 200);
                    pJMLoadForecastSevenDay.WriteJsonToFile(json, ".\\data\\PJMLoadForeCastSevenDay.json");
                    double dfirstVal = pJMLoadForecastSevenDay.GetFirstValue(json);
                    Console.WriteLine("First value: " + dfirstVal.ToString());
                    double dlastVal = pJMLoadForecastSevenDay.GetLastValue(json);
                    Console.WriteLine("Last value: " + dlastVal.ToString());
                    //pJMLoadForecastSevenDay.WriteJsonToCsv(json, ".\\data\\GptPromptDataCSV.txt");
                    pJMLoadForecastSevenDay.WriteCurrentDayLoadJsonToCsv(json, ".\\data\\PJMLoadForecastSevenDay.csv");

                    string promptText = File.ReadAllText(".\\prompts\\PromptPJMLoadForecastSevenDay.Txt");
                    string promptData = File.ReadAllText(".\\data\\PJMLoadForecastSevenDay.csv");
                    string prompt = promptText + " " + promptData;
                    Log2.Info(prompt);

                    TimeSeriesDataAnalyzer.Run(".\\data\\PJMLoadForecastSevenDay.csv");
                    LinearRegression.Run(".\\data\\PJMLoadForecastSevenDay.csv");

                    Console.WriteLine("Letting GPT analyze the Grid Data");

                    DateTime startGPTDateTime = DateTime.Now;
                    string customFormat = startGPTDateTime.ToString("yyyy-MM-dd HH:mm:ss");

                    Log2.Info("To Brain: " + customFormat + ": " + prompt);
                    Console.WriteLine("To Brain: " + customFormat + ": " + prompt);
                    Console.WriteLine("WAITING...");
                    MQTTPipe.PublishMessage(prompt);
                    string response = MQTTPipe.ReadMessage();
                    Log2.Info("From Brain: " + customFormat + ": " + response);
                    Console.WriteLine("From Brain: " + customFormat + ": " + response);
                }

                if (userInput == "2")
                {
                    PJMFiveMinLoadForecast pJMFiveMinLoadForecast = new PJMFiveMinLoadForecast();
                    string json = pJMFiveMinLoadForecast.GetJson("MID_ATLANTIC_REGION", 100);
                    pJMFiveMinLoadForecast.WriteJsonToFile(json, ".\\data\\PJMFiveMinLoadForecast.json");
                    double dVal = pJMFiveMinLoadForecast.GetLastValue(json);
                    Console.WriteLine("Last value: " + dVal.ToString());
                    pJMFiveMinLoadForecast.WriteCsvToFile(json, ".\\data\\PJMFiveMinLoadForcast.csv");
                }

                if (userInput == "3")
                {

                    PJMUnverifiedFiveMinLmp pJMUnverifiedFiveMinLmp = new PJMUnverifiedFiveMinLmp();
                    string json = pJMUnverifiedFiveMinLmp.GetJson("49955", 100);
                    pJMUnverifiedFiveMinLmp.WriteJsonToFile(json, ".\\data\\PJMUnverifiedFiveMinLmp.json");
                    double dVal = pJMUnverifiedFiveMinLmp.GetLastValue(json);
                    Console.WriteLine("Last value: " + dVal.ToString());
                    pJMUnverifiedFiveMinLmp.WriteCsvToFile(json, ".\\data\\PJMUnverifiesFiveMinLmp.csv");
                }

                if (userInput == "4")
                {

                    PJMHourLoadMetered pJMHourLoadMetered = new PJMHourLoadMetered();
                    string json = pJMHourLoadMetered.GetJson("DPLCO", 100);
                    pJMHourLoadMetered.WriteJsonToFile(json, ".\\data\\PJMHourLoadMetered.json");
                    double dVal = pJMHourLoadMetered.GetLastValue(json);
                    Console.WriteLine("Last value: " + dVal.ToString());
                    pJMHourLoadMetered.WriteCsvToFile(json, ".\\data\\PJMHourLoadMetered.csv");
                }

                if (userInput == "5")
                {

                    PJMHourLoadPrelim pJMHourLoadPrelim = new PJMHourLoadPrelim();
                    string json = pJMHourLoadPrelim.GetJson("DPLCO", 100);
                    pJMHourLoadPrelim.WriteJsonToFile(json, ".\\data\\PJMHourLoadPrelim.json");
                    double dVal = pJMHourLoadPrelim.GetLastValue(json);
                    Console.WriteLine("Last value: " + dVal.ToString());
                    pJMHourLoadPrelim.WriteCsvToFile(json, ".\\data\\PJMHourLoadPrelim.csv");
                }

                if (userInput == "6")
                {

                    PJMInstLoad pJMInstLoad = new PJMInstLoad();
                    string json = pJMInstLoad.GetJson("DPL", 100);
                    pJMInstLoad.WriteJsonToFile(json, ".\\data\\PJMInstLoad.json");
                    double dVal = pJMInstLoad.GetLastValue(json);
                    Console.WriteLine("Last value: " + dVal.ToString());
                    pJMInstLoad.WriteCsvToFile(json, ".\\data\\PJMInstLoad.csv");
                }

                if (userInput == "7")
                {

                    PJMOperationsSummary pJMOperationsSummary = new PJMOperationsSummary();
                    string json = pJMOperationsSummary.GetJson("MIDATL", 1);
                    pJMOperationsSummary.WriteJsonToFile(json, ".\\data\\PJMOperationsSummary.json");
                    double dVal = pJMOperationsSummary.GetLastValue(json);
                    Console.WriteLine("Last value: " + dVal.ToString());
                    //pJMOperationsSummary.WriteCsvToFile(json, ".\\data\\PJMOperationsSummary.csv");
                }

              
                ///////////////////////////////////////
                if (sleepMinutes == 0)
                {
                    break;
                }
                else if ((sleepMinutes < 0) || (sleepMinutes > 240))
                {
                    Log2.Error("SleepMinutes = " + sleepMinutes);
                    break;
                }
                else
                {
                    Log2.Info("Sleepytime Minutes = " + sleepMinutes);
                    int DesiredFrequency = sleepMinutes * 60 * 1000;
                    DateTime endTime = DateTime.Now;
                    TimeSpan executionTime = endTime - startTime;
                    
                    int timeToSleep = Math.Max(0, DesiredFrequency - (int)executionTime.TotalMilliseconds);
                    DateTime wakeTime = DateTime.Now.AddMilliseconds(timeToSleep);

                    Console.WriteLine("Cycling every " + sleepMinutes + " Mins");
                    Console.WriteLine("Sleeping for " + timeToSleep + " Mils. Will Wake Up at " + wakeTime.ToShortTimeString());
                    Thread.Sleep(timeToSleep);

                    Console.WriteLine("Awake");
                }
               
            }
           
       }
   }
}
