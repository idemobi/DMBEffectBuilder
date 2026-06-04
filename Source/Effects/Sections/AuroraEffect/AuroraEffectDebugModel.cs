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
    ///     Provides debug panel values for the aurora section effect.
    /// </summary>
    [DebugModel("Aurora", CodePattern = ".AuroraEffect(\"{0}\", \"{1}\", \"{2}\", {3}m, {4}m, {5}m, {6}m)")]
    public sealed class AuroraEffectDebugModel
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets color 1 used by the visual effect.
        /// </summary>
        [DebugProperty(Label = "Color 1", HelpText = "First aurora color (hex).")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-aurora-color1")]
        public string Color1 { get; set; } = "#64DCB4";

        /// <summary>
        ///     Gets or sets color 2 used by the visual effect.
        /// </summary>
        [DebugProperty(Label = "Color 2", HelpText = "Second aurora color (hex).")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-aurora-color2")]
        public string Color2 { get; set; } = "#508CFF";

        /// <summary>
        ///     Gets or sets color 3 used by the visual effect.
        /// </summary>
        [DebugProperty(Label = "Color 3", HelpText = "Third aurora color (hex).")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-aurora-color3")]
        public string Color3 { get; set; } = "#C864FF";

        /// <summary>
        ///     Gets or sets the opacity1 value used by the effect.
        /// </summary>
        [DebugProperty(Label = "Opacity 1", HelpText = "Transparency of color 1 (0.0 to 1.0).")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-aurora-opacity1")]
        public decimal Opacity1 { get; set; } = 0.45m;

        /// <summary>
        ///     Gets or sets the opacity2 value used by the effect.
        /// </summary>
        [DebugProperty(Label = "Opacity 2", HelpText = "Transparency of color 2 (0.0 to 1.0).")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-aurora-opacity2")]
        public decimal Opacity2 { get; set; } = 0.35m;

        /// <summary>
        ///     Gets or sets the opacity3 value used by the effect.
        /// </summary>
        [DebugProperty(Label = "Opacity 3", HelpText = "Transparency of color 3 (0.0 to 1.0).")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-aurora-opacity3")]
        public decimal Opacity3 { get; set; } = 0.30m;

        /// <summary>
        ///     Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)]
        public string SectionId { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the animation speed or duration value used by the effect.
        /// </summary>
        [DebugProperty(Label = "Speed (s)", HelpText = "Animation cycle duration in seconds.")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-aurora-speed", ValueSuffix = "s")]
        public decimal SpeedSeconds { get; set; } = 8m;

        #endregion
    }
}