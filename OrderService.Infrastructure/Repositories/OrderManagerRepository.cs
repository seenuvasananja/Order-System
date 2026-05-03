using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Data;

namespace OrderService.Infrastructure.Repositories
{
    public class OrderManagerRepository : IOrderMangerRepository
    {
        private static List<Order> _orders = new();

        public Task<List<Order>> GetAllAsync()
        {
            return Task.FromResult(_orders);
        }

        public Task<Order?> GetByIdAsync(int id)
        {
            var order = _orders.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(order);
        }

        public Task<Order> AddAsync(Order order)
        {
            order.Id = _orders.Count + 1;
            _orders.Add(order);
            return Task.FromResult(order);
        }
    }
}
