namespace Innergy_app.Factories
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Innergy_app.Infrastructure.Store;
    using Innergy_app.Models.Entities;

    /// <summary>
    /// Warehouse factory,
    /// </summary>
    /// <seealso cref="IWarehouseFactory" />
    public class WarehouseFactory : IWarehouseFactory
    {
        /// <summary>
        /// The service provider
        /// </summary>
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="WarehouseFactory" /> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public WarehouseFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Creates warehouse class.
        /// </summary>
        /// <param name="warehouseId">The warehouse identifier.</param>
        /// <returns>
        /// Warehouse.
        /// </returns>
        public Warehouse Create(string warehouseId)
        {
            var warehouseStore = serviceProvider.GetService<IWarehouseStore>();
            return new Warehouse(warehouseStore, warehouseId);
        }
    }
}
