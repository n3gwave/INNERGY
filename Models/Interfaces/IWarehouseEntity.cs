namespace Innergy_app.Models.Interfaces
{
    using Innergy_app.Models.Entities;
    using Innergy_app.Models.ValueObjects;

    /// <summary>
    /// Warehouse interface,
    /// </summary>
    public interface IWarehouseEntity
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Sets the stock.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="stock">The stock.</param>
        void Apply(Product product, Stock stock);
    }
}