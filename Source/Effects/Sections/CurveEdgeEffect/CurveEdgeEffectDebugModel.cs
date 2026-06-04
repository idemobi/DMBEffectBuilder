#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.Collections.Generic;
using DMBBootstrapBuilder;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Provides debug panel values for the curve edge section effect.
    /// </summary>
    [DebugModel("Curve Edge", CodePattern = ".CurveEdgeEffect(CurveEdge.{0}, Curvature.{1}, {2}m)")]
    public sealed class CurveEdgeEffectDebugModel
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the selectable curvatures values exposed by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)]
        public IReadOnlyList<string> AvailableCurvatures { get; set; } = new[] { "Convex", "Concave" };

        /// <summary>
        ///     Gets or sets the selectable edges values exposed by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)]
        public IReadOnlyList<string> AvailableEdges { get; set; } = new[] { "Bottom", "Top", "Both" };

        /// <summary>
        ///     Gets or sets the curve direction used by the section edge effect.
        /// </summary>
        [DebugProperty(Label = "Curvature", HelpText = "Convex curves outward, Concave curves inward.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-curve-curvature")]
        public string Curvature { get; set; } = "Convex";

        /// <summary>
        ///     Gets or sets the edge targeted by the section shape effect.
        /// </summary>
        [DebugProperty(Label = "Edge", HelpText = "Which edge(s) to curve.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-curve-edge")]
        public string Edge { get; set; } = "Bottom";

        /// <summary>
        ///     Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)]
        public string SectionId { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the tilt amount used by the section shape effect.
        /// </summary>
        [DebugProperty(Label = "Tilt (%)", HelpText = "Depth of the curve in percent (1–30).", Min = "1", Max = "30", Step = "1")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-curve-tilt")]
        public decimal Tilt { get; set; } = 8m;

        #endregion
    }
}