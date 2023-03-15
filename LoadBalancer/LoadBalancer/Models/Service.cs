namespace LoadBalancer.LoadBalancer.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int Connections { get; set; }
        public int Weight { get; set; }
    }
}