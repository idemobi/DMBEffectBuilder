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
    ///     Provides an extension method to apply a zoom hover effect to images using the PageBuilder framework.
    ///     This class enhances the rendering capabilities of <see cref="ImageRenderBuilder" /> by integrating CSS styles
    ///     and HTML class attributes to create interactive image effects.
    /// </summary>
    [Documented]
    public static class ZoomHoverEffect_ImageExtension
    {
        #region Static methods

        /// <summary>Scales the image up smoothly on hover via a CSS transform, giving a subtle zoom-in interaction effect.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder" /> to apply the effect to.</param>
        /// <param name="scale">Scale factor applied on hover (default 1.04 — a 4% zoom; use 1.08–1.15 for a more obvious effect).</param>
        /// <returns>The modified <see cref="ImageRenderBuilder" /> instance with the effect applied.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> Gallery thumbnails, card images, product listings, team member photos — any clickable or
        ///         interactive image container where hover feedback improves the sense of interactivity. Works universally and is
        ///         the most versatile image effect in the library.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> Sets the CSS variable <c>--eb-image-effect-scale</c> on the image element, then wraps it
        ///         in a media component with class <c>eb-image-effect-zoom-hover</c> (ZoomHoverEffect.css). The CSS applies a
        ///         <c>transform: scale()</c> on <c>:hover</c> with a transition, so the zoom animates smoothly in and out. The
        ///         wrapper clips overflow to prevent the zoomed image from spilling beyond its bounds.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> The most composable effect in the library — pairs well with virtually everything.
        ///         Combine with <c>ShineSweepEffect</c> for a premium product-image feel, with <c>SepiaToColorEffect</c> for a
        ///         color-reveal interaction, or with <c>SoftShadowEffect</c> to make the image appear to lift as it zooms. Avoid
        ///         stacking with <c>TiltParallaxEffect</c> — both compete for the same hover-transform slot.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> The default <c>scale: 1.04</c> is intentionally conservative — it communicates hover state
        ///         without being distracting. Use <c>scale: 1.1</c> for cards with <c>object-fit: cover</c> so the zoom is clearly
        ///         visible without cropping content. For full-bleed background-style images, <c>scale: 1.06–1.08</c> is a good
        ///         balance. Always set a fixed height and <c>object-fit: cover</c> on the media component when using this effect
        ///         on variable-height containers to prevent layout shift.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @Html.ImageRender("/images/photo.jpg").SetWidth(100, UnitSize.percent).SetHeight(180, UnitSize.px).InMediaComponent(m => m.SetStyle("object-fit", "cover")).ZoomHoverEffect(scale: 1.08)
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder ZoomHoverEffect(this ImageRenderBuilder builder, double scale = 1.04)
        {
            builder.SetStyle("--eb-image-effect-scale", scale.ToString(CultureInfo.InvariantCulture));

            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/ZoomHoverEffect.css");
            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-zoom-hover"));
        }

        #endregion
    }
}