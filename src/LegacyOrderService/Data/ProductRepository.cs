// Data/ProductRepository.cs
using System;
using System.Collections.Generic;
using System.Threading;
using LegacyOrderService.Interfaces;

namespace LegacyOrderService.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly Dictionary<string, decimal> _productPrices = new()
        {
            ["Widget"] = 12.99M,
            ["Gadget"] = 15.49M,
            ["Doohickey"] = 8.75M
        };

        public decimal GetPrice(string productName)
        {
            // Simulate an expensive lookup
            Thread.Sleep(500);

            return _productPrices.GetValueOrDefault(productName);
        }

        /// <inheritdoc />
        public bool Exists(string productName)
        {
            return _productPrices.ContainsKey(productName);
        }
    }
}
