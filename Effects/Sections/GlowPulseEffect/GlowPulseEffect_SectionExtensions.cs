#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj GlowPulseEffect_SectionExtensions.cs create at 2026/04/21
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;
using DMBPageBuilder;

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides extension methods to apply a pulsing glow background effect to a <see cref="SectionBuilder"/>.
    /// </summary>
    public static class GlowPulseEffect_SectionExtensions
    {
        #region Static methods

        /// <summary>
        /// Applies a pulsing radial glow animation to the section background.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="color">The glow color in CSS hex format. Defaults to <c>#64C8FF</c>.</param>
        /// <param name="opacity">Opacity of the glow (0–1). Defaults to <c>0.5</c>.</param>
        /// <param name="speedSeconds">Duration of one pulse cycle in seconds. Must be greater than 0. Defaults to <c>2</c>.</param>
        /// <returns>The same <see cref="SectionBuilder"/> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="section"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="color"/> is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="speedSeconds"/> is less than or equal to 0, or <paramref name="opacity"/> is not between 0 and 1.</exception>
        /// <remarks>
        /// <para>
        /// <b>Use cases:</b> call-to-action blocks, feature highlights, notification banners.
        /// The breathing glow draws the eye to the section and creates a sense of energy or urgency.
        /// </para>
        /// <para>
        /// <b>How it works:</b> animates an inset <c>box-shadow</c> on a <c>::after</c> pseudo-element
        /// between a tight and wide glow radius, creating a pulsing border-like effect that does not
        /// interfere with the section's content or <c>overflow</c> clipping.
        /// </para>
        /// <para>
        /// <b>Combinations:</b> chain after <c>BootstrapBackgroundEffect</c> or
        /// <c>AnimatedGradientEffect</c>. Pairs naturally with <c>PulseRingEffect</c> for a layered
        /// pulse look, but avoid using both simultaneously as they compete visually.
        /// </para>
        /// <para>
        /// <b>Tips:</b> blue or cyan tones (<c>#64C8FF</c>) work well on dark backgrounds.
        /// Use a warm tone (amber, orange) for urgency. A <c>speedSeconds</c> of 1.5–3 s feels
        /// natural; below 1 s feels frantic. Keep <c>opacity</c> between 0.3 and 0.6 for a
        /// subtle but visible pulse.
        /// </para>
        /// <para>
        /// <b>Example:</b>
        /// <code>
        /// Html.SectionBuilder()
        ///     .BootstrapBackgroundEffect(VariantStyle.Dark, autoTextColor: true)
        ///     .GlowPulseEffect(color: "#64C8FF", opacity: 0.5m, speedSeconds: 2m)
        /// </code>
        /// </para>
        /// </remarks>
        /// <seealso cref="PulseRingEffect_SectionExtensions"/>
        /// <seealso cref="BootstrapBackgroundEffect_SectionExtensions"/>
        [Documented]
        public static SectionBuilder GlowPulseEffect(this SectionBuilder section,
            string color = "#64C8FF",
            decimal opacity = 0.5m,
            decimal speedSeconds = 2m)
        {
            if (section == null) throw new ArgumentNullException(nameof(section));
            if (string.IsNullOrWhiteSpace(color)) throw new ArgumentException("Color cannot be null or empty.", nameof(color));
            if (opacity < 0m || opacity > 1m) throw new ArgumentOutOfRangeException(nameof(opacity), "Opacity must be between 0 and 1.");
            if (speedSeconds <= 0m) throw new ArgumentOutOfRangeException(nameof(speedSeconds), "Speed must be greater than 0.");

            section.EnsureId("glow-pulse");
            section.AddClass("eb-section-effect-glow-pulse");
            section.SetStyle("--eb-glow-color", color);
            section.SetStyle("--eb-glow-opacity", opacity.ToString(System.Globalization.CultureInfo.InvariantCulture));
            section.SetStyle("--eb-glow-speed", $"{speedSeconds.ToString(System.Globalization.CultureInfo.InvariantCulture)}s");

            PageInformation page = PageRegistry.GetOrCreatePageInformation(section.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/sectionEffects/GlowPulseEffect.css");

            #if DEBUG
            InjectDebugPanel(section, color, opacity, speedSeconds);
            #endif

            return section;
        }

        #if DEBUG
        private static void InjectDebugPanel(SectionBuilder section, string color, decimal opacity, decimal speedSeconds)
        {
            string sectionId = section.AttributeValue("id") ?? throw new InvalidOperationException("Section id could not be resolved.");
            section.AddDebugPanel(new GlowPulseEffectDebugModel
            {
                SectionId    = sectionId,
                Color        = color,
                Opacity      = opacity,
                SpeedSeconds = speedSeconds
            });
        }
        #endif

        #endregion
    }
}
