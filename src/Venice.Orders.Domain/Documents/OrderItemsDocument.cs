using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venice.Orders.Domain.Documents;

public sealed class OrderItemsDocument
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }   // Mapeia o campo _id

    [BsonGuidRepresentation(MongoDB.Bson.GuidRepresentation.Standard)]
    public Guid OrderId { get; set; }
    public List<OrderItem> Items { get; set; } = new();
}