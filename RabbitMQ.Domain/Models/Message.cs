using RabbitMQ.Domain.Enums;

namespace RabbitMQ.Domain.Models;

public class Message
{
    public Message(string exchange, string routingKey, string content)
    {
        Exchange = exchange;
        RoutingKey = routingKey;
        Content = content;
    }

    public Message(string exchange, string routingKey, Dictionary<string, string>? headers, string content)
    {
        Exchange = exchange;
        RoutingKey = routingKey;
        Headers = headers;
        Content = content;
    }

    public Message()
    {
    }

    public string Exchange { get; set; }

    public string RoutingKey { get; set; }

    private Dictionary<string, string>? _headers;

    public Dictionary<string, string>? Headers
    {
        get => Exchange == "headers" ? this._headers : null;
        set => _headers = Exchange == "headers" ? value : null;
    }

    public string Content { get; set; }
}