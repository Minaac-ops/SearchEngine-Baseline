using System;
using System.Net.Http;
using System.Threading.Tasks;
using Common;
using Newtonsoft.Json;
using RestSharp;

namespace ConsoleSearch
{
    public class App
    {
        public void Run()
        {
            HttpClient api = new HttpClient();
            //SearchLogic mSearchLogic = new SearchLogic(new Database());
            Console.WriteLine("Console Search");
            
            while (true)
            {
                Console.WriteLine("enter search terms - q for quit");
                string input = Console.ReadLine() ?? string.Empty;
                if (input.Equals("q")) break;

                Task<string> task = api.GetStringAsync("http://load-balancer/LoadBalancer?terms=" + input + "&numberOfResults=10");
                Console.WriteLine("her til");
                task.Wait();
                Console.WriteLine("her til også");
                string resultString = task.Result;
                SearchResult result = JsonConvert.DeserializeObject<SearchResult>(resultString);

                foreach (var ignored in result.IgnoredTerms)
                {
                    Console.WriteLine(ignored + " was ignored.");
                }

                foreach (var resultDocument in result.Documents)
                {
                    Console.WriteLine(resultDocument.Id + ": " + resultDocument.Path + " - number of terms found: " + resultDocument.NumberOfOccurences);
                }
                
                Console.WriteLine("Found " + result.Documents.Count + " in " + result.ElapsedMilliseconds + " ms");
            }
        }
    }
}
