#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj BlurToSharpEffect_ImageExtension.cs create at 2026/04/21
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
    /// Provides an extension method to apply a blur-to-sharp effect to images using the PageBuilder framework.
    /// This class enhances the rendering capabilities of images by integrating a specific CSS effect, thereby
    /// contributing to the visual richness of web pages.
    /// </summary>
    [Documented]
    public static class BlurToSharpEffect_ImageExtension
    {
        /// <summary>Animates the image from a blurred state to full sharpness on page load.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder"/> to apply the effect to.</param>
        /// <param name="blur">The initial blur radius; the animation starts at this value and resolves to zero. Default is 3px.</param>
        /// <param name="unit">The unit for the <paramref name="blur"/> value. Default is <see cref="UnitSize.px"/>.</param>
        /// <returns>The modified <see cref="ImageRenderBuilder"/> instance with the effect applied.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> Hero images, article thumbnails, or gallery items where a soft focus-pull entrance
        /// adds polish. Works especially well when the image loads lazily — the blur fills the placeholder visually
        /// before the full resolution arrives.</para>
        /// <para><b>How it works:</b> Sets the CSS custom property <c>--eb-image-effect-blur</c> to the specified
        /// blur radius and applies the class <c>eb-image-effect-blur-to-sharp</c> via <c>InMediaComponent</c>.
        /// The linked stylesheet <c>BlurToSharpEffect.css</c> drives a CSS animation from <c>blur(N)</c> to
        /// <c>blur(0)</c> on load.</para>
        /// <para><b>Combinations:</b> Pairs well with <c>DarkenToNormalEffect</c> for a cinematic reveal. Avoid
        /// combining with <c>GlitchEffect</c> or <c>ColorFlickerEffect</c> as the competing animations clash
        /// visually.</para>
        /// <para><b>Tips:</b> Keep <paramref name="blur"/> between 2px and 8px for a subtle entrance; values above
        /// 12px can feel too heavy and slow. Use <see cref="UnitSize.rem"/> if you want blur to scale with the
        /// user's font-size preference.</para>
        /// <para><b>Example:</b>
        /// <code>
        /// @Html.ImageRender("/images/photo.jpg").SetWidth(100, UnitSize.percent).BlurToSharpEffect(blur: 5)
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder BlurToSharpEffect(this ImageRenderBuilder builder, double blur = 3, UnitSize unit = UnitSize.px)
        {
            builder.SetStyle("--eb-image-effect-blur", $"{blur.ToString(CultureInfo.InvariantCulture)}{unit.GetCss()}");

            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/BlurToSharpEffect.css");
            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-blur-to-sharp"));
        }
    }
}
