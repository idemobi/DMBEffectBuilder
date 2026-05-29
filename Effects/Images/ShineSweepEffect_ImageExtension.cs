#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.Drawing;
using System.Globalization;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using DMBServerWebHelper;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Provides an extension method to apply a shine sweep effect to an image using the PageBuilder framework.
    ///     This class enhances the rendering capabilities of images by integrating a specific CSS effect, thereby
    ///     contributing to the visual richness of web pages.
    /// </summary>
    /// <remarks>
    ///     The ShineSweepEffect_ImageExtension class is designed to work within the PageBuilder rendering model,
    ///     specifically targeting the ImageRenderBuilder. It leverages the PageInformation class to manage
    ///     stylesheet dependencies, ensuring that the necessary CSS is included in the rendered page.
    /// </remarks>
    [Documented]
    public static class ShineSweepEffect_ImageExtension
    {
        #region Static methods

        /// <summary>
        ///     Sweeps a bright glare highlight diagonally across the image when the user hovers, simulating a light
        ///     reflection or lens flare.
        /// </summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder" /> to apply the effect to.</param>
        /// <param name="color">
        ///     Color of the shine band; defaults to <c>Color.White</c> for a classic glare. Pass a translucent
        ///     color to soften the effect.
        /// </param>
        /// <param name="angle">
        ///     Angle of the shine band in degrees (default 18 — a gentle diagonal sweep from bottom-left to
        ///     top-right).
        /// </param>
        /// <param name="width">
        ///     Width of the shine band as a percentage of the image width (default 30%; narrower values feel
        ///     sharper, wider feel softer).
        /// </param>
        /// <returns>The modified <see cref="ImageRenderBuilder" /> instance with the effect applied.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> Product images to suggest a premium or glossy surface, hero images, card thumbnails,
        ///         interactive gallery items. Works best on images with dark or rich colors where the bright sweep contrasts well.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> Sets CSS variables <c>--eb-image-effect-color</c> (RGBA string),
        ///         <c>--eb-image-effect-angle</c> (deg), and <c>--eb-image-effect-width</c> (%) on the image, then wraps it in a
        ///         component with class <c>eb-image-effect-shine-sweep</c> (ShineSweepEffect.css). The sweep is triggered on hover
        ///         via a CSS transform that moves the gradient pseudo-element across the image.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> Stacks well with <c>ZoomHoverEffect</c> — zoom and shine together feel high-end.
        ///         <c>SoftShadowEffect</c> grounds the image while the shine adds sparkle. Avoid with <c>VignetteEffect</c> — the
        ///         dark edges compete visually with the bright sweep.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> Use a semi-transparent white such as <c>Color.FromArgb(120, 255, 255, 255)</c> for a natural
        ///         sheen instead of a harsh flash. Angle values between 10–30 degrees produce the most convincing diagonal sweeps.
        ///         A very narrow <c>width: 10</c> creates a crisp specular highlight; <c>width: 60</c> creates a broad glow.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @Html.ImageRender("/images/product.jpg").SetWidth(100, UnitSize.percent).ShineSweepEffect(angle: 20, width: 25)
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder ShineSweepEffect(this ImageRenderBuilder builder, Color? color = null, int angle = 18, int width = 30)
        {
            Color actualColor = color ?? Color.White;
            builder.SetStyle("--eb-image-effect-color", actualColor.ToRgba());
            builder.SetStyle("--eb-image-effect-angle", $"{angle.ToString(CultureInfo.InvariantCulture)}deg");
            builder.SetStyle("--eb-image-effect-width", $"{width.ToString(CultureInfo.InvariantCulture)}%");

            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/ShineSweepEffect.css");
            return builder.MarkAsEffect().InWrapperComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-shine-sweep"));
        }

        #endregion
    }
}