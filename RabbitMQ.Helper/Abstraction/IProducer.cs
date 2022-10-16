using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Domain.Models;

namespace RabbitMQ.Helper.Abstraction
{
    public interface IProducer
    {
        //void SendMessage<T>(T message);
        void CreateQueue(Queue queue);

        void CreateExchange(Exchange exchange);

        void BindQueue(QueueBind queueBind);

        void PublishMessage(Message message);
    }
}
