#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.Globalization;
using DMBBootstrapBuilder;
using DMBPageBuilder;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Provides extension methods to apply a diagonal clip-path edge to a <see cref="SectionBuilder" />.
    /// </summary>
    public static class DiagonalEdge_SectionExtensions
    {
        #region Static methods

        #if DEBUG
        private static void InjectDebugPanel(SectionBuilder section, DiagonalEdge edge, DiagonalDirection direction, decimal tilt, int tornPoints)
        {
            string sectionId = section.AttributeValue("id") ?? throw new InvalidOperationException("Section id missing.");
            section.AddDebugPanel(new DiagonalEdgeEffectDebugModel
            {
                SectionId = sectionId,
                Edge = edge.ToString(),
                Direction = direction.ToString(),
                Tilt = tilt,
                TornPoints = tornPoints
            });
        }
        #endif

        /// <summary>
        ///     Applies a diagonal <c>clip-path</c> to the section, giving it an angled or torn edge appearance.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="edge">Which edge to clip — top or bottom. Defaults to <see cref="DiagonalEdge.Bottom" />.</param>
        /// <param name="direction">The diagonal direction. Defaults to <see cref="DiagonalDirection.LeftToRight" />.</param>
        /// <param name="tilt">Tilt angle in degrees (clamped to 1–40). If <c>null</c>, a random value between 8 and 18 is used.</param>
        /// <param name="tornPoints">Number of points used to create a torn/jagged edge. Defaults to <c>30</c>.</param>
        /// <returns>The same <see cref="SectionBuilder" /> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="section" /> is <c>null</c>.</exception>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> dynamic section separators, landing page layouts, parallelogram-style
        ///         feature blocks. The diagonal cut adds energy and direction to an otherwise static layout.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> applies a CSS <c>clip-path: polygon()</c> with an angled bottom or top edge.
        ///         When <c>tornPoints</c> is greater than 0, additional micro-jitter points are added along the edge
        ///         to simulate a torn paper or rough cut texture.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> chain after any background effect. Use on consecutive sections with
        ///         alternating directions (<c>LeftToRight</c> then <c>RightToLeft</c>) to create a zigzag layout.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> leave <c>tilt</c> as <c>null</c> for a random angle that feels natural.
        ///         Set a fixed value (e.g. <c>12</c>) for consistent alignment across multiple sections.
        ///         Set <c>tornPoints: 0</c> for a clean geometric diagonal; increase it (20–40) for a rough,
        ///         hand-torn look. Ensure enough bottom padding so content does not overlap the clipped area.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.SectionBuilder()
        ///     .AnimatedGradientEffect("#0f2027", "#203a43", angle: 135m, durationSeconds: 6m)
        ///     .SetDiagonalEdge(DiagonalEdge.Bottom, DiagonalDirection.LeftToRight, tilt: 12m, tornPoints: 0)
        /// </code>
        ///     </para>
        /// </remarks>
        /// <seealso cref="CurveEdge_SectionExtensions" />
        /// <seealso cref="WaveEffect_SectionExtensions" />
        [Documented]
        public static SectionBuilder SetDiagonalEdge(
            this SectionBuilder section,
            DiagonalEdge edge = DiagonalEdge.Bottom,
            DiagonalDirection direction = DiagonalDirection.LeftToRight,
            decimal? tilt = null,
            int tornPoints = 30
        )
        {
            if (section == null) throw new ArgumentNullException(nameof(section));

            decimal resolvedTilt = tilt ?? Random.Shared.Next(8, 18);
            resolvedTilt = Math.Clamp(resolvedTilt, 1m, 40m);

            section.EnsureId("diagonal-edge");
            section.AddClass("eb-section-effect-diagonal-edge");
            section.SetAttribute("data-diagonal-edge", edge.ToString());
            section.SetAttribute("data-diagonal-direction", direction.ToString());
            section.SetAttribute("data-diagonal-tilt", resolvedTilt.ToString(CultureInfo.InvariantCulture));
            section.SetAttribute("data-diagonal-torn-points", tornPoints.ToString());

            string clipPath = DiagonalEdgeHelper.BuildClipPath(edge, direction, resolvedTilt, tornPoints);
            section.SetStyle("clip-path", clipPath);

            PageInformation page = PageRegistry.GetOrCreatePageInformation(section.HtmlHelper.ViewContext.HttpContext);
            page.SetScriptFile("/js/sectionEffects/DiagonalEdgeEffect.js");

            #if DEBUG
            InjectDebugPanel(section, edge, direction, resolvedTilt, tornPoints);
            #endif

            return section;
        }

        #endregion
    }
}