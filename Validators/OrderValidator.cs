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
            this._productService = productService;
        }

        public ValidationResult Validate(string customerName, string product, string quantity)
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
            else if(!this._productService.Exists(product))
            {
                errors.Add($@"The product [{product}] is currently out of stock");
            }

            if(!int.TryParse(quantity, out int qty))
            {
                errors.Add("Quantity is not a valid number");
            }
            else if(qty <= 0)
            {
                errors.Add("Quantity is not greater than 0");
            }

            return new ValidationResult(errors);
        }
    }
}