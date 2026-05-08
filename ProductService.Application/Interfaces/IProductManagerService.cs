using ProductService.Application.DTOs;

namespace ProductService.Application.Interfaces
{
    public interface IProductManagerService
    {
        Task<List<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task<ProductDto> CreateAsync(CreateProductDto dto);
    }
}