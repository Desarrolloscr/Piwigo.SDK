using Dcsoftcr.Piwigo.SDK;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dcsoftcr.Piwigo.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var settings = ConfigurationManager.AppSettings;
            HttpClient client = new HttpClient { BaseAddress = new Uri(settings["URL"]) }; 
            var session = new Session(client);
            var status = await session.GetStatus();
            Console.WriteLine(status);
            var ress = await session.LogIn(settings["Username"], settings["Password"]);
            Console.WriteLine(ress);
            Console.ReadLine();
            var status2 = await session.GetStatus();
            Console.WriteLine(status2);
            Console.ReadLine();
        }
    }
}
