using LegacyOrderService.Services;
using LegacyOrderService.Models;
using LegacyOrderService.Validators;

namespace LegacyOrderService.Application
{
    public class OrderApplication
    {
        private ProductService _productService;
        private OrderService _orderService;
        private OrderValidator _orderValidator;
        
        public OrderApplication(ProductService productService, OrderService orderService, OrderValidator orderValidator)
        {
            _productService = productService;
            _orderService = orderService;
            _orderValidator = orderValidator;
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
            string qty = Console.ReadLine();

            Console.WriteLine("Validating Information...");
            ValidationResult validationResult = this._orderValidator.Validate(name, product, qty);
            if(!validationResult.IsValid)
            {
                Console.WriteLine(validationResult.GetFullErrorMessage());
                return;
            }
            
            Console.WriteLine("Processing order...");            
            Order order = this._orderService.CreateOrder(name, product, price, Convert.ToInt32(qty));

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