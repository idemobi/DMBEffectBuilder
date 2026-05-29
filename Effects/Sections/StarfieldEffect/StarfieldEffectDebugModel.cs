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
    ///     Provides debug panel values for the starfield section effect.
    /// </summary>
    [DebugModel("Starfield", CodePattern = ".StarfieldEffect(\"{0}\", {1}, {2}m)")]
    public sealed class StarfieldEffectDebugModel
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the primary color used by the visual effect.
        /// </summary>
        [DebugProperty(Label = "Color", HelpText = "Star color.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-starfield-color")]
        public string Color { get; set; } = "#ffffff";

        /// <summary>
        ///     Gets or sets the number of generated visual elements.
        /// </summary>
        [DebugProperty(Label = "Count", HelpText = "Number of stars.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-starfield-count")]
        public int Count { get; set; } = 120;

        /// <summary>
        ///     Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)]
        public string SectionId { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the animation speed or duration value used by the effect.
        /// </summary>
        [DebugProperty(Label = "Speed", HelpText = "Drift speed multiplier.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-starfield-speed")]
        public decimal Speed { get; set; } = 1.5m;

        #endregion
    }
}