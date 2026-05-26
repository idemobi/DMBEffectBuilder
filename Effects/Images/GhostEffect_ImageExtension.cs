#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj GhostEffect_ImageExtension.cs create at 2026/04/21
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
    /// Provides an extension method to apply a ghost effect to images using the PageBuilder framework.
    /// This class enhances the rendering capabilities of <see cref="ImageRenderBuilder"/> by adding
    /// CSS styles that create a ghostly appearance through opacity and blur effects.
    /// </summary>
    [Documented]
    public static class GhostEffect_ImageExtension
    {
        /// <summary>Renders the image with a persistent semi-transparent, blurred appearance that gives it a ghostly, ethereal look.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder"/> to apply the effect to.</param>
        /// <param name="opacity">The opacity of the ghost image (0.0 = invisible to 1.0 = fully opaque). Defaults to 0.2.</param>
        /// <param name="blur">The Gaussian blur radius applied to the image in the chosen unit. Defaults to 4px.</param>
        /// <param name="unit">The unit of measurement for the <paramref name="blur"/> radius. Defaults to <see cref="UnitSize.px"/>.</param>
        /// <returns>The modified <see cref="ImageRenderBuilder"/> instance with the effect applied.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> Decorative background layers, watermark-style images, disabled-state placeholders, and atmospheric UI elements where an image should recede visually without disappearing.</para>
        /// <para><b>How it works:</b> Sets CSS variables <c>--eb-ghost-opacity</c> and <c>--eb-ghost-blur</c>, then adds the class <c>eb-image-effect-ghost</c> to the media wrapper via <c>GhostEffect.css</c>. The effect is always-on, not hover-triggered.</para>
        /// <para><b>Combinations:</b> Combines well with <c>ColorFlickerEffect</c> to create a ghostly pulsing animation. Avoid combining with <c>FadeHoverEffect</c> or <c>FocusSpotEffect</c> as they also manipulate opacity and the values will conflict.</para>
        /// <para><b>Tips:</b> Use <paramref name="opacity"/> between 0.1 and 0.3 for a true ghost effect; higher values simply produce a blurred image without the translucent feel. Increasing <paramref name="blur"/> beyond 8px may render fine-detail images unrecognisable.</para>
        /// <para><b>Example:</b>
        /// <code>
        /// @Html.ImageRender("/images/photo.jpg").SetWidth(100, UnitSize.percent).GhostEffect(opacity: 0.2, blur: 4, unit: UnitSize.px)
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder GhostEffect(this ImageRenderBuilder builder, double opacity = 0.2, int blur = 4, UnitSize unit = UnitSize.px)
        {
            builder.SetStyle("--eb-ghost-opacity", $"{opacity.ToString(CultureInfo.InvariantCulture)}");
            builder.SetStyle("--eb-ghost-blur", $"{blur.ToString(CultureInfo.InvariantCulture)}{unit.GetCss()}");
            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/GhostEffect.css");
            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-ghost"));
        }
    }
}
