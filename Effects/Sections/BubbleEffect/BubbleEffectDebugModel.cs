#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj BubbleEffectDebugModel.cs create at 2026/04/17
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    [DebugModel("Bubble / Orbs", CodePattern = ".BubbleEffect(count: {0}, minSize: {1}, maxSize: {2}, blur: {3})")]
    public sealed class BubbleEffectDebugModel
    {
        #region Instance fields and properties

        [DebugProperty(Label = "Count", HelpText = "Number of floating orbs")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-bubble-count")]
        public int Count { get; set; } = 6;

        [DebugProperty(Label = "Min size (px)")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-bubble-min-size")]
        public int MinSize { get; set; } = 100;

        [DebugProperty(Label = "Max size (px)")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-bubble-max-size")]
        public int MaxSize { get; set; } = 300;

        [DebugProperty(Label = "Blur (px)", HelpText = "Blur radius — higher = softer orbs")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-bubble-blur")]
        public int Blur { get; set; } = 50;

        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
