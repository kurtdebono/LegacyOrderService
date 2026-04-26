using Xunit;
using Moq;

using System;

using LegacyOrderService.Interfaces;
using LegacyOrderService.Data;
using LegacyOrderService.Services;
using LegacyOrderService.Validators;
using LegacyOrderService.Models;

namespace LegacyOrderService.Tests
{
    public class OrderValidatorTests
    {
        [Fact]
        public void Validate_WithValidInput_ReturnsValidResultAndParsedQuantity()
        {
            string customerName = "Tony";
            string productName = "Gadget";            
            string quantity = "4";
            
            OrderValidator orderValidator = _createValidator(true);
            ValidationResult validationResult = orderValidator.Validate(customerName, productName, quantity);

            Assert.Empty(validationResult.Errors);
            Assert.True(validationResult.IsValid);
            Assert.Equal(4, validationResult.ParsedQuantity);
        }

        [Fact]
        public void Validate_WithEmptyCustomerName_ReturnsInvalidResultWithCorrectParsedQuantity()
        {
            string customerName = "";
            string productName = "Gadget";            
            string quantity = "4";

            OrderValidator orderValidator = _createValidator(true);
            ValidationResult validationResult = orderValidator.Validate(customerName, productName, quantity);

            Assert.NotEmpty(validationResult.Errors);
            Assert.Contains("Customer name is required", validationResult.Errors);
            Assert.False(validationResult.IsValid);
            Assert.Equal(4, validationResult.ParsedQuantity);
        }

        [Fact]
        public void Validate_WithWhitespaceCustomerName_ReturnsInvalidResultWithCorrectParsedQuantity()
        {
            string customerName = "     ";
            string productName = "Gadget";            
            string quantity = "4";

            OrderValidator orderValidator = _createValidator(true);
            ValidationResult validationResult = orderValidator.Validate(customerName, productName, quantity);

            Assert.NotEmpty(validationResult.Errors);
            Assert.Contains("Customer name is required", validationResult.Errors);
            Assert.False(validationResult.IsValid);
            Assert.Equal(4, validationResult.ParsedQuantity);
        }

        [Fact]
        public void Validate_WithEmptyProductName_ReturnsInvalidResultWithCorrectParsedQuantity()
        {
            string customerName = "Tony";
            string productName = "";            
            string quantity = "4";

            OrderValidator orderValidator = _createValidator(false);
            ValidationResult validationResult = orderValidator.Validate(customerName, productName, quantity);

            Assert.NotEmpty(validationResult.Errors);
            Assert.Contains("Product name is required", validationResult.Errors);
            Assert.False(validationResult.IsValid);
            Assert.Equal(4, validationResult.ParsedQuantity);
        }

        [Fact]
        public void Validate_WithWhitespaceProductName_ReturnsInvalidResultWithCorrectParsedQuantity()
        {
            string customerName = "Tony";
            string productName = "     ";            
            string quantity = "4";

            OrderValidator orderValidator = _createValidator(false);
            ValidationResult validationResult = orderValidator.Validate(customerName, productName, quantity);

            Assert.NotEmpty(validationResult.Errors);
            Assert.Contains("Product name is required", validationResult.Errors);
            Assert.False(validationResult.IsValid);
            Assert.Equal(4, validationResult.ParsedQuantity);
        }

        [Fact]
        public void Validate_WithNonExistentProduct_ReturnsInvalidResultWithCorrectParsedQuantity()
        {
            string customerName = "Tony";
            string productName = "Pencil";            
            string quantity = "4";

            OrderValidator orderValidator = _createValidator(false);
            ValidationResult validationResult = orderValidator.Validate(customerName, productName, quantity);

            Assert.NotEmpty(validationResult.Errors);
            Assert.Contains("The product [Pencil] is currently out of stock", validationResult.Errors);
            Assert.False(validationResult.IsValid);
            Assert.Equal(4, validationResult.ParsedQuantity);
        }

        [Fact]
        public void Validate_WithNegativeQuantity_ReturnsInvalidResultAndParsedQuantity()
        {
            string customerName = "Tony";
            string productName = "Gadget";            
            string quantity = "-2";

            OrderValidator orderValidator = _createValidator(true);
            ValidationResult validationResult = orderValidator.Validate(customerName, productName, quantity);

            Assert.Contains("Quantity is not greater than 0", validationResult.Errors);
            Assert.False(validationResult.IsValid);
            Assert.Null(validationResult.ParsedQuantity);
        }

        [Fact]
        public void Validate_WithWhitespaceQuantity_ReturnsInvalidResultAndParsedQuantity()
        {
            string customerName = "Tony";
            string productName = "Gadget";            
            string quantity = "     ";

            OrderValidator orderValidator = _createValidator(true);
            ValidationResult validationResult = orderValidator.Validate(customerName, productName, quantity);

            Assert.Contains("Quantity is required", validationResult.Errors);
            Assert.False(validationResult.IsValid);
            Assert.Null(validationResult.ParsedQuantity);
        }

        [Fact]
        public void Validate_WithEmptyQuantity_ReturnsInvalidResultAndParsedQuantity()
        {
            string customerName = "Tony";
            string productName = "Gadget";            
            string quantity = "";

            OrderValidator orderValidator = _createValidator(true);
            ValidationResult validationResult = orderValidator.Validate(customerName, productName, quantity);

            Assert.Contains("Quantity is required", validationResult.Errors);
            Assert.False(validationResult.IsValid);
            Assert.Null(validationResult.ParsedQuantity);
        }

        [Fact]
        public void Validate_WithInvalidQuantity_ReturnsInvalidResultAndParsedQuantity()
        {
            string customerName = "Tony";
            string productName = "Gadget";            
            string quantity = "Invalid";

            OrderValidator orderValidator = _createValidator(true);

            ValidationResult validationResult = orderValidator.Validate(customerName, productName, quantity);

            Assert.Contains("Quantity is not a valid number", validationResult.Errors);
            Assert.False(validationResult.IsValid);
            Assert.Null(validationResult.ParsedQuantity);
        }

        private static OrderValidator _createValidator(bool productExists = true)
        {
            Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();

            mockProductRepository
                .Setup(r => r.Exists(It.IsAny<string>()))
                .Returns(productExists);

            IProductService productService = new ProductService(mockProductRepository.Object);

            return new OrderValidator(productService);
        }
    }
}