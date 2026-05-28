#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj ZoomPanEffectDebugModel.cs create at 2026/04/21
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides debug panel values for the zoom pan section effect.
    /// </summary>
    [DebugModel("Zoom & Pan", CodePattern = ".ZoomPanEffect(\"{0}\", {1}m)")]
    public sealed class ZoomPanEffectDebugModel
    {
        #region Instance fields and properties

        /// <summary>
        /// Gets or sets the image URL used by the visual effect preview.
        /// </summary>
        [DebugProperty(Label = "Image URL", HelpText = "Path to the background image.")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-zoom-pan-image", ValuePrefix = "url(", ValueSuffix = ")")]
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the animation speed or duration value used by the effect.
        /// </summary>
        [DebugProperty(Label = "Speed (s)", HelpText = "Zoom/pan cycle duration in seconds.")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-zoom-pan-speed", ValueSuffix = "s")]
        public decimal SpeedSeconds { get; set; } = 12m;

        /// <summary>
        /// Gets or sets the selectable image urls values exposed by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)] public IReadOnlyList<string> AvailableImageUrls { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
