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
    ///     Provides an extension method to apply a neon frame effect to images using the PageBuilder framework.
    ///     This class enhances the rendering capabilities of images by integrating a specific CSS stylesheet and wrapping them
    ///     in a designated HTML component.
    /// </summary>
    /// <remarks>
    ///     The NeonFrameEffect_ImageExtension class is designed to work within the PageBuilder rendering model, specifically
    ///     targeting images rendered through the ImageRenderBuilder.
    ///     By applying this effect, developers can visually distinguish images with a neon frame, enhancing the aesthetic
    ///     appeal of web pages.
    ///     The extension method modifies the PageInformation to include a required CSS stylesheet, ensuring that the neon
    ///     frame effect is properly applied during rendering.
    /// </remarks>
    [Documented]
    public static class NeonFrameEffect_ImageExtension
    {
        #region Static methods

        /// <summary>Wraps the image with a neon-colored glowing border that pulses or shines with a vivid luminous halo.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder" /> to apply the effect to.</param>
        /// <param name="color">
        ///     The color of the neon border and its surrounding glow. Defaults to Bootstrap primary blue (
        ///     <c>rgb(13,110,253)</c>).
        /// </param>
        /// <param name="size">The blur radius of the glow spread around the border, in <paramref name="unit" />. Default is 12.</param>
        /// <param name="unit">The unit of measurement for <paramref name="size" />. Default is <see cref="UnitSize.px" />.</param>
        /// <returns>The modified <see cref="ImageRenderBuilder" /> instance with the effect applied.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> Cyberpunk or gaming UI themes, featured product images, avatar frames in leaderboard
        ///         components, and any image that needs to stand out with a sci-fi or arcade aesthetic.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> Sets CSS variables <c>--eb-image-effect-color</c> and <c>--eb-image-effect-size</c>,
        ///         then wraps the image in a wrapper component (not a media component) with the class
        ///         <c>eb-image-effect-neon-frame</c> via <c>NeonFrameEffect.css</c>. The CSS renders a solid border in the chosen
        ///         color and layers multiple <c>box-shadow</c> values to produce the characteristic neon bloom.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> Pairs well with <c>PulseEffect</c> for an animated neon beacon feel. Avoid combining
        ///         with <c>GlowHoverEffect</c> — both produce <c>box-shadow</c> halos around the element and will visually
        ///         compete, especially since <c>NeonFrameEffect</c> uses a wrapper component while <c>GlowHoverEffect</c> targets
        ///         the media wrapper.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> Saturated hues (cyan, magenta, lime, electric blue) reproduce the neon effect most convincingly;
        ///         desaturated or pastel colors look flat and lose the luminous quality. A <paramref name="size" /> of 10–20 px
        ///         works for thumbnails; scale up to 30–40 px for large hero images to keep the glow proportional. Because the
        ///         effect wraps via <c>InWrapperComponent</c>, ensure the parent layout has enough margin to show the glow without
        ///         clipping.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @Html.ImageRender("/images/photo.jpg").SetWidth(200, UnitSize.px).SetHeight(200, UnitSize.px)
        ///     .NeonFrameEffect(Color.FromArgb(0, 255, 234), size: 18)
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder NeonFrameEffect(this ImageRenderBuilder builder, Color? color = null, int size = 12, UnitSize unit = UnitSize.px)
        {
            Color actualColor = color ?? Color.FromArgb(13, 110, 253);
            builder.SetStyle("--eb-image-effect-color", actualColor.ToRgba());
            builder.SetStyle("--eb-image-effect-size", $"{size.ToString(CultureInfo.InvariantCulture)}{unit.GetCss()}");

            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/NeonFrameEffect.css");
            return builder.MarkAsEffect().InWrapperComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-neon-frame"));
        }

        #endregion
    }
}