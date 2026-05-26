#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj DiagonalEdgeEffectDebugModel.cs create at 2026/04/23
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    [DebugModel("Diagonal Edge", CodePattern = ".SetDiagonalEdge(DiagonalEdge.{0}, DiagonalDirection.{1}, {2}m, {3})")]
    public sealed class DiagonalEdgeEffectDebugModel
    {
        #region Instance fields and properties

        [DebugProperty(Label = "Edge", HelpText = "Which edge(s) to clip.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-diagonal-edge")]
        public string Edge { get; set; } = "Bottom";

        [DebugProperty(Ignore = true)]
        public IReadOnlyList<string> AvailableEdges { get; set; } = new[] { "Bottom", "Top", "Both" };

        [DebugProperty(Label = "Direction", HelpText = "Diagonal slope direction.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-diagonal-direction")]
        public string Direction { get; set; } = "LeftToRight";

        [DebugProperty(Ignore = true)]
        public IReadOnlyList<string> AvailableDirections { get; set; } = new[] { "LeftToRight", "RightToLeft" };

        [DebugProperty(Label = "Tilt (deg)", HelpText = "Angle depth in percent (1–40).", Min = "1", Max = "40", Step = "1")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-diagonal-tilt")]
        public decimal Tilt { get; set; } = 13m;

        [DebugProperty(Label = "Torn points", HelpText = "Number of jagged points along the edge (0 = smooth).", Min = "0", Max = "60", Step = "5")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-diagonal-torn-points")]
        public int TornPoints { get; set; } = 30;

        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
