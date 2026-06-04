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
    ///     Provides an extension method to apply a grayscale hover effect to images using the PageBuilder framework.
    ///     This class enhances the rendering capabilities of <see cref="ImageRenderBuilder" /> by integrating a CSS stylesheet
    ///     and applying specific classes to achieve the desired visual effect.
    /// </summary>
    [Documented]
    public static class GrayscaleHoverEffect_ImageExtension
    {
        #region Static methods

        /// <summary>Drains the image to grayscale when the user hovers over it, then restores full color on mouse-out.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder" /> to apply the effect to.</param>
        /// <param name="amount">
        ///     The grayscale intensity applied on hover (0.0 = no change, 1.0 = fully desaturated). Default is
        ///     1.0.
        /// </param>
        /// <returns>The modified <see cref="ImageRenderBuilder" /> instance with the effect applied.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> Portfolio grids where the color "rewards" the hover, team member photos that desaturate to
        ///         suggest inactivity, and decorative images where reducing color on hover draws attention away from the image and
        ///         toward surrounding UI.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> Sets the CSS variable <c>--eb-image-effect-grayscale</c> and adds the class
        ///         <c>eb-image-effect-grayscale-hover</c> to the media wrapper via <c>GrayscaleHoverEffect.css</c>. The CSS
        ///         applies a <c>filter: grayscale()</c> transition driven by the variable.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> Pairs naturally with <c>LiftHoverEffect</c> — the color returns as the image rises,
        ///         creating a rich compound interaction. Avoid combining with <c>GrayscaleToColorEffect</c> since both manipulate
        ///         the same CSS <c>filter</c> property and will conflict.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> Values between 0.6 and 1.0 produce the most dramatic before/after contrast. An
        ///         <paramref name="amount" /> below 0.4 is barely perceptible and rarely worth the added CSS cost. For a subtler
        ///         look on dark backgrounds, try 0.7 with a slight <c>GlowHoverEffect</c> to compensate for the lost vibrancy.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @Html.ImageRender("/images/photo.jpg").SetWidth(320, UnitSize.px).SetHeight(220, UnitSize.px)
        ///     .SetRounded().GrayscaleHoverEffect(0.8)
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder GrayscaleHoverEffect(this ImageRenderBuilder builder, double amount = 1.0)
        {
            builder.SetStyle("--eb-image-effect-grayscale", amount.ToString(CultureInfo.InvariantCulture));

            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/GrayscaleHoverEffect.css");
            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-grayscale-hover"));
        }

        #endregion
    }
}