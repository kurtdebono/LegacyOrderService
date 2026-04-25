using System;
using Microsoft.Data.Sqlite;
using LegacyOrderService.Models;
using LegacyOrderService.Interfaces;

namespace LegacyOrderService.Data
{
    public class OrderRepository : IOrderRepository
    {
        private string _connectionString = $"Data Source={Path.Combine(AppContext.BaseDirectory, "orders.db")}";
        public void Save(Order order)
        {
            using(SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                using(SqliteCommand command = connection.CreateCommand())
                {                    
                    command.CommandText = @"
                        INSERT INTO Orders (CustomerName, ProductName, Quantity, Price)
                        VALUES (@CustomerName, @ProductName, @Quantity, @Price)";

                    command.Parameters.AddWithValue("@CustomerName", order.CustomerName);
                    command.Parameters.AddWithValue("@ProductName", order.ProductName);
                    command.Parameters.AddWithValue("@Quantity", order.Quantity);
                    command.Parameters.AddWithValue("@Price", order.Price);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void SeedBadData()
        {
            using(SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                using(SqliteCommand command = connection.CreateCommand())
                {                    
                    command.CommandText = @"
                        INSERT INTO Orders (CustomerName, ProductName, Quantity, Price)
                        VALUES (@CustomerName, @ProductName, @Quantity, @Price)";
                    
                    command.Parameters.AddWithValue("@CustomerName", "John");
                    command.Parameters.AddWithValue("@ProductName", "Widget");
                    command.Parameters.AddWithValue("@Quantity", 9999);
                    command.Parameters.AddWithValue("@Price", 9.99);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
