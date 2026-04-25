public interface IProductRepository
{
    double GetPrice(string productName);

    bool Exists(string productName);
}