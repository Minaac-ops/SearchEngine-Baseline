using System.Collections.Generic;
using LoadBalancer.LoadBalancer.Models;

namespace LoadBalancer.LoadBalancer
{
    public interface ILoadBalancerStrategy
    {
        public string NextService(List<Service> services);
        public void CloseConnection(string url);
    }
}