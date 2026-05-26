#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj FixedBackgroundEffect_SectionExtensions.cs create at 2026/04/07 21:04:27
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides extension methods to apply a fixed parallax background image to a <see cref="SectionBuilder"/>.
    /// </summary>
    public static class FixedBackgroundEffect_SectionExtensions
    {
        #region Static methods

        /// <summary>
        /// Applies a fixed (parallax-like) background image to the section using CSS <c>background-attachment: fixed</c>.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="imageUrl">The URL of the background image. Cannot be null or empty.</param>
        /// <returns>The same <see cref="SectionBuilder"/> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="section"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="imageUrl"/> is null or empty.</exception>
        /// <remarks>
        /// <para>
        /// <b>Use cases:</b> depth and layering effects, portfolio sections, photography showcases.
        /// The image stays locked to the viewport while the page content scrolls over it,
        /// creating a natural parallax illusion.
        /// </para>
        /// <para>
        /// <b>How it works:</b> sets <c>background-attachment: fixed</c> on the section, which pins
        /// the background image to the viewport coordinate system rather than the element.
        /// </para>
        /// <para>
        /// <b>Combinations:</b> use standalone as the primary background. Can be combined with
        /// <c>HighlightSweepEffect</c> for an extra luminous layer over the fixed image.
        /// </para>
        /// <para>
        /// <b>Tips:</b> use a large, high-resolution landscape image (at least 1920×1080) to avoid
        /// pixelation at wide viewports. Note that <c>background-attachment: fixed</c> is ignored
        /// on iOS Safari within scrollable containers — for mobile-friendly parallax, prefer
        /// <c>ParallaxBackground</c> which uses a JavaScript-based approach.
        /// </para>
        /// <para>
        /// <b>Example:</b>
        /// <code>
        /// Html.SectionBuilder()
        ///     .FixedBackgroundEffect("/images/landscape.jpg")
        /// </code>
        /// </para>
        /// </remarks>
        /// <seealso cref="ParallaxEffect_SectionExtensions"/>
        [Documented]
        public static SectionBuilder FixedBackgroundEffect(
            this SectionBuilder section,
            string imageUrl
        )
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                throw new ArgumentException("Image URL cannot be null or empty.", nameof(imageUrl));
            }

            section.EnsureId("fixed_bg");

            section.AddClass("fixed-bg-section");

            section.SetAttribute("data-fixed-bg", "true");
            section.SetAttribute("data-fixed-bg-image", imageUrl);

            section.SetStyle($"background-image", $"url('{HtmlEncoder.Default.Encode(imageUrl)}')");
            section.SetStyle($"background-size", $"cover");
            section.SetStyle($"background-repeat", $"no-repeat");
            section.SetStyle($"background-position", $"center center");
            section.SetStyle($"background-attachment", $"fixed");

            #if DEBUG
            InjectDebugPanel(section, imageUrl);
            #endif

            return section;
        }

        #if DEBUG
        private static void InjectDebugPanel(
            SectionBuilder section,
            string imageUrl
        )
        {
            string sectionId = section.AttributeValue("id")
                               ?? throw new InvalidOperationException("Section id missing.");

            section.AddDebugPanel(new FixedBackgroundEffectDebugModel
            {
                SectionId          = sectionId,
                ImageUrl           = imageUrl,
                AvailableImageUrls = SectionEffectsDebugHelper.GetEffectsImageUrls(section.HtmlHelper)
            });
        }
        #endif

        #endregion
    }
}