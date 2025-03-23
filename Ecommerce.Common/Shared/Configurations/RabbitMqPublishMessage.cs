using System.Collections.Generic;
using Ecommerce.Common.Shared.Enums;

namespace Ecommerce.Common.Shared.Configurations;

public abstract class RabbitMqPublishMessage<T>
{
    public T Payload { get; set; }
    public string ExchangeName { get; set; }
    public ExchangeTypes ExchangeType { get; set; }
    public string RouteKey { get; set; }
    public IDictionary<string, object> Header { get; set; }
}