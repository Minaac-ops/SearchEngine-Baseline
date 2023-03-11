using System.Collections.Generic;

namespace LoadBalancer.LoadBalancer
{
    public class LoadBalancer : ILoadBalancer
    {
        private ILoadBalancerStrategy? _strategy;
        private readonly List<string> urls = new();
        private static LoadBalancer? _instance;

        private LoadBalancer()
        { }

        public static LoadBalancer GetInstance()
        {
            return _instance ??= new LoadBalancer();
        }
    
        public List<string?> GetAllServices()
        {
            return urls;
        }

        public int AddService(string? url)
        {
            urls.Add(url);
            return urls.Count - 1;
        }

        public int RemoveService(int id)
        {
            urls.RemoveAt(id);
            return id;
        }

        public ILoadBalancerStrategy? GetActiveStrategy()
        {
            return _strategy;
        }

        public void SetActiveStrategy(ILoadBalancerStrategy? strategy)
        {
            this._strategy = strategy;
        }

        public string? NextService()
        {
            return _strategy?.NextService(GetAllServices());
        }
    }
}