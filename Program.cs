using LegacyOrderService.Interfaces;
using LegacyOrderService.Application;
using LegacyOrderService.Data;
using LegacyOrderService.Services;
using LegacyOrderService.Validators;

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

            OrderApplication orderApp = new OrderApplication(productService, orderService, orderValidator);

            orderApp.Run();
        }
    }
}
