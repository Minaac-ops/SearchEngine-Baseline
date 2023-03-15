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
            var minConnection = services.MinBy(s => s.Connections) ?? services.First();

            minConnection.Connections++;
            Console.WriteLine("url: "+minConnection.Url + ", connections: "+ minConnection.Connections);
            return minConnection.Url;
        }

        public void CloseConnection(string url)
        {
            foreach (var service in _services)
            {
                if (service.Url == url)
                {
                    var releaseValue = service.Connections - 1;
                    service.Connections = releaseValue;
                    Console.WriteLine("Url " + url + " has " + service.Connections + " after release.");
                }
            }
        }
    }
}