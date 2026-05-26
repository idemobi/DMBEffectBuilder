#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj RippleEffectDebugModel.cs create at 2026/04/17
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    [DebugModel("Ripple / Ondulation", CodePattern = ".RippleEffect(\"{0}\", {1}m, {2})")]
    public sealed class RippleEffectDebugModel
    {
        #region Instance fields and properties

        [DebugProperty(Label = "Color", HelpText = "Ring color")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-ripple-color")]
        public string Color { get; set; } = "#ffffff";

        [DebugProperty(Label = "Opacity", HelpText = "Max ring opacity (0–1)", Min = "0", Max = "1", Step = "0.1")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-ripple-opacity")]
        public decimal Opacity { get; set; } = 0.5m;

        [DebugProperty(Label = "Speed", HelpText = "Expansion speed in px/frame")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-ripple-speed")]
        public int Speed { get; set; } = 3;

        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
