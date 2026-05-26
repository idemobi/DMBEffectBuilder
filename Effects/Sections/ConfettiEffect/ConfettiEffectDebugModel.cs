#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj ConfettiEffectDebugModel.cs create at 2026/04/21
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    [DebugModel("Confetti", CodePattern = ".ConfettiEffect(new[] { \"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\" }, {5}, {6}m)")]
    public sealed class ConfettiEffectDebugModel
    {
        #region Instance fields and properties

        [DebugProperty(Label = "Color 1", HelpText = "First confetti color (hex).")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-confetti-color1")]
        public string Color1 { get; set; } = "#ff6ecf";

        [DebugProperty(Label = "Color 2", HelpText = "Second confetti color (hex).")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-confetti-color2")]
        public string Color2 { get; set; } = "#efff5c";

        [DebugProperty(Label = "Color 3", HelpText = "Third confetti color (hex).")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-confetti-color3")]
        public string Color3 { get; set; } = "#00f5d4";

        [DebugProperty(Label = "Color 4", HelpText = "Fourth confetti color (hex).")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-confetti-color4")]
        public string Color4 { get; set; } = "#ff9f43";

        [DebugProperty(Label = "Color 5", HelpText = "Fifth confetti color (hex).")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-confetti-color5")]
        public string Color5 { get; set; } = "#a29bfe";

        [DebugProperty(Label = "Count", HelpText = "Number of confetti pieces.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-confetti-count")]
        public int Count { get; set; } = 60;

        [DebugProperty(Label = "Speed", HelpText = "Fall speed multiplier.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-confetti-speed")]
        public decimal Speed { get; set; } = 1m;

        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
