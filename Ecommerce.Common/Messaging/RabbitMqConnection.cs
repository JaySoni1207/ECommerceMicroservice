

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Ecommerce.Common.Messaging.Interfaces;
using Ecommerce.Common.Shared.Configurations;
using Ecommerce.Common.Shared.Enums;
using Ecommerce.Common.Shared.Extensions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;

namespace Ecommerce.Common.Messaging;

public class RabbitMqConnection : IRabbitMqConnection, IAsyncDisposable
{
    private readonly ConnectionFactory _config;
    private IConnection? _connection;
    private IChannel? _channel;
    public RabbitMqConnection(IOptions<RabbitMqConnectionConfiguration> rabbitMqConnectionConfig)
    {
        var rabbitMqConnectionConfiguration = rabbitMqConnectionConfig.Value;
        _config = new ConnectionFactory
        {
            HostName = rabbitMqConnectionConfiguration.HostName,
            UserName = rabbitMqConnectionConfiguration.UserName,
            Password = rabbitMqConnectionConfiguration.Password,
            Port = rabbitMqConnectionConfiguration.Port
        };
    }

    public async Task ConfigureRabbitMqConnectionAsync<T>(SubscribeConfiguration subscribeConfiguration,
        Func<T, Task> functionAsync)
    {
        _connection = await _config.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        await _channel.ExchangeDeclareAsync(subscribeConfiguration.ExchangeName, subscribeConfiguration.ExchangeType.ToDescription());
        await _channel.QueueDeclareAsync(subscribeConfiguration.QueueName, true, false, false);
        await ConfigureQueueBindByExchangeTypes(subscribeConfiguration);
        
        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await functionAsync(JsonConvert.DeserializeObject<T>(message) ?? throw new InvalidOperationException());
            
                await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        };
        
        await _channel.BasicConsumeAsync(
            queue: subscribeConfiguration.QueueName,
            autoAck: false,
            consumer: consumer
        );
    }

    public async Task PublishMessageAsync<T>(RabbitMqPublishMessage<T> message)
    {
        await using var connection = await _config.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        var properties = new BasicProperties();
        if (message.Header is { Count: > 0 })
        {
            properties.Headers = message.Header;
        }
        
        await channel.BasicPublishAsync(
            new PublicationAddress(message.ExchangeType.ToDescription(), message.ExchangeName, message.RouteKey), 
            properties, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message.Payload)));
    }

    private async Task ConfigureQueueBindByExchangeTypes(SubscribeConfiguration  subscribeConfiguration)
    {
        
        switch (subscribeConfiguration.ExchangeType)
        {
            case ExchangeTypes.Direct:
                await _channel!.QueueBindAsync(subscribeConfiguration.QueueName, subscribeConfiguration.ExchangeName, subscribeConfiguration.RouteKey);
                break;
            
            case ExchangeTypes.Fanout:
                await _channel!.QueueBindAsync(subscribeConfiguration.QueueName, subscribeConfiguration.ExchangeName, "");
                break;
            
            case ExchangeTypes.Topic:
                await _channel!.QueueBindAsync(subscribeConfiguration.QueueName, subscribeConfiguration.ExchangeName, subscribeConfiguration.RouteKey);
                break;
            
            case ExchangeTypes.Headers:
                await _channel!.QueueBindAsync(subscribeConfiguration.QueueName, subscribeConfiguration.ExchangeName, string.Empty, 
                    (subscribeConfiguration.Headers ?? new Dictionary<string, object> { { "format", "json" } })!);
                break;
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _connection!.DisposeAsync();
        await _channel!.DisposeAsync();
        await _connection.CloseAsync();
        await _channel.CloseAsync();
    }
}