using System;
using System.Net.Http;
using System.Threading.Tasks;
using Common;
using LoadBalancer.LoadBalancer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LoadBalancer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoadBalancerController : ControllerBase
    {

        public LoadBalancerController()
        {
            LoadBalancer.LoadBalancer.GetInstance().SetActiveStrategy(new RoundRobinStrategy());
        }
        
        [HttpPost]
        public int AddService([FromBody] ApiProp apiProp)
        {
            Console.WriteLine("Adding service at url " + apiProp.Url);
            return LoadBalancer.LoadBalancer.GetInstance().AddService(apiProp.Url);
        }

        [HttpGet]
        public async Task<SearchResult> Search(string terms,int numberOfResults)
        {
            HttpClient api = new HttpClient();
            
            api.BaseAddress = new Uri(LoadBalancer.LoadBalancer.GetInstance().NextService() ?? throw new InvalidOperationException());
            Console.WriteLine("Chose service at url: "+api.BaseAddress);
            Task<string> task = api.GetStringAsync("/Search?terms=" + terms + "&numberOfResults=" + numberOfResults);
            task.Wait();

            string resultString = task.Result;
            SearchResult? result = JsonConvert.DeserializeObject<SearchResult>(resultString);
            return result;
        }
    }
}