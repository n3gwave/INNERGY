namespace Innergy_app.Infrastructure.HostedServices
{
    using System.Threading;
    using System.Threading.Channels;
    using System.Threading.Tasks;
    using Innergy_app.Infrastructure.Channels;
    using Innergy_app.Models.Enums;
    using Innergy_app.Services.Readers;
    using Microsoft.Extensions.Hosting;

    public class DataReaderHostedService : BackgroundService, IRunnableService
    {
        /// <summary>
        /// The migration data reader
        /// </summary>
        private readonly IMigrationDataReader migrationDataReader;

        /// <summary>
        /// The channel reader
        /// </summary>
        private readonly ChannelWriter<string> channelManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationEngineHostedService" /> class.
        /// </summary>
        /// <param name="migrationDataReader">The migration data reader.</param>
        /// <param name="channelManager">The channel reader.</param>
        public DataReaderHostedService(IMigrationDataReader migrationDataReader, IChannelManager channelManager)
        {
            this.migrationDataReader = migrationDataReader;
            this.channelManager = channelManager.Channel.Writer;
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
        /// Runs the service.
        /// </summary>
        /// <param name="stoppingToken">The stopping token.</param>
        /// <returns>Awaitable task.</returns>
        public async Task RunAsync(CancellationToken stoppingToken)
        {
            await foreach ((ReadResult result, string item) in this.migrationDataReader.ReadAsync(stoppingToken))
            {
                switch (result)
                {
                    case ReadResult.Comment:
                        break;
                    case ReadResult.Data:
                        await this.channelManager.WriteAsync(item, stoppingToken);
                        break;
                    default:
                        this.channelManager.Complete();
                        break;

                }
            }
        }
    }
}