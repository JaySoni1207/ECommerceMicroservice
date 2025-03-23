using System.ComponentModel;
using RabbitMQ.Client;

namespace Ecommerce.Common.Shared.Enums;

public enum ExchangeTypes
{
    [Description(ExchangeType.Direct)]
    Direct,
    [Description(ExchangeType.Fanout)]
    Fanout,
    [Description(ExchangeType.Headers)]
    Headers,
    [Description(ExchangeType.Topic)]
    Topic,
}