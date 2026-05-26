#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj LavaLampEffectDebugModel.cs create at 2026/04/17
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    [DebugModel("Lava Lamp", CodePattern = ".LavaLampEffect(\"{0}\", count: {1}, blur: {2})")]
    public sealed class LavaLampEffectDebugModel
    {
        #region Instance fields and properties

        [DebugProperty(Label = "Background", HelpText = "Section background color")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-lavalamp-bg")]
        [DebugPropertyTarget(DebugTarget.Style, "backgroundColor")]
        public string BackgroundColor { get; set; } = "#0d0d0d";

        [DebugProperty(Label = "Count", HelpText = "Number of blobs")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-lavalamp-count")]
        public int Count { get; set; } = 5;

        [DebugProperty(Label = "Blur (px)", HelpText = "Blur for the merge effect — higher = softer blobs")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-lavalamp-blur")]
        public int Blur { get; set; } = 30;

        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
