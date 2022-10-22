using RabbitMQ.Domain.Models;
// ReSharper disable InconsistentNaming
#pragma warning disable CS8618

namespace RabbitMQ.Domain.ViewModel
{
    public class HomeVM
    {
        public List<Queue> Queues { get; set; }

        public List<Exchange> Exchanges { get; set; }
    }
}