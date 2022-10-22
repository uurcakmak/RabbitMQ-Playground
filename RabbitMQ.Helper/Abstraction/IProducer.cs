using RabbitMQ.Domain.Models;

namespace RabbitMQ.Helper.Abstraction
{
    public interface IProducer
    {
        string CreateQueue(Queue queue);

        string CreateExchange(Exchange exchange);

        string BindQueue(QueueBind queueBind);

        string PublishMessage(Message message);
    }
}
