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
    ///     Provides an extension method to apply the distort effect to images using the PageBuilder framework.
    /// </summary>
    [Documented]
    public static class DistortEffect_ImageExtension
    {
        #region Static methods

        /// <summary>Applies a CSS skew transform to the image on hover, giving it a distorted, warped appearance.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder" /> to apply the effect to.</param>
        /// <param name="size">The skew angle in degrees applied to the image on hover. Defaults to 8.</param>
        /// <returns>The modified <see cref="ImageRenderBuilder" /> instance with the effect applied.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> Edgy or alternative brand imagery, game cards, and decorative tiles where a geometric
        ///         tilt-on-hover interaction reinforces a dynamic feel.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> Sets the CSS variable <c>--eb-distort-size</c> in degrees and adds the class
        ///         <c>eb-image-effect-distort</c> to the media wrapper via <c>DistortEffect.css</c>. The skew activates on hover.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> Pairs naturally with <c>FlipEffect</c> for compound geometric hover interactions. Avoid
        ///         combining with <c>GlitchEffect</c> as simultaneous skew and RGB-split produce a chaotic result.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> Keep <paramref name="size" /> between 4 and 12 degrees for readable distortion; larger values
        ///         may clip content outside its container. Test with <c>overflow: hidden</c> on the wrapper if clipping is an
        ///         issue.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @Html.ImageRender("/images/photo.jpg").SetWidth(100, UnitSize.percent).DistortEffect(size: 8)
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder DistortEffect(this ImageRenderBuilder builder, int size = 8)
        {
            builder.SetStyle("--eb-distort-size", $"{size.ToString(CultureInfo.InvariantCulture)}deg");
            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/DistortEffect.css");
            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-distort"));
        }

        #endregion
    }
}