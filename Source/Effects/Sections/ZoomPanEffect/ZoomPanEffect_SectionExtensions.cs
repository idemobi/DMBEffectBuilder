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
    ///     Provides extension methods to apply a slow Ken Burns zoom-and-pan background animation to a
    ///     <see cref="SectionBuilder" />.
    /// </summary>
    public static class ZoomPanEffect_SectionExtensions
    {
        #region Static methods

        #if DEBUG
        private static void InjectDebugPanel(SectionBuilder section, string imageUrl, decimal speedSeconds)
        {
            string sectionId = section.AttributeValue("id") ?? throw new InvalidOperationException("Section id could not be resolved.");
            section.AddDebugPanel(new ZoomPanEffectDebugModel
            {
                SectionId = sectionId,
                ImageUrl = imageUrl,
                SpeedSeconds = speedSeconds,
                AvailableImageUrls = SectionEffectsDebugHelper.GetEffectsImageUrls(section.HtmlHelper)
            });
        }
        #endif

        /// <summary>
        ///     Applies a slow zoom-and-pan (Ken Burns) animation to a background image on the section.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="imageUrl">The URL of the background image. Cannot be null or empty.</param>
        /// <param name="speedSeconds">Duration of one zoom-pan cycle in seconds. Must be greater than 0. Defaults to <c>12</c>.</param>
        /// <returns>The same <see cref="SectionBuilder" /> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="section" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="imageUrl" /> is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="speedSeconds" /> is less than or equal to 0.</exception>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> cinematic headers, event or film-inspired pages, photography portfolios,
        ///         documentary-style storytelling sections. The slow drift brings a static image to life.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> applies a CSS keyframe animation that simultaneously scales and
        ///         translates the background image, alternating between a zoomed-in corner and the opposite
        ///         corner to create a continuous panning motion — the Ken Burns effect.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> use standalone as the primary background. Can be combined with
        ///         <c>HighlightSweepEffect</c> for an extra cinematic glare layer over the moving image.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> use a high-resolution image (at least 1920×1080) — the zoom reveals parts
        ///         of the image that would otherwise be cropped, so low-resolution images will pixelate.
        ///         A <c>speedSeconds</c> of 15–25 s is imperceptibly slow but constantly moving, creating
        ///         a living background feel. Values below 8 s look rushed and mechanical.
        ///         Choose images with a clear subject that remains readable even when partially zoomed out.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.SectionBuilder()
        ///     .ZoomPanEffect("/images/cityscape.jpg", speedSeconds: 12m)
        /// </code>
        ///     </para>
        /// </remarks>
        /// <seealso cref="FixedBackgroundEffect_SectionExtensions" />
        /// <seealso cref="ParallaxEffect_SectionExtensions" />
        /// <seealso cref="FadeEffect_SectionExtensions" />
        [Documented]
        public static SectionBuilder ZoomPanEffect(
            this SectionBuilder section,
            string imageUrl,
            decimal speedSeconds = 12m
        )
        {
            if (section == null) throw new ArgumentNullException(nameof(section));
            if (string.IsNullOrWhiteSpace(imageUrl)) throw new ArgumentException("Image URL cannot be null or empty.", nameof(imageUrl));
            if (speedSeconds <= 0m) throw new ArgumentOutOfRangeException(nameof(speedSeconds), "Speed must be greater than 0.");

            section.EnsureId("zoom-pan");
            section.AddClass("eb-section-effect-zoom-pan");
            section.SetStyle("--eb-zoom-pan-image", $"url('{imageUrl}')");
            section.SetStyle("--eb-zoom-pan-speed", $"{speedSeconds.ToString(System.Globalization.CultureInfo.InvariantCulture)}s");

            PageInformation page = PageRegistry.GetOrCreatePageInformation(section.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/sectionEffects/ZoomPanEffect.css");

            #if DEBUG
            InjectDebugPanel(section, imageUrl, speedSeconds);
            #endif

            return section;
        }

        #endregion
    }
}