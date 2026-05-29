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
    ///     Provides debug panel values for the noise section effect.
    /// </summary>
    [DebugModel("Noise / Grain", CodePattern = ".NoiseEffect({0}m, {1}m)")]
    public sealed class NoiseEffectDebugModel
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the opacity value used by the effect.
        /// </summary>
        [DebugProperty(Label = "Opacity", HelpText = "Grain visibility — 0 = invisible, 1 = full", Min = "0", Max = "1", Step = "0.01")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-noise-opacity")]
        public decimal Opacity { get; set; } = 0.12m;

        /// <summary>
        ///     Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)]
        public string SectionId { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the animation speed or duration value used by the effect.
        /// </summary>
        [DebugProperty(Label = "Speed (s)", HelpText = "Grain animation cycle in seconds — lower = faster", Min = "0.1", Max = "5", Step = "0.1")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-noise-speed", ValueSuffix = "s")]
        public decimal Speed { get; set; } = 0.4m;

        #endregion
    }
}