using LegacyOrderService.Interfaces;
using LegacyOrderService.Data;
using LegacyOrderService.Models;

namespace LegacyOrderService.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Order CreateOrder(string customerName, string productName, double price, int quantity)
        {
            Order order = new Order();
            order.CustomerName = customerName;
            order.ProductName = productName;
            order.Quantity = quantity;
            order.Price = price;

            return order;
        }

        public void Save(Order order)
        {
            this._orderRepository.Save(order);
        }
    }
}