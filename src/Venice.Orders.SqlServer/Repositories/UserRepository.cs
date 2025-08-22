using Microsoft.EntityFrameworkCore;
using Venice.Orders.Domain.Entities;
using Venice.Orders.Domain.Repositories;
using Venice.Orders.Infrastructure.Persistence;
using Venice.Orders.Infrastructure.Persistence.SqlServer;

namespace Venice.Orders.Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly DefaultContext _context;

    public UserRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
        => await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

    public async Task<User?> GetByEmailAsync(string email)
        => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
}
