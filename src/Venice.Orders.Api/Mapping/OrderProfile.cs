using AutoMapper;
using Venice.Orders.Api.Features.Order.CreateOrder;
using Venice.Orders.Api.Features.Order.DTOs;
using Venice.Orders.Api.Features.Order.GetOrder;
using Venice.Orders.Application.Command.Orders.CreateOrder;
using Venice.Orders.Application.Command.Orders.GetOrder;
using Venice.Orders.Domain.Documents;
using Venice.Orders.Domain.Entity;

namespace Venice.Orders.Api.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            // DTO API → Command
            CreateMap<CreateOrderRequest, CreateOrderCommand>();
            CreateMap<OrderItemDto, OrderItem>();

            // Domain + Mongo → DTO Response
            CreateMap<(Order order, OrderItemsDocument? itemsDoc), OrderResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.order.Id))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.order.CustomerId))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.order.Date))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.order.Status))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.itemsDoc != null ? src.itemsDoc.Items : null));

            CreateMap<GetOrderRequest, GetOrderCommand>();
            CreateMap<GetOrderCommandDto, OrderResponse>();
        }
    }
}
