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
    ///     Provides extension methods to apply a scroll-driven fade-in/fade-out background image effect to a
    ///     <see cref="SectionBuilder" />.
    /// </summary>
    public static class FadeEffect_SectionExtensions
    {
        #region Static methods

        /// <summary>
        ///     Applies a scroll-driven fade-in and fade-out background image effect to the section,
        ///     with optional scale and vertical parallax offset.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="imageUrl">The URL of the background image. Cannot be null or empty.</param>
        /// <param name="maxDarkness">Maximum darkness overlay applied at the section edges (0–1). Defaults to <c>0.65</c>.</param>
        /// <param name="scale">
        ///     Additional scale factor applied to the image (0–1). Use <c>0</c> for no scaling. Defaults to
        ///     <c>0</c>.
        /// </param>
        /// <param name="verticalOffset">Vertical parallax offset as a percentage. Defaults to <c>0</c>.</param>
        /// <returns>The same <see cref="SectionBuilder" /> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="section" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="imageUrl" /> is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="scale" /> is not between 0 and 1.</exception>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> atmospheric hero sections, cinematic storytelling layouts, immersive
        ///         background reveals that react to the user scrolling through the page.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> a JavaScript scroll listener updates the background image opacity
        ///         based on how much of the section is visible in the viewport. A dark overlay fades in/out
        ///         at the top and bottom edges, controlled by <c>maxDarkness</c>.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> use standalone — combining with other background effects is not recommended
        ///         as this effect fully controls the section background. Can be paired with <c>HighlightSweepEffect</c>
        ///         for an extra cinematic layer.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> use a high-resolution image with a clear subject in the center so it remains
        ///         visible at varying opacities. Set <c>maxDarkness: 0.8</c> for a dramatic vignette effect.
        ///         The <c>scale</c> parameter adds a subtle zoom on scroll; values between 0.05 and 0.15 work best.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.SectionBuilder()
        ///     .FadeInFadeOutEffect("/images/hero.jpg", maxDarkness: 0.65m, scale: 0.1m, verticalOffset: 0m)
        /// </code>
        ///     </para>
        /// </remarks>
        /// <seealso cref="ParallaxEffect_SectionExtensions" />
        /// <seealso cref="ZoomPanEffect_SectionExtensions" />
        /// <seealso cref="HighlightSweepEffect_SectionExtensions" />
        [Documented]
        public static SectionBuilder FadeInFadeOutEffect(
            this SectionBuilder section,
            string imageUrl,
            decimal maxDarkness = 0.65m,
            decimal scale = 0m,
            decimal verticalOffset = 0m
        )
        {
            if (section == null) throw new ArgumentNullException(nameof(section));
            if (string.IsNullOrWhiteSpace(imageUrl)) throw new ArgumentException("Image URL cannot be null.", nameof(imageUrl));
            if (scale < 0m || scale > 1m) throw new ArgumentOutOfRangeException(nameof(scale), "Scale must be between 0 and 1.");

            section.EnsureId("fade");
            section.AddClass("eb-section-effect-fade");

            section.SetAttribute("data-fade-effect", "true");
            section.SetAttribute("data-fade-image", imageUrl);
            section.SetAttribute("data-fade-max-darkness", maxDarkness.ToString(CultureInfo.InvariantCulture));
            section.SetAttribute("data-fade-scale", scale.ToString(CultureInfo.InvariantCulture));
            section.SetAttribute("data-fade-offset-y", verticalOffset.ToString(CultureInfo.InvariantCulture));

            section.SetStyle("--eb-fade-image", $"url('{HtmlEncoder.Default.Encode(imageUrl)}')");
            section.SetStyle("--eb-fade-scale", scale.ToString(CultureInfo.InvariantCulture));
            section.SetStyle("--eb-fade-offset-y", $"{verticalOffset.ToString(CultureInfo.InvariantCulture)}%");
            section.SetStyle("--eb-fade-max-darkness", maxDarkness.ToString(CultureInfo.InvariantCulture));

            PageInformation page = PageRegistry.GetOrCreatePageInformation(section.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/sectionEffects/FadeEffect.css");

            #if DEBUG
            InjectFadeDebugPanel(section, imageUrl, maxDarkness, scale, verticalOffset);
            #endif

            return section;
        }

        #if DEBUG
        private static void InjectFadeDebugPanel(
            SectionBuilder section,
            string imageUrl,
            decimal maxDarkness,
            decimal scale,
            decimal verticalOffset
        )
        {
            string sectionId =
                section.AttributeValue("id")
                ?? throw new InvalidOperationException("Section id missing.");

            section.AddDebugPanel(new FadeEffectDebugModel
            {
                SectionId = sectionId,
                ImageUrl = imageUrl,
                MaxDarkness = maxDarkness,
                Scale = scale,
                VerticalOffset = verticalOffset,
                AvailableImageUrls = SectionEffectsDebugHelper.GetEffectsImageUrls(section.HtmlHelper)
            });
        }
        #endif

        #endregion
    }
}