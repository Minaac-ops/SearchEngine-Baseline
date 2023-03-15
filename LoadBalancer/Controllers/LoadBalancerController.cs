using System;
using System.Net.Http;
using System.Threading.Tasks;
using Common;
using LoadBalancer.LoadBalancer;
using LoadBalancer.LoadBalancer.Models;
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
        }
        
        [HttpPost]
        public int AddService([FromBody] Service apiProp)
        {
            Console.WriteLine("Adding service at url " + apiProp.Url + '(' + DateTime.Now+')');
            return LoadBalancer.LoadBalancer.GetInstance().AddService(apiProp.Url);
        }

        [HttpGet]
        public async Task<SearchResult> Search(string terms,int numberOfResults)
        {
            HttpClient api = new HttpClient();
            
            api.BaseAddress = new Uri(LoadBalancer.LoadBalancer.GetInstance().NextService() ?? throw new InvalidOperationException());
            Task<string> task = api.GetStringAsync("/Search?terms=" + terms + "&numberOfResults=" + numberOfResults);
            task.Wait();

            string resultString = task.Result;
            string toClose = api.BaseAddress.ToString();
            string readyToClose = api.BaseAddress.ToString().Remove(toClose.Length - 1, 1);
            LoadBalancer.LoadBalancer.GetInstance().CloseConnection(readyToClose);
            SearchResult? result = JsonConvert.DeserializeObject<SearchResult>(resultString);
            return result;
        }
    }
}