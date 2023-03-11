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
        public int AddService([FromBody] ApiProp apiProp)
        {
            Console.WriteLine("Adding service at url " + apiProp.Url);
            return LoadBalancer.LoadBalancer.GetInstance().AddService(apiProp.Url);
        }

    }
}