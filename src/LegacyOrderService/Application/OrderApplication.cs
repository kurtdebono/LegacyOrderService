using LegacyOrderService.Services;
using LegacyOrderService.Models;
using LegacyOrderService.Validators;
using LegacyOrderService.Exceptions;

using Microsoft.Extensions.Logging;

namespace LegacyOrderService.Application
{
    public class OrderApplication
    {
        private IProductService _productService;
        private OrderService _orderService;
        private OrderValidator _orderValidator;

        private ILogger<OrderApplication> _logger;
        
        public OrderApplication(IProductService productService, OrderService orderService, OrderValidator orderValidator, ILogger<OrderApplication> logger)
        {
            _productService = productService;
            _orderService = orderService;
            _orderValidator = orderValidator;

            _logger = logger;
        }

        public void Run()
        {
            try
            {
                Console.WriteLine("Welcome to Order Processor!");
                Console.WriteLine("Enter customer name:");
                string name = Console.ReadLine() ?? string.Empty;

                Console.WriteLine("Enter product name:");
                string product = Console.ReadLine() ?? string.Empty;
                decimal price = _productService.GetPrice(product);

                Console.WriteLine("Enter quantity:");
                string quantityInput = Console.ReadLine() ?? string.Empty;

                Console.WriteLine("Validating Information...");
                ValidationResult validationResult = _orderValidator.Validate(name, product, quantityInput);
                if(!validationResult.IsValid)
                {
                    Console.WriteLine(validationResult.GetFullErrorMessage());
                    return;
                }
                
                Console.WriteLine("Processing order...");                            
                Order order = _orderService.CreateOrder(name, product, price, validationResult.ParsedQuantity!.Value);

                Console.WriteLine("Order complete!");
                Console.WriteLine("Customer: " + order.CustomerName);
                Console.WriteLine("Product: " + order.ProductName);
                Console.WriteLine("Quantity: " + order.Quantity);
                Console.WriteLine("Total: $" + order.Total);

                Console.WriteLine("Saving order to database...");
                _orderService.Save(order);

                Console.WriteLine("Done.");   
            }
            catch(DatabaseOperationException dbOpEx)
            {
                _logger.LogError(dbOpEx, "Database error while saving order.");
                Console.WriteLine($@"Order could not be completed, and was not saved. Please try again later.");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while processing order.");
                Console.WriteLine($@"Order could not be completed, as an unexpected error occurred.");
            }            
        }
    }
}