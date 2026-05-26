#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj VignetteEffect_ImageExtension.cs create at 2026/04/21
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
    /// Provides an extension method to apply a vignette effect to images using the PageBuilder framework.
    /// This class enhances the rendering capabilities of <see cref="ImageRenderBuilder"/> by integrating
    /// a specific CSS stylesheet and marking the image as an effect.
    /// </summary>
    /// <remarks>
    /// The vignette effect is a visual technique that darkens the edges of an image, creating a
    /// soft focus on the center. This extension method is part of the DMBEffectBuilder namespace and
    /// leverages the PageBuilder rendering model to ensure seamless integration with other components.
    /// </remarks>
    [Documented]
    public static class VignetteEffect_ImageExtension
    {
        /// <summary>Overlays a radial dark gradient around the edges of the image, focusing visual attention on the center.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder"/> to apply the effect to.</param>
        /// <param name="color">Color of the vignette overlay; defaults to <c>rgba(0,0,0,0.45)</c> — a mid-strength black border. Use a colored value for creative tinting.</param>
        /// <param name="opacity">Opacity of the vignette overlay (default 1.0 — fully visible). Lower values reduce the effect without changing its color.</param>
        /// <param name="innerSize">Diameter of the clear central area as a percentage of the element (default 55%; larger values push the dark edges further out).</param>
        /// <param name="unit">Unit for <paramref name="innerSize"/> (default <c>UnitSize.percent</c>).</param>
        /// <returns>The modified <see cref="ImageRenderBuilder"/> instance with the effect applied.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> Vintage or cinematic photography, hero banners, atmospheric game-UI images, profile portraits where the subject is centered. Any image where you want to draw the eye inward and de-emphasize the periphery.</para>
        /// <para><b>How it works:</b> Sets CSS variables <c>--eb-image-effect-color</c> (RGBA), <c>--eb-image-effect-opacity</c>, and <c>--eb-image-effect-size</c> on the image element, then wraps it in a media component with class <c>eb-image-effect-vignette</c> (VignetteEffect.css). The vignette is rendered as a CSS radial-gradient pseudo-element positioned over the image.</para>
        /// <para><b>Combinations:</b> Pairs naturally with <c>SepiaEffect</c> for a period-photograph look. Combine with <c>ZoomHoverEffect</c> so the vignette intensifies as the image zooms — the shrinking field of view reinforces depth. Avoid with <c>ShineSweepEffect</c> — the bright sweep fights the dark border.</para>
        /// <para><b>Tips:</b> Increase <c>innerSize</c> to 70–80% for a barely-there edge treatment that adds mood without dominating. Lower it to 30–40% for a dramatic, tunnel-like vignette. To create a colored vignette (warm amber, deep teal), set <c>color</c> with a partially transparent hue and keep <c>opacity</c> at 1.0. Combining a reduced <c>opacity</c> with a small <c>innerSize</c> avoids over-darkening while still shaping the composition.</para>
        /// <para><b>Example:</b>
        /// <code>
        /// @Html.ImageRender("/images/portrait.jpg").SetWidth(100, UnitSize.percent).VignetteEffect(innerSize: 60, opacity: 0.85)
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder VignetteEffect(this ImageRenderBuilder builder, Color? color = null, double opacity = 1.0, float innerSize = 55, UnitSize unit = UnitSize.percent)
        {
            Color actualColor = color ?? Color.FromArgb(115, 0, 0, 0);
            builder.SetStyle("--eb-image-effect-color", actualColor.ToRgba());
            builder.SetStyle("--eb-image-effect-opacity", opacity.ToString(CultureInfo.InvariantCulture));
            builder.SetStyle("--eb-image-effect-size", $"{innerSize.ToString(CultureInfo.InvariantCulture)}{unit.GetCss()}");

            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/VignetteEffect.css");
            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-vignette"));
        }
    }
}
