namespace Innergy_app.Utilities.Comparers
{
    using System;
    using System.Collections.Generic;
    using Innergy_app.Models.Entities;

    public class DefaultWarehouseComparer : IComparer<Warehouse>
    {
        /// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
        /// <param name="first">The first object to compare.</param>
        /// <param name="second">The second object to compare.</param>
        /// <returns>A signed integer that indicates the relative values of <paramref name="first" /> and <paramref name="second" />, as shown in the following table.
        ///  Value
        ///  Meaning
        ///  Less than zero
        /// <paramref name="first" /> is less than <paramref name="second" />.
        ///  Zero
        /// <paramref name="first" /> equals <paramref name="second" />.
        ///  Greater than zero
        /// <paramref name="first" /> is greater than <paramref name="second" />.</returns>
        public int Compare(Warehouse first, Warehouse second)
        {
            if (first == null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second == null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (first.Total < second.Total || (first.Total == second.Total) && string.Compare(first.Id, second.Id, StringComparison.OrdinalIgnoreCase) < 0)
            {
                return -1;
            }

            if (first.Total > second.Total || (first.Total == second.Total) && string.Compare(first.Id, second.Id, StringComparison.OrdinalIgnoreCase) > 0)
            {
                return 1;
            }

            return 0;
        }
    }
}
