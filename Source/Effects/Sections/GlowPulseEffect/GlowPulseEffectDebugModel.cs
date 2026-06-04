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
    ///     Provides debug panel values for the glow pulse section effect.
    /// </summary>
    [DebugModel("Glow pulse", CodePattern = ".GlowPulseEffect(\"{0}\", {1}m, {2}m)")]
    public sealed class GlowPulseEffectDebugModel
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the primary color used by the visual effect.
        /// </summary>
        [DebugProperty(Label = "Color", HelpText = "Glow color (hex).")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-glow-color")]
        public string Color { get; set; } = "#64C8FF";

        /// <summary>
        ///     Gets or sets the opacity value used by the effect.
        /// </summary>
        [DebugProperty(Label = "Opacity", HelpText = "Glow transparency (0.0 to 1.0).", Min = "0", Max = "1", Step = "0.1")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-glow-opacity")]
        public decimal Opacity { get; set; } = 0.5m;

        /// <summary>
        ///     Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)]
        public string SectionId { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the animation speed or duration value used by the effect.
        /// </summary>
        [DebugProperty(Label = "Speed (s)", HelpText = "Pulse cycle duration in seconds.")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-glow-speed", ValueSuffix = "s")]
        public decimal SpeedSeconds { get; set; } = 2m;

        #endregion
    }
}