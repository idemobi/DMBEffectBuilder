#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj ParallaxEffectDebugModel.cs create at 2026/04/07 21:04:27
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    [DebugModel("Parallax background", CodePattern = ".ParallaxBackground(\"{0}\", {1}m)")]
    public sealed class ParallaxEffectDebugModel
    {
        #region Instance fields and properties

        [DebugProperty(Label = "Image", HelpText = "Background image from /wwwroot/images/test.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-parallax-image")]
        [DebugPropertyTarget(DebugTarget.Style, "backgroundImage", ValuePrefix = "url(", ValueSuffix = ")")]
        public string ImageUrl { get; set; } = string.Empty;

        [DebugProperty(Label = "Speed", HelpText = "Higher values increase the background movement on scroll.", Min = "0.01", Max = "2", Step = "0.05")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-parallax-speed")]
        public decimal Speed { get; set; } = 0.25m;

        [DebugProperty(Label = "Base Y (%)", HelpText = "Default vertical anchor of the background.", Min = "0", Max = "100", Step = "1")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-parallax-base-y")]
        [DebugPropertyTarget(DebugTarget.Style, "backgroundPosition", ValuePrefix = "center calc(", ValueSuffix = "% + 0px)")]
        public decimal BaseY { get; set; } = 50m;

        [DebugProperty(Ignore = true)] public IReadOnlyList<string> AvailableImageUrls { get; set; } = Array.Empty<string>();

        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
