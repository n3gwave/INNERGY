namespace Innergy_app.Factories
{
    using Innergy_app.Models.Entities;

    /// <summary>
    /// Warehouse factory.
    /// </summary>
    public interface IWarehouseFactory
    {
        /// <summary>
        /// Creates warehouse class.
        /// </summary>
        /// <param name="warehouseId">The warehouse identifier.</param>
        /// <returns>
        /// Warehouse.
        /// </returns>
        Warehouse Create(string warehouseId);
    }
}