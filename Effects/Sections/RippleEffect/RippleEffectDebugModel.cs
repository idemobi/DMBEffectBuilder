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
    ///     Provides debug panel values for the ripple section effect.
    /// </summary>
    [DebugModel("Ripple / Ondulation", CodePattern = ".RippleEffect(\"{0}\", {1}m, {2})")]
    public sealed class RippleEffectDebugModel
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the primary color used by the visual effect.
        /// </summary>
        [DebugProperty(Label = "Color", HelpText = "Ring color")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-ripple-color")]
        public string Color { get; set; } = "#ffffff";

        /// <summary>
        ///     Gets or sets the opacity value used by the effect.
        /// </summary>
        [DebugProperty(Label = "Opacity", HelpText = "Max ring opacity (0–1)", Min = "0", Max = "1", Step = "0.1")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-ripple-opacity")]
        public decimal Opacity { get; set; } = 0.5m;

        /// <summary>
        ///     Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)]
        public string SectionId { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the animation speed or duration value used by the effect.
        /// </summary>
        [DebugProperty(Label = "Speed", HelpText = "Expansion speed in px/frame")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-ripple-speed")]
        public int Speed { get; set; } = 3;

        #endregion
    }
}