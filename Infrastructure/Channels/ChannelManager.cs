namespace Innergy_app.Infrastructure.Channels
{
    using System.Threading.Channels;

    /// <summary>
    /// Channel manager.
    /// </summary>
    /// <seealso cref="IChannelManager" />
    public class ChannelManager : IChannelManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelManager"/> class.
        /// </summary>
        public ChannelManager()
        {
            this.Channel = System.Threading.Channels.Channel.CreateUnbounded<string>(new UnboundedChannelOptions()
            {
                SingleReader = true,
                SingleWriter = true,
            });

        }

        /// <summary>
        /// Gets the channel.
        /// </summary>
        public Channel<string> Channel { get; }
    }
}
