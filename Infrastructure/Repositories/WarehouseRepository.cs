namespace Innergy_app.Infrastructure.Repositories
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;
    using Innergy_app.Factories;
    using Innergy_app.Models.Entities;
    using Innergy_app.Utilities.Comparers;

    public class WarehouseRepository : IWarehouseRepository
    {
        /// <summary>
        /// The warehouse factory
        /// </summary>
        private readonly IWarehouseFactory warehouseFactory;

        /// <summary>
        /// The warehouses
        /// </summary>
        private readonly ConcurrentDictionary<string, Warehouse> warehouses = new ConcurrentDictionary<string, Warehouse>();

        /// <summary>
        /// Initializes a new instance of the <see cref="WarehouseRepository"/> class.
        /// </summary>
        /// <param name="warehouseFactory">The warehouse factory.</param>
        public WarehouseRepository(IWarehouseFactory warehouseFactory)
        {
            this.warehouseFactory = warehouseFactory;
        }

        /// <summary>
        /// Gets the or add warehouse.
        /// </summary>
        /// <param name="warehouseId">The warehouse identifier.</param>
        /// <returns>Warehouse.</returns>
        public Task<Warehouse> GetOrAdd(string warehouseId)
        {
            return Task.FromResult(this.warehouses.GetOrAdd(warehouseId, id => this.warehouseFactory.Create(id)));
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>Get all warehouses</returns>
        public async IAsyncEnumerable<Warehouse> GetAll(IComparer<Warehouse> comparer = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            if (comparer == null)
            {
                comparer = new DefaultWarehouseComparer();
            }

            var sortedList = this.warehouses.Values.OrderByDescending(x => x, comparer);
            foreach (var warehouse in sortedList)
            {
                cancellationToken.ThrowIfCancellationRequested();

                yield return await new ValueTask<Warehouse>(warehouse);
            }
        }
    }
}