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
    ///     Provides debug panel values for the lava lamp section effect.
    /// </summary>
    [DebugModel("Lava Lamp", CodePattern = ".LavaLampEffect(\"{0}\", count: {1}, blur: {2})")]
    public sealed class LavaLampEffectDebugModel
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the background color used behind the effect.
        /// </summary>
        [DebugProperty(Label = "Background", HelpText = "Section background color")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-lavalamp-bg")]
        [DebugPropertyTarget(DebugTarget.Style, "backgroundColor")]
        public string BackgroundColor { get; set; } = "#0d0d0d";

        /// <summary>
        ///     Gets or sets the blur radius used by the effect.
        /// </summary>
        [DebugProperty(Label = "Blur (px)", HelpText = "Blur for the merge effect — higher = softer blobs")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-lavalamp-blur")]
        public int Blur { get; set; } = 30;

        /// <summary>
        ///     Gets or sets the number of generated visual elements.
        /// </summary>
        [DebugProperty(Label = "Count", HelpText = "Number of blobs")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-lavalamp-count")]
        public int Count { get; set; } = 5;

        /// <summary>
        ///     Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)]
        public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}