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
    ///     Provides debug panel values for the bubble section effect.
    /// </summary>
    [DebugModel("Bubble / Orbs", CodePattern = ".BubbleEffect(count: {0}, minSize: {1}, maxSize: {2}, blur: {3})")]
    public sealed class BubbleEffectDebugModel
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the blur radius used by the effect.
        /// </summary>
        [DebugProperty(Label = "Blur (px)", HelpText = "Blur radius — higher = softer orbs")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-bubble-blur")]
        public int Blur { get; set; } = 50;

        /// <summary>
        ///     Gets or sets the number of generated visual elements.
        /// </summary>
        [DebugProperty(Label = "Count", HelpText = "Number of floating orbs")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-bubble-count")]
        public int Count { get; set; } = 6;

        /// <summary>
        ///     Gets or sets the maximum generated element size in pixels.
        /// </summary>
        [DebugProperty(Label = "Max size (px)")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-bubble-max-size")]
        public int MaxSize { get; set; } = 300;

        /// <summary>
        ///     Gets or sets the minimum generated element size in pixels.
        /// </summary>
        [DebugProperty(Label = "Min size (px)")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-bubble-min-size")]
        public int MinSize { get; set; } = 100;

        /// <summary>
        ///     Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)]
        public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}