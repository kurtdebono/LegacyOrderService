namespace LegacyOrderService.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public decimal GetPrice(string productName)
        {        
            return _productRepository.GetPrice(productName);
        }

        /// <inheritdoc />
        public bool Exists(string productName)
        {            
            return _productRepository.Exists(productName);
        }
    }
}