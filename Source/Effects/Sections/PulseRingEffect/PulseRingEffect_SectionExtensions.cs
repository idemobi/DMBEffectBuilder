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
    ///     Provides extension methods to apply concentric expanding ring pulse animations to a <see cref="SectionBuilder" />.
    /// </summary>
    public static class PulseRingEffect_SectionExtensions
    {
        #region Static methods

        #if DEBUG
        private static void InjectDebugPanel(SectionBuilder section, string color, decimal opacity, int count, decimal speedSeconds)
        {
            string sectionId = section.AttributeValue("id") ?? throw new InvalidOperationException("Section id could not be resolved.");
            section.AddDebugPanel(new PulseRingEffectDebugModel
            {
                SectionId = sectionId,
                Color = color,
                Opacity = opacity,
                Count = count,
                SpeedSeconds = speedSeconds
            });
        }
        #endif

        /// <summary>
        ///     Applies an animated concentric expanding ring pulse effect to the section.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="color">The ring color in CSS hex format. Defaults to <c>#ffffff</c>.</param>
        /// <param name="opacity">Opacity of the rings (0–1). Defaults to <c>0.6</c>.</param>
        /// <param name="count">Number of concentric rings. Must be greater than 0. Defaults to <c>3</c>.</param>
        /// <param name="speedSeconds">Duration of one expansion cycle in seconds. Must be greater than 0. Defaults to <c>3</c>.</param>
        /// <returns>The same <see cref="SectionBuilder" /> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="section" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="color" /> is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when <paramref name="count" /> or <paramref name="speedSeconds" />
        ///     is less than or equal to 0, or <paramref name="opacity" /> is not between 0 and 1.
        /// </exception>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> radar or sonar themed sections, broadcast or signal visualizations,
        ///         location-based services, technology or IoT product showcases.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> renders concentric rings that expand outward from the section center
        ///         and fade out, staggered in time so that multiple rings are always visible simultaneously.
        ///         Implemented with CSS keyframe animations on absolutely-positioned elements.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> chain after <c>BootstrapBackgroundEffect(Dark)</c> or a dark
        ///         <c>AnimatedGradientEffect</c>. The rings are most visible on dark backgrounds.
        ///         Avoid combining with <c>GlowPulseEffect</c> as both compete for the same visual space.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> use white or light-colored rings on dark backgrounds. A <c>count</c> of 3–5
        ///         creates a continuous ripple; more rings risk visual clutter. A <c>speedSeconds</c> of
        ///         2–4 s feels energetic; 5–8 s creates a slow, meditative pulse. Reduce <c>opacity</c>
        ///         to 0.3–0.4 for a subtle ambient effect.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.SectionBuilder()
        ///     .BootstrapBackgroundEffect(VariantStyle.Dark, autoTextColor: true)
        ///     .PulseRingEffect(color: "#ffffff", opacity: 0.6m, count: 3, speedSeconds: 3m)
        /// </code>
        ///     </para>
        /// </remarks>
        /// <seealso cref="GlowPulseEffect_SectionExtensions" />
        /// <seealso cref="RippleEffect_SectionExtensions" />
        [Documented]
        public static SectionBuilder PulseRingEffect(
            this SectionBuilder section,
            string color = "#ffffff",
            decimal opacity = 0.6m,
            int count = 3,
            decimal speedSeconds = 3m
        )
        {
            if (section == null) throw new ArgumentNullException(nameof(section));
            if (string.IsNullOrWhiteSpace(color)) throw new ArgumentException("Color cannot be null or empty.", nameof(color));
            if (opacity < 0m || opacity > 1m) throw new ArgumentOutOfRangeException(nameof(opacity), "Opacity must be between 0 and 1.");
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count), "Count must be greater than 0.");
            if (speedSeconds <= 0m) throw new ArgumentOutOfRangeException(nameof(speedSeconds), "Speed must be greater than 0.");

            section.EnsureId("pulse-ring");
            section.AddClass("eb-section-effect-pulse-ring");
            section.SetAttribute("data-pulse-color", color);
            section.SetAttribute("data-pulse-opacity", opacity.ToString(System.Globalization.CultureInfo.InvariantCulture));
            section.SetAttribute("data-pulse-count", count.ToString());
            section.SetAttribute("data-pulse-speed", speedSeconds.ToString(System.Globalization.CultureInfo.InvariantCulture));

            PageInformation page = PageRegistry.GetOrCreatePageInformation(section.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/sectionEffects/PulseRingEffect.css");
            page.SetScriptFile("/js/sectionEffects/PulseRingEffect.js");

            #if DEBUG
            InjectDebugPanel(section, color, opacity, count, speedSeconds);
            #endif

            return section;
        }

        #endregion
    }
}