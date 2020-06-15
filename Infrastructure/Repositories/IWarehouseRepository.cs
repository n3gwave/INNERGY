namespace Innergy_app.Infrastructure.Repositories
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Innergy_app.Models.Entities;

    /// <summary>
    /// Warehouse provider
    /// </summary>
    public interface IWarehouseRepository
    {
        /// <summary>
        /// Gets the or add warehouse.
        /// </summary>
        /// <param name="warehouseId">The warehouse identifier.</param>
        /// <returns>Warehouse.</returns>
        Task<Warehouse> GetOrAdd(string warehouseId);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// Get all warehouses
        /// </returns>
        IAsyncEnumerable<Warehouse> GetAll(IComparer<Warehouse> comparer = null, CancellationToken cancellationToken = default);
    }
}
