#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj MatrixEffectDebugModel.cs create at 2026/04/21
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    [DebugModel("Matrix", CodePattern = ".MatrixEffect(\"{0}\", {1}, {2}m)")]
    public sealed class MatrixEffectDebugModel
    {
        #region Instance fields and properties

        [DebugProperty(Label = "Color", HelpText = "Character rain color.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-matrix-color")]
        public string Color { get; set; } = "#00ff41";

        [DebugProperty(Label = "Font size (px)", HelpText = "Size of each character in pixels.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-matrix-font-size")]
        public int FontSize { get; set; } = 14;

        [DebugProperty(Label = "Speed", HelpText = "Fall speed multiplier.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-matrix-speed")]
        public decimal Speed { get; set; } = 1m;

        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
