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
    ///     Provides debug panel values for the firefly section effect.
    /// </summary>
    [DebugModel("Firefly", CodePattern = ".FireflyEffect(\"{0}\", {1}, {2}m)")]
    public sealed class FireflyEffectDebugModel
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the primary color used by the visual effect.
        /// </summary>
        [DebugProperty(Label = "Color", HelpText = "Glow color of the fireflies.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-firefly-color")]
        public string Color { get; set; } = "#aaff88";

        /// <summary>
        ///     Gets or sets the number of generated visual elements.
        /// </summary>
        [DebugProperty(Label = "Count", HelpText = "Number of fireflies.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-firefly-count")]
        public int Count { get; set; } = 20;

        /// <summary>
        ///     Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)]
        public string SectionId { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the generated visual size used by the effect.
        /// </summary>
        [DebugProperty(Label = "Size (px)", HelpText = "Radius of each firefly in pixels.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-firefly-size")]
        public decimal Size { get; set; } = 3m;

        #endregion
    }
}