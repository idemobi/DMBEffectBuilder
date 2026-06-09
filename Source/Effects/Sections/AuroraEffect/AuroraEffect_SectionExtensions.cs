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
    ///     Provides extension methods to apply an aurora borealis background effect to a <see cref="SectionBuilder" />.
    /// </summary>
    public static class AuroraEffect_SectionExtensions
    {
        #region Static methods

        /// <summary>
        ///     Applies an animated aurora borealis background effect to the section using three customizable color layers.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="color1">First aurora color in CSS hex format. Defaults to <c>#64DCB4</c>.</param>
        /// <param name="color2">Second aurora color in CSS hex format. Defaults to <c>#508CFF</c>.</param>
        /// <param name="color3">Third aurora color in CSS hex format. Defaults to <c>#C864FF</c>.</param>
        /// <param name="opacity1">Opacity of color 1 (0–1). Defaults to <c>0.45</c>.</param>
        /// <param name="opacity2">Opacity of color 2 (0–1). Defaults to <c>0.35</c>.</param>
        /// <param name="opacity3">Opacity of color 3 (0–1). Defaults to <c>0.30</c>.</param>
        /// <param name="speedSeconds">Duration of one animation cycle in seconds. Must be greater than 0. Defaults to <c>8</c>.</param>
        /// <returns>The same <see cref="SectionBuilder" /> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="section" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when <paramref name="speedSeconds" /> is less than or equal to 0,
        ///     or any opacity is not between 0 and 1.
        /// </exception>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> premium hero sections, landing pages that need an organic and immersive feel,
        ///         nature or wellness themes, high-end SaaS homepages.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> three semi-transparent color blobs are animated independently using CSS keyframes,
        ///         creating a soft flowing aurora effect. The blending relies on <c>mix-blend-mode</c> and blur filters.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> works well standalone. Can be layered with <c>NoiseEffect</c> to add texture,
        ///         or with <c>ScanlineEffect</c> for a retro twist.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> choose analogous colors (e.g. teal + blue + purple) for a natural aurora feel.
        ///         Complementary colors create a more energetic result. Keep opacities below 0.5 for a subtle look.
        ///         Speed between 6 s and 12 s gives the most natural movement.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.SectionBuilder()
        ///     .AuroraEffect(color1: "#a855f7", color2: "#22c55e", color3: "#3b82f6", speedSeconds: 8m)
        /// </code>
        ///     </para>
        /// </remarks>
        /// <seealso cref="AnimatedGradientEffect_SectionExtensions" />
        /// <seealso cref="NoiseEffect_SectionExtensions" />
        [Documented]
        public static SectionBuilder AuroraEffect(
            this SectionBuilder section,
            string color1 = "#64DCB4",
            string color2 = "#508CFF",
            string color3 = "#C864FF",
            decimal opacity1 = 0.45m,
            decimal opacity2 = 0.35m,
            decimal opacity3 = 0.30m,
            decimal speedSeconds = 8m
        )
        {
            if (section == null) throw new ArgumentNullException(nameof(section));
            if (opacity1 < 0m || opacity1 > 1m) throw new ArgumentOutOfRangeException(nameof(opacity1), "Opacity must be between 0 and 1.");
            if (opacity2 < 0m || opacity2 > 1m) throw new ArgumentOutOfRangeException(nameof(opacity2), "Opacity must be between 0 and 1.");
            if (opacity3 < 0m || opacity3 > 1m) throw new ArgumentOutOfRangeException(nameof(opacity3), "Opacity must be between 0 and 1.");
            if (speedSeconds <= 0m) throw new ArgumentOutOfRangeException(nameof(speedSeconds), "Speed must be greater than 0.");

            section.EnsureId("aurora");
            section.AddClass("eb-section-effect-aurora");
            section.SetStyle("--eb-aurora-color1", color1);
            section.SetStyle("--eb-aurora-color2", color2);
            section.SetStyle("--eb-aurora-color3", color3);
            section.SetStyle("--eb-aurora-opacity1", opacity1.ToString(System.Globalization.CultureInfo.InvariantCulture));
            section.SetStyle("--eb-aurora-opacity2", opacity2.ToString(System.Globalization.CultureInfo.InvariantCulture));
            section.SetStyle("--eb-aurora-opacity3", opacity3.ToString(System.Globalization.CultureInfo.InvariantCulture));
            section.SetStyle("--eb-aurora-speed", $"{speedSeconds.ToString(System.Globalization.CultureInfo.InvariantCulture)}s");

            PageInformation page = PageRegistry.GetOrCreatePageInformation(section.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/sectionEffects/AuroraEffect.css");

            #if DEBUG
            InjectDebugPanel(section, color1, color2, color3, opacity1, opacity2, opacity3, speedSeconds);
            #endif

            return section;
        }

        #if DEBUG
        private static void InjectDebugPanel(
            SectionBuilder section,
            string color1,
            string color2,
            string color3,
            decimal opacity1,
            decimal opacity2,
            decimal opacity3,
            decimal speedSeconds
        )
        {
            string sectionId = section.AttributeValue("id") ?? throw new InvalidOperationException("Section id could not be resolved.");
            section.AddDebugPanel(new AuroraEffectDebugModel
            {
                SectionId = sectionId,
                Color1 = color1,
                Color2 = color2,
                Color3 = color3,
                Opacity1 = opacity1,
                Opacity2 = opacity2,
                Opacity3 = opacity3,
                SpeedSeconds = speedSeconds
            });
        }
        #endif

        #endregion
    }
}