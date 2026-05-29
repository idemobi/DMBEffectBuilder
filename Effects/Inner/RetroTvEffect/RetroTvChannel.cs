#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Represents a single channel displayed on the retro TV screen.
    ///     A channel is either a solid color with metadata, or an image with a title and description.
    /// </summary>
    public sealed class RetroTvChannel
    {
        #region Static methods

        /// <summary>
        ///     Creates a channel that fills the screen with a solid color and displays name, hex value, and RGB bars.
        /// </summary>
        /// <param name="name">The channel title rendered by the overlay.</param>
        /// <param name="hexColor">The hexadecimal color used as the channel background.</param>
        /// <param name="desc">The optional channel description rendered by the overlay.</param>
        /// <returns>A configured <see cref="RetroTvChannel" /> using a solid color screen.</returns>
        public static RetroTvChannel FromColor(string name, string hexColor, string desc = "")
            => new() { Name = name, Color = hexColor, Desc = desc };

        /// <summary>
        ///     Creates a channel that fills the screen with an image and overlays the name and description.
        /// </summary>
        /// <param name="imageSrc">The image source URL rendered as the channel screen content.</param>
        /// <param name="name">The channel title rendered by the overlay.</param>
        /// <param name="desc">The optional channel description rendered by the overlay.</param>
        /// <returns>A configured <see cref="RetroTvChannel" /> using an image screen.</returns>
        public static RetroTvChannel FromImage(string imageSrc, string name, string desc = "")
            => new() { Name = name, ImageSrc = imageSrc, Desc = desc };

        #endregion

        #region Instance fields and properties

        /// <summary>
        ///     Gets the optional solid color rendered as the channel screen content.
        /// </summary>
        public string? Color { get; private set; }

        /// <summary>
        ///     Gets the channel description rendered by the retro TV overlay.
        /// </summary>
        public string Desc { get; private set; } = string.Empty;

        /// <summary>
        ///     Gets the optional image source rendered as the channel screen content.
        /// </summary>
        public string? ImageSrc { get; private set; }

        /// <summary>
        ///     Gets the channel title rendered by the retro TV overlay.
        /// </summary>
        public string Name { get; private set; } = string.Empty;

        #endregion
    }
}