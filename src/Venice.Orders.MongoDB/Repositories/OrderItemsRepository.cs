using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Venice.Orders.CrossCutting.Shareable.Config;
using Venice.Orders.Domain.Documents;
using Venice.Orders.Domain.Repositories;

namespace Venice.Orders.Infrastructure.Persistence.MongoDB.Repositories;

public sealed class OrderItemsRepository : IOrderItemsRepository
{
    private readonly IMongoCollection<OrderItemsDocument> _col;

    public OrderItemsRepository(IOptions<AppConfig> opt)
    {
        var client = new MongoClient(opt.Value.MongoSettings.Connection);
        var db = client.GetDatabase(opt.Value.MongoSettings.Database);
        _col = db.GetCollection<OrderItemsDocument>("order_items");
    }

    public Task UpsertAsync(OrderItemsDocument doc, CancellationToken ct) =>
        _col.ReplaceOneAsync(x => x.OrderId == doc.OrderId, doc, new ReplaceOptions { IsUpsert = true }, ct);

    public async Task<OrderItemsDocument?> GetAsync(Guid orderId, CancellationToken ct) =>
        (await _col.FindAsync(x => x.OrderId == orderId, cancellationToken: ct)).FirstOrDefault(ct);
}

