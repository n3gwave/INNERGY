namespace Innergy_app.Services.Writers
{
    using System.Threading.Tasks;
    using Innergy_app.Models.Entities;

    /// <summary>
    /// Data writer
    /// </summary>
    public interface IMigrationDataWriter
    {
        /// <summary>
        /// Writes the asynchronous.
        /// </summary>
        /// <returns>Awaitable task.</returns>
        public ValueTask WriteAsync(Warehouse warehouse);
    }
}
