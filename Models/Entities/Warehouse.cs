namespace Innergy_app.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Innergy_app.Infrastructure.Store;
    using Innergy_app.Models.Interfaces;
    using Innergy_app.Models.ValueObjects;

    public class Warehouse : IQueryableStock, IWarehouseEntity
    {
        /// <summary>
        /// The store
        /// </summary>
        private readonly IWarehouseStore store;

        /// <summary>
        /// Initializes a new instance of the <see cref="Warehouse" /> class.
        /// </summary>
        /// <param name="store">The store.</param>
        /// <param name="id">The identifier.</param>
        public Warehouse(IWarehouseStore store, string id)
        {
            if (store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }
            
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Invalid warehouse id name.", nameof(id));
            }

            this.store = store;
            this.Id = id;
        }

        /// <summary>
        /// Gets the warehouse identifier.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Sets the stock.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <param name="stock">The stock.</param>
        public void Apply(Product product, Stock stock)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            this.store.Set(product, stock);
        }

        /// <summary>
        /// Gets the total.
        /// </summary>
        public Stock Total => this.store.Total;

        /// <summary>
        /// Gets the entire stock data.
        /// </summary>
        /// <returns>Collection with products stock.</returns>
        public IReadOnlyDictionary<Product, Stock> GetStock()
        {
            return this.store.GetItems().ToImmutableDictionary();
        }
    }
}
