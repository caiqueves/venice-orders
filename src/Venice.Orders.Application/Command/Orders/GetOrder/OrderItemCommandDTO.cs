namespace Venice.Orders.Application.Command.Orders.GetOrder;

public class OrderItemCommandDTO
{
    public string Product { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }
}