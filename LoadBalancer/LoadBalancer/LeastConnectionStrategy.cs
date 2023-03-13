using System;
using System.Collections.Generic;
using System.Linq;
using LoadBalancer.LoadBalancer.Models;

namespace LoadBalancer.LoadBalancer
{
    public class LeastConnectionsStrategy : ILoadBalancerStrategy
    {
        private List<Service> _services = new List<Service>();
    
        public string NextService(List<Service> services)
        {
            _services = services;
            var minConnection = services.MinBy(s => s.Connections);

            if (minConnection == null) return services.First().Url;
            
            minConnection.Connections = int.MaxValue;
            return minConnection.Url;
        }

        public void CloseConnection(string url)
        {
            var serviceToRelease = _services.FirstOrDefault(service => service.Url == url);
            if (serviceToRelease == null) return;
            var releaseValue = serviceToRelease.Connections - int.MaxValue;
            serviceToRelease.Connections = releaseValue;
            serviceToRelease.Connections++;
        }
    }
}