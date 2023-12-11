using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;


namespace ChatterBox
{

        class Program
        {
            static async Task Main(string[] args)
            {
                //Console.WriteLine("Enter a string of sentences:");
                //string input = Console.ReadLine();

                //string input = "Why is the sky blue?";

                string promptText = File.ReadAllText(".\\GptPromptText.Txt");
                //File.WriteAllText(path, string);
                string promptData = File.ReadAllText(".\\GptPromptData.Txt");
                string prompt = promptText + " " + promptData;

                var chatGptService = new ChatGptService();
                string response = await chatGptService.SendMessageToChatGptAsync(prompt);

                Console.WriteLine($"Response from ChatGPT: {response}");
                //Console.ReadKey();
            }
        }
}
