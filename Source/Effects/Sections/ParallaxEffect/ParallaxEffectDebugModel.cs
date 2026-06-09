#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Provides debug panel values for the parallax section effect.
    /// </summary>
    [DebugModel("Parallax background", CodePattern = ".ParallaxBackground(\"{0}\", {1}m)")]
    public sealed class ParallaxEffectDebugModel
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the selectable image urls values exposed by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)]
        public IReadOnlyList<string> AvailableImageUrls { get; set; } = Array.Empty<string>();

        /// <summary>
        ///     Gets or sets the baseline vertical background position used by the parallax effect.
        /// </summary>
        [DebugProperty(Label = "Base Y (%)", HelpText = "Default vertical anchor of the background.", Min = "0", Max = "100", Step = "1")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-parallax-base-y")]
        [DebugPropertyTarget(DebugTarget.Style, "backgroundPosition", ValuePrefix = "center calc(", ValueSuffix = "% + 0px)")]
        public decimal BaseY { get; set; } = 50m;

        /// <summary>
        ///     Gets or sets the image URL used by the visual effect preview.
        /// </summary>
        [DebugProperty(Label = "Image", HelpText = "Background image from /wwwroot/images/test.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-parallax-image")]
        [DebugPropertyTarget(DebugTarget.Style, "backgroundImage", ValuePrefix = "url(", ValueSuffix = ")")]
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)]
        public string SectionId { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the animation speed or duration value used by the effect.
        /// </summary>
        [DebugProperty(Label = "Speed", HelpText = "Higher values increase the background movement on scroll.", Min = "0.01", Max = "2", Step = "0.05")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-parallax-speed")]
        public decimal Speed { get; set; } = 0.25m;

        #endregion
    }
}