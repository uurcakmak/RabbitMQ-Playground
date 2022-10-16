using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace RabbitMQ.Domain.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ExchangeTypes
    {
        Direct,
        Topic,
        FanOut,
        Headers
    }
}
