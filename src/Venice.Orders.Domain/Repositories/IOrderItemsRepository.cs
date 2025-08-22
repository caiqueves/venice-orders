using Venice.Orders.Domain.Documents;

namespace Venice.Orders.Domain.Repositories;

public interface IOrderItemsRepository
{
    Task UpsertAsync(OrderItemsDocument doc, CancellationToken ct);
    Task<OrderItemsDocument?> GetAsync(Guid orderId, CancellationToken ct);
}
