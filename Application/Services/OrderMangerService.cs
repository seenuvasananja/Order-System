using OrderService.Application.DTOs;
using OrderService.Application.Exceptions;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;

namespace OrderService.Application.Services
{
    public class OrderMangerService : IOrderMangerService
    {
        private readonly IOrderMangerRepository _repo;

        public OrderMangerService(IOrderMangerRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Order> CreateAsync(CreateOrderDto dto)
        {
            if (dto == null)
                throw new BadRequestException("Request cannot be null");

            if (string.IsNullOrEmpty(dto.ProductName))
                throw new BadRequestException("Product name is required");

            var order = new Order(dto.UserId, dto.ProductId, dto.Quantity, dto.ProductName);

            return await _repo.AddAsync(order);
        }
    }
}
