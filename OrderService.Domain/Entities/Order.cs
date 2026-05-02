namespace OrderService.Domain.Entities
{
    public class Order
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public string Status { get; private set; }

        public Order(int userId, int productId, int quantity)
        {
            if (quantity <= 0)
            {
                throw new Exception("Quantity must be greater than 0");
            }

            UserId = userId;
            ProductId = productId;
            Quantity = quantity;
            Status = "Pending";
        }
    }
}
