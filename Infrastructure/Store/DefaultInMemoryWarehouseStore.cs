namespace Innergy_app.Infrastructure.Store
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using Innergy_app.Models.Entities;
    using Innergy_app.Models.ValueObjects;

    public class DefaultInMemoryWarehouseStore : IWarehouseStore
    {
        /// <summary>
        /// The stock collection
        /// </summary>
        readonly ConcurrentDictionary<Product, int> stockCollection = new ConcurrentDictionary<Product, int>();

        /// <summary>
        /// Gets the total.
        /// </summary>
        public Stock Total => new Stock(this.stockCollection.Values.Sum(x => x));

        /// <summary>
        /// Sets the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="stock">The stock.</param>
        public void Set(Product product, Stock stock)
        {
            this.stockCollection.AddOrUpdate(product, stock.Count, (p, oldValue) => stock.Count);
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <returns>List of products and their stock</returns>
        public IDictionary<Product, Stock> GetItems()
        {
            return this.stockCollection.ToDictionary(x => x.Key, x => new Stock(x.Value));
        }
    }
}