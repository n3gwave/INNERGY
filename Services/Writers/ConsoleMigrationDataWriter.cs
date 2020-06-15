namespace Innergy_app.Services.Writers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Innergy_app.Models.Entities;
    using Innergy_app.Models.ValueObjects;

    /// <summary>
    /// Console migration data writer.
    /// </summary>
    /// <seealso cref="IMigrationDataWriter" />
    public class ConsoleMigrationDataWriter : IMigrationDataWriter
    {
        /// <summary>
        /// The is first
        /// </summary>
        private bool isFirst = true;

        /// <summary>
        /// Writes the asynchronous.
        /// </summary>
        /// <returns>Awaitable task.</returns>
        public ValueTask WriteAsync(Warehouse warehouse)
        {
            this.EnsureLineIsAppended();

            Console.WriteLine($"{warehouse.Id} (total {warehouse.Total})");

            foreach (var (product, stock) in OrderBy(warehouse))
            {
                Console.WriteLine($"{product.Id}: {stock}");
            }


            return default;
        }

        /// <summary>
        /// Ensures the line is appended.
        /// </summary>
        private void EnsureLineIsAppended()
        {
            if (!this.isFirst)
            {
                Console.WriteLine();
            }
            else
            {
                this.isFirst = false;
            }
        }

        /// <summary>
        /// Orders the by.
        /// </summary>
        /// <param name="warehouse">The warehouse.</param>
        /// <returns></returns>
        private static IOrderedEnumerable<KeyValuePair<Product, Stock>> OrderBy(Warehouse warehouse)
        {
            return warehouse.GetStock().OrderBy(x => x.Key.Id, StringComparer.OrdinalIgnoreCase);
        }
    }
}