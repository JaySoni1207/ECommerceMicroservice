using System;
using System.Threading.Tasks;
using Ecommerce.Common.Shared.Configurations;

namespace Ecommerce.Common.Messaging.Interfaces;

public interface IRabbitMqConnection
{
    Task ConfigureRabbitMqConnectionAsync<T>(SubscribeConfiguration rabbitMqConnectionConfiguration,
        Func<T, Task> func);

    Task PublishMessageAsync<T>(RabbitMqPublishMessage<T> message);
}