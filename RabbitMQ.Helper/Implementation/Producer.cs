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

    public string CreateQueue(Queue queue)
    {
        string result = string.Empty;
        try
        {
            _channel.QueueDeclare(queue.Name, queue.Durable, queue.Exclusive, queue.AutoDelete);
        }
        catch (Exception e)
        {
            result = e.Message;
        }

        return result;
    }

    public string CreateExchange(Exchange exchange)
    {
        string result = string.Empty;
        try
        {
            _channel.ExchangeDeclare(exchange.Name, exchange.Type, exchange.Durable, exchange.AutoDelete);
        }
        catch (Exception e)
        {
            result = e.Message;
        }

        return result;
    }

    public string BindQueue(QueueBind queueBind)
    {
        string result = string.Empty;
        try
        {
            _channel.QueueBind(queueBind.Name, queueBind.Exchange, queueBind.RoutingKey);
        }
        catch (Exception e)
        {
            result = e.Message;
        }

        return result;
    }

    public string PublishMessage(Message message)
    {
        string result = string.Empty;
        
        try
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message.Content));

            _channel.BasicPublish(message.Exchange, message.RoutingKey, true, null, body);
        }
        catch (Exception e)
        {
            result = e.Message;
        }

        return result;
    }
}