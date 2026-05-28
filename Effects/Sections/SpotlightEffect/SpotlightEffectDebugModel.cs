#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj SpotlightEffectDebugModel.cs create at 2026/04/17
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides debug panel values for the spotlight section effect.
    /// </summary>
    [DebugModel("Spotlight", CodePattern = ".SpotlightEffect(\"{0}\", {1}m, {2})")]
    public sealed class SpotlightEffectDebugModel
    {
        #region Instance fields and properties

        /// <summary>
        /// Gets or sets the primary color used by the visual effect.
        /// </summary>
        [DebugProperty(Label = "Color", HelpText = "Base color of the spotlight (hex)")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-spotlight-color")]
        public string Color { get; set; } = "#ffffff";

        /// <summary>
        /// Gets or sets the opacity value used by the effect.
        /// </summary>
        [DebugProperty(Label = "Opacity", HelpText = "Transparency of the spotlight (0.0 to 1.0)", Min = "0", Max = "1", Step = "0.1")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-spotlight-opacity")]
        public decimal Opacity { get; set; } = 0.2m;

        /// <summary>
        /// Gets or sets the generated visual size used by the effect.
        /// </summary>
        [DebugProperty(Label = "Size (px)", HelpText = "Spotlight radius in pixels")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-spotlight-size", ValueSuffix = "px")]
        public int SizePx { get; set; } = 300;

        /// <summary>
        /// Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
