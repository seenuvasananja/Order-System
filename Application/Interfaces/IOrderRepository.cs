using OrderService.Domain.Entities;

namespace OrderService.Application.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetAll();
        Order Add(Order order);
    }
}
