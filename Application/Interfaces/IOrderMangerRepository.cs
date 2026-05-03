using OrderService.Domain.Entities;

namespace OrderService.Application.Interfaces
{
    public interface IOrderMangerRepository
    {
        Task<List<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task<Order> AddAsync(Order order);
    }
}
