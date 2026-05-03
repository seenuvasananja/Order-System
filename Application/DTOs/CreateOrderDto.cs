namespace OrderService.Application.DTOs
{
    public class CreateOrderDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; } 
        public int Quantity { get; set; }
    }
}
