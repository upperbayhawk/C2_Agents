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
using Newtonsoft.Json.Linq;
using System.Security.Claims;


namespace ChatterBoxGPT
{

   class Program
    { 
       static async Task Main(string[] args)
       {
            MyAppConfig.SetMyAppConfig("ClusterAgent");
            //string traceMode = MyAppConfig.GetParameter("TraceMode");
            string traceMode = "trace";
            Log2.LogInit("ChatterBoxGPT", "ClusterAgent", traceMode);
            Log2.Info("DebugLevel = " + traceMode);

            // --------------------------------------
            int sleepMinutes = 0;
            string timeString = null;

            // Check if there are any command-line arguments
            if (args.Length == 0)
            {
     
            }

            // Loop through each command-line argument
            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];

                // You can perform different actions based on the argument value
                if (arg.StartsWith("--"))
                {
                    // Handle long options (e.g., --help, --version)
                    if (arg == "--help")
                    {
                        Console.WriteLine("Displaying help information: TBD");
                        // Add your help logic here
                        return;
                    }
                    else if (arg == "--version")
                    {
                        Console.WriteLine("Version 0.6.9");
                        return;
                        // Add your version logic here
                    }
                    // Add more options as needed
                    else
                    {
                        Console.WriteLine($"Unknown option: {arg}");
                    }
                }
                else if (arg.StartsWith("-"))
                {
                    // Handle short options (e.g., -f, -v)
                    // Add your short option logic here
                    if (arg == "-m")
                    {
                        sleepMinutes = int.Parse(args[i + 1]);
                        Console.WriteLine("SleepMinutes = " + sleepMinutes);
                    }
                    else if (arg == "-h")
                    {
                        int sleepHours = int.Parse(args[i + 1]);
                        sleepMinutes = sleepHours * 60;
                        Console.WriteLine("SleepMinutes = " + sleepMinutes);
                    }
                    else if (arg == "-t")
                    {
                        timeString = args[i + 1];
                        string[] timeComponents = timeString.Split(':');
                        if (timeComponents.Length != 2 || !int.TryParse(timeComponents[0], out int hour) || !int.TryParse(timeComponents[1], out int minute))
                        {
                            Console.WriteLine("Invalid time string format: " + timeString);
                            return;
                        }
                        Console.WriteLine("WakeTime = " + timeString);
                    }
                    // Add more options as needed
                    else
                    {
                        Console.WriteLine($"Unknown option: {arg}");
                    }
                }
                else
                {
                    // Handle positional arguments (non-option arguments)
                    Console.WriteLine($"Positional argument {i + 1}: {arg}");
                    // Add your positional argument logic here
                }
            }

            //---------------------------------------
            DateTime anchorDateTime = DateTime.Now;

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
            Console.WriteLine("2. PJMDayAheadHourlyLmp");
            Console.WriteLine("3. PJMFiveMinLoadForecast");
            Console.WriteLine("4. PJMUnverifiedFiveMinLmp");
            Console.WriteLine("5. PJMHourLoadMetered");
            Console.WriteLine("6. PJMHourLoadPrelim");
            Console.WriteLine("7. PJMInstLoad");
            Console.WriteLine("8. PJMOperationsSummary");
            Console.WriteLine("9. Exit");

            Console.Write("Enter your choice (1-9): ");

            userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    // Code for Option 3
                    Console.WriteLine("PJMLoadForecastSevenDay selected.");
                    break;
                case "2":
                    // Code for Option 1
                    Console.WriteLine("PJMDayAheadHourlyLMP selected.");
                    break;
                case "3":
                    // Code for Option 1
                    Console.WriteLine("PJMFiveMinLoadForecast selected.");
                    break;
                case "4":
                    // Code for Option 3
                    Console.WriteLine("PJMUnverifiedFiveMinLmp selected.");
                    break;
                case "5":
                    // Code for Option 3
                    Console.WriteLine("PJMHourLoadMetered selected.");
                    break;
                case "6":
                    // Code for Option 3
                    Console.WriteLine("PJMHourLoadPrelim selected.");
                    break;
                case "7":
                    // Code for Option 3
                    Console.WriteLine("PJMInstLoad selected.");
                    break;
                case "8":
                    // Code for Option 3
                    Console.WriteLine("PJMOperationsSummary selected.");
                    break;
                case "9":
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

                Console.WriteLine("Getting Grid Data");


                if (userInput == "1")
                {
                    //PJMLoadForecastSevenDay

                    PJMLoadForecastSevenDay pJMLoadForecastSevenDay = new PJMLoadForecastSevenDay();
                    string json = pJMLoadForecastSevenDay.GetJson("RTO_COMBINED", 200);
                    if (json != null)
                    {
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
                }

                if (userInput == "2")
                {
                    //PJMDayAheadHourlyLMP

                    PJMDayAheadHourlyLMP pJMDayAheadHourlyLMP = new PJMDayAheadHourlyLMP();
                    string json = pJMDayAheadHourlyLMP.GetJson("1", 24);
                    if (json != null)
                    {
                        pJMDayAheadHourlyLMP.WriteJsonToFile(json, ".\\data\\PJMDayAheadHourlyLMP.json");
                        //double dlastVal = pJMGayAheadHourlyLmp.GetLastValue(json);
                        //Console.WriteLine("Last value: " + dlastVal.ToString());
                        pJMDayAheadHourlyLMP.WriteCurrentDayAheadHourlyLMPToCsv(json, ".\\data\\PJMDayAheadHourlyLMP.csv");

                        string promptText = File.ReadAllText(".\\prompts\\PromptPJMDayAheadHourlyLMP.Txt");
                        string promptData = File.ReadAllText(".\\data\\PJMDayAheadHourlyLMP.csv");
                        string prompt = promptText + " " + promptData;
                        Log2.Info(prompt);

                        TimeSeriesDataAnalyzer.Run(".\\data\\PJMDayAheadHourlyLMP.csv");
                        LinearRegression.Run(".\\data\\PJMDayAheadHourlyLMP.csv");

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
                }


                if (userInput == "3")
                {
                    PJMFiveMinLoadForecast pJMFiveMinLoadForecast = new PJMFiveMinLoadForecast();
                    string json = pJMFiveMinLoadForecast.GetJson("MID_ATLANTIC_REGION", 100);
                    pJMFiveMinLoadForecast.WriteJsonToFile(json, ".\\data\\PJMFiveMinLoadForecast.json");
                    double dlastVal = pJMFiveMinLoadForecast.GetLastValue(json);
                    Console.WriteLine("Last value: " + dlastVal.ToString());
                    //pJMFiveMinLoadForecast.WriteCsvToFile(json, ".\\data\\PJMFiveMinLoadForcast.csv");
                }

                if (userInput == "4")
                {

                    PJMUnverifiedFiveMinLmp pJMUnverifiedFiveMinLmp = new PJMUnverifiedFiveMinLmp();
                    string json = pJMUnverifiedFiveMinLmp.GetJson("49955", 100);
                    pJMUnverifiedFiveMinLmp.WriteJsonToFile(json, ".\\data\\PJMUnverifiedFiveMinLmp.json");
                    double dlastVal = pJMUnverifiedFiveMinLmp.GetLastValue(json);
                    Console.WriteLine("Last value: " + dlastVal.ToString());
                    //pJMUnverifiedFiveMinLmp.WriteCsvToFile(json, ".\\data\\PJMUnverifiesFiveMinLmp.csv");
                }

                if (userInput == "5")
                {

                    PJMHourLoadMetered pJMHourLoadMetered = new PJMHourLoadMetered();
                    string json = pJMHourLoadMetered.GetJson("DPLCO", 100);
                    pJMHourLoadMetered.WriteJsonToFile(json, ".\\data\\PJMHourLoadMetered.json");
                    double dlastVal = pJMHourLoadMetered.GetLastValue(json);
                    Console.WriteLine("Last value: " + dlastVal.ToString());
                    //pJMHourLoadMetered.WriteCsvToFile(json, ".\\data\\PJMHourLoadMetered.csv");
                }

                if (userInput == "6")
                {

                    PJMHourLoadPrelim pJMHourLoadPrelim = new PJMHourLoadPrelim();
                    string json = pJMHourLoadPrelim.GetJson("DPLCO", 100);
                    pJMHourLoadPrelim.WriteJsonToFile(json, ".\\data\\PJMHourLoadPrelim.json");
                    double dlastVal = pJMHourLoadPrelim.GetLastValue(json);
                    Console.WriteLine("Last value: " + dlastVal.ToString());
                    //pJMHourLoadPrelim.WriteCsvToFile(json, ".\\data\\PJMHourLoadPrelim.csv");
                }

                if (userInput == "7")
                {

                    PJMInstLoad pJMInstLoad = new PJMInstLoad();
                    string json = pJMInstLoad.GetJson("DPL", 100);
                    pJMInstLoad.WriteJsonToFile(json, ".\\data\\PJMInstLoad.json");
                    double dlastVal = pJMInstLoad.GetLastValue(json);
                    Console.WriteLine("Last value: " + dlastVal.ToString());
                    //pJMInstLoad.WriteCsvToFile(json, ".\\data\\PJMInstLoad.csv");
                }

                if (userInput == "8")
                {

                    PJMOperationsSummary pJMOperationsSummary = new PJMOperationsSummary();
                    string json = pJMOperationsSummary.GetJson("MIDATL", 1);
                    pJMOperationsSummary.WriteJsonToFile(json, ".\\data\\PJMOperationsSummary.json");
                    double dlastVal = pJMOperationsSummary.GetLastValue(json);
                    Console.WriteLine("Last value: " + dlastVal.ToString());
                    //pJMOperationsSummary.WriteCsvToFile(json, ".\\data\\PJMOperationsSummary.csv");
                }

              
                ///////////////////////////////////////
                if ((sleepMinutes == 0) && (timeString == null))
                {
                    break;
                }
                else if (timeString != null)
                {

                    DateTime tomorrow = DateTime.Today.AddDays(1);
                    string[] timeComponents = timeString.Split(':');
                    if (timeComponents.Length != 2 || !int.TryParse(timeComponents[0], out int hour) || !int.TryParse(timeComponents[1], out int minute))
                    {
                        Console.WriteLine("Invalid time string format.");
                        return;
                    }
                    DateTime tomorrowAtTime = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, hour, minute, 0);

                    TimeSpan timeSpan = tomorrowAtTime - anchorDateTime;
                    sleepMinutes = (int)Math.Ceiling(timeSpan.TotalMinutes);
                    Log2.Info("SleepMinutes = " + sleepMinutes);
                    Console.WriteLine("Cycling every " + sleepMinutes + " Mins");

                    int timeToSleep = sleepMinutes * 60000;
                    DateTime alarm = DateTime.Now.AddMilliseconds(timeToSleep);
                    Console.WriteLine("Sleeping for " + timeToSleep.ToString() + " Mils. Will Wake Up at " + alarm.ToShortTimeString());
                    Thread.Sleep(timeToSleep);
                    Console.WriteLine("Awake");
                }
                else
                {
                    Log2.Info("Sleepytime Minutes = " + sleepMinutes);
                    int DesiredFrequency = sleepMinutes * 60 * 1000;
                    DateTime endTime = DateTime.Now;
                    TimeSpan executionTime = endTime - startTime;
                    
                    int timeToSleep = Math.Max(0, DesiredFrequency - (int)executionTime.TotalMilliseconds);
                    DateTime alarm = DateTime.Now.AddMilliseconds(timeToSleep);

                    Console.WriteLine("Cycling every " + sleepMinutes + " Mins");
                    Console.WriteLine("Sleeping for " + timeToSleep + " Mils. Will Wake Up at " + alarm.ToShortTimeString());
                    Thread.Sleep(timeToSleep);

                    Console.WriteLine("Awake");
                }
               
            }
           
       }
   }
}
