using AutoMapper;
using MediatR;
using System.Text.Json;
using Venice.Orders.Application.Interfaces;
using Venice.Orders.Domain.Repositories;

namespace Venice.Orders.Application.Command.Orders.GetOrder;

public sealed class GetOrderCommandHandler : IRequestHandler<GetOrderCommand, GetOrderCommandDto?>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemsRepository _orderItemsRepository;
    private readonly IRedisService _cache;
    private readonly IMapper _mapper;

    public GetOrderCommandHandler(
        IOrderRepository orderRepository,
        IOrderItemsRepository orderItemsRepository,
        IRedisService cache,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _orderItemsRepository = orderItemsRepository;
        _cache = cache;
        _mapper = mapper;
    }

    public async Task<GetOrderCommandDto?> Handle(GetOrderCommand command, CancellationToken ct)
    {
        try
        {

            var cacheKey = $"order:{command.Id}";
            var cached = await _cache.GetAsync(cacheKey);

            if (cached is not null)
            {
                return JsonSerializer.Deserialize<GetOrderCommandDto>(cached);
            }

            var order = await _orderRepository.GetByIdAsync(command.Id, ct);
            if (order is null) return null;

            var itemsDoc = await _orderItemsRepository.GetAsync(order.Id, ct);
            if (itemsDoc is null) return null;

            var orderItemDtoList = new List<OrderItemCommandDTO>();

            foreach (var item in itemsDoc!.Items)
            {
                orderItemDtoList.Add(new OrderItemCommandDTO { Product = item.Product, Quantity = item.Quantity, UnitPrice = item.UnitPrice });
            }

            var response = new GetOrderCommandDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                Date = order.Date,
                Status = order.Status,
                Total = order.Total,
                Items = orderItemDtoList
            };

            var json = JsonSerializer.Serialize(response);
            await _cache.SetAsync(cacheKey, json, TimeSpan.FromMinutes(2));

            return response;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString());
        }
    }
}
