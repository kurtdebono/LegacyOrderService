using LegacyOrderService.Models;

namespace LegacyOrderService.Interfaces
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Saves the provided <paramref name="order"/> to the database.
        /// </summary>
        /// <param name="order">The order to be saved.</param>
        void Save(Order order);
        void SeedBadData();
    }
}