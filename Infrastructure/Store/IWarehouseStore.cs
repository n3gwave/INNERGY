namespace Innergy_app.Infrastructure.Store
{
    using System.Collections.Generic;
    using Innergy_app.Models.Entities;
    using Innergy_app.Models.ValueObjects;

    /// <summary>
    /// Warehouse store interface.
    /// </summary>
    public interface IWarehouseStore
    {
        /// <summary>
        /// Gets the total.
        /// </summary>
        Stock Total { get; }

        /// <summary>
        /// Sets the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="stock">The stock.</param>
        void Set(Product product, Stock stock);

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <returns>List of products and their stock</returns>
        IDictionary<Product, Stock> GetItems();
    }
}
