#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj PulseRingEffectDebugModel.cs create at 2026/04/21
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    [DebugModel("Pulse ring", CodePattern = ".PulseRingEffect(\"{0}\", {1}m, {2}, {3}m)")]
    public sealed class PulseRingEffectDebugModel
    {
        #region Instance fields and properties

        [DebugProperty(Label = "Color", HelpText = "Ring color (hex).")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-pulse-color")]
        public string Color { get; set; } = "#ffffff";

        [DebugProperty(Label = "Opacity", HelpText = "Ring transparency (0.0 to 1.0).", Min = "0", Max = "1", Step = "0.1")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-pulse-opacity")]
        public decimal Opacity { get; set; } = 0.6m;

        [DebugProperty(Label = "Count", HelpText = "Number of concentric rings.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-pulse-count")]
        public int Count { get; set; } = 3;

        [DebugProperty(Label = "Speed (s)", HelpText = "Expansion cycle duration in seconds.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-pulse-speed")]
        public decimal SpeedSeconds { get; set; } = 3m;

        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
