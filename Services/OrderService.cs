using LegacyOrderService.Interfaces;
using LegacyOrderService.Data;
using LegacyOrderService.Models;
using Microsoft.Data.Sqlite;
using LegacyOrderService.Exceptions;

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
            try
            {
                this._orderRepository.Save(order);
            }
            catch(SqliteException sqlEx)
            {
                throw new DatabaseOperationException("Failed to save order.", sqlEx);
            }
        }
    }
}