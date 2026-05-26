#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj CurveEdgeEffectDebugModel.cs create at 2026/04/23
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    [DebugModel("Curve Edge", CodePattern = ".CurveEdgeEffect(CurveEdge.{0}, Curvature.{1}, {2}m)")]
    public sealed class CurveEdgeEffectDebugModel
    {
        #region Instance fields and properties

        [DebugProperty(Label = "Edge", HelpText = "Which edge(s) to curve.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-curve-edge")]
        public string Edge { get; set; } = "Bottom";

        [DebugProperty(Ignore = true)]
        public IReadOnlyList<string> AvailableEdges { get; set; } = new[] { "Bottom", "Top", "Both" };

        [DebugProperty(Label = "Curvature", HelpText = "Convex curves outward, Concave curves inward.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-curve-curvature")]
        public string Curvature { get; set; } = "Convex";

        [DebugProperty(Ignore = true)]
        public IReadOnlyList<string> AvailableCurvatures { get; set; } = new[] { "Convex", "Concave" };

        [DebugProperty(Label = "Tilt (%)", HelpText = "Depth of the curve in percent (1–30).", Min = "1", Max = "30", Step = "1")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-curve-tilt")]
        public decimal Tilt { get; set; } = 8m;

        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}
