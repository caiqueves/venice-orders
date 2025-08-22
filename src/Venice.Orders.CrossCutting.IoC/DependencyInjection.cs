using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Venice.Orders.Application;
using Venice.Orders.Application.Interfaces;
using Venice.Orders.CrossCutting.Shareable.Config;
using Venice.Orders.Domain;
using Venice.Orders.Domain.Repositories;
using Venice.Orders.Infrastructure.Caching.Redis;
using Venice.Orders.Infrastructure.Messaging.RabbitMQ;
using Venice.Orders.Infrastructure.Persistence.MongoDB.Repositories;
using Venice.Orders.Infrastructure.Persistence.SqlServer;
using Venice.Orders.Infrastructure.Persistence.SqlServer.Repositories;
using Venice.Orders.Infrastructure.Repositories;

namespace Venice.Orders.CrossCutting.IoC;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureAppDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(IApplicationEntryPoint).Assembly));
        var appConfig = configuration.Get<AppConfig>(x => x.BindNonPublicProperties = true);
        services.AddSingleton(appConfig);

        var mongoConnectionString = string.IsNullOrEmpty(appConfig!.MongoSettings.Connection!)
            ? throw new InvalidOperationException("Connection string do MongoDB não encontrada.")
            : appConfig.MongoSettings.Connection;

        services.AddSingleton<IMongoClient>(sp => new MongoClient(appConfig.MongoSettings.Connection));

        
        var connectionString = configuration.GetSection("ConnectionStrings:Default").Value
                   ?? throw new InvalidOperationException("Connection string do SqlServer não encontrada");

        services.AddDbContext<DefaultContext>(options =>
            options.UseSqlServer(connectionString));

        return services;

    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemsRepository, OrderItemsRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IRabbitMqEventPublisher, RabbitMqEventPublisher>();
        services.AddScoped<IRedisService, RedisService>();
        return services;
    }
}
