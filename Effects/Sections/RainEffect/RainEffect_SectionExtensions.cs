#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj RainEffect_SectionExtensions.cs create at 2026/04/17
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using System.Globalization;
using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides extension methods to apply an animated rain drop effect to a <see cref="SectionBuilder"/>.
    /// </summary>
    public static class RainEffect_SectionExtensions
    {
        #region Static methods

        /// <summary>
        /// Applies an animated rain drop canvas effect to the section.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="color">The rain drop color in CSS hex format. Defaults to <c>#00ff41</c>.</param>
        /// <param name="opacity">Opacity of the rain drops (0–1). Defaults to <c>0.6</c>.</param>
        /// <param name="drops">Number of simultaneous rain drops. Must be greater than 0. Defaults to <c>80</c>.</param>
        /// <returns>The same <see cref="SectionBuilder"/> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="section"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="color"/> is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="opacity"/> is not between 0 and 1, or <paramref name="drops"/> is less than or equal to 0.</exception>
        /// <remarks>
        /// <para>
        /// <b>Use cases:</b> atmospheric, moody or tech-themed sections. Works well for cyberpunk,
        /// developer tools, or weather-related product pages.
        /// </para>
        /// <para>
        /// <b>How it works:</b> renders vertical streaks on a canvas element using the JavaScript
        /// animation loop, each drop falling at a randomized speed and x-position, with a short
        /// motion-blur trail.
        /// </para>
        /// <para>
        /// <b>Combinations:</b> chain after a dark background — <c>BootstrapBackgroundEffect(Dark)</c>
        /// or a dark <c>AnimatedGradientEffect</c>. Green rain (<c>#00ff41</c>) over black is the
        /// classic Matrix-inspired variant; blue rain over dark navy evokes a stormy night.
        /// Can be combined with <c>ScanlineEffect</c> for a terminal aesthetic.
        /// </para>
        /// <para>
        /// <b>Tips:</b> keep <c>opacity</c> between 0.4 and 0.8 — too low and the rain disappears,
        /// too high and it overwhelms the content. A <c>drops</c> count of 60–120 gives dense rain;
        /// 20–40 creates a sparse drizzle. The color does not need to be green — white rain on a
        /// dark photo background creates a beautiful snow-like effect.
        /// </para>
        /// <para>
        /// <b>Example:</b>
        /// <code>
        /// Html.SectionBuilder()
        ///     .BootstrapBackgroundEffect(VariantStyle.Dark, autoTextColor: true)
        ///     .RainEffect(color: "#00ff41", opacity: 0.6m, drops: 80)
        /// </code>
        /// </para>
        /// <para>
        /// <b>Performance:</b> rain drops are drawn on a canvas via <c>requestAnimationFrame</c>.
        /// Keep <c>drops</c> below 150 to avoid frame rate drops on mobile devices.
        /// Avoid combining with other canvas-based effects on the same section.
        /// </para>
        /// </remarks>
        /// <seealso cref="MatrixEffect_SectionExtensions"/>
        /// <seealso cref="ScanlineEffect_SectionExtensions"/>
        [Documented]
        public static SectionBuilder RainEffect(this SectionBuilder section, string color = "#00ff41", decimal opacity = 0.6m, int drops = 80)
        {
            if (section == null) throw new ArgumentNullException(nameof(section));
            if (string.IsNullOrWhiteSpace(color)) throw new ArgumentException("Color cannot be null or empty.", nameof(color));
            if (opacity < 0m || opacity > 1m) throw new ArgumentOutOfRangeException(nameof(opacity), "Opacity must be between 0 and 1.");
            if (drops <= 0) throw new ArgumentOutOfRangeException(nameof(drops), "Drops count must be greater than 0.");

            section.EnsureId("rain");
            section.AddClass("eb-section-effect-rain");
            section.SetAttribute("data-rain-color", HtmlEncoder.Default.Encode(color));
            section.SetAttribute("data-rain-opacity", opacity.ToString(CultureInfo.InvariantCulture));
            section.SetAttribute("data-rain-drops", drops.ToString());

            PageInformation page = PageRegistry.GetOrCreatePageInformation(section.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/sectionEffects/RainEffect.css");
            page.SetScriptFile("/js/sectionEffects/RainEffect.js");

            #if DEBUG
            InjectDebugPanel(section, color, opacity, drops);
            #endif

            return section;
        }

        #if DEBUG
        private static void InjectDebugPanel(SectionBuilder section, string color, decimal opacity, int drops)
        {
            string sectionId = section.AttributeValue("id") ?? throw new InvalidOperationException("Section id could not be resolved.");
            section.AddDebugPanel(new RainEffectDebugModel
            {
                SectionId = sectionId,
                Color     = color,
                Opacity   = opacity,
                Drops     = drops
            });
        }
        #endif

        #endregion
    }
}
