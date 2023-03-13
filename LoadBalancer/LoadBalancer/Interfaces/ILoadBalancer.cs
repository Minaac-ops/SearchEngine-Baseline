using System.Collections.Generic;
using LoadBalancer.LoadBalancer.Models;

namespace LoadBalancer.LoadBalancer
{
    public interface ILoadBalancer
    {
        public List<Service> GetAllServices();
        public int AddService(string? url);
        public int RemoveService(int id);
        public ILoadBalancerStrategy GetActiveStrategy();
        public void SetActiveStrategy(ILoadBalancerStrategy strategy);
        public string NextService();
        public void CloseConnection(string url);
    }
}