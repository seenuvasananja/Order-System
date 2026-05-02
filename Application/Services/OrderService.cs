using OrderService.Application.DTOs;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;

namespace OrderService.Application.Services
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderRepository _repo;

        public OrderManager(IOrderRepository repo)
        {
            _repo = repo;
        }

        public List<Order> GetAll()
        {
            return _repo.GetAll();
        }

        public Order Create(CreateOrderDto dto)
        {
            var order = new Order(dto.UserId, dto.ProductId, dto.Quantity);
            return _repo.Add(order);
        }
    }
}
