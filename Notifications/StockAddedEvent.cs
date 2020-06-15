namespace Innergy_app.Notifications
{
    using System;
    using Innergy_app.Models;
    using Innergy_app.Models.Entities;
    using Innergy_app.Models.ValueObjects;
    using MediatR;

    /// <summary>
    /// Stock added event.
    /// </summary>
    /// <seealso cref="INotification" />
    public class StockAddedEvent : INotification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StockAddedEvent"/> class.
        /// </summary>
        /// <param name="warehouseId">The warehouse identifier.</param>
        /// <param name="product">The product.</param>
        /// <param name="stockCount">The count.</param>
        /// <exception cref="ArgumentNullException">
        /// warehouseId
        /// or
        /// productId
        /// </exception>
        public StockAddedEvent(string warehouseId, Product product, Stock stockCount)
        {
            if (string.IsNullOrWhiteSpace(warehouseId))
            {
                throw new ArgumentNullException(nameof(warehouseId));
            }
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            this.WarehouseId = warehouseId;
            this.Product = product;
            this.StockCount = stockCount;
        }

        /// <summary>
        /// Gets the warehouse identifier.
        /// </summary>
        public string WarehouseId { get; }

        /// <summary>
        /// Gets the product.
        /// </summary>
        public Product Product { get; }

        /// <summary>
        /// Gets the count.
        /// </summary>
        public Stock StockCount { get; }
    }
}
