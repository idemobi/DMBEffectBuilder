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
    ///     Provides debug panel values for the scanline section effect.
    /// </summary>
    [DebugModel("Scanline", CodePattern = ".ScanlineEffect({0}m, {1})")]
    public sealed class ScanlineEffectDebugModel
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the opacity value used by the effect.
        /// </summary>
        [DebugProperty(Label = "Opacity", HelpText = "Line opacity (0–1).", Min = "0", Max = "1", Step = "0.01")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-scanline-opacity")]
        public decimal Opacity { get; set; } = 0.15m;

        /// <summary>
        ///     Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)]
        public string SectionId { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the spacing in pixels between generated scanlines.
        /// </summary>
        [DebugProperty(Label = "Spacing (px)", HelpText = "Distance between scanlines in pixels.")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-scanline-spacing", ValueSuffix = "px")]
        public int SpacingPx { get; set; } = 4;

        #endregion
    }
}