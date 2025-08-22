
namespace Venice.Orders.Application.Interfaces;

public interface IRabbitMqEventPublisher
{
    Task PublishEvent(string eventMessage);
}
