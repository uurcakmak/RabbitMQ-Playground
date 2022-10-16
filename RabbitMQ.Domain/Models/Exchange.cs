using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;
using RabbitMQ.Domain.Enums;

namespace RabbitMQ.Domain.Models
{
    public class Exchange
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ExchangeTypes Type { get; set; }

        public string Name { get; set; }

        public bool Durability { get; set; } = false;

        public bool AutoDelete { get; set; } = false;

    }
}
