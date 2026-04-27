using Xunit;
using Microsoft.Data.Sqlite;

using LegacyOrderService.Data;
using LegacyOrderService.Models;
using LegacyOrderService.Interfaces;

namespace LegacyOrderService.Tests
{
    public class OrderRepositoryIntegrationTests
    {
        [Fact]
        public void Save_WithValidOrder_PersistsOrderToDatabase()
        {
            string databasePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.db");
            string connectionString = $"Data Source={databasePath}";

            _createOrdersTable(connectionString);

            IOrderRepository orderRepository = new OrderRepository(connectionString);
            
            Order order = new Order()
            {
                CustomerName = "Philip",
                ProductName = "Doohickey",
                Price = 8.75M,
                Quantity = 2
            };

            try
            {            
                orderRepository.Save(order);
                
                using(SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    using(SqliteCommand command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                            SELECT CustomerName, ProductName, Price, Quantity
                            FROM Orders
                            LIMIT 1";

                        using(SqliteDataReader reader = command.ExecuteReader())
                        {
                            Assert.True(reader.Read());
                            Assert.Equal(order.CustomerName, reader.GetString(0));
                            Assert.Equal(order.ProductName, reader.GetString(1));
                            Assert.Equal(order.Price, reader.GetDecimal(2));
                            Assert.Equal(order.Quantity, reader.GetInt32(3));
                        }
                    }
                }                    
            }
            finally
            {
                if(File.Exists(databasePath))
                {
                    File.Delete(databasePath);      
                }
            }            
        }

        [Fact]
        public void Save_WithNameHavingSymbol_PersistsOrderToDatabase()
        {
            string databasePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.db");
            string connectionString = $"Data Source={databasePath}";

            _createOrdersTable(connectionString);

            IOrderRepository orderRepository = new OrderRepository(connectionString);
            
            Order order = new Order()
            {
                CustomerName = "O'Reilly",
                ProductName = "Doohickey",
                Price = 8.75M,
                Quantity = 2
            };

            try
            {                
                orderRepository.Save(order);
                
                using(SqliteConnection connection = new SqliteConnection(connectionString))
                {
                    connection.Open();

                    using(SqliteCommand command = connection.CreateCommand())
                    {
                        command.CommandText = @"
                            SELECT CustomerName, ProductName, Price, Quantity
                            FROM Orders
                            LIMIT 1";

                        using(SqliteDataReader reader = command.ExecuteReader())
                        {
                            Assert.True(reader.Read());
                            Assert.Equal(order.CustomerName, reader.GetString(0));
                            Assert.Equal(order.ProductName, reader.GetString(1));
                            Assert.Equal(order.Price, reader.GetDecimal(2));
                            Assert.Equal(order.Quantity, reader.GetInt32(3));
                        }
                    }
                }   
            }
            finally
            {                
                if(File.Exists(databasePath))
                {
                    File.Delete(databasePath);      
                }             
            }        
        }

        [Fact]
        public void Save_WhenOrderTableDoesNotExist_ThrowsSqliteException()
        {
            string databasePath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.db");
            string connectionString = $"Data Source={databasePath}";

            IOrderRepository orderRepository = new OrderRepository(connectionString);
            
            Order order = new Order()
            {
                CustomerName = "Philip",
                ProductName = "Doohickey",
                Price = 8.75M,
                Quantity = 2
            };

            try
            {                
                Assert.Throws<SqliteException>(() => orderRepository.Save(order));    
            }
            finally
            {                
                if(File.Exists(databasePath))
                {
                    File.Delete(databasePath);      
                }
            }            
        }

        private void _createOrdersTable(string connectionString)
        {
            using(SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using(SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        CREATE TABLE Orders (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            CustomerName TEXT NOT NULL,
                            ProductName TEXT NOT NULL,
                            Quantity INTEGER NOT NULL,
                            Price REAL NOT NULL)";
                    
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}