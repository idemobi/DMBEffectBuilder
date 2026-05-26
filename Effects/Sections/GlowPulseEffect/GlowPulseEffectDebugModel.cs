#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj GlowPulseEffectDebugModel.cs create at 2026/04/21
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    [DebugModel("Glow pulse", CodePattern = ".GlowPulseEffect(\"{0}\", {1}m, {2}m)")]
    public sealed class GlowPulseEffectDebugModel
    {
        #region Instance fields and properties

        [DebugProperty(Label = "Color", HelpText = "Glow color (hex).")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-glow-color")]
        public string Color { get; set; } = "#64C8FF";

        [DebugProperty(Label = "Opacity", HelpText = "Glow transparency (0.0 to 1.0).", Min = "0", Max = "1", Step = "0.1")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-glow-opacity")]
        public decimal Opacity { get; set; } = 0.5m;

        [DebugProperty(Label = "Speed (s)", HelpText = "Pulse cycle duration in seconds.")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-glow-speed", ValueSuffix = "s")]
        public decimal SpeedSeconds { get; set; } = 2m;

        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
