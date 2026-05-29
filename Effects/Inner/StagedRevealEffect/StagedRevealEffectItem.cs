#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Represents one image or text item that can be displayed inside a staged reveal slot.
    /// </summary>
    public class StagedRevealEffectItem
    {
        #region Static methods

        /// <summary>Creates an item that displays an image.</summary>
        /// <param name="src">The image source URL.</param>
        /// <param name="alt">The alternative text rendered for the image.</param>
        /// <returns>A staged reveal item configured for image content.</returns>
        public static StagedRevealEffectItem FromImage(string src, string alt = "")
            => new() { ContentType = StagedRevealEffectContentType.Image, ImageSrc = src, AltText = alt };

        /// <summary>Creates an item that displays text.</summary>
        /// <param name="text">The text content rendered inside the staged reveal slot.</param>
        /// <returns>A staged reveal item configured for text content.</returns>
        public static StagedRevealEffectItem FromText(string text)
            => new() { ContentType = StagedRevealEffectContentType.Text, TextContent = text };

        #endregion

        #region Instance fields and properties

        /// <summary>Alt text for the image.</summary>
        public string? AltText { get; private set; }

        /// <summary>The type of content held by this item.</summary>
        public StagedRevealEffectContentType ContentType { get; private set; }

        /// <summary>Image URL, set when <see cref="ContentType" /> is <see cref="StagedRevealEffectContentType.Image" />.</summary>
        public string? ImageSrc { get; private set; }

        /// <summary>Text content, set when <see cref="ContentType" /> is <see cref="StagedRevealEffectContentType.Text" />.</summary>
        public string? TextContent { get; private set; }

        #endregion

        #region Instance constructors and destructors

        private StagedRevealEffectItem()
        {
        }

        #endregion
    }
}