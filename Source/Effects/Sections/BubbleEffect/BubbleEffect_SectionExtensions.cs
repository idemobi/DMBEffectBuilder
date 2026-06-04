#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using System.Linq;
using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Provides extension methods to apply floating colored bubble orbs to a <see cref="SectionBuilder" />.
    /// </summary>
    public static class BubbleEffect_SectionExtensions
    {
        #region Static methods

        /// <summary>
        ///     Applies an animated floating bubble orb effect to the section background.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="colors">Array of CSS hex colors for the bubbles. Defaults to pink, yellow, cyan.</param>
        /// <param name="count">Number of bubbles. Must be greater than 0. Defaults to <c>6</c>.</param>
        /// <param name="minSize">Minimum bubble diameter in pixels. Defaults to <c>100</c>.</param>
        /// <param name="maxSize">
        ///     Maximum bubble diameter in pixels. Must be greater than or equal to <paramref name="minSize" />.
        ///     Defaults to <c>300</c>.
        /// </param>
        /// <param name="blur">Blur radius applied to bubbles in pixels. Must be 0 or greater. Defaults to <c>50</c>.</param>
        /// <returns>The same <see cref="SectionBuilder" /> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="section" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when <paramref name="count" /> is less than or equal to 0,
        ///     <paramref name="blur" /> is negative, or the size range is invalid.
        /// </exception>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> creative agency homepages, portfolio sections, colorful landing pages,
        ///         app feature showcases. Adds a lively, modern feel without being overwhelming.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> renders absolutely-positioned blurred colored orbs that drift slowly
        ///         using CSS keyframe animations. Each orb has a randomized size, position and animation delay.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> pairs well with <c>BootstrapBackgroundEffect(Dark)</c> as a base.
        ///         Can also be layered with <c>NoiseEffect</c> for a textured finish.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> use 3–5 colors from the same palette for a cohesive look. Increase <c>blur</c>
        ///         (60–100 px) for a softer, more atmospheric result. Keep <c>count</c> between 4 and 8 —
        ///         too many orbs can hurt performance on low-end devices.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.SectionBuilder()
        ///     .BootstrapBackgroundEffect(VariantStyle.Dark, true)
        ///     .BubbleEffect(new[] { "#ff6ecf", "#efff5c", "#00f5d4" }, count: 5, minSize: 100, maxSize: 260, blur: 55)
        /// </code>
        ///     </para>
        ///     <para>
        ///         <b>Performance:</b> each bubble is a CSS-animated DOM element. Keep <c>count</c> at or below 8
        ///         to avoid layout thrashing on low-end devices. Prefer a single particle effect per page.
        ///     </para>
        /// </remarks>
        /// <seealso cref="LavaLampEffect_SectionExtensions" />
        /// <seealso cref="FireflyEffect_SectionExtensions" />
        [Documented]
        public static SectionBuilder BubbleEffect(
            this SectionBuilder section,
            string[]? colors = null,
            int count = 6,
            int minSize = 100,
            int maxSize = 300,
            int blur = 50
        )
        {
            if (section == null) throw new ArgumentNullException(nameof(section));
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count), "Count must be greater than 0.");
            if (minSize <= 0 || maxSize <= 0 || minSize > maxSize) throw new ArgumentOutOfRangeException(nameof(minSize), "Invalid size range.");
            if (blur < 0) throw new ArgumentOutOfRangeException(nameof(blur), "Blur must be 0 or greater.");

            colors ??= new[] { "#ff6ecf", "#efff5c", "#00f5d4" };

            var encodedColors = string.Join(",", colors.Select(c => HtmlEncoder.Default.Encode(c)));

            section.EnsureId("bubble");
            section.AddClass("eb-section-effect-bubble");
            section.SetAttribute("data-bubble-count", count.ToString());
            section.SetAttribute("data-bubble-colors", encodedColors);
            section.SetAttribute("data-bubble-min-size", minSize.ToString());
            section.SetAttribute("data-bubble-max-size", maxSize.ToString());
            section.SetAttribute("data-bubble-blur", blur.ToString());

            PageInformation page = PageRegistry.GetOrCreatePageInformation(section.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/sectionEffects/BubbleEffect.css");
            page.SetScriptFile("/js/sectionEffects/BubbleEffect.js");

            #if DEBUG
            InjectDebugPanel(section, count, minSize, maxSize, blur);
            #endif

            return section;
        }

        #if DEBUG
        private static void InjectDebugPanel(SectionBuilder section, int count, int minSize, int maxSize, int blur)
        {
            string sectionId = section.AttributeValue("id") ?? throw new InvalidOperationException("Section id could not be resolved.");
            section.AddDebugPanel(new BubbleEffectDebugModel
            {
                SectionId = sectionId,
                Count = count,
                MinSize = minSize,
                MaxSize = maxSize,
                Blur = blur
            });
        }
        #endif

        #endregion
    }
}