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
    ///     Provides an extension method to apply a lift hover effect to images using the PageBuilder framework.
    ///     This class enhances the rendering capabilities of <see cref="ImageRenderBuilder" /> by integrating CSS styles
    ///     and HTML class attributes that create a dynamic visual effect when an image is hovered over.
    /// </summary>
    /// <remarks>
    ///     The lift hover effect is achieved by adding a specific CSS class to the image, which triggers
    ///     CSS transitions defined in an external stylesheet. This method is part of the PageBuilder rendering pipeline,
    ///     allowing developers to easily apply complex visual effects without manually managing CSS and HTML.
    ///     The <see cref="LiftHoverEffect" /> method modifies the <see cref="PageInformation" /> to include the necessary
    ///     CSS file, ensuring that the styles are loaded when the page is rendered. It also marks the image as an effect
    ///     and wraps it in a media component with the appropriate CSS class.
    ///     This extension is particularly useful for enhancing user experience by providing visual feedback on interactive
    ///     elements
    ///     without the need for JavaScript.
    ///     Usage example:
    ///     <code>
    /// var imageBuilder = new ImageRenderBuilder(writer, htmlHelper, "image.jpg", "Sample Image");
    /// imageBuilder.LiftHoverEffect();
    /// </code>
    ///     This will render an image with the lift hover effect applied.
    /// </remarks>
    [Documented]
    public static class LiftHoverEffect_ImageExtension
    {
        #region Static methods

        /// <summary>Lifts the image upward and adds a drop shadow on hover, giving a card-rise interaction feel.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder" /> to apply the effect to.</param>
        /// <param name="distance">How far the image translates upward on hover, in <paramref name="unit" />. Default is 4.</param>
        /// <param name="unit">The unit of measurement for <paramref name="distance" />. Default is <see cref="UnitSize.px" />.</param>
        /// <param name="scale">An additional scale factor applied on hover (1.0 = no scale change). Default is 1.0.</param>
        /// <param name="shadowOpacity">The opacity of the <c>box-shadow</c> added on hover (0.0–1.0). Default is 0.15.</param>
        /// <returns>The modified <see cref="ImageRenderBuilder" /> instance with the effect applied.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> Clickable image cards, product thumbnails, team member photos, and any image inside a link
        ///         that benefits from a tangible "pick up" cue. The shadow grounds the lifted image and makes the interaction feel
        ///         physical.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> Sets CSS variables <c>--eb-image-effect-lift-distance</c>,
        ///         <c>--eb-image-effect-scale</c>, and <c>--eb-image-effect-shadow-opacity</c> on the image element, then adds the
        ///         class <c>eb-image-effect-lift-hover</c> to the media wrapper via <c>LiftHoverEffect.css</c>. The CSS applies a
        ///         <c>transform: translateY()</c> combined with <c>box-shadow</c> on <c>:hover</c>.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> Pairs very well with <c>GlowHoverEffect</c> — the lift separates the image spatially
        ///         while the glow frames it. Also combines cleanly with <c>GrayscaleToColorEffect</c> for a dual-cue hover. Avoid
        ///         pairing with <c>PolaroidEffect</c> since the Polaroid's own shadow and border visually interfere with the lift
        ///         shadow.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> A <paramref name="distance" /> of 4–8 px feels natural for small card images; use 10–16 px for
        ///         larger hero-sized images. Keep <paramref name="shadowOpacity" /> between 0.10 and 0.25 — lower values look
        ///         barely lifted, higher values produce a heavy shadow that competes with the image. Only set
        ///         <paramref name="scale" /> above 1.0 if you also need the image to grow; a value of 1.03–1.05 combined with a
        ///         lift reads as a confident "pop".
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @Html.ImageRender("/images/photo.jpg").SetWidth(100, UnitSize.percent).SetHeight(180, UnitSize.px)
        ///     .InMediaComponent(m => m.SetStyle("object-fit", "cover"))
        ///     .LiftHoverEffect(distance: 8, shadowOpacity: 0.20)
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder LiftHoverEffect(this ImageRenderBuilder builder, double distance = 4, UnitSize unit = UnitSize.px, double scale = 1.0, double shadowOpacity = 0.15)
        {
            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/LiftHoverEffect.css");

            builder.SetStyle("--eb-image-effect-lift-distance", $"{distance.ToString(CultureInfo.InvariantCulture)}{unit.GetCss()}");
            builder.SetStyle("--eb-image-effect-scale", scale.ToString(CultureInfo.InvariantCulture));
            builder.SetStyle("--eb-image-effect-shadow-opacity", shadowOpacity.ToString(CultureInfo.InvariantCulture));

            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-lift-hover"));
        }

        #endregion
    }
}