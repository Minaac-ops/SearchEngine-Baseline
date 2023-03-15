using System;
using System.Collections.Generic;
using System.Linq;
using LoadBalancer.LoadBalancer.Models;

namespace LoadBalancer.LoadBalancer
{
    public class LoadBalancer : ILoadBalancer
    {
        private ILoadBalancerStrategy _strategy;
        private static readonly List<Service> urls = new();
        private static LoadBalancer? _instance;

        private LoadBalancer()
        { SetActiveStrategy(new LeastConnectionsStrategy(GetAllServices()));}

        public static LoadBalancer GetInstance()
        {
            return _instance ??= new LoadBalancer();
        }
    
        public List<Service?> GetAllServices()
        {
            return urls;
        }

        public int AddService(string? url)
        {
            var service = new Service
            {
                Id = urls.Count - 1,
                Connections = 0,
                Url = url
            };
            urls.Add(service);
            return (int) service.Id;
        }

        public int RemoveService(int id)
        {
            var serviceToRemove = urls.FirstOrDefault(item => item.Id == id);
            urls.Remove(serviceToRemove);
            return (int) serviceToRemove.Id;
        }

        public ILoadBalancerStrategy? GetActiveStrategy()
        {
            return _strategy;
        }

        public void SetActiveStrategy(ILoadBalancerStrategy? strategy)
        {
            _strategy = strategy;
        }

        public string? NextService()
        {
            return _strategy.NextService(urls);
        }

        public void CloseConnection(string url)
        {
            _strategy.CloseConnection(url);
        }
    }
}