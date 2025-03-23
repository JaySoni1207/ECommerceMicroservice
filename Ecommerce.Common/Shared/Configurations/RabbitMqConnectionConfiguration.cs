namespace Ecommerce.Common.Shared.Configurations;

public class RabbitMqConnectionConfiguration
{
    public int Port { get; set; } = 5672;
    public string UserName { get; set; }
    public string Password { get; set; }
    public string HostName { get; set; }
}