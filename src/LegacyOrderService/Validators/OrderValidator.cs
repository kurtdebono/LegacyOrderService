using System;
using LegacyOrderService.Services;
using LegacyOrderService.Models;

namespace LegacyOrderService.Validators
{
    public class OrderValidator
    {
        private readonly ProductService _productService;

        public OrderValidator(ProductService productService)
        {
            _productService = productService;
        }

        public ValidationResult Validate(string customerName, string product, string quantityInput)
        {
            List<string> errors = new List<string>();

            if(string.IsNullOrWhiteSpace(customerName))
            {
                errors.Add("Customer name is required");
            }

            if(string.IsNullOrWhiteSpace(product))
            {
                errors.Add("Product name is required");
            }
            else if(!_productService.Exists(product))
            {
                errors.Add($"The product [{product}] is currently out of stock");
            }

            int? quantity = null;
            if(string.IsNullOrWhiteSpace(quantityInput))
            {
                errors.Add("Quantity is required");
            }
            else if(!int.TryParse(quantityInput, out int parsedQuantity))
            {
                errors.Add("Quantity is not a valid number");                                    
            }
            else if(parsedQuantity <= 0)
            {
                errors.Add("Quantity is not greater than 0");
            }
            else
            {
                quantity = parsedQuantity;
            }

            return new ValidationResult(errors, quantity);
        }
    }
}