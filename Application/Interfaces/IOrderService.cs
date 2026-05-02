using OrderService.Application.DTOs;
using OrderService.Domain.Entities;

namespace OrderService.Application.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetAll();
        Order Create(CreateOrderDto dto);
    }
}
