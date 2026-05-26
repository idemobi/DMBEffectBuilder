#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj CurtainEffectDebugModel.cs create at 2026/04/17
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    [DebugModel("Curtain", CodePattern = ".CurtainEffect(\"{0}\", CurtainStop.Meet)")]
    public sealed class CurtainEffectDebugModel
    {
        #region Instance fields and properties

        [DebugProperty(Label = "Color", HelpText = "Curtain panel color")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-curtain-color")]
        public string Color { get; set; } = "#8B0000";

        [DebugProperty(Label = "Stop (%)", HelpText = "50 = meet · >50 = overlap · <50 = gap")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-curtain-stop", ValueSuffix = "%")]
        public decimal StopPercent { get; set; } = 50m;

        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
