namespace Innergy_app.Services.Readers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;
    using Innergy_app.Models.Enums;

    /// <summary>
    /// Migration data reader.
    /// </summary>
    /// <seealso cref="IMigrationDataReader" />
    public class FileMigrationDataReader : IMigrationDataReader
    {
        /// <summary>
        /// Tries the read.
        /// </summary>
        /// <returns>
        /// Reader result.
        /// </returns>
        public async IAsyncEnumerable<(ReadResult, string)> ReadAsync([EnumeratorCancellation] CancellationToken cancellationToken = default(CancellationToken))
        {
            using var reader = new StreamReader(Path.Combine(Environment.CurrentDirectory, "data.txt"));
            
            // Redirect standard input from the console to the input file.
            Console.SetIn(reader);

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