#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using DMBBootstrapBuilder;
using DMBEffectBuilder;

#endregion

namespace DMBEffectBuilderLabs.Models
{
    /// <summary>
    ///     Describes a video section effect preview card rendered by the DMBEffectBuilder labs pages.
    /// </summary>
    public class VideoEffectCardViewModel
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the column size for the preview card.
        /// </summary>
        public ColSize ColSize { get; set; } = ColSize.Col6;

        /// <summary>
        ///     Gets or sets the short effect description.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the optional API documentation URL for the effect.
        /// </summary>
        public string DocUrl { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the preview height in pixels.
        /// </summary>
        public uint HeightPx { get; set; } = 300;

        /// <summary>
        ///     Gets or sets the Bootstrap icon class displayed with the card title.
        /// </summary>
        public string Icon { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets a value indicating whether the video preview should render with an overlay.
        /// </summary>
        public bool Overlay { get; set; } = true;

        /// <summary>
        ///     Gets or sets the effect title.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the sample API usage text.
        /// </summary>
        public string Usage { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the optional action used to configure the video preview header.
        /// </summary>
        public Func<VideoSectionEffectHeader, VideoSectionEffectHeader>? VideoHeaderConfig { get; set; }

        /// <summary>
        ///     Gets or sets the video URL rendered in the preview.
        /// </summary>
        public required string VideoUrl { get; set; }

        #endregion
    }
}
