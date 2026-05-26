#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj RainbowEffect_ImageExtension.cs create at 2026/04/21
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
    /// Provides an extension method to apply a rainbow effect to images using the PageBuilder framework.
    /// This class enhances the rendering capabilities of <see cref="ImageRenderBuilder"/> by allowing developers to
    /// add a dynamic rainbow effect to images, which can be customized with the degree of rotation.
    /// </summary>
    [Documented]
    public static class RainbowEffect_ImageExtension
    {
        /// <summary>Applies a continuously cycling hue-rotate animation to the image, shifting its colors through the full rainbow spectrum.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder"/> to apply the effect to.</param>
        /// <param name="deg">The total hue rotation swept in one animation cycle, in degrees. Default is 360 (a full spectrum loop).</param>
        /// <param name="saturation">The <c>saturate()</c> filter value applied during the animation to amplify color intensity. Default is 2.0 (double saturation).</param>
        /// <param name="brightness">The <c>brightness()</c> filter value applied during the animation. Default is 1.0 (no brightness change).</param>
        /// <returns>The modified <see cref="ImageRenderBuilder"/> instance with the effect applied.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> Pride-themed content, game rewards and achievements, festive or party UIs, decorative logo animations, and any image where a psychedelic or celebratory looping color shift fits the brand.</para>
        /// <para><b>How it works:</b> Sets CSS variables <c>--eb-rainbow-deg</c> (formatted as <c>{deg}deg</c>), <c>--eb-image-effect-saturation</c>, and <c>--eb-image-effect-brightness</c>, then adds the class <c>eb-image-effect-rainbow</c> to the media wrapper via <c>RainbowEffect.css</c>. The CSS runs a looping <c>@keyframes</c> animation that animates <c>filter: hue-rotate()</c> from 0 to the specified degree, paired with constant <c>saturate()</c> and <c>brightness()</c> filters.</para>
        /// <para><b>Combinations:</b> Use standalone — combining with other filter-based effects (<c>GrayscaleHoverEffect</c>, <c>GrayscaleToColorEffect</c>, <c>RetroScanlinesEffect</c>) will cause CSS <c>filter</c> conflicts since only one filter chain can apply per element. Can overlay with <c>PulseEffect</c> for a pulsing rainbow beacon, as that effect uses <c>transform</c> rather than <c>filter</c>.</para>
        /// <para><b>Tips:</b> A <paramref name="deg"/> of 360 produces a seamless endless loop. Lower values (e.g., 180) make the animation oscillate between complementary hues rather than cycling the full spectrum. Increasing <paramref name="saturation"/> beyond 2.5 can blow out pastels and light images — test with your specific image palette. Reduce <paramref name="brightness"/> slightly (e.g., 0.9) if the boosted saturation makes the image look over-exposed on bright screens.</para>
        /// <para><b>Example:</b>
        /// <code>
        /// @Html.ImageRender("/images/logo.png").SetWidth(150, UnitSize.px).SetHeight(150, UnitSize.px)
        ///     .RainbowEffect(deg: 360, saturation: 1.8, brightness: 0.95)
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder RainbowEffect(this ImageRenderBuilder builder, int deg = 360, double saturation = 2.0, double brightness = 1.0)
        {
            builder.SetStyle("--eb-rainbow-deg", $"{deg.ToString(CultureInfo.InvariantCulture)}deg");
            builder.SetStyle("--eb-image-effect-saturation", saturation.ToString(CultureInfo.InvariantCulture));
            builder.SetStyle("--eb-image-effect-brightness", brightness.ToString(CultureInfo.InvariantCulture));

            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/RainbowEffect.css");
            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-rainbow"));
        }
    }
}
