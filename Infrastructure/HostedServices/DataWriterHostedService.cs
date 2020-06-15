namespace Innergy_app.Infrastructure.HostedServices
{
    using System.Threading;
    using System.Threading.Channels;
    using System.Threading.Tasks;
    using Innergy_app.Infrastructure.Channels;
    using Innergy_app.Infrastructure.Repositories;
    using Innergy_app.Models.Entities;
    using Innergy_app.Services.Writers;
    using Microsoft.Extensions.Hosting;

    public class DataWriterHostedService : BackgroundService, IRunnableService
    {
        /// <summary>
        /// The channel reader
        /// </summary>
        private readonly ChannelReader<string> channelReader;

        /// <summary>
        /// The data writer
        /// </summary>
        private readonly IMigrationDataWriter dataWriter;

        /// <summary>
        /// The warehouse provider
        /// </summary>
        private readonly IWarehouseRepository warehouseRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationEngineHostedService" /> class.
        /// </summary>
        /// <param name="channelManager">The channel manager.</param>
        /// <param name="dataWriter">The data writer.</param>
        /// <param name="warehouseRepository">The warehouse provider.</param>
        public DataWriterHostedService(IChannelManager channelManager, IMigrationDataWriter dataWriter, IWarehouseRepository warehouseRepository)
        {
            this.dataWriter = dataWriter;
            this.warehouseRepository = warehouseRepository;
            this.channelReader = channelManager.Channel.Reader;
        }

        /// <summary>
        /// This method is called when the <see cref="T:Microsoft.Extensions.Hosting.IHostedService" /> starts. The implementation should return a task that represents
        /// the lifetime of the long running operation(s) being performed.
        /// </summary>
        /// <param name="stoppingToken">Triggered when <see cref="M:Microsoft.Extensions.Hosting.IHostedService.StopAsync(System.Threading.CancellationToken)" /> is called.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task" /> that represents the long running operations.</returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await this.RunAsync(stoppingToken);
        }

        /// <summary>
        /// Handles the asynchronous.
        /// </summary>
        /// <param name="stoppingToken">The stopping token.</param>
        /// <returns>Awaitable task.</returns>
        public async Task RunAsync(CancellationToken stoppingToken)
        {
            await this.channelReader.Completion;

            await foreach (Warehouse warehouse in this.warehouseRepository.GetAll(cancellationToken: stoppingToken))
            {
                await this.dataWriter.WriteAsync(warehouse);
            }
        }
    }
}
