using Microsoft.Extensions.Logging;
using ProductService.Application.DTOs;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;

namespace ProductService.Application.Services
{
    public class ProductManagerService : IProductManagerService
    {
        private readonly IProductManagerRepository _repo;
        private readonly ILogger<ProductManagerService> _logger;

        public ProductManagerService(IProductManagerRepository repo, ILogger<ProductManagerService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all products");

            var products = await _repo.GetAllAsync();

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }).ToList();
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id);

            if (product == null)
                throw new KeyNotFoundException("Product not found");

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price
            };

            var created = await _repo.AddAsync(product);

            return new ProductDto
            {
                Id = created.Id,
                Name = created.Name,
                Price = created.Price
            };
        }
    }
}