#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using System.Drawing;
using System.Globalization;
using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using DMBServerWebHelper;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Provides extension methods to apply an animated gradient background effect to a <see cref="SectionBuilder" />.
    /// </summary>
    public static class AnimatedGradientEffect_SectionExtensions
    {
        #region Static methods

        /// <summary>
        ///     Applies an animated gradient background effect to the section using <see cref="Color" /> values.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="colorStart">The starting color of the gradient.</param>
        /// <param name="colorEnd">The ending color of the gradient.</param>
        /// <param name="angle">The gradient angle in degrees. Defaults to <c>90</c>.</param>
        /// <param name="durationSeconds">Duration of one animation cycle in seconds. Defaults to <c>8</c>.</param>
        /// <param name="curve">The animation easing curve. Defaults to <see cref="GradientAnimationCurve.EaseInOut" />.</param>
        /// <returns>The same <see cref="SectionBuilder" /> instance for chaining.</returns>
        [Documented]
        public static SectionBuilder AnimatedGradientEffect(
            this SectionBuilder section,
            Color colorStart,
            Color colorEnd,
            decimal angle = 90m,
            decimal durationSeconds = 8m,
            GradientAnimationCurve curve = GradientAnimationCurve.EaseInOut
        )
        {
            return AnimatedGradientEffect(section, ColorHelper.ToHex(colorStart), ColorHelper.ToHex(colorEnd), angle, durationSeconds, curve);
        }

        /// <summary>
        ///     Applies an animated gradient background effect to the section using CSS color strings.
        ///     The gradient smoothly shifts between <paramref name="colorStart" /> and <paramref name="colorEnd" />
        ///     using a CSS keyframe animation.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="colorStart">The starting CSS color (e.g. <c>#ff0000</c> or <c>rgb(255,0,0)</c>).</param>
        /// <param name="colorEnd">The ending CSS color (e.g. <c>#0000ff</c> or <c>rgb(0,0,255)</c>).</param>
        /// <param name="angle">The gradient angle in degrees. Defaults to <c>90</c>.</param>
        /// <param name="durationSeconds">Duration of one animation cycle in seconds. Must be greater than 0. Defaults to <c>8</c>.</param>
        /// <param name="curve">The animation easing curve. Defaults to <see cref="GradientAnimationCurve.EaseInOut" />.</param>
        /// <returns>The same <see cref="SectionBuilder" /> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="section" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">
        ///     Thrown when <paramref name="colorStart" /> or <paramref name="colorEnd" /> is null
        ///     or empty.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when <paramref name="durationSeconds" /> is less than or equal to
        ///     0.
        /// </exception>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> hero sections, colorful CTAs, onboarding pages, app feature banners.
        ///         The most versatile background effect — works standalone or as a base layer for chaining
        ///         other FX &amp; Ambiance overlays.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> animates a CSS linear gradient between two colors using a keyframe
        ///         that shifts the background position on an oversized background, creating a smooth
        ///         infinite color cycle.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> designed to be the first effect in a chain. Most FX &amp; Ambiance effects
        ///         (e.g. <c>CurtainEffect</c>, <c>NoiseEffect</c>, <c>StarfieldEffect</c>) are built to sit on
        ///         top of it:
        ///         <code>
        /// Html.SectionBuilder()
        ///     .AnimatedGradientEffect("#9b5de5", "#f15bb5", angle: 90m, durationSeconds: 5m)
        ///     .CurtainEffect("#4a0e4e")
        /// </code>
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> angle 0° = top→bottom, 90° = left→right, 135° = diagonal.
        ///         Use analogous colors (neighbors on the color wheel) for a harmonious flow,
        ///         or complementary colors for high energy. Duration 4–6 s feels dynamic;
        ///         10–20 s is subtle and ambient. <see cref="GradientAnimationCurve.EaseInOut" />
        ///         gives the smoothest result for most use cases.
        ///     </para>
        /// </remarks>
        /// <seealso cref="AuroraEffect_SectionExtensions" />
        /// <seealso cref="BootstrapBackgroundEffect_SectionExtensions" />
        [Documented]
        public static SectionBuilder AnimatedGradientEffect(
            this SectionBuilder section,
            string colorStart,
            string colorEnd,
            decimal angle = 90m,
            decimal durationSeconds = 8m,
            GradientAnimationCurve curve = GradientAnimationCurve.EaseInOut
        )
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            if (string.IsNullOrWhiteSpace(colorStart))
            {
                throw new ArgumentException("Start color cannot be null or empty.", nameof(colorStart));
            }

            if (string.IsNullOrWhiteSpace(colorEnd))
            {
                throw new ArgumentException("End color cannot be null or empty.", nameof(colorEnd));
            }

            if (durationSeconds <= 0m)
            {
                throw new ArgumentOutOfRangeException(nameof(durationSeconds), "Duration must be greater than 0.");
            }

            PageInformation page = PageRegistry.GetOrCreatePageInformation(section.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/sectionEffects/AnimatedGradientEffect.css");

            section.EnsureId("animated_gradient");

            section.AddClass("eb-section-effect-animated-gradient");

            section.SetAttribute("data-animated-gradient", "true");
            section.SetAttribute("data-gradient-start", colorStart);
            section.SetAttribute("data-gradient-end", colorEnd);
            section.SetAttribute("data-gradient-angle", angle.ToString(CultureInfo.InvariantCulture));
            section.SetAttribute("data-gradient-duration", durationSeconds.ToString(CultureInfo.InvariantCulture));
            section.SetAttribute("data-gradient-curve", curve.GetCss());

            section.SetStyle($"--eb-animated-gradient-start", $"{HtmlEncoder.Default.Encode(colorStart)}");
            section.SetStyle($"--eb-animated-gradient-end", $"{HtmlEncoder.Default.Encode(colorEnd)}");
            section.SetStyle($"--eb-animated-gradient-angle", $"{angle.ToString(CultureInfo.InvariantCulture)}deg");
            section.SetStyle($"--eb-animated-gradient-duration", $"{durationSeconds.ToString(CultureInfo.InvariantCulture)}s");
            section.SetStyle($"--eb-animated-gradient-curve", $"{curve.GetCss()}");
            section.SetStyle($"background", $"linear-gradient(var(--eb-animated-gradient-angle), var(--eb-animated-gradient-start), var(--eb-animated-gradient-end))");
            section.SetStyle($"background-size", $"200% 200%");
            section.SetStyle($"animation", $"eb_animated_gradient_shift_keyframes var(--eb-animated-gradient-duration) var(--eb-animated-gradient-curve) infinite alternate");

            #if DEBUG
            InjectDebugPanel(section, colorStart, colorEnd, angle, durationSeconds, curve);
            #endif

            return section;
        }

        #if DEBUG
        private static void InjectDebugPanel(
            SectionBuilder section,
            string colorStart,
            string colorEnd,
            decimal angle,
            decimal durationSeconds,
            GradientAnimationCurve curve
        )
        {
            string sectionId = section.AttributeValue("id")
                               ?? throw new InvalidOperationException("Section id missing.");

            var model = new AnimatedGradientEffectDebugModel
            {
                SectionId = sectionId,
                ColorStart = colorStart,
                ColorEnd = colorEnd,
                Angle = angle,
                DurationSeconds = durationSeconds,
                Curve = curve
            };

            section.AddDebugPanel(model);
        }
        #endif

        #endregion
    }
}