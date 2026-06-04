#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using DMBBootstrapBuilder;
using DMBEffectBuilder;
using DMBPageBuilder;

#endregion

namespace DMBEffectBuilderLabs.Models
{
    /// <summary>
    ///     Describes an inner title effect preview card rendered by the DMBEffectBuilder labs pages.
    /// </summary>
    public class InnerTitleEffectCardViewModel
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the background variant used behind the title preview.
        /// </summary>
        public VariantStyle BackgroundVariant { get; set; } = VariantStyle.Normal;

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
        ///     Gets or sets the preview height in pixels.
        /// </summary>
        public uint HeightPx { get; set; } = 180;

        /// <summary>
        ///     Gets or sets the Bootstrap icon class displayed with the card title.
        /// </summary>
        public string Icon { get; set; } = "bi-type";

        /// <summary>
        ///     Gets or sets the sample title text.
        /// </summary>
        public string Label { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the action that applies the effect to the preview <see cref="SectionInnerEffectTitle"/>.
        /// </summary>
        public required Func<SectionInnerEffectTitle, SectionInnerEffectTitle> TitleConfig { get; set; }

        /// <summary>
        ///     Gets or sets the sample API usage text.
        /// </summary>
        public string Usage { get; set; } = string.Empty;

        #endregion
    }
}
