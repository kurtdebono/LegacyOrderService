namespace LegacyOrderService
{
    public interface IProductService
    {
        decimal GetPrice(string productName);

        /// <summary>
        /// Checks that product <paramref name="productName"/> exists. 
        /// </summary>
        /// <param name="productName">The product to be checked.</param>
        /// <returns>True if product exists. False if otherwise.</returns>
        bool Exists(string productName);
    }
}