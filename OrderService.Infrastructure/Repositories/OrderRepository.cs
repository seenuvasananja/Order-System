using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Data;

namespace OrderService.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public Order Add(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }
    }
}
