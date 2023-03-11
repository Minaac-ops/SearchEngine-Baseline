using System.Collections.Generic;
using System.Linq;

namespace LoadBalancer.LoadBalancer
{
    public class RoundRobinStrategy : ILoadBalancerStrategy
    {
        public string NextService(List<string> services)
        {
            return services.First();
        }
    }
}