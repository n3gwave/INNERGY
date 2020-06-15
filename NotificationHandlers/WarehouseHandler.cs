namespace Innergy_app.NotificationHandlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Innergy_app.Infrastructure.Repositories;
    using Innergy_app.Notifications;
    using MediatR;

    /// <summary>
    /// Warehouse handler.
    /// </summary>
    /// <seealso cref="MediatR.INotificationHandler{StockAddedEvent}" />
    public class WarehouseHandler : INotificationHandler<StockAddedEvent>
    {
        /// <summary>
        /// The warehouse provider
        /// </summary>
        private readonly IWarehouseRepository warehouseRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="WarehouseHandler"/> class.
        /// </summary>
        /// <param name="warehouseRepository">The warehouse provider.</param>
        public WarehouseHandler(IWarehouseRepository warehouseRepository)
        {
            this.warehouseRepository = warehouseRepository;
        }

        /// <summary>Handles a notification</summary>
        /// <param name="notification">The notification</param>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task Handle(StockAddedEvent notification, CancellationToken cancellationToken)
        {
            if (notification == null)
            {
                throw new ArgumentNullException(nameof(notification));
            }

            cancellationToken.ThrowIfCancellationRequested();

            var warehouse = await this.warehouseRepository.GetOrAdd(notification.WarehouseId);
            warehouse.Apply(notification.Product, notification.StockCount);
        }
    }
}
