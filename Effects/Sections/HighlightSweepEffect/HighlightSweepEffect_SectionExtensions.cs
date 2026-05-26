#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj HighlightSweepEffect_SectionExtensions.cs create at 2026/04/12 12:04:31
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using System.Globalization;
using DMBBootstrapBuilder;
using DMBPageBuilder;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides extension methods to apply a luminous horizontal sweep animation to a <see cref="SectionBuilder"/>.
    /// </summary>
    public static class HighlightSweepEffect_SectionExtensions
    {
        #region Static methods

        /// <summary>
        /// Applies a luminous horizontal sweep effect over the section, optionally combined with a background image.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="opacity">Opacity of the sweep highlight (0–1). Defaults to <c>0.3</c>.</param>
        /// <param name="imageUrl">Optional background image URL. If empty, no background image is applied.</param>
        /// <param name="fixedBackground">When <c>true</c> and an image URL is provided, applies <c>background-attachment: fixed</c>. Defaults to <c>false</c>.</param>
        /// <returns>The same <see cref="SectionBuilder"/> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="section"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="opacity"/> is not between 0 and 1.</exception>
        /// <remarks>
        /// <para>
        /// <b>Use cases:</b> hero banners with background images, cinematic headers, product showcases.
        /// The sweep creates a sense of light passing over the scene, adding life to otherwise static images.
        /// </para>
        /// <para>
        /// <b>How it works:</b> a semi-transparent white gradient overlay is animated from left to right
        /// using a CSS keyframe, simulating a light reflection sweeping across the surface. An optional
        /// background image is applied beneath the sweep.
        /// </para>
        /// <para>
        /// <b>Combinations:</b> designed to be used with an <c>imageUrl</c> for a cinematic feel.
        /// Without an image, pair it after <c>AnimatedGradientEffect</c> or <c>BootstrapBackgroundEffect</c>
        /// for a sheen effect on a colored background.
        /// </para>
        /// <para>
        /// <b>Tips:</b> keep <c>opacity</c> between 0.2 and 0.5 — higher values look too artificial.
        /// Use <c>fixedBackground: true</c> combined with an image for a parallax sweep effect.
        /// Works best on wide, landscape-oriented images.
        /// </para>
        /// <para>
        /// <b>Example:</b>
        /// <code>
        /// Html.SectionBuilder()
        ///     .HighlightSweepEffect(opacity: 0.3m, imageUrl: "/images/hero.jpg", fixedBackground: false)
        /// </code>
        /// </para>
        /// </remarks>
        /// <seealso cref="ZoomPanEffect_SectionExtensions"/>
        /// <seealso cref="FadeEffect_SectionExtensions"/>
        [Documented]
        public static SectionBuilder HighlightSweepEffect(this SectionBuilder section, decimal opacity = 0.3m, string imageUrl = "", bool fixedBackground = false)
        {
            if (section == null) throw new ArgumentNullException(nameof(section));
            if (opacity < 0m || opacity > 1m) throw new ArgumentOutOfRangeException(nameof(opacity), "Opacity must be between 0 and 1");
            section.EnsureId("sweep");
            section.AddClass("eb-section-effect-highlight-sweep");
            section.SetAttribute("data-sweep-effect", "true");
            section.SetAttribute("data-sweep-opacity", opacity.ToString(CultureInfo.InvariantCulture));
            section.SetAttribute("data-render-counter", "0");
            section.SetStyle("--eb-sweep-opacity", $"{opacity.ToString(CultureInfo.InvariantCulture)}");

            section.SetAttribute("data-sweep-image", imageUrl);

            if (!string.IsNullOrWhiteSpace(imageUrl))
            {
                section.SetStyle("background-image", $"url({imageUrl})");
                if (fixedBackground)
                {
                    section.SetStyle("background-attachment", "fixed");
                }
            }

            PageInformation page = PageRegistry.GetOrCreatePageInformation(section.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/sectionEffects/HighlightSweepEffect.css");

            #if DEBUG
            InjectDebugPanel(section, opacity, imageUrl);
            #endif

            return section;
        }

        #if DEBUG
        private static void InjectDebugPanel(SectionBuilder section, decimal opacity, string imageUrl)
        {
            string sectionId = section.AttributeValue("id") ?? throw new InvalidOperationException("section is missing");
            var model = new HighlightSweepEffectDebugModel
            {
                SectionId          = sectionId,
                Opacity            = opacity,
                ImageUrl           = imageUrl,
                AvailableImageUrls = SectionEffectsDebugHelper.GetEffectsImageUrls(section.HtmlHelper)
            };
            section.AddDebugPanel(model);
        }
        #endif

        #endregion
    }
}