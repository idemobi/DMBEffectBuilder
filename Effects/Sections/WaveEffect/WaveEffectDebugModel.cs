#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj WaveEffectDebugModel.cs create at 2026/04/17
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides debug panel values for the wave section effect.
    /// </summary>
    [DebugModel("Wave", CodePattern = ".WaveEffect(WaveEdge.{0}, {1})")]
    public sealed class WaveEffectDebugModel
    {
        #region Instance fields and properties

        /// <summary>
        /// Gets or sets the edge targeted by the section shape effect.
        /// </summary>
        [DebugProperty(Label = "Edge", HelpText = "Which edge to apply the wave to.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-wave-edge")]
        public string Edge { get; set; } = "Bottom";

        /// <summary>
        /// Gets or sets the selectable edges values exposed by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)]
        public IReadOnlyList<string> AvailableEdges { get; set; } = new[] { "Bottom", "Top" };

        /// <summary>
        /// Gets or sets the wave amplitude in pixels.
        /// </summary>
        [DebugProperty(Label = "Amplitude (px)", HelpText = "Wave height in pixels.", Min = "10", Max = "200", Step = "5")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-wave-height", ValueSuffix = "px")]
        public int Amplitude { get; set; } = 80;

        /// <summary>
        /// Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
