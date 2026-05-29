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
    ///     Provides debug panel values for the diagonal edge section effect.
    /// </summary>
    [DebugModel("Diagonal Edge", CodePattern = ".SetDiagonalEdge(DiagonalEdge.{0}, DiagonalDirection.{1}, {2}m, {3})")]
    public sealed class DiagonalEdgeEffectDebugModel
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the selectable directions values exposed by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)]
        public IReadOnlyList<string> AvailableDirections { get; set; } = new[] { "LeftToRight", "RightToLeft" };

        /// <summary>
        ///     Gets or sets the selectable edges values exposed by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)]
        public IReadOnlyList<string> AvailableEdges { get; set; } = new[] { "Bottom", "Top", "Both" };

        /// <summary>
        ///     Gets or sets the diagonal direction used by the section shape effect.
        /// </summary>
        [DebugProperty(Label = "Direction", HelpText = "Diagonal slope direction.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-diagonal-direction")]
        public string Direction { get; set; } = "LeftToRight";

        /// <summary>
        ///     Gets or sets the edge targeted by the section shape effect.
        /// </summary>
        [DebugProperty(Label = "Edge", HelpText = "Which edge(s) to clip.")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-diagonal-edge")]
        public string Edge { get; set; } = "Bottom";

        /// <summary>
        ///     Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)]
        public string SectionId { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the tilt amount used by the section shape effect.
        /// </summary>
        [DebugProperty(Label = "Tilt (deg)", HelpText = "Angle depth in percent (1–40).", Min = "1", Max = "40", Step = "1")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-diagonal-tilt")]
        public decimal Tilt { get; set; } = 13m;

        /// <summary>
        ///     Gets or sets the number of generated torn-edge points.
        /// </summary>
        [DebugProperty(Label = "Torn points", HelpText = "Number of jagged points along the edge (0 = smooth).", Min = "0", Max = "60", Step = "5")]
        [DebugPropertyTarget(DebugTarget.DataAttribute, "data-diagonal-torn-points")]
        public int TornPoints { get; set; } = 30;

        #endregion
    }
}