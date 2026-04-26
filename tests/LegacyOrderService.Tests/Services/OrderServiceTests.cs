using Xunit;
using Moq;

using Microsoft.Data.Sqlite;

using LegacyOrderService.Interfaces;
using LegacyOrderService.Data;
using LegacyOrderService.Models;
using LegacyOrderService.Services;
using LegacyOrderService.Exceptions;

namespace LegacyOrderService.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public void CreateOrder_WithValidInput_ReturnsExpectedOrder()
        {
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            OrderService orderService = new OrderService(mockOrderRepository.Object);

            string name = "Kurt";
            string product = "Widget";
            decimal price = 10.5M;
            int quantity = 2;

            Order order = orderService.CreateOrder(name, product, price, quantity);

            Assert.Equal(name, order.CustomerName);
            Assert.Equal(product, order.ProductName);
            Assert.Equal(price, order.Price);
            Assert.Equal(quantity, order.Quantity);
        }

        [Fact]
        public void Save_WithValidOrder_CallsRepositoryOnce()
        {
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();
            OrderService orderService = new OrderService(mockOrderRepository.Object);

            Order order = new Order()
            {
                CustomerName = "Peter",
                ProductName = "Widget",
                Price = 10.5M,
                Quantity = 2
            };

            orderService.Save(order);

            mockOrderRepository.Verify(r => r.Save(order), Times.Once);
        }

        [Fact]
        public void Save_WhenRepositoryThrowsSqliteException_ThrowsDatabaseOperationExceptionInstead()
        {
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();

            mockOrderRepository.Setup(r => r.Save(It.IsAny<Order>())).Throws(new SqliteException("Database exception", 500));

            OrderService orderService = new OrderService(mockOrderRepository.Object);

            Order order = new Order()
            {
                CustomerName = "Peter",
                ProductName = "Widget",
                Price = 10.5M,
                Quantity = 2
            };            

            Assert.Throws<DatabaseOperationException>(() => orderService.Save(order));
        }

        [Fact]
        public void Save_WhenRepositoryThrowsException_ExceptionTypeStaysTheSame()
        {
            Mock<IOrderRepository> mockOrderRepository = new Mock<IOrderRepository>();

            mockOrderRepository.Setup(r => r.Save(It.IsAny<Order>())).Throws(new Exception("Database exception"));

            OrderService orderService = new OrderService(mockOrderRepository.Object);

            Order order = new Order()
            {
                CustomerName = "Peter",
                ProductName = "Widget",
                Price = 10.5M,
                Quantity = 2
            };            

            Assert.Throws<Exception>(() => orderService.Save(order));
        }
    }
}