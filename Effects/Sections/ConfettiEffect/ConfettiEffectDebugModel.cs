#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj ConfettiEffectDebugModel.cs create at 2026/04/21
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides debug panel values for the confetti section effect.
    /// </summary>
    [DebugModel("Confetti", CodePattern = ".ConfettiEffect(new[] { \"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\" }, {5}, {6}m)")]
    public sealed class ConfettiEffectDebugModel
    {
        #region Instance fields and properties

        /// <summary>
        /// Gets or sets color 1 used by the visual effect.
        /// </summary>
        [DebugProperty(Label = "Color 1", HelpText = "First confetti color (hex).")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-confetti-color1")]
        public string Color1 { get; set; } = "#ff6ecf";

        /// <summary>
        /// Gets or sets color 2 used by the visual effect.
        /// </summary>
        [DebugProperty(Label = "Color 2", HelpText = "Second confetti color (hex).")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-confetti-color2")]
        public string Color2 { get; set; } = "#efff5c";

        /// <summary>
        /// Gets or sets color 3 used by the visual effect.
        /// </summary>
        [DebugProperty(Label = "Color 3", HelpText = "Third confetti color (hex).")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-confetti-color3")]
        public string Color3 { get; set; } = "#00f5d4";

        /// <summary>
        /// Gets or sets color 4 used by the visual effect.
        /// </summary>
        [DebugProperty(Label = "Color 4", HelpText = "Fourth confetti color (hex).")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-confetti-color4")]
        public string Color4 { get; set; } = "#ff9f43";

        /// <summary>
        /// Gets or sets color 5 used by the visual effect.
        /// </summary>
        [DebugProperty(Label = "Color 5", HelpText = "Fifth confetti color (hex).")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-confetti-color5")]
        public string Color5 { get; set; } = "#a29bfe";

        /// <summary>
        /// Gets or sets the number of generated visual elements.
        /// </summary>
        [DebugProperty(Label = "Count", HelpText = "Number of confetti pieces.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-confetti-count")]
        public int Count { get; set; } = 60;

        /// <summary>
        /// Gets or sets the animation speed or duration value used by the effect.
        /// </summary>
        [DebugProperty(Label = "Speed", HelpText = "Fall speed multiplier.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-confetti-speed")]
        public decimal Speed { get; set; } = 1m;

        /// <summary>
        /// Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
