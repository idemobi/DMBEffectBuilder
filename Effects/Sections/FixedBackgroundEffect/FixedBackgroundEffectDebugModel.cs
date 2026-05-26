#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj FixedBackgroundEffectDebugModel.cs create at 2026/04/07 21:04:27
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    [DebugModel("Fixed background", CodePattern = ".FixedBackgroundEffect(\"{0}\")")]
    public sealed class FixedBackgroundEffectDebugModel
    {
        #region Instance fields and properties

        [DebugProperty(Label = "Image", HelpText = "Background image from /wwwroot/images/test.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-fixed-bg-image")]
        [DebugPropertyTarget(DebugTarget.Style, "backgroundImage", ValuePrefix = "url(", ValueSuffix = ")")]
        public string ImageUrl { get; set; } = string.Empty;

        [DebugProperty(Ignore = true)]
        public IReadOnlyList<string> AvailableImageUrls { get; set; } = Array.Empty<string>();

        public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
