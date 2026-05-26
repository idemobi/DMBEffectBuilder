#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj DarkenToNormalEffect_ImageExtension.cs create at 2026/04/21
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using DMBBootstrapBuilder;
using DMBPageBuilder;
using System.Globalization;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides an extension method to apply a darken-to-normal effect to images using the PageBuilder framework.
    /// This class enhances the rendering capabilities of images by integrating a specific CSS effect, thereby
    /// contributing to the visual richness of web pages.
    /// </summary>
    /// <remarks>
    /// The <see cref="DarkenToNormalEffect_ImageExtension"/> class is designed to work within the PageBuilder
    /// rendering model, specifically targeting <see cref="ImageRenderBuilder"/> instances. It leverages the
    /// PageInformation to ensure that the necessary CSS stylesheet is included in the page, thus enabling
    /// the visual effect.
    /// The method <see cref="DarkenToNormalEffect"/> modifies the image builder by marking it as an effect
    /// and wrapping it in a media component with a specific CSS class. This approach ensures that the effect
    /// is applied correctly during the rendering pipeline.
    /// Usage of this extension method requires that the PageBuilder framework is properly set up and that
    /// the associated CSS file ("/css/imageEffects/DarkenToNormalEffect.css") is available in the project.
    /// </remarks>
    [Documented]
    public static class DarkenToNormalEffect_ImageExtension
    {
        /// <summary>Renders the image at a reduced brightness on load and transitions it to full normal brightness, creating a dramatic reveal effect.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder"/> to apply the effect to.</param>
        /// <param name="brightness">The starting brightness multiplier (0.0 = black to 1.0 = normal). Defaults to 0.55.</param>
        /// <returns>The modified <see cref="ImageRenderBuilder"/> instance with the effect applied.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> Hero banners, portfolio thumbnails, or product images where a fade-in-from-dark entrance adds cinematic weight to page load or scroll-into-view moments.</para>
        /// <para><b>How it works:</b> Sets the CSS variable <c>--eb-image-effect-brightness</c> and adds the class <c>eb-image-effect-darken-to-normal</c> to the media wrapper via <c>DarkenToNormalEffect.css</c>. The transition plays once on load.</para>
        /// <para><b>Combinations:</b> Works well alongside <c>FocusSpotEffect</c> to draw the eye after the reveal. Avoid combining with <c>GhostEffect</c> as both lower perceived brightness and the result will appear too muted.</para>
        /// <para><b>Tips:</b> Values between 0.3 and 0.6 for <paramref name="brightness"/> give a noticeable but not extreme darkening. Avoid values below 0.2 on images with fine detail as it obscures too much at load time.</para>
        /// <para><b>Example:</b>
        /// <code>
        /// @Html.ImageRender("/images/photo.jpg").SetWidth(100, UnitSize.percent).DarkenToNormalEffect(brightness: 0.4)
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder DarkenToNormalEffect(this ImageRenderBuilder builder, double brightness = 0.55)
        {
            builder.SetStyle("--eb-image-effect-brightness", brightness.ToString(CultureInfo.InvariantCulture));

            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/DarkenToNormalEffect.css");
            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-darken-to-normal"));
        }
    }
}
