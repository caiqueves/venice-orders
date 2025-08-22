
namespace Venice.Orders.CrossCutting.Shareable.Config
{
    public class AppConfig
    {
        public MongoSettings MongoSettings { get; set; } = new();
        public RedisSettings RedisSettings { get; set; } = new();
        public RabbitMqSettings RabbitMqSettings { get; set; } = new();
    }
}
