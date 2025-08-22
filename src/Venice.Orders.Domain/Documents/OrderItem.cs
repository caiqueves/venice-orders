namespace Venice.Orders.Domain.Documents;

public sealed class OrderItem
{
    public string Product { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
