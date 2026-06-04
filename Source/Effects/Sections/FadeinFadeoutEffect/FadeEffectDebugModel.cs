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
    ///     Provides debug panel values for the fade section effect.
    /// </summary>
    [DebugModel("Fade effect", CodePattern = ".FadeInFadeOutEffect(\"{0}\", {1}m, {2}m, {3}m)")]
    public sealed class FadeEffectDebugModel
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
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-fade-image")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-fade-image", ValuePrefix = "url(", ValueSuffix = ")")]
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the maximum darkness applied by the fade effect.
        /// </summary>
        [DebugProperty(Label = "Max darkness", HelpText = "0 = no darkening, 1 = maximum darkening.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-fade-max-darkness")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-fade-max-darkness")]
        public decimal MaxDarkness { get; set; } = 0.65m;

        /// <summary>
        ///     Gets or sets the zoom scale applied by the fade effect.
        /// </summary>
        [DebugProperty(Label = "Scale", HelpText = "0 = normal size, 1 = maximum zoom.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-fade-scale")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-fade-scale")]
        public decimal Scale { get; set; } = 0m;

        /// <summary>
        ///     Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        public string SectionId { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the vertical image offset used by the fade effect.
        /// </summary>
        [DebugProperty(Label = "Vertical offset (%)", HelpText = "Positive values move the image downward, negative upward.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-fade-offset-y")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-fade-offset-y", ValueSuffix = "%")]
        public decimal VerticalOffset { get; set; } = 0m;

        #endregion
    }
}