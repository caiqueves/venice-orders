using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection;
using Venice.Orders.Domain.Entity;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Venice.Orders.Domain.Entities;

namespace Venice.Orders.Infrastructure.Persistence.SqlServer;

public sealed class DefaultContext : DbContext
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<User> Users => Set<User>();
    public DefaultContext(DbContextOptions<DefaultContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder b)
    {
        b.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(b);
    }
}

public class YourDbContextFactory : IDesignTimeDbContextFactory<DefaultContext>
{
    public DefaultContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<DefaultContext>();
        var connectionString = configuration.GetConnectionString("Default");

        builder.UseSqlServer(
               connectionString,
               b => b.MigrationsAssembly("Venice.Orders.SqlServer")
        );

        return new DefaultContext(builder.Options);
    }
}