namespace LegacyOrderService
{
    public interface IProductService
    {
        decimal GetPrice(string productName);
        bool Exists(string productName);
    }
}