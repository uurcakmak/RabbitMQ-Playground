using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using RabbitMQ.Helper.Abstraction;

namespace RabbitMQ.Consumer;

internal class Program
{
    private static IConnectionFactory? _connectionFactory;
    private static IConnection? _connection;
    private static IModel? _channel;
    private readonly IRabbitApi _api;

    static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfiguration config = builder.Build();
        var connectionString = config["RabbitMQ:ConnectionString"];

        Console.WriteLine("Mesajları listelemek istediğiniz RabbitMQ kuyruğunun adını giriniz:");
        var queueName = Console.ReadLine();

        try
        {
            _connectionFactory = new ConnectionFactory()
            {
                Uri = new Uri(connectionString)
            };

            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();


            var consumerEvent = new EventingBasicConsumer(_channel);
            consumerEvent.Received += (sender, eventArgs) =>
            {
                var byteArr = eventArgs.Body.ToArray();
                var msgStr = Encoding.UTF8.GetString(byteArr);

                Console.WriteLine($"Alınan mesaj: {msgStr}");
            };

            _channel.BasicConsume(queueName, true, consumerEvent);

        }
        catch (OperationInterruptedException e)
        {
            Console.WriteLine(e);
        }

        Console.ReadKey();
    }
}