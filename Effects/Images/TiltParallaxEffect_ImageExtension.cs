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
    ///     Provides an extension method to apply a tilt parallax effect to an image using the PageBuilder framework.
    ///     This class enhances the rendering capabilities of images by integrating a CSS-based tilt parallax effect,
    ///     which creates a dynamic visual experience when the image is viewed.
    /// </summary>
    /// <remarks>
    ///     The TiltParallaxEffect_ImageExtension class is a static utility class that extends the functionality of
    ///     ImageRenderBuilder. It leverages the PageInformation to manage CSS stylesheet inclusion and modifies
    ///     the HTML structure of the image element to apply the tilt parallax effect.
    /// </remarks>
    [Documented]
    public static class TiltParallaxEffect_ImageExtension
    {
        #region Static methods

        /// <summary>
        ///     Applies a 3D tilt transform to the image on hover, creating a parallax depth illusion driven by CSS
        ///     perspective.
        /// </summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder" /> to apply the effect to.</param>
        /// <param name="scale">
        ///     Scale factor applied on hover to subtly grow the image as it tilts (default 1.03 — a barely
        ///     perceptible lift).
        /// </param>
        /// <param name="rotateX">Rotation around the horizontal axis in degrees (default 4 — top edge tilts away from the viewer).</param>
        /// <param name="rotateY">Rotation around the vertical axis in degrees (default -6 — right edge tilts toward the viewer).</param>
        /// <param name="translateZ">Forward translation along the Z-axis, pushing the image toward the viewer (default 14 px).</param>
        /// <param name="unit">Unit for the <paramref name="translateZ" /> value (default <c>UnitSize.px</c>).</param>
        /// <returns>The modified <see cref="ImageRenderBuilder" /> instance with the effect applied.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> Hero images, feature cards, product showcases — anywhere a sense of physical depth makes
        ///         the UI feel interactive. Works especially well on large images where the 3D perspective is visible. Also
        ///         effective for framed or book-cover style images.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> Sets CSS variables <c>--eb-image-effect-scale</c>, <c>--eb-image-effect-rotate-x</c>,
        ///         <c>--eb-image-effect-rotate-y</c>, and <c>--eb-image-effect-translate-z</c> on the image element, then wraps it
        ///         in a media component with class <c>eb-image-effect-tilt-parallax</c> (TiltParallaxEffect.css). The effect
        ///         relies on CSS <c>perspective</c> and <c>transform</c> on hover — no JavaScript is used.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> Works well on its own or with <c>SoftShadowEffect</c> — the shadow gets "cast" as the
        ///         image tilts, reinforcing the 3D illusion. Avoid combining with <c>RotateEffect</c> or <c>ShakeEffect</c> —
        ///         conflicting transforms will cancel or distort the parallax.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> The defaults produce a right-leaning tilt; swap the sign of <c>rotateY</c> to lean left. Keep
        ///         <c>rotateX</c> and <c>rotateY</c> between -10 and 10 degrees for a realistic effect — beyond that the
        ///         perspective distortion becomes exaggerated. Increase <c>translateZ</c> to 30–40 px for a more dramatic pop-out
        ///         on larger images. The effect requires <c>perspective</c> to be set on a parent element in the stylesheet —
        ///         confirm TiltParallaxEffect.css sets it on the wrapper.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @Html.ImageRender("/images/cover.jpg").SetWidth(100, UnitSize.percent).TiltParallaxEffect(scale: 1.05, rotateX: 3, rotateY: -8, translateZ: 20)
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder TiltParallaxEffect(
            this ImageRenderBuilder builder,
            double scale = 1.03,
            int rotateX = 4,
            int rotateY = -6,
            int translateZ = 14,
            UnitSize unit = UnitSize.px
        )
        {
            builder.SetStyle("--eb-image-effect-scale", scale.ToString(CultureInfo.InvariantCulture));
            builder.SetStyle("--eb-image-effect-rotate-x", $"{rotateX.ToString(CultureInfo.InvariantCulture)}deg");
            builder.SetStyle("--eb-image-effect-rotate-y", $"{rotateY.ToString(CultureInfo.InvariantCulture)}deg");
            builder.SetStyle("--eb-image-effect-translate-z", $"{translateZ.ToString(CultureInfo.InvariantCulture)}{unit.GetCss()}");

            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/TiltParallaxEffect.css");
            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-tilt-parallax"));
        }

        #endregion
    }
}