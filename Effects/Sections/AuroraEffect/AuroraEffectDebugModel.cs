#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj AuroraEffectDebugModel.cs create at 2026/04/21
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    [DebugModel("Aurora", CodePattern = ".AuroraEffect(\"{0}\", \"{1}\", \"{2}\", {3}m, {4}m, {5}m, {6}m)")]
    public sealed class AuroraEffectDebugModel
    {
        #region Instance fields and properties

        [DebugProperty(Label = "Color 1", HelpText = "First aurora color (hex).")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-aurora-color1")]
        public string Color1 { get; set; } = "#64DCB4";

        [DebugProperty(Label = "Color 2", HelpText = "Second aurora color (hex).")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-aurora-color2")]
        public string Color2 { get; set; } = "#508CFF";

        [DebugProperty(Label = "Color 3", HelpText = "Third aurora color (hex).")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-aurora-color3")]
        public string Color3 { get; set; } = "#C864FF";

        [DebugProperty(Label = "Opacity 1", HelpText = "Transparency of color 1 (0.0 to 1.0).")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-aurora-opacity1")]
        public decimal Opacity1 { get; set; } = 0.45m;

        [DebugProperty(Label = "Opacity 2", HelpText = "Transparency of color 2 (0.0 to 1.0).")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-aurora-opacity2")]
        public decimal Opacity2 { get; set; } = 0.35m;

        [DebugProperty(Label = "Opacity 3", HelpText = "Transparency of color 3 (0.0 to 1.0).")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-aurora-opacity3")]
        public decimal Opacity3 { get; set; } = 0.30m;

        [DebugProperty(Label = "Speed (s)", HelpText = "Animation cycle duration in seconds.")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-aurora-speed", ValueSuffix = "s")]
        public decimal SpeedSeconds { get; set; } = 8m;

        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
