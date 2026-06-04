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
    ///     Provides debug panel values for the highlight sweep section effect.
    /// </summary>
    [DebugModel("Highlight Sweep", CodePattern = ".HighlightSweepEffect({0}m, \"{1}\")")]
    public sealed class HighlightSweepEffectDebugModel
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
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-sweep-image")]
        [DebugPropertyTarget(DebugTarget.Style, "backgroundImage", ValuePrefix = "url(", ValueSuffix = ")")]
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the opacity value used by the effect.
        /// </summary>
        [DebugProperty(Label = "Opacity", HelpText = "Opacity between 0 and 1", Target = DebugTarget.DataAttribute, TargetName = "data-sweep-opacity", InputType = DebugInputType.Number, ValueSuffix = "")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-sweep-opacity")]
        public decimal Opacity { get; set; } = 0.3m;

        /// <summary>
        ///     Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)]
        public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}