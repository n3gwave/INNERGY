namespace Innergy_app.Infrastructure
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Executive service
    /// </summary>
    public interface IRunnableService
    {
        /// <summary>
        /// Handles the asynchronous.
        /// </summary>
        /// <param name="stoppingToken">The stopping token.</param>
        /// <returns>Awaitable task.</returns>
        Task RunAsync(CancellationToken stoppingToken);
    }
}
