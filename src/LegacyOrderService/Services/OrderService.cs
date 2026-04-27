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

        public Order CreateOrder(string customerName, string productName, decimal price, int quantity)
        {
            return new Order()
            {
                CustomerName = customerName,
                ProductName = productName,
                Quantity = quantity,
                Price = price
            };            
        }

        /// <inheritdoc />
        public void Save(Order order)
        {
            try
            {
                _orderRepository.Save(order);
            }
            catch(SqliteException sqlEx)
            {
                throw new DatabaseOperationException("Failed to save order.", sqlEx);
            }
        }
    }
}