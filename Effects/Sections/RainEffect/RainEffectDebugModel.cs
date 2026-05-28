#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj RainEffectDebugModel.cs create at 2026/04/17
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides debug panel values for the rain section effect.
    /// </summary>
    [DebugModel("Rain", CodePattern = ".RainEffect(\"{0}\", {1}m, {2})")]
    public sealed class RainEffectDebugModel
    {
        #region Instance fields and properties

        /// <summary>
        /// Gets or sets the primary color used by the visual effect.
        /// </summary>
        [DebugProperty(Label = "Color", HelpText = "Drop color")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-rain-color")]
        public string Color { get; set; } = "#00ff41";

        /// <summary>
        /// Gets or sets the opacity value used by the effect.
        /// </summary>
        [DebugProperty(Label = "Opacity", HelpText = "Max drop opacity (0–1)", Min = "0", Max = "1", Step = "0.1")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-rain-opacity")]
        public decimal Opacity { get; set; } = 0.6m;

        /// <summary>
        /// Gets or sets the number of generated rain drops.
        /// </summary>
        [DebugProperty(Label = "Drops", HelpText = "Number of simultaneous drops")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-rain-drops")]
        public int Drops { get; set; } = 80;

        /// <summary>
        /// Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
