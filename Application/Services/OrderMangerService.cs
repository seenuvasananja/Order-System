using OrderService.Application.DTOs;
using OrderService.Application.Exceptions;
using OrderService.Application.Interfaces;
using OrderService.Domain.Entities;
using Microsoft.Extensions.Logging;
namespace OrderService.Application.Services
{
    public class OrderMangerService : IOrderMangerService
    {
        private readonly IOrderMangerRepository _repo;
        private readonly ILogger<OrderMangerService> _logger;

        public OrderMangerService(IOrderMangerRepository repo, ILogger<OrderMangerService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all orders");
            return await _repo.GetAllAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching order with Id: {Id}", id);
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Order> CreateAsync(CreateOrderDto dto)
        {
            _logger.LogInformation("Creating order for product: {Product}", dto.ProductName);
            if (dto == null)
                throw new BadRequestException("Request cannot be null");

            if (string.IsNullOrEmpty(dto.ProductName))
                throw new BadRequestException("Product name is required");

            var order = new Order(dto.UserId, dto.ProductId, dto.Quantity, dto.ProductName);

            return await _repo.AddAsync(order);
        }
    }
}
