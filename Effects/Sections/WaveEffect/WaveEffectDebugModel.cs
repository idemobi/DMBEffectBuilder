#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj WaveEffectDebugModel.cs create at 2026/04/17
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    [DebugModel("Wave", CodePattern = ".WaveEffect(WaveEdge.{0}, {1})")]
    public sealed class WaveEffectDebugModel
    {
        #region Instance fields and properties

        [DebugProperty(Label = "Edge", HelpText = "Which edge to apply the wave to.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-wave-edge")]
        public string Edge { get; set; } = "Bottom";

        [DebugProperty(Ignore = true)]
        public IReadOnlyList<string> AvailableEdges { get; set; } = new[] { "Bottom", "Top" };

        [DebugProperty(Label = "Amplitude (px)", HelpText = "Wave height in pixels.", Min = "10", Max = "200", Step = "5")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-wave-height", ValueSuffix = "px")]
        public int Amplitude { get; set; } = 80;

        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
