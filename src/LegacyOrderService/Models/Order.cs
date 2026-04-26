namespace LegacyOrderService.Models
{
    public class Order
    {
        public required string CustomerName { get; set; }
        public required string ProductName { get; set; }
        public int Quantity;
        public decimal Price;

        public decimal Total => Price * Quantity;
    }
}
