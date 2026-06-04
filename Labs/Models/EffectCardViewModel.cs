#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using DMBBootstrapBuilder;

#endregion

namespace DMBEffectBuilderLabs.Models
{
    /// <summary>
    ///     Describes a section effect preview card rendered by the DMBEffectBuilder labs pages.
    /// </summary>
    public class EffectCardViewModel
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the medium-breakpoint column size for the preview card.
        /// </summary>
        public ColSize ColSize { get; set; } = ColSize.Col4;

        /// <summary>
        ///     Gets or sets the short effect description.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the optional API documentation URL for the effect.
        /// </summary>
        public string DocUrl { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the action that applies the effect to the preview <see cref="SectionBuilder"/>.
        /// </summary>
        public required Func<SectionBuilder, SectionBuilder> EffectAction { get; set; }

        /// <summary>
        ///     Gets or sets the preview height in pixels.
        /// </summary>
        public uint HeightPx { get; set; } = 180;

        /// <summary>
        ///     Gets or sets the Bootstrap icon class displayed with the card title.
        /// </summary>
        public string Icon { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the effect title.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the sample API usage text.
        /// </summary>
        public string Usage { get; set; } = string.Empty;

        #endregion
    }
}
