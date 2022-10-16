using RabbitMQ.Domain.Enums;

namespace RabbitMQ.Domain.Models;

public class Message
{
    public Message(Exchange exchange, string routingKey, object content)
    {
        Exchange = exchange;
        RoutingKey = routingKey;
        Content = content;
    }

    public Message(Exchange exchange, string routingKey, Dictionary<string, string>? headers, object content)
    {
        Exchange = exchange;
        RoutingKey = routingKey;
        Headers = headers;
        Content = content;
    }

    public Exchange Exchange { get; set; }

    public string RoutingKey { get; set; }

    private Dictionary<string, string>? _headers;

    public Dictionary<string, string>? Headers
    {
        get => Exchange.Type == ExchangeTypes.Headers ? this._headers : null;
        set => _headers = Exchange.Type == ExchangeTypes.Headers ? value : null;
    }

    public object Content { get; set; }
}