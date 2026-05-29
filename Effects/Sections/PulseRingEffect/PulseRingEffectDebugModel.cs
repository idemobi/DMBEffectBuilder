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
    ///     Provides debug panel values for the pulse ring section effect.
    /// </summary>
    [DebugModel("Pulse ring", CodePattern = ".PulseRingEffect(\"{0}\", {1}m, {2}, {3}m)")]
    public sealed class PulseRingEffectDebugModel
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the primary color used by the visual effect.
        /// </summary>
        [DebugProperty(Label = "Color", HelpText = "Ring color (hex).")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-pulse-color")]
        public string Color { get; set; } = "#ffffff";

        /// <summary>
        ///     Gets or sets the number of generated visual elements.
        /// </summary>
        [DebugProperty(Label = "Count", HelpText = "Number of concentric rings.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-pulse-count")]
        public int Count { get; set; } = 3;

        /// <summary>
        ///     Gets or sets the opacity value used by the effect.
        /// </summary>
        [DebugProperty(Label = "Opacity", HelpText = "Ring transparency (0.0 to 1.0).", Min = "0", Max = "1", Step = "0.1")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-pulse-opacity")]
        public decimal Opacity { get; set; } = 0.6m;

        /// <summary>
        ///     Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)]
        public string SectionId { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the animation speed or duration value used by the effect.
        /// </summary>
        [DebugProperty(Label = "Speed (s)", HelpText = "Expansion cycle duration in seconds.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-pulse-speed")]
        public decimal SpeedSeconds { get; set; } = 3m;

        #endregion
    }
}