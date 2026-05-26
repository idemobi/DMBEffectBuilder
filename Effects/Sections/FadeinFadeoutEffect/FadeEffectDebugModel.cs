#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj FadeEffectDebugModel.cs create at 2026/04/07 21:04:27
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    [DebugModel("Fade effect", CodePattern = ".FadeInFadeOutEffect(\"{0}\", {1}m, {2}m, {3}m)")]
    public sealed class FadeEffectDebugModel
    {
        #region Instance fields and properties

        [DebugProperty(Label = "Image", HelpText = "Background image from /wwwroot/images/test.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-fade-image")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-fade-image", ValuePrefix = "url(", ValueSuffix = ")")]
        public string ImageUrl { get; set; } = string.Empty;

        [DebugProperty(Label = "Max darkness", HelpText = "0 = no darkening, 1 = maximum darkening.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-fade-max-darkness")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-fade-max-darkness")]
        public decimal MaxDarkness { get; set; } = 0.65m;

        [DebugProperty(Label = "Scale", HelpText = "0 = normal size, 1 = maximum zoom.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-fade-scale")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-fade-scale")]
        public decimal Scale { get; set; } = 0m;

        [DebugProperty(Label = "Vertical offset (%)", HelpText = "Positive values move the image downward, negative upward.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-fade-offset-y")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-fade-offset-y", ValueSuffix = "%")]
        public decimal VerticalOffset { get; set; } = 0m;

        [DebugProperty(Ignore = true)]
        public IReadOnlyList<string> AvailableImageUrls { get; set; } = Array.Empty<string>();

        public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
