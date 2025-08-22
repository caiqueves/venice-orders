using Venice.Orders.Domain.Documents;
using Venice.Orders.Domain.Entity;

namespace Venice.Orders.Domain.Repositories;

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Order order, CancellationToken cancellationToken = default);
    Task UpdateAsync(Order order, CancellationToken cancellationToken = default);
    Task DeleteAsync(Order order, CancellationToken cancellationToken = default);
}
