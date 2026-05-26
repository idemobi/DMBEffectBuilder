#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj GlitchEffect_ImageExtension.cs create at 2026/04/21
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
    /// Provides an extension method to apply a glitch effect to images using the PageBuilder framework.
    /// This class enhances the rendering capabilities of <see cref="ImageRenderBuilder"/> by adding
    /// specific styles and classes that trigger the glitch effect.
    /// </summary>
    [Documented]
    public static class GlitchEffect_ImageExtension
    {
        /// <summary>Applies a looping glitch animation with RGB channel splitting and horizontal displacement to the image, simulating a digital signal distortion.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder"/> to apply the effect to.</param>
        /// <param name="size">The maximum displacement range of the RGB-split glitch slices in the chosen unit. Defaults to 3px.</param>
        /// <param name="unit">The unit of measurement for the <paramref name="size"/> value. Defaults to <see cref="UnitSize.px"/>.</param>
        /// <returns>The modified <see cref="ImageRenderBuilder"/> instance with the effect applied.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> Cyberpunk, retro-tech, or horror-themed UIs, game splash screens, and any creative context where an intentional digital-corruption aesthetic is desired.</para>
        /// <para><b>How it works:</b> Sets the CSS variable <c>--eb-glitch-size</c> and adds the class <c>eb-image-effect-glitch</c> to the media wrapper via <c>GlitchEffect.css</c>. The animation runs continuously using keyframe-based offset transforms for red, green, and blue pseudo-layers.</para>
        /// <para><b>Combinations:</b> Pairs well with <c>ColorFlickerEffect</c> for an aggressive corrupted-signal look. Avoid combining with <c>DistortEffect</c> or <c>FlipEffect</c> as simultaneous geometric transforms will produce an uncontrolled visual result.</para>
        /// <para><b>Tips:</b> Keep <paramref name="size"/> between 2 and 6px for a readable image with glitch accents. Values above 10px produce a heavily fragmented image where the content becomes secondary to the effect — intentional only for purely decorative uses.</para>
        /// <para><b>Example:</b>
        /// <code>
        /// @Html.ImageRender("/images/photo.jpg").SetWidth(100, UnitSize.percent).GlitchEffect(size: 3, unit: UnitSize.px)
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder GlitchEffect(this ImageRenderBuilder builder, int size = 3, UnitSize unit = UnitSize.px)
        {
            builder.SetStyle("--eb-glitch-size", $"{size.ToString(CultureInfo.InvariantCulture)}{unit.GetCss()}");
            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/GlitchEffect.css");
            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-glitch"));
        }
    }
}
