#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.Globalization;
using DMBBootstrapBuilder;
using DMBPageBuilder;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Provides an extension method to apply a fade hover effect to images using the PageBuilder framework.
    ///     This class enhances the rendering capabilities of <see cref="ImageRenderBuilder" /> by integrating CSS styles
    ///     and HTML class attributes to create interactive visual effects.
    /// </summary>
    /// <remarks>
    ///     The FadeHoverEffect_ImageExtension class is designed to work within the PageBuilder rendering model,
    ///     specifically targeting images. It leverages the <see cref="PageInformation" /> class to manage CSS
    ///     stylesheet inclusion and modifies the HTML structure through <see cref="ImageRenderBuilder" /> to apply
    ///     the fade hover effect.
    ///     The extension method <see cref="FadeHoverEffect" /> is intended to be used in conjunction with
    ///     <see cref="ImageRenderBuilder" /> instances. It sets up the necessary CSS stylesheet and applies
    ///     a specific HTML class to enable the fade hover effect.
    ///     This method is part of the rendering pipeline, where it contributes to the visual presentation
    ///     of images by enhancing their interactivity. It is particularly useful in scenarios where a subtle
    ///     visual cue for user interaction is desired.
    ///     The method interacts with <see cref="HtmlBuilderBase{TBuilder}" /> to ensure compatibility and
    ///     seamless integration within the PageBuilder framework. It also participates in the lifecycle of
    ///     <see cref="PageInformation" /> by managing stylesheet inclusion.
    ///     Usage example:
    ///     ```csharp
    ///     var imageBuilder = new ImageRenderBuilder(writer, htmlHelper, "image.jpg", "Sample Image");
    ///     imageBuilder.FadeHoverEffect();
    ///     ```
    ///     This will apply the fade hover effect to the specified image, enhancing its visual appeal and
    ///     interactivity.
    /// </remarks>
    [Documented]
    public static class FadeHoverEffect_ImageExtension
    {
        #region Static methods

        /// <summary>
        ///     Reduces the image opacity to a configurable level when the user hovers over it, producing a smooth fade
        ///     interaction.
        /// </summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder" /> to apply the effect to.</param>
        /// <param name="opacity">The target opacity on hover (0.0 = fully transparent to 1.0 = fully opaque). Defaults to 0.72.</param>
        /// <returns>The modified <see cref="ImageRenderBuilder" /> instance with the effect applied.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> Image grids, product listings, and navigation thumbnails where a gentle opacity dip on
        ///         hover signals interactivity without distracting from the content.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> Sets the CSS variable <c>--eb-image-effect-opacity</c> and adds the class
        ///         <c>eb-image-effect-fade-hover</c> to the media wrapper via <c>FadeHoverEffect.css</c>. Opacity transitions
        ///         smoothly on hover in and out.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> Combines well with <c>FocusSpotEffect</c> or <c>GlassRevealEffect</c> for a layered
        ///         hover response. Avoid combining with <c>GhostEffect</c> as both set the same opacity variable and will
        ///         conflict.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> Values between 0.5 and 0.8 give a natural fade. Values below 0.4 make the image nearly invisible
        ///         on hover, which may confuse users expecting a highlight rather than a disappear effect.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @Html.ImageRender("/images/photo.jpg").SetWidth(100, UnitSize.percent).FadeHoverEffect(opacity: 0.72)
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder FadeHoverEffect(this ImageRenderBuilder builder, double opacity = 0.72)
        {
            builder.SetStyle("--eb-image-effect-opacity", opacity.ToString(CultureInfo.InvariantCulture));

            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/FadeHoverEffect.css");
            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-fade-hover"));
        }

        #endregion
    }
}