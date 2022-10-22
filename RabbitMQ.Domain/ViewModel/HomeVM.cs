using RabbitMQ.Domain.Models;

namespace RabbitMQ.Domain.ViewModel
{
    public class HomeVM
    {
        public List<Queue> Queues { get; set; }

        public List<Exchange> Exchanges { get; set; }
    }
}
