using System.Collections.Generic;
using Ecommerce.Common.Shared.Enums;

namespace Ecommerce.Common.Shared.Configurations;

public class SubscribeConfiguration
{
    public string QueueName { get; set; }
    public string ExchangeName { get; set; }
    public string RouteKey { get; set; }
    public Dictionary<string, object>? Headers { get; set; }
    public ExchangeTypes ExchangeType { get; set; }
}