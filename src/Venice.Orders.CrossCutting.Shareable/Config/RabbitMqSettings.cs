namespace Venice.Orders.CrossCutting.Shareable.Config;

public class RabbitMqSettings
{
    public string HostName { get; set; } = null!;
    public int Port { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string VirtualHost { get; set; } = "/";
    public string Exchange { get; set; } = null!;
    public string Queue { get; set; } = null!;
}