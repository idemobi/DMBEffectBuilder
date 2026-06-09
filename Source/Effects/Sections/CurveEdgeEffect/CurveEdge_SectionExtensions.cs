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
    ///     Provides extension methods to apply a curved clip-path edge to a <see cref="SectionBuilder" />.
    /// </summary>
    public static class CurveEdge_SectionExtensions
    {
        #region Static methods

        /// <summary>
        ///     Applies a curved <c>clip-path</c> to the section, giving it a smooth concave or convex edge.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="edge">Which edge(s) to curve — top, bottom or both. Defaults to <see cref="CurveEdge.Bottom" />.</param>
        /// <param name="curvature">The curvature style (Convex or Concave). Defaults to <see cref="Curvature.Convex" />.</param>
        /// <param name="tilt">The depth of the curve in percentage (0 to 100, usually between 5 and 20). Defaults to <c>8</c>.</param>
        /// <returns>The same <see cref="SectionBuilder" /> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="section" /> is <c>null</c>.</exception>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> elegant section transitions, modern container shapes, flowing page layouts.
        ///         The curved edge creates a smooth visual break between sections without a hard line.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> applies a CSS <c>clip-path: ellipse()</c> computed from the <c>tilt</c>
        ///         percentage, cutting the chosen edge(s) with a smooth curve. The clipped area becomes transparent,
        ///         revealing whatever is behind the section.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> chain after any background effect. Works especially well with
        ///         <c>AnimatedGradientEffect</c> or <c>BootstrapBackgroundEffect</c>. Can be applied to both
        ///         edges simultaneously using <c>CurveEdge.Both</c>.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> a <c>tilt</c> between 5 and 15 gives a natural curve; above 20 creates
        ///         a very pronounced arch. Use <c>Curvature.Convex</c> for an outward bulge and
        ///         <c>Curvature.Concave</c> for an inward dip. Ensure the section has enough height padding
        ///         so content is not hidden behind the clipped area.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.SectionBuilder()
        ///     .AnimatedGradientEffect("#3a86ff", "#8338ec", angle: 180m, durationSeconds: 5m)
        ///     .CurveEdgeEffect(CurveEdge.Bottom, Curvature.Convex, tilt: 12m)
        /// </code>
        ///     </para>
        /// </remarks>
        /// <seealso cref="DiagonalEdge_SectionExtensions" />
        /// <seealso cref="WaveEffect_SectionExtensions" />
        [Documented]
        public static SectionBuilder CurveEdgeEffect(
            this SectionBuilder section,
            CurveEdge edge = CurveEdge.Bottom,
            Curvature curvature = Curvature.Convex,
            decimal tilt = 8m
        )
        {
            if (section == null) throw new ArgumentNullException(nameof(section));

            section.EnsureId("curve-edge");
            section.AddClass("eb-section-effect-curve-edge");
            section.SetAttribute("data-curve-edge", edge.ToString());
            section.SetAttribute("data-curve-curvature", curvature.ToString());
            section.SetAttribute("data-curve-tilt", tilt.ToString(CultureInfo.InvariantCulture));

            string clipPath = CurveEdgeHelper.BuildClipPath(edge, curvature, tilt);
            section.SetStyle("clip-path", clipPath);

            PageInformation page = PageRegistry.GetOrCreatePageInformation(section.HtmlHelper.ViewContext.HttpContext);
            page.SetScriptFile("/js/sectionEffects/CurveEdgeEffect.js");

            #if DEBUG
            InjectDebugPanel(section, edge, curvature, tilt);
            #endif

            return section;
        }

        #if DEBUG
        private static void InjectDebugPanel(SectionBuilder section, CurveEdge edge, Curvature curvature, decimal tilt)
        {
            string sectionId = section.AttributeValue("id") ?? throw new InvalidOperationException("Section id missing.");
            section.AddDebugPanel(new CurveEdgeEffectDebugModel
            {
                SectionId = sectionId,
                Edge = edge.ToString(),
                Curvature = curvature.ToString(),
                Tilt = tilt
            });
        }
        #endif

        #endregion
    }
}