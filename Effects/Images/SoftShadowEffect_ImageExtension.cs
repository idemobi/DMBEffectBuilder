#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj SoftShadowEffect_ImageExtension.cs create at 2026/04/21
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using DMBBootstrapBuilder;
using DMBPageBuilder;
using DMBServerWebHelper;
using System.Drawing;
using System.Globalization;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides an extension method to apply a soft shadow effect to images using the PageBuilder framework.
    /// This class enhances the rendering capabilities of <see cref="ImageRenderBuilder"/> by integrating CSS styles
    /// and HTML class attributes to create a visual shadow effect around images.
    /// </summary>
    [Documented]
    public static class SoftShadowEffect_ImageExtension
    {
        /// <summary>Adds a soft drop shadow beneath the image using a configurable color, blur radius, and vertical offset.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder"/> to apply the effect to.</param>
        /// <param name="color">Shadow color; defaults to <c>rgba(0,0,0,0.15)</c> — a very light black shadow. Use a colored shadow to match the image's dominant hue for a natural glow.</param>
        /// <param name="blur">Blur radius of the shadow (default 30 px); higher values produce a wider, more diffuse shadow.</param>
        /// <param name="blurUnit">Unit for the blur radius (default <c>UnitSize.px</c>).</param>
        /// <param name="offsetY">Vertical offset of the shadow, pushing it downward (default 8 px).</param>
        /// <param name="offsetYUnit">Unit for the vertical offset (default <c>UnitSize.px</c>).</param>
        /// <returns>The modified <see cref="ImageRenderBuilder"/> instance with the effect applied.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> Cards, thumbnails, product photos, avatars — any image that should feel lifted off the page. Especially effective on images with white or transparent backgrounds where the shadow separates the subject from the layout.</para>
        /// <para><b>How it works:</b> Sets CSS variables <c>--eb-image-effect-color</c> (RGBA), <c>--eb-image-effect-blur</c>, and <c>--eb-image-effect-offset-y</c> on the image element, then wraps it in a media component with class <c>eb-image-effect-soft-shadow</c> (SoftShadowEffect.css). Implemented via CSS <c>filter: drop-shadow()</c>, which respects transparent PNG edges rather than boxing them.</para>
        /// <para><b>Combinations:</b> Excellent baseline to combine with almost any other effect — <c>ZoomHoverEffect</c>, <c>RotateSlightlyEffect</c>, <c>ShineSweepEffect</c>. For a photo-print look, combine with <c>RotateRandomEffect</c>. The shadow makes the lift feel real.</para>
        /// <para><b>Tips:</b> Default values are deliberately subtle — increase <c>blur</c> to 50–80 and <c>offsetY</c> to 16–20 for a more theatrical floating effect. For a colored ambient shadow matching an image, sample the dominant hue: <c>Color.FromArgb(60, 200, 100, 50)</c>. Avoid very opaque shadows (alpha > 100) — they look painted-on.</para>
        /// <para><b>Example:</b>
        /// <code>
        /// @Html.ImageRender("/images/product.png").SetWidth(100, UnitSize.percent).SoftShadowEffect(blur: 40, offsetY: 12)
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder SoftShadowEffect(this ImageRenderBuilder builder, Color? color = null, int blur = 30, UnitSize blurUnit = UnitSize.px, int offsetY = 8, UnitSize offsetYUnit = UnitSize.px)
        {
            Color actualColor = color ?? Color.FromArgb(38, 0, 0, 0);
            builder.SetStyle("--eb-image-effect-color", actualColor.ToRgba());
            builder.SetStyle("--eb-image-effect-blur", $"{blur.ToString(CultureInfo.InvariantCulture)}{blurUnit.GetCss()}");
            builder.SetStyle("--eb-image-effect-offset-y", $"{offsetY.ToString(CultureInfo.InvariantCulture)}{offsetYUnit.GetCss()}");

            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/SoftShadowEffect.css");
            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-soft-shadow"));
        }
    }
}
