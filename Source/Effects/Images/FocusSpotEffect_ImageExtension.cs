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
    ///     Provides an extension method to apply a focus spot effect to images using the PageBuilder framework.
    ///     This class enhances the rendering capabilities of images by integrating a specific CSS effect, thereby
    ///     enriching the visual presentation of images on web pages.
    /// </summary>
    /// <remarks>
    ///     The FocusSpotEffect_ImageExtension class is designed to work within the PageBuilder rendering model,
    ///     specifically targeting the ImageRenderBuilder. It leverages the PageInformation class to manage
    ///     stylesheet inclusion, ensuring that the necessary CSS for the focus spot effect is loaded.
    ///     The extension method FocusSpotEffect modifies the ImageRenderBuilder instance by marking it as an effect
    ///     and wrapping it in a media component with the appropriate CSS class. This approach allows for seamless
    ///     integration of the effect into the rendering pipeline, ensuring that the image is rendered with the desired visual
    ///     enhancement.
    ///     Usage of this extension method requires that the PageBuilder framework is properly set up and configured
    ///     within the application. It is intended to be used in conjunction with other PageBuilder components and builders
    ///     to create complex, visually appealing web pages.
    ///     This class is static and contains only one public method, FocusSpotEffect, which is an extension method
    ///     for ImageRenderBuilder. The method does not throw any exceptions under normal circumstances but may
    ///     behave unpredictably if the PageBuilder framework is not correctly initialized.
    ///     <see cref="ImageRenderBuilder" />: The builder class that this extension method targets, allowing for
    ///     the application of image-specific effects.
    ///     <see cref="PageInformation" />: Manages the inclusion of stylesheets required for the focus spot effect.
    ///     <see cref="PageRegistry" />: Provides access to the current page information, ensuring that stylesheets
    ///     are correctly associated with the appropriate HTTP context.
    ///     <see cref="HtmlBuilderBase{TBuilder}" />: The base class for all HTML builders, providing foundational
    ///     functionality for rendering HTML elements.
    ///     <see cref="CustomClassesExtensions.AddClass{TBuilder}" />: Extends the functionality of HTML builders by
    ///     allowing the addition of custom CSS classes.
    ///     <c>WriteTo</c>: The lifecycle method responsible for rendering the final HTML output.
    ///     <c>Begin</c>: Marks the beginning of the rendering process for a builder instance.
    ///     <c>Dispose</c>: Cleans up resources used by the builder, ensuring proper disposal of objects.
    /// </remarks>
    [Documented]
    public static class FocusSpotEffect_ImageExtension
    {
        #region Static methods

        /// <summary>
        ///     Overlays a radial spotlight vignette on the image that lightens on hover while gently scaling the image up,
        ///     drawing focus to the centre.
        /// </summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder" /> to apply the effect to.</param>
        /// <param name="opacity">The opacity of the dark vignette overlay at rest (0.0 to 1.0). Defaults to 0.55.</param>
        /// <param name="hoverOpacity">
        ///     The reduced opacity of the overlay on hover, revealing more of the image (0.0 to 1.0).
        ///     Defaults to 0.18.
        /// </param>
        /// <param name="scale">The scale multiplier applied to the image on hover. Defaults to 1.025 (a subtle 2.5% zoom).</param>
        /// <param name="spotSize">The radius of the clear centre spotlight in the chosen unit. Defaults to 28%.</param>
        /// <param name="unit">
        ///     The unit of measurement for <paramref name="spotSize" />. Defaults to
        ///     <see cref="UnitSize.percent" />.
        /// </param>
        /// <returns>The modified <see cref="ImageRenderBuilder" /> instance with the effect applied.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> Product spotlights, portrait thumbnails, and featured-content cards where guiding the
        ///         viewer's eye to the centre of the image on hover improves perceived interactivity.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> Sets CSS variables <c>--eb-image-effect-opacity</c>,
        ///         <c>--eb-image-effect-hover-opacity</c>, <c>--eb-image-effect-scale</c>, and <c>--eb-image-effect-spot-size</c>,
        ///         then adds the class <c>eb-image-effect-focus-spot</c> to the media wrapper via <c>FocusSpotEffect.css</c>.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> Pairs naturally with <c>DarkenToNormalEffect</c> for a load-then-hover two-stage reveal.
        ///         Avoid combining with <c>GlassRevealEffect</c> as both introduce overlay layers that will visually compete.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> Increase <paramref name="spotSize" /> for wide, subject-heavy images and decrease it for
        ///         portrait-oriented images with a centred subject. Keep <paramref name="scale" /> close to 1.0 (1.01–1.04) to
        ///         avoid layout shifts on small containers.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @Html.ImageRender("/images/photo.jpg").SetWidth(100, UnitSize.percent).FocusSpotEffect(opacity: 0.55, hoverOpacity: 0.18, scale: 1.025, spotSize: 28)
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder FocusSpotEffect(
            this ImageRenderBuilder builder,
            double opacity = 0.55,
            double hoverOpacity = 0.18,
            double scale = 1.025,
            int spotSize = 28,
            UnitSize unit = UnitSize.percent
        )
        {
            builder.SetStyle("--eb-image-effect-opacity", opacity.ToString(CultureInfo.InvariantCulture));
            builder.SetStyle("--eb-image-effect-hover-opacity", hoverOpacity.ToString(CultureInfo.InvariantCulture));
            builder.SetStyle("--eb-image-effect-scale", scale.ToString(CultureInfo.InvariantCulture));
            builder.SetStyle("--eb-image-effect-spot-size", $"{spotSize.ToString(CultureInfo.InvariantCulture)}{unit.GetCss()}");

            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/FocusSpotEffect.css");
            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-focus-spot"));
        }

        #endregion
    }
}