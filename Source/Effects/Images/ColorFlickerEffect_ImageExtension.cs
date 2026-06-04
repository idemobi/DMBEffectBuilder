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
    ///     Provides an extension method to apply a color flicker effect to images using the PageBuilder framework.
    ///     This class enhances the rendering capabilities of <see cref="ImageRenderBuilder" /> by adding a flicker effect
    ///     that can be customized with a specified size.
    /// </summary>
    [Documented]
    public static class ColorFlickerEffect_ImageExtension
    {
        #region Static methods

        /// <summary>
        ///     Animates the image in a continuous loop, alternating between full color and grayscale to create a flickering
        ///     color effect.
        /// </summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder" /> to apply the effect to.</param>
        /// <param name="size">Controls the intensity of the flicker displacement in the chosen unit. Defaults to 3px.</param>
        /// <param name="unit">The unit of measurement for the flicker size. Defaults to <see cref="UnitSize.px" />.</param>
        /// <returns>The modified <see cref="ImageRenderBuilder" /> instance with the effect applied.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> Retro or neon-style imagery, attention-grabbing hero images, gaming or sci-fi themed UIs
        ///         where a looping animated energy feel is desired.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> Sets the CSS variable <c>--eb-color-flicker-size</c> and adds the class
        ///         <c>eb-image-effect-color-flicker</c> to the media wrapper via <c>ColorFlickerEffect.css</c>. The animation runs
        ///         continuously without user interaction.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> Pairs well with <c>GlitchEffect</c> for a distressed look. Avoid combining with
        ///         <c>GhostEffect</c> or <c>FadeHoverEffect</c> as competing opacity/color changes will clash visually.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> Keep <paramref name="size" /> between 2 and 6px for a subtle flicker; larger values produce a
        ///         more aggressive displacement. Use sparingly — constant animation can distract users on content-heavy pages.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @Html.ImageRender("/images/photo.jpg").SetWidth(100, UnitSize.percent).ColorFlickerEffect(size: 3, unit: UnitSize.px)
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder ColorFlickerEffect(this ImageRenderBuilder builder, int size = 3, UnitSize unit = UnitSize.px)
        {
            builder.SetStyle("--eb-color-flicker-size", $"{size.ToString(CultureInfo.InvariantCulture)}{unit.GetCss()}");
            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/ColorFlickerEffect.css");
            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-color-flicker"));
        }

        #endregion
    }
}