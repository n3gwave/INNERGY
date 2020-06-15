namespace Innergy_app.Services.Readers
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;
    using Innergy_app.Models.Enums;

    /// <summary>
    /// Migration data reader.
    /// </summary>
    /// <seealso cref="IMigrationDataReader" />
    public class ConsoleMigrationDataReader : IMigrationDataReader
    {
        /// <summary>
        /// Tries the read.
        /// </summary>
        /// <returns>
        /// Reader result.
        /// </returns>
        public async IAsyncEnumerable<(ReadResult, string)> ReadAsync([EnumeratorCancellation] CancellationToken cancellationToken = default(CancellationToken))
        {
            while (true)
            {
                var input = await Task.Run(Console.ReadLine, cancellationToken);

                if (string.IsNullOrEmpty(input))
                {
                    yield return (ReadResult.End, null);
                    break;
                }

                if (input.StartsWith("#"))
                {
                    yield return (ReadResult.Comment, null);
                }
                else
                {
                    yield return (ReadResult.Data, input);
                }
            }
        }
    }
}