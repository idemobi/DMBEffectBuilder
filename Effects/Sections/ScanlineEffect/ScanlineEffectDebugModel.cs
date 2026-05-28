#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj ScanlineEffectDebugModel.cs create at 2026/04/21
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides debug panel values for the scanline section effect.
    /// </summary>
    [DebugModel("Scanline", CodePattern = ".ScanlineEffect({0}m, {1})")]
    public sealed class ScanlineEffectDebugModel
    {
        #region Instance fields and properties

        /// <summary>
        /// Gets or sets the opacity value used by the effect.
        /// </summary>
        [DebugProperty(Label = "Opacity", HelpText = "Line opacity (0–1).", Min = "0", Max = "1", Step = "0.01")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-scanline-opacity")]
        public decimal Opacity { get; set; } = 0.15m;

        /// <summary>
        /// Gets or sets the spacing in pixels between generated scanlines.
        /// </summary>
        [DebugProperty(Label = "Spacing (px)", HelpText = "Distance between scanlines in pixels.")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-scanline-spacing", ValueSuffix = "px")]
        public int SpacingPx { get; set; } = 4;

        /// <summary>
        /// Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
