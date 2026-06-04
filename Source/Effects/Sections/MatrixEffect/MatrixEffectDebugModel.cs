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
    ///     Provides debug panel values for the matrix section effect.
    /// </summary>
    [DebugModel("Matrix", CodePattern = ".MatrixEffect(\"{0}\", {1}, {2}m)")]
    public sealed class MatrixEffectDebugModel
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the primary color used by the visual effect.
        /// </summary>
        [DebugProperty(Label = "Color", HelpText = "Character rain color.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-matrix-color")]
        public string Color { get; set; } = "#00ff41";

        /// <summary>
        ///     Gets or sets the font size used by the generated characters.
        /// </summary>
        [DebugProperty(Label = "Font size (px)", HelpText = "Size of each character in pixels.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-matrix-font-size")]
        public int FontSize { get; set; } = 14;

        /// <summary>
        ///     Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)]
        public string SectionId { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the animation speed or duration value used by the effect.
        /// </summary>
        [DebugProperty(Label = "Speed", HelpText = "Fall speed multiplier.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-matrix-speed")]
        public decimal Speed { get; set; } = 1m;

        #endregion
    }
}