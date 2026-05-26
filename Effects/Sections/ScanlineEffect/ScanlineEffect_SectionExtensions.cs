#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj ScanlineEffect_SectionExtensions.cs create at 2026/04/21
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;
using DMBPageBuilder;

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides extension methods to apply a CRT scanline overlay to a <see cref="SectionBuilder"/>.
    /// </summary>
    public static class ScanlineEffect_SectionExtensions
    {
        #region Static methods

        /// <summary>
        /// Applies a CRT-style horizontal scanline overlay to the section.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="opacity">Opacity of the scanline overlay (0–1). Defaults to <c>0.15</c>.</param>
        /// <param name="spacingPx">Vertical spacing between scanlines in pixels. Must be greater than 0. Defaults to <c>4</c>.</param>
        /// <returns>The same <see cref="SectionBuilder"/> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="section"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="opacity"/> is not between 0 and 1, or <paramref name="spacingPx"/> is less than or equal to 0.</exception>
        /// <remarks>
        /// <para>
        /// <b>Use cases:</b> retro gaming, synthwave, vintage UI, CRT monitor aesthetics, 80s-inspired pages.
        /// Adds an unmistakable analog television texture to any section.
        /// </para>
        /// <para>
        /// <b>How it works:</b> a repeating CSS linear gradient of alternating transparent and dark horizontal
        /// lines is applied as a pseudo-element overlay. Pure CSS — no JavaScript or canvas required,
        /// making it extremely lightweight.
        /// </para>
        /// <para>
        /// <b>Combinations:</b> designed as an overlay — always chain after a background effect.
        /// The classic combination is a dark <c>AnimatedGradientEffect</c> + <c>ScanlineEffect</c>.
        /// Can be stacked with <c>NoiseEffect</c> for a heavy retro-CRT look (use low opacities for both).
        /// Also complements <c>MatrixEffect</c> and <c>RainEffect</c> for a terminal aesthetic.
        /// </para>
        /// <para>
        /// <b>Tips:</b> an <c>opacity</c> of 0.10–0.20 is subtly noticeable. Above 0.35 the lines
        /// become prominent and intentional — suitable for a strong retro statement.
        /// A <c>spacingPx</c> of 3–5 mimics a real CRT screen; larger values (8–12) create
        /// a coarser, more stylized grid look.
        /// </para>
        /// <para>
        /// <b>Example:</b>
        /// <code>
        /// Html.SectionBuilder()
        ///     .AnimatedGradientEffect("#1a0033", "#330066", angle: 180m, durationSeconds: 8m)
        ///     .ScanlineEffect(opacity: 0.15m, spacingPx: 4)
        /// </code>
        /// </para>
        /// </remarks>
        /// <seealso cref="NoiseEffect_SectionExtensions"/>
        /// <seealso cref="MatrixEffect_SectionExtensions"/>
        [Documented]
        public static SectionBuilder ScanlineEffect(this SectionBuilder section,
            decimal opacity = 0.15m,
            int spacingPx = 4)
        {
            if (section == null) throw new ArgumentNullException(nameof(section));
            if (opacity < 0m || opacity > 1m) throw new ArgumentOutOfRangeException(nameof(opacity), "Opacity must be between 0 and 1.");
            if (spacingPx <= 0) throw new ArgumentOutOfRangeException(nameof(spacingPx), "Spacing must be greater than 0.");

            section.EnsureId("scanline");
            section.AddClass("eb-section-effect-scanline");
            section.SetStyle("--eb-scanline-opacity", opacity.ToString(System.Globalization.CultureInfo.InvariantCulture));
            section.SetStyle("--eb-scanline-spacing", $"{spacingPx}px");

            PageInformation page = PageRegistry.GetOrCreatePageInformation(section.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/sectionEffects/ScanlineEffect.css");

            #if DEBUG
            InjectDebugPanel(section, opacity, spacingPx);
            #endif

            return section;
        }

        #if DEBUG
        private static void InjectDebugPanel(SectionBuilder section, decimal opacity, int spacingPx)
        {
            string sectionId = section.AttributeValue("id") ?? throw new InvalidOperationException("Section id could not be resolved.");
            section.AddDebugPanel(new ScanlineEffectDebugModel
            {
                SectionId = sectionId,
                Opacity   = opacity,
                SpacingPx = spacingPx
            });
        }
        #endif

        #endregion
    }
}
