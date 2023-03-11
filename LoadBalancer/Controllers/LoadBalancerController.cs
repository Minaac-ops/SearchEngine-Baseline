using System;
using LoadBalancer.LoadBalancer;
using Microsoft.AspNetCore.Mvc;

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
        public int AddService([FromBody] string? url)
        {
            Console.WriteLine("Adding service at url " + url);
            return LoadBalancer.LoadBalancer.GetInstance().AddService(url);
        }

    }
}