using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RabbitMQ.Domain.Enums;

namespace RabbitMQ.Domain.Models
{
    public class Exchange
    {
        //[JsonConverter(typeof(StringEnumConverter))]
        public string Type { get; set; }

        public string Name { get; set; }

        public bool Durable { get; set; } = false;

        //[JsonProperty("auto_delete")]
        public bool AutoDelete { get; set; } = false;
    }
}
