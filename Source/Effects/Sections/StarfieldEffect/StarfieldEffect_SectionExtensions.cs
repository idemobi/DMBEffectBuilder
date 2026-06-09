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
    ///     Provides extension methods to apply a scrolling starfield particle effect to a <see cref="SectionBuilder" />.
    /// </summary>
    public static class StarfieldEffect_SectionExtensions
    {
        #region Static methods

        #if DEBUG
        private static void InjectDebugPanel(SectionBuilder section, string color, int count, decimal speed)
        {
            string sectionId = section.AttributeValue("id") ?? throw new InvalidOperationException("Section id could not be resolved.");
            section.AddDebugPanel(new StarfieldEffectDebugModel
            {
                SectionId = sectionId,
                Color = color,
                Count = count,
                Speed = speed
            });
        }
        #endif

        /// <summary>
        ///     Applies an animated scrolling starfield canvas effect to the section.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="color">The star color in CSS hex format. Defaults to <c>#ffffff</c>.</param>
        /// <param name="count">Number of stars. Must be greater than 0. Defaults to <c>120</c>.</param>
        /// <param name="speed">Scroll speed multiplier. Must be greater than 0. Defaults to <c>1.5</c>.</param>
        /// <returns>The same <see cref="SectionBuilder" /> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="section" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="color" /> is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when <paramref name="count" /> or <paramref name="speed" /> is
        ///     less than or equal to 0.
        /// </exception>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> space, science, tech or sci-fi themed sections. Creates an immersive
        ///         sense of depth and movement, as if traveling through space or looking at a night sky.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> renders stars on a canvas as dots that expand outward from the center,
        ///         simulating a warp-speed or zoom-through-space perspective. Stars are recycled as they
        ///         reach the canvas edge to maintain a continuous flow.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> best over a very dark background. Chain after a dark
        ///         <c>AnimatedGradientEffect</c> (deep blues, purples or near-black). Avoid combining
        ///         with dense particle effects like <c>MatrixEffect</c> or <c>RainEffect</c>.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> a <c>count</c> of 80–150 creates a dense star field; 30–60 is more sparse and calm.
        ///         A <c>speed</c> of 1–2 gives gentle star movement; above 3 creates a warp-drive feel.
        ///         Use off-white (<c>#e8e8ff</c>) for a realistic star color, or tinted colors for a stylized look.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.SectionBuilder()
        ///     .AnimatedGradientEffect("#000010", "#000033", angle: 180m, durationSeconds: 10m)
        ///     .StarfieldEffect(color: "#ffffff", count: 120, speed: 1.5m)
        /// </code>
        ///     </para>
        ///     <para>
        ///         <b>Performance:</b> stars are rendered on a canvas via <c>requestAnimationFrame</c>.
        ///         Keep <c>count</c> below 200 to maintain smooth frame rates on mobile devices.
        ///         Avoid combining with other canvas-based effects on the same section.
        ///     </para>
        /// </remarks>
        /// <seealso cref="FireflyEffect_SectionExtensions" />
        /// <seealso cref="AnimatedGradientEffect_SectionExtensions" />
        [Documented]
        public static SectionBuilder StarfieldEffect(
            this SectionBuilder section,
            string color = "#ffffff",
            int count = 120,
            decimal speed = 1.5m
        )
        {
            if (section == null) throw new ArgumentNullException(nameof(section));
            if (string.IsNullOrWhiteSpace(color)) throw new ArgumentException("Color cannot be null or empty.", nameof(color));
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count), "Count must be greater than 0.");
            if (speed <= 0m) throw new ArgumentOutOfRangeException(nameof(speed), "Speed must be greater than 0.");

            section.EnsureId("starfield");
            section.AddClass("eb-section-effect-starfield");
            section.SetAttribute("data-starfield-color", color);
            section.SetAttribute("data-starfield-count", count.ToString());
            section.SetAttribute("data-starfield-speed", speed.ToString(System.Globalization.CultureInfo.InvariantCulture));

            PageInformation page = PageRegistry.GetOrCreatePageInformation(section.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/sectionEffects/StarfieldEffect.css");
            page.SetScriptFile("/js/sectionEffects/StarfieldEffect.js");

            #if DEBUG
            InjectDebugPanel(section, color, count, speed);
            #endif

            return section;
        }

        #endregion
    }
}