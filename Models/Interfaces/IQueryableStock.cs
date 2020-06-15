namespace Innergy_app.Models.Interfaces
{
    using System.Collections.Generic;
    using Innergy_app.Models.Entities;
    using Innergy_app.Models.ValueObjects;

    /// <summary>
    /// Warehouse interface,
    /// </summary>
    public interface IQueryableStock
    {
        /// <summary>
        /// Gets the total.
        /// </summary>
        Stock Total { get; }

        /// <summary>
        /// Sets the stock.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="stock">The stock.</param>
        void Apply(Product product, Stock stock);

        /// <summary>
        /// Gets the entire stock data.
        /// </summary>
        /// <returns>Collection with products stock.</returns>
        IReadOnlyDictionary<Product, Stock> GetStock();
    }
}