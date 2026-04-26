using LegacyOrderService.Interfaces;
using LegacyOrderService.Application;
using LegacyOrderService.Data;
using LegacyOrderService.Services;
using LegacyOrderService.Validators;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace LegacyOrderService
{
    class Program
    {
        static void Main(string[] args)
        {
            IProductRepository productRepo = new ProductRepository();
            IOrderRepository orderRepo = new OrderRepository();

            ProductService productService = new ProductService(productRepo);
            OrderService orderService = new OrderService(orderRepo);            

            OrderValidator orderValidator = new OrderValidator(productService);

            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });

            ILogger<OrderApplication> logger = loggerFactory.CreateLogger<OrderApplication>();

            OrderApplication orderApp = new OrderApplication(productService, orderService, orderValidator, logger);

            orderApp.Run();
        }
    }
}