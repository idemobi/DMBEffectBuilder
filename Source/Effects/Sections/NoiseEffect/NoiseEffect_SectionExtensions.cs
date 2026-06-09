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
    ///     Provides extension methods to apply a film grain noise overlay to a <see cref="SectionBuilder" />.
    /// </summary>
    public static class NoiseEffect_SectionExtensions
    {
        #region Static methods

        #if DEBUG
        private static void InjectDebugPanel(SectionBuilder section, decimal opacity, decimal speed)
        {
            string sectionId = section.AttributeValue("id") ?? throw new InvalidOperationException("Section id could not be resolved.");
            section.AddDebugPanel(new NoiseEffectDebugModel
            {
                SectionId = sectionId,
                Opacity = opacity,
                Speed = speed
            });
        }
        #endif

        /// <summary>
        ///     Applies an animated film grain noise overlay to the section.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="opacity">Opacity of the noise layer (0–1). Defaults to <c>0.12</c>.</param>
        /// <param name="speed">Animation speed of the noise in seconds. Must be greater than 0. Defaults to <c>0.4</c>.</param>
        /// <returns>The same <see cref="SectionBuilder" /> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="section" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when <paramref name="opacity" /> is not between 0 and 1, or
        ///     <paramref name="speed" /> is less than or equal to 0.
        /// </exception>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> retro, editorial, cinematic or premium sections. Adds tactile texture to
        ///         otherwise flat digital designs, giving them a physical, printed quality.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> a small SVG-based noise pattern is rendered as a tiling CSS background
        ///         and animated with rapid position shifts, simulating the look of analog film grain.
        ///         The effect is pure CSS — no canvas or JavaScript required.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> designed as an overlay — always chain it after a background effect.
        ///         Works especially well over <c>AnimatedGradientEffect</c>, <c>AuroraEffect</c> or
        ///         <c>BootstrapBackgroundEffect</c>. Can be stacked with <c>ScanlineEffect</c> for a heavy
        ///         retro-CRT look, but use low opacities for both to avoid visual clutter.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> an <c>opacity</c> of 0.08–0.20 is barely perceptible but adds professional
        ///         depth. Above 0.35 the grain becomes very prominent and intentional. A <c>speed</c> of 0.3–0.5 s
        ///         mimics real film grain; higher values create a flickering digital glitch effect.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.SectionBuilder()
        ///     .AuroraEffect(new[] { "#3a86ff", "#8338ec" }, speed: 8m)
        ///     .NoiseEffect(opacity: 0.12m, speed: 0.4m)
        /// </code>
        ///     </para>
        /// </remarks>
        /// <seealso cref="ScanlineEffect_SectionExtensions" />
        /// <seealso cref="AuroraEffect_SectionExtensions" />
        [Documented]
        public static SectionBuilder NoiseEffect(this SectionBuilder section, decimal opacity = 0.12m, decimal speed = 0.4m)
        {
            if (section == null) throw new ArgumentNullException(nameof(section));
            if (opacity < 0m || opacity > 1m) throw new ArgumentOutOfRangeException(nameof(opacity), "Opacity must be between 0 and 1.");
            if (speed <= 0m) throw new ArgumentOutOfRangeException(nameof(speed), "Speed must be greater than 0.");

            section.EnsureId("noise");
            section.AddClass("eb-section-effect-noise");
            section.SetStyle("--eb-noise-opacity", opacity.ToString(CultureInfo.InvariantCulture));
            section.SetStyle("--eb-noise-speed", $"{speed.ToString(CultureInfo.InvariantCulture)}s");

            PageInformation page = PageRegistry.GetOrCreatePageInformation(section.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/sectionEffects/NoiseEffect.css");

            #if DEBUG
            InjectDebugPanel(section, opacity, speed);
            #endif

            return section;
        }

        #endregion
    }
}