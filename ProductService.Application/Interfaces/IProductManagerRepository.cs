using ProductService.Domain.Entities;

namespace ProductService.Application.Interfaces
{
    public interface IProductManagerRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<Product> AddAsync(Product product);
    }
}