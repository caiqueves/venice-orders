using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Venice.Orders.Application.Command.Orders.CreateOrder;
using Venice.Orders.Application.Interfaces;
using Venice.Orders.Domain.Documents;
using Venice.Orders.Domain.Entity;
using Venice.Orders.Domain.Events;
using Venice.Orders.Domain.Repositories;

namespace Venice.Orders.Application.Command.Orders.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemsRepository _orderItemsRepository;
    private readonly IRabbitMqEventPublisher _publisher;

    public CreateOrderHandler(
        IOrderRepository orderRepository,
        IOrderItemsRepository orderItemsRepository,
        IRabbitMqEventPublisher publisher)
    {
        _orderRepository = orderRepository;
        _orderItemsRepository = orderItemsRepository;
        _publisher = publisher;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // Criar pedido
        var order = new Order(request.CustomerId, request.Date);

        order.SetTotal(request.Items);

        await _orderRepository.AddAsync(order, cancellationToken);

        // Salvar itens no MongoDB
        var orderItemsDoc = new OrderItemsDocument
        {
            OrderId = order.Id,
            Items = request.Items
        };
        await _orderItemsRepository.UpsertAsync(orderItemsDoc, cancellationToken);

        // Publicar evento no RabbitMQ
        var orderCreatedEvent = new OrderCreatedEvent(order.Id, order.CustomerId, request.Items, order.Date, order.Status, order.Total);
        await _publisher.PublishEvent(JsonSerializer.Serialize(orderCreatedEvent));

        return order.Id;
    }
}