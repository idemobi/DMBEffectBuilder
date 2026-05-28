#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj FixedBackgroundEffectDebugModel.cs create at 2026/04/07 21:04:27
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides debug panel values for the fixed background section effect.
    /// </summary>
    [DebugModel("Fixed background", CodePattern = ".FixedBackgroundEffect(\"{0}\")")]
    public sealed class FixedBackgroundEffectDebugModel
    {
        #region Instance fields and properties

        /// <summary>
        /// Gets or sets the image URL used by the visual effect preview.
        /// </summary>
        [DebugProperty(Label = "Image", HelpText = "Background image from /wwwroot/images/test.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-fixed-bg-image")]
        [DebugPropertyTarget(DebugTarget.Style, "backgroundImage", ValuePrefix = "url(", ValueSuffix = ")")]
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the selectable image urls values exposed by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)]
        public IReadOnlyList<string> AvailableImageUrls { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
