using RabbitMQ.Client;
using RabbitMQ.Helper.Abstraction;

namespace RabbitMQ.Helper.Implementation;

public class Consumer : IConsumer
{
    public readonly IConnectionFactory ConnectionFactory;
    public readonly IConnection Connection;
    public readonly IModel Channel;

    public Consumer(string rabbitUri)
    {
        ConnectionFactory = CreateOrReplaceFactory(rabbitUri);
        Connection = GetConnection();
        Channel = CreateOrReplaceChannel();
    }

    public IConnectionFactory CreateOrReplaceFactory(string uri)
    {
        return new ConnectionFactory()
            {
                Uri = new Uri(uri)
            };
    }

    public IConnection GetConnection()
    {
        return ConnectionFactory.CreateConnection();
    }

    public IModel CreateOrReplaceChannel()
    {
        return Connection.CreateModel();
    }
}