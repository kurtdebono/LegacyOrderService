using LegacyOrderService.Interfaces;
using LegacyOrderService.Application;
using LegacyOrderService.Data;
using LegacyOrderService.Services;

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

            OrderApplication orderApp = new OrderApplication(productService, orderService);

            orderApp.Run();
        }
    }
}
