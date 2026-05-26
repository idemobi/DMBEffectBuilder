#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj RetroTvChannel.cs create at 2026/05/11
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    /// <summary>
    /// Represents a single channel displayed on the retro TV screen.
    /// A channel is either a solid color with metadata, or an image with a title and description.
    /// </summary>
    public sealed class RetroTvChannel
    {
        public string? ImageSrc { get; private set; }
        public string? Color    { get; private set; }
        public string  Name     { get; private set; } = string.Empty;
        public string  Desc     { get; private set; } = string.Empty;

        /// <summary>Creates a channel that fills the screen with a solid color and displays name, hex value and RGB bars.</summary>
        public static RetroTvChannel FromColor(string name, string hexColor, string desc = "")
            => new() { Name = name, Color = hexColor, Desc = desc };

        /// <summary>Creates a channel that fills the screen with an image and overlays the name and description.</summary>
        public static RetroTvChannel FromImage(string imageSrc, string name, string desc = "")
            => new() { Name = name, ImageSrc = imageSrc, Desc = desc };
    }
}
