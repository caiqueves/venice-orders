namespace Venice.Orders.Domain.Repositories;

using Venice.Orders.Domain.Entities;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
}
