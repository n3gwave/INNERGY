namespace Innergy_app
{
    using System;
    using System.Threading.Channels;
    using System.Threading.Tasks;
    using Innergy_app.Factories;
    using Innergy_app.Infrastructure.Channels;
    using Innergy_app.Infrastructure.HostedServices;
    using Innergy_app.Infrastructure.Parsers;
    using Innergy_app.Infrastructure.Repositories;
    using Innergy_app.Infrastructure.Store;
    using Innergy_app.NotificationHandlers;
    using Innergy_app.Services.Readers;
    using Innergy_app.Services.Writers;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Program entry point
    /// </summary>
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Channel<string> channel = Channel.CreateUnbounded<string>();

            var hostBuilder = new HostBuilder();
            hostBuilder
                .UseConsoleLifetime()
                    .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<HostOptions>(option => { option.ShutdownTimeout = TimeSpan.FromSeconds(5); });

                    services.AddSingleton<IChannelManager, ChannelManager>();
                    services.AddSingleton<IProductStockParser, ProductStockParser>();

                    services.AddSingleton<IMigrationDataReader, ConsoleMigrationDataReader>();
                    //services.AddSingleton<IMigrationDataReader, FileMigrationDataReader>();
                    services.AddSingleton<IMigrationDataWriter, ConsoleMigrationDataWriter>();
                    
                    services.AddTransient<IWarehouseStore, DefaultInMemoryWarehouseStore>();
                    services.AddSingleton<IWarehouseFactory, WarehouseFactory>();
                    services.AddSingleton<IWarehouseRepository, WarehouseRepository>();

                    services.AddHostedService<DataReaderHostedService>();
                    services.AddHostedService<DataWriterHostedService>();
                    services.AddHostedService<MigrationEngineHostedService>();
                    
                    services.AddMediatR(typeof(WarehouseHandler));
                });

            await hostBuilder.RunConsoleAsync();
        }
    }
}
