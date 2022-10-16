using System.ComponentModel.DataAnnotations;

namespace RabbitMQ.Domain.Models;

public class QueueBind
{
    public QueueBind(string name, Exchange exchange, string routingKey)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Exchange = exchange ?? throw new ArgumentNullException(nameof(exchange));
        RoutingKey = routingKey ?? throw new ArgumentNullException(nameof(routingKey));
    }

    /// <summary>
    /// Kuyruk Adı
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Exchange
    /// </summary>
    [Required]
    public Exchange Exchange { get; set; }

    /// <summary>
    /// Exchange Tipi için routing-key 
    /// </summary>
    [StringLength(255)]
    [Required]
    public string RoutingKey { get; set; }
}