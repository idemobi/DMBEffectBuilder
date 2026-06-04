#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using Microsoft.AspNetCore.Html;

#endregion

namespace DMBEffectBuilderLabs.Models
{
    /// <summary>
    ///     Describes an image effect preview card rendered by the DMBEffectBuilder labs pages.
    /// </summary>
    public class ImageEffectCardViewModel
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets a value indicating whether the image preview should be centered in the card.
        /// </summary>
        public bool Centered { get; set; } = false;

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
        ///     Gets or sets the Bootstrap icon class displayed with the card title.
        /// </summary>
        public string Icon { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the rendered image preview content.
        /// </summary>
        public IHtmlContent ImageContent { get; set; } = default!;

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
