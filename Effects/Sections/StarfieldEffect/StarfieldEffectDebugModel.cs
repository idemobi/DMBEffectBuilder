#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj StarfieldEffectDebugModel.cs create at 2026/04/21
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    [DebugModel("Starfield", CodePattern = ".StarfieldEffect(\"{0}\", {1}, {2}m)")]
    public sealed class StarfieldEffectDebugModel
    {
        #region Instance fields and properties

        [DebugProperty(Label = "Color", HelpText = "Star color.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-starfield-color")]
        public string Color { get; set; } = "#ffffff";

        [DebugProperty(Label = "Count", HelpText = "Number of stars.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-starfield-count")]
        public int Count { get; set; } = 120;

        [DebugProperty(Label = "Speed", HelpText = "Drift speed multiplier.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-starfield-speed")]
        public decimal Speed { get; set; } = 1.5m;

        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
