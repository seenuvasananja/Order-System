using Microsoft.AspNetCore.Mvc;
using OrderService.Application.DTOs;
using OrderService.Application.Interfaces;

namespace OrderService.API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderMangerService _service;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderMangerService service, ILogger<OrdersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Fetching all orders");
            var result = await _service.GetAllAsync();
            _logger.LogInformation("Fetched {Count} orders", result.Count);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("Fetching order with Id: {Id}", id);
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                _logger.LogWarning("Order not found with Id: {Id}", id);
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
        {
            _logger.LogInformation("Creating new order");
            var result = await _service.CreateAsync(dto);
            _logger.LogInformation("Order created with Id: {Id}", result.Id);
            return Ok(result);
        }
    }
}