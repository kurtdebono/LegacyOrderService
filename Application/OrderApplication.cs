using LegacyOrderService.Services;
using LegacyOrderService.Models;

namespace LegacyOrderService.Application
{
    public class OrderApplication
    {
        private ProductService _productService;
        private OrderService _orderService;

        public OrderApplication(ProductService productService, OrderService orderService)
        {
            _productService = productService;
            _orderService = orderService;
        }

        public void Run()
        {
            Console.WriteLine("Welcome to Order Processor!");
            Console.WriteLine("Enter customer name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter product name:");
            string product = Console.ReadLine();
            double price = this._productService.GetPrice(product);

            Console.WriteLine("Enter quantity:");
            int qty = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Processing order...");            
            Order order = this._orderService.CreateOrder(name, product, price, qty);

            Console.WriteLine("Order complete!");
            Console.WriteLine("Customer: " + order.CustomerName);
            Console.WriteLine("Product: " + order.ProductName);
            Console.WriteLine("Quantity: " + order.Quantity);
            Console.WriteLine("Total: $" + order.Total);

            Console.WriteLine("Saving order to database...");
            this._orderService.Save(order);

            Console.WriteLine("Done.");
        }
    }
}