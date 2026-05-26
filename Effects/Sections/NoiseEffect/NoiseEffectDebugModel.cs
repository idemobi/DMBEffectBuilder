#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj NoiseEffectDebugModel.cs create at 2026/04/17
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    [DebugModel("Noise / Grain", CodePattern = ".NoiseEffect({0}m, {1}m)")]
    public sealed class NoiseEffectDebugModel
    {
        #region Instance fields and properties

        [DebugProperty(Label = "Opacity", HelpText = "Grain visibility — 0 = invisible, 1 = full", Min = "0", Max = "1", Step = "0.01")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-noise-opacity")]
        public decimal Opacity { get; set; } = 0.12m;

        [DebugProperty(Label = "Speed (s)", HelpText = "Grain animation cycle in seconds — lower = faster", Min = "0.1", Max = "5", Step = "0.1")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-noise-speed", ValueSuffix = "s")]
        public decimal Speed { get; set; } = 0.4m;

        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
