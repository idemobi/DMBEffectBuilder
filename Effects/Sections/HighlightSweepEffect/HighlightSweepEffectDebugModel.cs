#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj HighlightSweepEffectDebugModel.cs create at 2026/04/12 12:04:31
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    [DebugModel("Highlight Sweep", CodePattern = ".HighlightSweepEffect({0}m, \"{1}\")")]
    public sealed class HighlightSweepEffectDebugModel
    {
        #region Instance fields and properties

        [DebugProperty(Label = "Image", HelpText = "Background image from /wwwroot/images/test.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-sweep-image")]
        [DebugPropertyTarget(DebugTarget.Style, "backgroundImage", ValuePrefix = "url(", ValueSuffix = ")")]
        public string ImageUrl { get; set; } = string.Empty;

        [DebugProperty(Label = "Opacity", HelpText = "Opacity between 0 and 1", Target = DebugTarget.DataAttribute, TargetName = "data-sweep-opacity", InputType = DebugInputType.Number, ValueSuffix = "")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-sweep-opacity")]
        public decimal Opacity { get; set; } = 0.3m;

        [DebugProperty(Ignore = true)] public IReadOnlyList<string> AvailableImageUrls { get; set; } = Array.Empty<string>();

        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}