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
    ///     Provides extension methods to apply a spotlight cursor-tracking effect to a <see cref="SectionBuilder" />.
    /// </summary>
    public static class SpotlightEffect_SectionExtensions
    {
        #region Static methods

        #if DEBUG
        private static void InjectDebugPanel(SectionBuilder section, string color, decimal opacity, int sizePx)
        {
            string sectionId = section.AttributeValue("id") ?? throw new InvalidOperationException("Section id could not be resolved.");
            section.AddDebugPanel(new SpotlightEffectDebugModel
            {
                SectionId = sectionId,
                Color = color,
                Opacity = opacity,
                SizePx = sizePx
            });
        }
        #endif

        /// <summary>
        ///     Applies a spotlight radial glow effect that follows the cursor (or moves automatically) over the section.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="color">The spotlight color in CSS hex format. Defaults to <c>#ffffff</c>.</param>
        /// <param name="opacity">Opacity of the spotlight glow (0–1). Defaults to <c>0.2</c>.</param>
        /// <param name="sizePx">Diameter of the spotlight in pixels. Must be greater than 0. Defaults to <c>300</c>.</param>
        /// <param name="autoMove">
        ///     When <c>true</c>, the spotlight moves automatically. When <c>false</c>, it follows the cursor.
        ///     Defaults to <c>false</c>.
        /// </param>
        /// <returns>The same <see cref="SectionBuilder" /> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="section" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="color" /> is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when <paramref name="sizePx" /> is less than or equal to 0, or
        ///     <paramref name="opacity" /> is not between 0 and 1.
        /// </exception>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> dark hero sections, product showcases, interactive feature demonstrations.
        ///         In cursor-follow mode, it creates an engaging "torch in the dark" experience.
        ///         In auto mode, it provides a drifting ambient glow.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> a radial gradient centered on the cursor position (or an animated position
        ///         in auto mode) is applied via a JavaScript mousemove listener that updates a CSS custom property.
        ///         The gradient fades from the spotlight color to transparent.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> chain after <c>BootstrapBackgroundEffect(Dark)</c> or a dark
        ///         <c>AnimatedGradientEffect</c>. The spotlight sits above the background but below section content.
        ///         Pairs well with <c>NoiseEffect</c> for a textured dark atmosphere.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> a <c>sizePx</c> of 250–400 covers a natural hand-sized area. Larger values
        ///         (500+) create a broad, diffuse ambient glow. Keep <c>opacity</c> between 0.15 and 0.30
        ///         for a subtle effect — higher values can obscure content. Use a warm color (amber, gold)
        ///         for a theatrical spotlight feel, or white for a clean torch effect.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.SectionBuilder()
        ///     .BootstrapBackgroundEffect(VariantStyle.Dark, autoTextColor: true)
        ///     .SpotlightEffect(color: "#ffc832", opacity: 0.25m, sizePx: 300, autoMove: true)
        /// </code>
        ///     </para>
        /// </remarks>
        /// <seealso cref="GlowPulseEffect_SectionExtensions" />
        /// <seealso cref="BootstrapBackgroundEffect_SectionExtensions" />
        [Documented]
        public static SectionBuilder SpotlightEffect(this SectionBuilder section, string color = "#ffffff", decimal opacity = 0.2m, int sizePx = 300, bool autoMove = false)
        {
            if (section == null) throw new ArgumentNullException(nameof(section));
            if (string.IsNullOrWhiteSpace(color)) throw new ArgumentException("Color cannot be null or empty.", nameof(color));
            if (sizePx <= 0) throw new ArgumentOutOfRangeException(nameof(sizePx), "Size must be greater than 0.");
            if (opacity < 0m || opacity > 1m) throw new ArgumentOutOfRangeException(nameof(opacity), "Opacity must be between 0 and 1.");

            section.EnsureId("spotlight");
            section.AddClass("eb-section-effect-spotlight");
            section.SetStyle("--eb-spotlight-color", color);
            section.SetStyle("--eb-spotlight-opacity", opacity.ToString(System.Globalization.CultureInfo.InvariantCulture));
            section.SetStyle("--eb-spotlight-size", $"{sizePx}px");
            section.SetAttribute("data-spotlight-auto", autoMove ? "true" : "false");

            PageInformation page = PageRegistry.GetOrCreatePageInformation(section.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/sectionEffects/SpotlightEffect.css");
            page.SetScriptFile("/js/sectionEffects/SpotlightEffect.js");

            #if DEBUG
            InjectDebugPanel(section, color, opacity, sizePx);
            #endif

            return section;
        }

        #endregion
    }
}