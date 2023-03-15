using System;
using System.Collections.Generic;
using System.Linq;
using LoadBalancer.LoadBalancer.Models;

namespace LoadBalancer.LoadBalancer
{
    public class LeastConnectionsStrategy : ILoadBalancerStrategy
    {
        private static List<Service> _services = new List<Service>();

        public LeastConnectionsStrategy(List<Service> services)
        {
            _services = services;
        }
    
        public string NextService(List<Service> services)
        {
            var minConnection = services.MinBy(s => s.Connections) ?? services.First();

            minConnection.Connections++;
            Console.WriteLine("Chose url: "+minConnection.Url + ", with connections: "+ minConnection.Connections + " at " + DateTime.Now);
            return minConnection.Url;
        }

        public void CloseConnection(string url)
        {
            var serviceToRelease = _services.FirstOrDefault(s => s.Url == url);
            serviceToRelease.Connections--;
            Console.WriteLine("Released url: " + serviceToRelease.Url + ", with connections after release: "
                              + serviceToRelease.Connections + " at " +DateTime.Now);
        }
    }
}