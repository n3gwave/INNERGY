namespace Innergy_app.Services.Readers
{
    using System.Collections.Generic;
    using System.Threading;
    using Innergy_app.Models.Enums;

    /// <summary>
    /// Data reader interface.
    /// </summary>
    public interface IMigrationDataReader
    {
        /// <summary>
        /// Tries read data.
        /// </summary>
        /// <returns>
        /// Reader result.
        /// </returns>
        public IAsyncEnumerable<(ReadResult, string)> ReadAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}