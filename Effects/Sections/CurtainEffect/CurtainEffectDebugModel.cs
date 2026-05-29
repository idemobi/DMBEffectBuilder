#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Provides debug panel values for the curtain section effect.
    /// </summary>
    [DebugModel("Curtain", CodePattern = ".CurtainEffect(\"{0}\", CurtainStop.Meet)")]
    public sealed class CurtainEffectDebugModel
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the primary color used by the visual effect.
        /// </summary>
        [DebugProperty(Label = "Color", HelpText = "Curtain panel color")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-curtain-color")]
        public string Color { get; set; } = "#8B0000";

        /// <summary>
        ///     Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)]
        public string SectionId { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the stop percent value used by the effect debug panel.
        /// </summary>
        [DebugProperty(Label = "Stop (%)", HelpText = "50 = meet · >50 = overlap · <50 = gap")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-curtain-stop", ValueSuffix = "%")]
        public decimal StopPercent { get; set; } = 50m;

        #endregion
    }
}