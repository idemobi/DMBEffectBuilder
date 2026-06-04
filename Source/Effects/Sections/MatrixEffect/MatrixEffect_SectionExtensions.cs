#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using DMBBootstrapBuilder;
using DMBPageBuilder;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Provides extension methods to apply a Matrix-style falling character rain effect to a <see cref="SectionBuilder" />
    ///     .
    /// </summary>
    public static class MatrixEffect_SectionExtensions
    {
        #region Static methods

        #if DEBUG
        private static void InjectDebugPanel(SectionBuilder section, string color, int fontSize, decimal speed)
        {
            string sectionId = section.AttributeValue("id") ?? throw new InvalidOperationException("Section id could not be resolved.");
            section.AddDebugPanel(new MatrixEffectDebugModel
            {
                SectionId = sectionId,
                Color = color,
                FontSize = fontSize,
                Speed = speed
            });
        }
        #endif

        /// <summary>
        ///     Applies an animated Matrix-style falling character rain rendered on a canvas to the section.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="color">The character color in CSS hex format. Defaults to <c>#00ff41</c> (Matrix green).</param>
        /// <param name="fontSize">Font size of the characters in pixels. Must be greater than 0. Defaults to <c>14</c>.</param>
        /// <param name="speed">Fall speed multiplier. Must be greater than 0. Defaults to <c>1</c>.</param>
        /// <returns>The same <see cref="SectionBuilder" /> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="section" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="color" /> is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when <paramref name="fontSize" /> or <paramref name="speed" /> is
        ///     less than or equal to 0.
        /// </exception>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> hacker, cyberpunk or developer-themed pages, tech conference landing pages,
        ///         security or AI product showcases.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> renders columns of falling katakana and latin characters on a canvas element.
        ///         Each column resets to the top at a random interval, creating the iconic cascading rain effect.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> best over a near-black background set via <c>AnimatedGradientEffect</c>
        ///         (e.g. <c>#000000</c> to <c>#001100</c>). Can be combined with <c>ScanlineEffect</c>
        ///         for an extra retro-terminal feel.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> the classic look uses <c>#00ff41</c> (Matrix green) on black. For a blue-tinted
        ///         cyberpunk variant, try <c>#00cfff</c> on dark navy. A <c>fontSize</c> of 12–16 px
        ///         and <c>speed: 1</c> gives the canonical appearance. Increase speed above 2 for an
        ///         overclocked feel; reduce it below 0.5 for a slow, ominous effect.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.SectionBuilder()
        ///     .BootstrapBackgroundEffect(VariantStyle.Dark, autoTextColor: true)
        ///     .MatrixEffect(color: "#00ff41", fontSize: 14, speed: 1m)
        /// </code>
        ///     </para>
        ///     <para>
        ///         <b>Performance:</b> the effect runs a <c>requestAnimationFrame</c> loop that clears and
        ///         redraws the entire canvas on every frame. Avoid using on multiple sections simultaneously,
        ///         and prefer a single full-width canvas per page.
        ///     </para>
        /// </remarks>
        /// <seealso cref="RainEffect_SectionExtensions" />
        /// <seealso cref="ScanlineEffect_SectionExtensions" />
        [Documented]
        public static SectionBuilder MatrixEffect(
            this SectionBuilder section,
            string color = "#00ff41",
            int fontSize = 14,
            decimal speed = 1m
        )
        {
            if (section == null) throw new ArgumentNullException(nameof(section));
            if (string.IsNullOrWhiteSpace(color)) throw new ArgumentException("Color cannot be null or empty.", nameof(color));
            if (fontSize <= 0) throw new ArgumentOutOfRangeException(nameof(fontSize), "Font size must be greater than 0.");
            if (speed <= 0m) throw new ArgumentOutOfRangeException(nameof(speed), "Speed must be greater than 0.");

            section.EnsureId("matrix");
            section.AddClass("eb-section-effect-matrix");
            section.SetAttribute("data-matrix-color", color);
            section.SetAttribute("data-matrix-font-size", fontSize.ToString());
            section.SetAttribute("data-matrix-speed", speed.ToString(System.Globalization.CultureInfo.InvariantCulture));

            PageInformation page = PageRegistry.GetOrCreatePageInformation(section.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/sectionEffects/MatrixEffect.css");
            page.SetScriptFile("/js/sectionEffects/MatrixEffect.js");

            #if DEBUG
            InjectDebugPanel(section, color, fontSize, speed);
            #endif

            return section;
        }

        #endregion
    }
}