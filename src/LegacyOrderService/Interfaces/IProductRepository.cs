public interface IProductRepository
{
    decimal GetPrice(string productName);

    bool Exists(string productName);
}