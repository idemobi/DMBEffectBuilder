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
    ///     Provides extension methods to apply a firefly particle effect to a <see cref="SectionBuilder" />.
    /// </summary>
    public static class FireflyEffect_SectionExtensions
    {
        #region Static methods

        /// <summary>
        ///     Applies an animated firefly particle effect to the section.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="color">The firefly glow color in CSS hex format. Defaults to <c>#aaff88</c> (soft green).</param>
        /// <param name="count">Number of fireflies. Must be greater than 0. Defaults to <c>20</c>.</param>
        /// <param name="size">Diameter of each firefly in pixels. Must be greater than 0. Defaults to <c>3</c>.</param>
        /// <returns>The same <see cref="SectionBuilder" /> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="section" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="color" /> is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when <paramref name="count" /> or <paramref name="size" /> is less
        ///     than or equal to 0.
        /// </exception>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> nature, wellness, meditation or ambient dark sections.
        ///         Creates a calm, magical atmosphere reminiscent of a summer night.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> renders small glowing dots on a canvas, each with a randomized position,
        ///         drift path, and opacity blink animation to simulate organic firefly movement.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> best over a very dark background such as a dark
        ///         <c>AnimatedGradientEffect</c> (deep greens or near-blacks). Avoid bright backgrounds
        ///         where the glow would not be visible.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> keep <c>count</c> between 15 and 30 for a natural density.
        ///         Use warm yellows (<c>#ffee88</c>) for classic fireflies, or soft blues for a fantasy feel.
        ///         A <c>size</c> of 2–4 px is realistic; larger values create a more fantastical effect.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.SectionBuilder()
        ///     .AnimatedGradientEffect("#0a1a0a", "#0d2b0d", angle: 180m, durationSeconds: 8m)
        ///     .FireflyEffect(color: "#aaff88", count: 25, size: 3m)
        /// </code>
        ///     </para>
        ///     <para>
        ///         <b>Performance:</b> each firefly is animated on a shared canvas via <c>requestAnimationFrame</c>.
        ///         Keep <c>count</c> below 40 to maintain smooth frame rates on mobile devices.
        ///         Avoid combining with other canvas-based effects on the same section.
        ///     </para>
        /// </remarks>
        /// <seealso cref="StarfieldEffect_SectionExtensions" />
        /// <seealso cref="BubbleEffect_SectionExtensions" />
        [Documented]
        public static SectionBuilder FireflyEffect(
            this SectionBuilder section,
            string color = "#aaff88",
            int count = 20,
            decimal size = 3m
        )
        {
            if (section == null) throw new ArgumentNullException(nameof(section));
            if (string.IsNullOrWhiteSpace(color)) throw new ArgumentException("Color cannot be null or empty.", nameof(color));
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count), "Count must be greater than 0.");
            if (size <= 0m) throw new ArgumentOutOfRangeException(nameof(size), "Size must be greater than 0.");

            section.EnsureId("firefly");
            section.AddClass("eb-section-effect-firefly");
            section.SetAttribute("data-firefly-color", color);
            section.SetAttribute("data-firefly-count", count.ToString());
            section.SetAttribute("data-firefly-size", size.ToString(System.Globalization.CultureInfo.InvariantCulture));

            PageInformation page = PageRegistry.GetOrCreatePageInformation(section.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/sectionEffects/FireflyEffect.css");
            page.SetScriptFile("/js/sectionEffects/FireflyEffect.js");

            #if DEBUG
            InjectDebugPanel(section, color, count, size);
            #endif

            return section;
        }

        #if DEBUG
        private static void InjectDebugPanel(SectionBuilder section, string color, int count, decimal size)
        {
            string sectionId = section.AttributeValue("id") ?? throw new InvalidOperationException("Section id could not be resolved.");
            section.AddDebugPanel(new FireflyEffectDebugModel
            {
                SectionId = sectionId,
                Color = color,
                Count = count,
                Size = size
            });
        }
        #endif

        #endregion
    }
}