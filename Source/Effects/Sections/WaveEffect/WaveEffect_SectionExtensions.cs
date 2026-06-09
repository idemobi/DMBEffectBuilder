#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using DMBPageBuilder;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Provides extension methods to apply a wave clip-path edge to a <see cref="SectionBuilder" />.
    /// </summary>
    public static class WaveEffect_SectionExtensions
    {
        #region Static methods

        #if DEBUG
        private static void InjectDebugPanel(SectionBuilder section, WaveEdge edge, int amplitude)
        {
            string sectionId = section.AttributeValue("id") ?? throw new InvalidOperationException("Section id could not be resolved.");
            section.AddDebugPanel(new WaveEffectDebugModel
            {
                SectionId = sectionId,
                Edge = edge.ToString(),
                Amplitude = amplitude
            });
        }
        #endif

        /// <summary>
        ///     Clips the section with a smooth wave curve on the specified edge, making the wave area transparent.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="edge">Which edge to apply the wave to. Defaults to <see cref="WaveEdge.Bottom" />.</param>
        /// <param name="amplitude">Height of the wave in pixels. Must be greater than 0. Defaults to <c>80</c>.</param>
        /// <returns>The same <see cref="SectionBuilder" /> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="section" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="amplitude" /> is less than or equal to 0.</exception>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> smooth transitions between sections, hero footers, ocean or nature-themed pages.
        ///         The wave creates an organic, flowing visual break that is less rigid than a straight diagonal cut.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> generates an inline SVG path that forms a sinusoidal wave, then applies it
        ///         as a CSS <c>clip-path</c> using a <c>url()</c> reference. The SVG is embedded directly in the
        ///         page so no external file is needed.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> chain after any background effect. Particularly effective with
        ///         <c>AnimatedGradientEffect</c> — the animated gradient under the wave clip creates a
        ///         colorful flowing edge. Can be applied to <c>WaveEdge.Top</c>, <c>WaveEdge.Bottom</c>
        ///         or both simultaneously using separate chained calls.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> an <c>amplitude</c> of 40–80 px gives a gentle wave; 100–150 px creates
        ///         a dramatic, deep trough. Ensure the section has enough padding on the waved edge so
        ///         that content is not clipped. On adjacent sections, mirror the wave direction (one top,
        ///         one bottom) for a seamless interlocking appearance.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.SectionBuilder()
        ///     .AnimatedGradientEffect("#3a86ff", "#8338ec", angle: 180m, durationSeconds: 5m)
        ///     .WaveEffect(WaveEdge.Bottom, amplitude: 80)
        /// </code>
        ///     </para>
        /// </remarks>
        /// <seealso cref="DiagonalEdge_SectionExtensions" />
        /// <seealso cref="CurveEdge_SectionExtensions" />
        [Documented]
        public static SectionBuilder WaveEffect(this SectionBuilder section, WaveEdge edge = WaveEdge.Bottom, int amplitude = 80)
        {
            if (section == null) throw new ArgumentNullException(nameof(section));
            if (amplitude <= 0) throw new ArgumentOutOfRangeException(nameof(amplitude), "Amplitude must be greater than 0.");

            section.EnsureId("wave");
            section.AddClass("eb-section-effect-wave");
            section.SetAttribute("data-wave-edge", edge.ToString());
            section.SetStyle("--eb-wave-height", $"{amplitude}px");
            section.SetStyle("clip-path", WaveEdgeHelper.BuildClipPath(edge));

            PageInformation page = PageRegistry.GetOrCreatePageInformation(section.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/sectionEffects/WaveEffect.css");
            page.SetScriptFile("/js/sectionEffects/WaveEffect.js");

            #if DEBUG
            InjectDebugPanel(section, edge, amplitude);
            #endif

            return section;
        }

        #endregion
    }
}