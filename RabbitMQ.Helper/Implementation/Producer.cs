using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Domain.Models;
using RabbitMQ.Helper.Abstraction;
using Newtonsoft.Json;

namespace RabbitMQ.Helper.Implementation;

public class Producer : IProducer
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public Producer(string rabbitUri)
    {
        _connectionFactory = CreateOrReplaceFactory(rabbitUri);
        _connection = GetConnection();
        _channel = CreateOrReplaceChannel();
    }

    private IConnectionFactory CreateOrReplaceFactory(string uri)
    {
        return new ConnectionFactory()
            {
                Uri = new Uri(uri)
            };
    }

    private IConnection GetConnection()
    {
        return _connectionFactory.CreateConnection();
    }

    private IModel CreateOrReplaceChannel()
    {
        return _connection.CreateModel();
    }

    public void CreateQueue(Queue queue)
    {
        _channel.QueueDeclare(queue.Name, queue.Durability, queue.Exclusivity, queue.AutoDelete);
    }

    public void CreateExchange(Exchange exchange)
    {
        _channel.ExchangeDeclare(exchange.Name, JsonConvert.SerializeObject(exchange.Type), exchange.Durability, exchange.AutoDelete);
    }

    public void BindQueue(QueueBind queueBind)
    {
        _channel.QueueBind(queueBind.Name, queueBind.Exchange.Name, queueBind.RoutingKey);
    }

    public void PublishMessage(Message message)
    {
        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message.Content));
        
        _channel.BasicPublish(message.Exchange.Name, message.RoutingKey, true, null, body);
    }
}