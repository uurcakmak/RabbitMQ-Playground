using Newtonsoft.Json;

namespace RabbitMQ.Domain.Models;

public class Queue
{
    public string Name { get; set; }

    public bool Durable { get; set; } = false;

    public bool Exclusive { get; set; } = false;

    [JsonProperty("auto_delete")]
    public bool AutoDelete { get; set; } = false;
}