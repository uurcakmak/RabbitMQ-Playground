namespace RabbitMQ.Domain.Models;

public class Queue
{
    public string Name { get; set; }

    public bool Durability { get; set; } = false;

    public bool Exclusivity { get; set; } = false;

    public bool AutoDelete { get; set; } = false;
}