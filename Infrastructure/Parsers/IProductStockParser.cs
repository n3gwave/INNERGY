namespace Innergy_app.Infrastructure.Parsers
{
    using System.Collections.Generic;
    using Innergy_app.Models.Entities;

    /// <summary>
    /// product stock parser.
    /// </summary>
    public interface IProductStockParser
    {
        /// <summary>
        /// Reads the specified stock entry.
        /// </summary>
        /// <param name="input">The stock entry.</param>
        /// <returns>Deserialized stock data.</returns>
        (Product product, Dictionary<string, int>) Parse(string input);
    }
}
