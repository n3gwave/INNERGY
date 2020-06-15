namespace Innergy_app.Infrastructure.HostedServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Channels;
    using System.Threading.Tasks;
    using Innergy_app.Infrastructure.Channels;
    using Innergy_app.Infrastructure.Parsers;
    using Innergy_app.Models.Entities;
    using Innergy_app.Models.ValueObjects;
    using Innergy_app.Notifications;
    using MediatR;
    using Microsoft.Extensions.Hosting;

    public class MigrationEngineHostedService : BackgroundService, IRunnableService
    {
        /// <summary>
        /// The channel reader
        /// </summary>
        private readonly ChannelReader<string> channelReader;

        /// <summary>
        /// The mediator
        /// </summary>
        private readonly IMediator mediator;

        /// <summary>
        /// The stock reader
        /// </summary>
        private readonly IProductStockParser stockParser;

        /// <summary>
        /// Initializes a new instance of the <see cref="MigrationEngineHostedService" /> class.
        /// </summary>
        /// <param name="channelManager">The channel manager.</param>
        /// <param name="mediator">The mediator.</param>
        /// <param name="stockParser">The stock reader.</param>
        public MigrationEngineHostedService(IChannelManager channelManager, IMediator mediator, IProductStockParser stockParser)
        {
            this.channelReader = channelManager.Channel.Reader;
            this.mediator = mediator;
            this.stockParser = stockParser;
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
            await foreach (string item in this.channelReader.ReadAllAsync(stoppingToken))
            {
                Product product;
                Dictionary<string, int> stockCollection;
                
                try
                {
                    (product, stockCollection) = this.stockParser.Parse(item);
                }
                catch (Exception)
                {
                    // swallow exception.. precondition states data should be well formatted, hence skipping exception handling.
                    continue;
                }

                foreach (var warehouseStock in stockCollection)
                {
                    var stock = new Stock(warehouseStock.Value);

                    await this.mediator.Publish(new StockAddedEvent(warehouseStock.Key, product, stock), stoppingToken);
                }
            }
        }
    }
}
