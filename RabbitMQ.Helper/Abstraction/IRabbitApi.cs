using RabbitMQ.Domain.Models;

namespace RabbitMQ.Helper.Abstraction;

public interface IRabbitApi
{
    Task<List<Queue>> ListQueues();

    Task<List<Exchange>> ListExchanges();
}