namespace Innergy_app.Infrastructure.Channels
{
    using System.Threading.Channels;

    /// <summary>
    /// Channel manager interface.
    /// </summary>
    public interface IChannelManager
    {
        /// <summary>
        /// Gets the channel.
        /// </summary>
        Channel<string> Channel { get; }
    }
}