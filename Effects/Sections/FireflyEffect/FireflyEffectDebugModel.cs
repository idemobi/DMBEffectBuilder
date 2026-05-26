#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj FireflyEffectDebugModel.cs create at 2026/04/21
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    [DebugModel("Firefly", CodePattern = ".FireflyEffect(\"{0}\", {1}, {2}m)")]
    public sealed class FireflyEffectDebugModel
    {
        #region Instance fields and properties

        [DebugProperty(Label = "Color", HelpText = "Glow color of the fireflies.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-firefly-color")]
        public string Color { get; set; } = "#aaff88";

        [DebugProperty(Label = "Count", HelpText = "Number of fireflies.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-firefly-count")]
        public int Count { get; set; } = 20;

        [DebugProperty(Label = "Size (px)", HelpText = "Radius of each firefly in pixels.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-firefly-size")]
        public decimal Size { get; set; } = 3m;

        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
