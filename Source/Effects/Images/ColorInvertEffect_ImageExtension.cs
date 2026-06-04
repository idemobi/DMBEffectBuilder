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
    ///     Provides an extension method to apply a color inversion effect to images using the PageBuilder framework.
    ///     This class enhances the rendering capabilities by allowing developers to easily add a color inversion effect
    ///     to images, which can be useful for creating visually distinct or artistic effects on web pages.
    /// </summary>
    [Documented]
    public static class ColorInvertEffect_ImageExtension
    {
        #region Static methods

        /// <summary>Inverts the image colors on hover, producing a negative-film effect with a configurable inversion amount.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder" /> to apply the effect to.</param>
        /// <param name="amount">
        ///     The percentage of color inversion applied on hover, from 0 (none) to 100 (full inversion).
        ///     Defaults to 100.
        /// </param>
        /// <returns>The modified <see cref="ImageRenderBuilder" /> instance with the effect applied.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> Dark-themed UIs, artistic galleries, or interactive cards where a striking negative-film
        ///         look on hover adds visual contrast.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> Sets the CSS variable <c>--eb-color-invert-amount</c> as a percentage and adds the class
        ///         <c>eb-image-effect-color-invert</c> to the media wrapper via <c>ColorInvertEffect.css</c>. The inversion
        ///         activates on hover.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> Combines well with <c>FadeHoverEffect</c> for a layered hover response. Avoid pairing
        ///         with <c>ColorFlickerEffect</c> since both manipulate color rendering and will conflict.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> A value of 100 gives a clean full inversion; values around 50 produce a muted, desaturated look.
        ///         Use partial inversion (60–80) for a subtler artistic effect rather than a sharp negative.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @Html.ImageRender("/images/photo.jpg").SetWidth(100, UnitSize.percent).ColorInvertEffect(amount: 100)
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder ColorInvertEffect(this ImageRenderBuilder builder, int amount = 100)
        {
            builder.SetStyle("--eb-color-invert-amount", $"{amount.ToString(CultureInfo.InvariantCulture)}%");
            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/ColorInvertEffect.css");
            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-color-invert"));
        }

        #endregion
    }
}