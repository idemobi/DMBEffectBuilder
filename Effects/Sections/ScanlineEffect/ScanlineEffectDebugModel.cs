#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj ScanlineEffectDebugModel.cs create at 2026/04/21
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    [DebugModel("Scanline", CodePattern = ".ScanlineEffect({0}m, {1})")]
    public sealed class ScanlineEffectDebugModel
    {
        #region Instance fields and properties

        [DebugProperty(Label = "Opacity", HelpText = "Line opacity (0–1).", Min = "0", Max = "1", Step = "0.01")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-scanline-opacity")]
        public decimal Opacity { get; set; } = 0.15m;

        [DebugProperty(Label = "Spacing (px)", HelpText = "Distance between scanlines in pixels.")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-scanline-spacing", ValueSuffix = "px")]
        public int SpacingPx { get; set; } = 4;

        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
