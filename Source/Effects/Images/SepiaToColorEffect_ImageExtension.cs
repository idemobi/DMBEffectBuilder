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
    ///     Provides an extension method to apply a sepia-to-color effect to images using the PageBuilder framework.
    ///     This class enhances the rendering capabilities by allowing developers to easily integrate visual effects into their
    ///     web pages.
    /// </summary>
    /// <remarks>
    ///     The <see cref="SepiaToColorEffect_ImageExtension" /> class is designed to work within the PageBuilder rendering
    ///     model.
    ///     It extends the functionality of <see cref="ImageRenderBuilder" /> by adding a sepia-to-color effect to images.
    ///     This effect is achieved by applying specific CSS styles and classes, which are managed through the
    ///     <see cref="PageInformation" /> class.
    /// </remarks>
    [Documented]
    public static class SepiaToColorEffect_ImageExtension
    {
        #region Static methods

        /// <summary>Transitions the image from sepia at rest to full color on hover, using a normalized 0–1 scale for both states.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder" /> to apply the effect to.</param>
        /// <param name="amount">Sepia intensity at rest as a normalized value (0.0 = no sepia, 1.0 = fully sepia); default 1.0.</param>
        /// <param name="hoverAmount">
        ///     Sepia intensity on hover as a normalized value (0.0 = full color, 1.0 = fully sepia); default
        ///     0.0.
        /// </param>
        /// <returns>The modified <see cref="ImageRenderBuilder" /> instance with the effect applied.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> Image grids where color is the reward for engagement — portfolio thumbnails, team member
        ///         photos, product listings. Particularly effective in editorial layouts where sepia evokes mood until the user
        ///         interacts.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> Multiplies <paramref name="amount" /> and <paramref name="hoverAmount" /> by 100 and
        ///         writes them as percentages to CSS variables <c>--eb-image-effect-sepia</c> and
        ///         <c>--eb-image-effect-hover-sepia</c>. Wraps the image in a media component with class
        ///         <c>eb-image-effect-sepia-to-color</c> (SepiaToColorEffect.css). The CSS handles the transition timing.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> Excellent with <c>ZoomHoverEffect</c> — the zoom-and-colorize double interaction feels
        ///         premium. <c>SoftShadowEffect</c> adds depth. Avoid combining with <c>SepiaEffect</c> — they share the same CSS
        ///         variables and will conflict.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> The default (<c>amount: 1.0, hoverAmount: 0.0</c>) is the canonical sepia-to-color reveal — use
        ///         it as-is in most cases. For a partial reveal, set <c>hoverAmount: 0.3</c> to leave a faint warm tint on hover.
        ///         To reverse the effect (color at rest, sepia on hover), swap the defaults: <c>amount: 0.0, hoverAmount: 1.0</c>.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @Html.ImageRender("/images/photo.jpg").SetWidth(100, UnitSize.percent).SepiaToColorEffect(amount: 1.0, hoverAmount: 0.0)
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder SepiaToColorEffect(this ImageRenderBuilder builder, double amount = 1.0, double hoverAmount = 0.0)
        {
            builder.SetStyle("--eb-image-effect-sepia", $"{(amount * 100).ToString(CultureInfo.InvariantCulture)}%");
            builder.SetStyle("--eb-image-effect-hover-sepia", $"{(hoverAmount * 100).ToString(CultureInfo.InvariantCulture)}%");

            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/SepiaToColorEffect.css");
            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-sepia-to-color"));
        }

        #endregion
    }
}