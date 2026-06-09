#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.Globalization;
using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Provides extension methods to apply a split-curtain reveal animation to a <see cref="SectionBuilder" />.
    /// </summary>
    public static class CurtainEffect_SectionExtensions
    {
        #region Static methods

        /// <summary>
        ///     Applies a split-curtain animation effect to the section, revealing content as two panels slide apart.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="color">The curtain panel color in CSS format. Defaults to <c>#8B0000</c> (dark red).</param>
        /// <param name="stop">Controls where the two curtain panels meet or overlap. Defaults to <see cref="CurtainStop.Meet" />.</param>
        /// <returns>The same <see cref="SectionBuilder" /> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="section" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="color" /> is null or empty.</exception>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> dramatic page intros, hero section reveals, event or film-inspired layouts.
        ///         The curtain plays once on load and stays closed, revealing the content underneath.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> two absolutely-positioned panels slide inward from the left and right edges
        ///         using CSS keyframe animations, then hold their final position with <c>animation-fill-mode: forwards</c>.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> chain after a background effect such as <c>AnimatedGradientEffect</c>
        ///         so the curtain panels reveal a lively background. The curtain overlays any content inside the section.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> use <c>CurtainStop.Meet</c> for a clean center close, <c>CurtainStop.Overlap</c>
        ///         for a dramatic overreach, or <c>CurtainStop.Gap</c> to leave a thin gap of content visible.
        ///         Match the curtain <c>color</c> to your brand or page background for a seamless transition.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.SectionBuilder()
        ///     .AnimatedGradientEffect("#9b5de5", "#f15bb5", angle: 90m, durationSeconds: 5m)
        ///     .CurtainEffect(color: "#4a0e4e", stop: CurtainStop.Meet)
        /// </code>
        ///     </para>
        /// </remarks>
        /// <seealso cref="AnimatedGradientEffect_SectionExtensions" />
        /// <seealso cref="BootstrapBackgroundEffect_SectionExtensions" />
        [Documented]
        public static SectionBuilder CurtainEffect(
            this SectionBuilder section,
            string color = "#8B0000",
            CurtainStop stop = CurtainStop.Meet
        )
        {
            if (section == null) throw new ArgumentNullException(nameof(section));
            if (string.IsNullOrWhiteSpace(color)) throw new ArgumentException("Color cannot be null or empty.", nameof(color));

            decimal stopPct = stop switch
            {
                CurtainStop.Meet => 50m,
                CurtainStop.Overlap => 60m,
                CurtainStop.Gap => 38m,
                _ => 50m
            };

            var ci = CultureInfo.InvariantCulture;

            section.EnsureId("curtain");
            section.AddClass("eb-section-effect-curtain");
            section.SetStyle("--eb-curtain-color", HtmlEncoder.Default.Encode(color));
            section.SetStyle("--eb-curtain-stop", $"{stopPct.ToString(ci)}%");
            section.SetStyle("--eb-curtain-duration", "1.5s");

            PageInformation page = PageRegistry.GetOrCreatePageInformation(section.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/sectionEffects/CurtainEffect.css");

            #if DEBUG
            InjectDebugPanel(section, color, stopPct);
            #endif

            return section;
        }

        #if DEBUG
        private static void InjectDebugPanel(SectionBuilder section, string color, decimal stopPct)
        {
            string sectionId = section.AttributeValue("id") ?? throw new InvalidOperationException("Section id could not be resolved.");
            section.SetAttribute("data-debug-restart-animation", "eb-section-effect-curtain");
            section.AddDebugPanel(new CurtainEffectDebugModel
            {
                SectionId = sectionId,
                Color = color,
                StopPercent = stopPct
            });
        }
        #endif

        #endregion
    }
}