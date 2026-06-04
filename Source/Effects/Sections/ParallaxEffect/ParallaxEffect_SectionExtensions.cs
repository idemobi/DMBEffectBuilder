#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using System.Globalization;
using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Provides extension methods to apply a scroll-driven parallax background to a <see cref="SectionBuilder" />.
    /// </summary>
    public static class ParallaxEffect_SectionExtensions
    {
        #region Static methods

        #if DEBUG
        private static void InjectDebugPanel(SectionBuilder section, string imageUrl, decimal speed)
        {
            string sectionId = section.AttributeValue("id")
                               ?? throw new InvalidOperationException("Section id could not be resolved.");

            section.AddDebugPanel(new ParallaxEffectDebugModel
            {
                SectionId = sectionId,
                ImageUrl = imageUrl,
                Speed = speed,
                BaseY = 50m,
                AvailableImageUrls = SectionEffectsDebugHelper.GetEffectsImageUrls(section.HtmlHelper)
            });
        }
        #endif

        /// <summary>
        ///     Applies a parallax scrolling background image to the section.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="imageUrl">The URL of the background image. Cannot be null or empty.</param>
        /// <param name="speed">Parallax scroll speed factor. Higher values increase movement. Defaults to <c>0.25</c>.</param>
        /// <returns>The same <see cref="SectionBuilder" /> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="section" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="imageUrl" /> is null or empty.</exception>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> immersive hero sections, travel or landscape pages, photography portfolios.
        ///         The background appears to move slower than the page scroll, creating a compelling depth illusion.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> a JavaScript scroll listener computes the vertical offset of the background
        ///         image relative to the scroll position, applying it as a CSS <c>background-position-y</c>.
        ///         Unlike <c>FixedBackgroundEffect</c>, this works correctly on iOS Safari and within
        ///         scrollable containers.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> use standalone as the primary background. Can be combined with
        ///         <c>HighlightSweepEffect</c> for an extra cinematic layer, or with <c>FadeInFadeOutEffect</c>
        ///         for a scroll-reactive opacity transition.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> a <c>speed</c> of 0.2–0.4 gives a subtle, natural parallax. Values above 0.6
        ///         create a very pronounced movement that can feel disorienting. Use tall images (portrait
        ///         orientation, at least 1.5× the section height) to avoid showing empty areas when scrolling.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.SectionBuilder()
        ///     .ParallaxBackground("/images/mountains.jpg", speed: 0.25m)
        /// </code>
        ///     </para>
        /// </remarks>
        /// <seealso cref="FixedBackgroundEffect_SectionExtensions" />
        /// <seealso cref="FadeEffect_SectionExtensions" />
        /// <seealso cref="ZoomPanEffect_SectionExtensions" />
        [Documented]
        public static SectionBuilder ParallaxBackground(this SectionBuilder section, string imageUrl, decimal speed = 0.25m)
        {
            if (section == null) throw new ArgumentNullException(nameof(section));
            if (string.IsNullOrWhiteSpace(imageUrl)) throw new ArgumentException("Image URL cannot be null or empty.", nameof(imageUrl));

            section.EnsureId("parallax");
            section.AddClass("eb-section-effect-parallax");

            section.SetAttribute("data-parallax", "true");
            section.SetAttribute("data-parallax-image", imageUrl);
            section.SetAttribute("data-parallax-speed", speed.ToString(CultureInfo.InvariantCulture));
            section.SetAttribute("data-parallax-base-y", "50");

            section.SetStyle("background-image", $"url('{HtmlEncoder.Default.Encode(imageUrl)}')");
            section.SetStyle("background-size", "cover");
            section.SetStyle("background-repeat", "no-repeat");
            section.SetStyle("background-position", "center calc(50% + 0px)");
            section.SetStyle("will-change", "background-position");

            PageInformation page = PageRegistry.GetOrCreatePageInformation(section.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/sectionEffects/ParallaxEffect.css");
            page.SetScriptFile("/js/sectionEffects/ParallaxEffect.js");

            #if DEBUG
            InjectDebugPanel(section, imageUrl, speed);
            #endif

            return section;
        }

        #endregion
    }
}