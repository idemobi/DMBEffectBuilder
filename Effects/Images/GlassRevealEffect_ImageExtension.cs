#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj GlassRevealEffect_ImageExtension.cs create at 2026/04/21
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
    /// Provides an extension method to apply a glass reveal effect to images using the PageBuilder framework.
    /// This class enhances the rendering capabilities of <see cref="ImageRenderBuilder"/> by integrating a specific CSS effect.
    /// </summary>
    [Documented]
    public static class GlassRevealEffect_ImageExtension
    {
        /// <summary>Overlays a frosted glass panel on the image that fades away on hover, revealing the image beneath with a polished glass-morphism effect.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder"/> to apply the effect to.</param>
        /// <param name="blur">The backdrop blur strength of the glass overlay in the chosen unit. Defaults to 5px.</param>
        /// <param name="unit">The unit of measurement for the <paramref name="blur"/> value. Defaults to <see cref="UnitSize.px"/>.</param>
        /// <param name="opacity">The opacity of the glass overlay at rest (0.0 = transparent to 1.0 = fully opaque). Defaults to 0.85.</param>
        /// <returns>The modified <see cref="ImageRenderBuilder"/> instance with the effect applied.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> Modern card UIs, feature image panels, and landing page heroes where a glassmorphism-style reveal on hover adds a premium, contemporary feel.</para>
        /// <para><b>How it works:</b> Sets CSS variables <c>--eb-image-effect-blur</c> and <c>--eb-image-effect-opacity</c>, then adds the class <c>eb-image-effect-glass-reveal</c> to the wrapper component (not media component) via <c>GlassRevealEffect.css</c>. The glass overlay dissolves on hover.</para>
        /// <para><b>Combinations:</b> Combines well with <c>FadeHoverEffect</c> for a multi-layer hover reveal. Avoid combining with <c>FocusSpotEffect</c> as both introduce overlay layers that visually compete on the same image.</para>
        /// <para><b>Tips:</b> A <paramref name="blur"/> of 5–10px and <paramref name="opacity"/> of 0.7–0.9 produces the most convincing frosted glass. Very low opacity at rest (below 0.5) makes the glass panel barely visible, reducing the reveal impact.</para>
        /// <para><b>Example:</b>
        /// <code>
        /// @Html.ImageRender("/images/photo.jpg").SetWidth(100, UnitSize.percent).GlassRevealEffect(blur: 5, unit: UnitSize.px, opacity: 0.85)
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder GlassRevealEffect(this ImageRenderBuilder builder, double blur = 5, UnitSize unit = UnitSize.px, double opacity = 0.85)
        {
            builder.SetStyle("--eb-image-effect-blur", $"{blur.ToString(CultureInfo.InvariantCulture)}{unit.GetCss()}");
            builder.SetStyle("--eb-image-effect-opacity", opacity.ToString(CultureInfo.InvariantCulture));

            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/GlassRevealEffect.css");
            return builder.MarkAsEffect().InWrapperComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-glass-reveal"));
        }
    }
}
