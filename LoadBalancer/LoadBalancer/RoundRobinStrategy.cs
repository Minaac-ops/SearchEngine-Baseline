using System;
using System.Collections.Generic;
using System.Linq;
using LoadBalancer.LoadBalancer.Models;

namespace LoadBalancer.LoadBalancer
{
    public class RoundRobinStrategy : ILoadBalancerStrategy
    {
        private static int _requestNo;
        
        public string NextService(List<Service> services)
        {
            if (_requestNo == services.Count) _requestNo = 0;
            var nextService = services[_requestNo];
            _requestNo++;
            return nextService.Url;
        }

        public void CloseConnection(string url)
        {
            return;
        }
    }
}