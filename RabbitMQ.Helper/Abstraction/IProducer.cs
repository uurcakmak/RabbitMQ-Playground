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
        string CreateQueue(Queue queue);

        string CreateExchange(Exchange exchange);

        string BindQueue(QueueBind queueBind);

        string PublishMessage(Message message);
    }
}
