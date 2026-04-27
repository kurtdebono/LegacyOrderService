using System;
using LegacyOrderService.Services;
using LegacyOrderService.Models;

namespace LegacyOrderService.Validators
{
    public class OrderValidator
    {
        private readonly IProductService _productService;

        public OrderValidator(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// </summary>
        /// Validates the order input provided by the user, ensuring all required fields 
        /// are present, correctly formatted and meet rules.
        /// <param name="customerName">The name of the customer placing the order.</param>
        /// <param name="product">The name of the product being ordered.</param>
        /// <param name="quantityInput">The raw quantity input provided by the user.</param>
        /// <returns>A <see cref="ValidationResult"/> indicating whether the input is valid, 
        /// and also providing a list of errors and the parsed quantity when applicable.</returns>
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