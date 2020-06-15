namespace Innergy_app.Infrastructure.Parsers
{
    using System;
    using System.Collections.Generic;
    using Innergy_app.Models.Entities;
    using Innergy_app.Utilities;

    /// <summary>
    /// Product stock parser.
    /// </summary>
    /// <seealso cref="IProductStockParser" />
    public class ProductStockParser : IProductStockParser
    {
        /// <summary>
        /// Reads the specified stock entry.
        /// </summary>
        /// <param name="input">The raw input.</param>
        /// <returns>Deserialized stock data.</returns>
        public (Product product, Dictionary<string, int>) Parse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(nameof(input));
            }

            var dataElement = input.Split(';');

            var product = new Product(dataElement[1], dataElement[0]);

            var dictionary = ParseWarehouseStocks(dataElement[2]);

            return (product, dictionary);
        }

        /// <summary>
        /// Parses the warehouse stocks.
        /// </summary>
        /// <param name="stockData">The stock data.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">invalid input data - warehouseId
        /// or
        /// Could not parse stock data - count</exception>
        private static Dictionary<string, int> ParseWarehouseStocks(string stockData)
        {
            var dictionary = new Dictionary<string, int>();

            var stockInfo = stockData.Split('|');

            foreach (var stockItem in stockInfo)
            {
                var (warehouseId, count, _) = stockItem.Split(',');

                if (string.IsNullOrEmpty(warehouseId))
                {
                    throw new ArgumentException("invalid input data", nameof(warehouseId));
                }

                if (!int.TryParse(count, out int stock))
                {
                    throw new ArgumentException("Could not parse stock data", nameof(count));
                }

                dictionary.Add(warehouseId, stock);
            }

            return dictionary;
        }
    }
}