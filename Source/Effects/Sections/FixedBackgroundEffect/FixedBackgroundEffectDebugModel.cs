#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.Collections.Generic;
using DMBBootstrapBuilder;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Provides debug panel values for the fixed background section effect.
    /// </summary>
    [DebugModel("Fixed background", CodePattern = ".FixedBackgroundEffect(\"{0}\")")]
    public sealed class FixedBackgroundEffectDebugModel
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the selectable image urls values exposed by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)]
        public IReadOnlyList<string> AvailableImageUrls { get; set; } = Array.Empty<string>();

        /// <summary>
        ///     Gets or sets the image URL used by the visual effect preview.
        /// </summary>
        [DebugProperty(Label = "Image", HelpText = "Background image from /wwwroot/images/test.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-fixed-bg-image")]
        [DebugPropertyTarget(DebugTarget.Style, "backgroundImage", ValuePrefix = "url(", ValueSuffix = ")")]
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}