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
    ///     Provides an extension method to apply a rotate effect to images using the PageBuilder framework.
    /// </summary>
    [Documented]
    public static class RotateEffect_ImageExtension
    {
        #region Static methods

        /// <summary>Applies a continuous or single-pass rotation animation to the image, spinning it around its center.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder" /> to apply the effect to.</param>
        /// <param name="deg">Total rotation angle in degrees per animation cycle (default 360 — a full spin).</param>
        /// <param name="scale">
        ///     Scale factor applied during the animation; use values above 1.0 to grow the image as it spins
        ///     (default 1.0 — no scaling).
        /// </param>
        /// <param name="infinite">
        ///     When <c>true</c> the animation loops forever; when <c>false</c> it plays once and stops (default
        ///     true).
        /// </param>
        /// <returns>The modified <see cref="ImageRenderBuilder" /> instance with the effect applied.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> Loading spinners, decorative icons, logo animations, or any small graphic where perpetual
        ///         motion adds energy to the layout.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> Sets the CSS variables <c>--eb-rotate-deg</c>, <c>--eb-image-effect-scale</c>, and
        ///         <c>--eb-image-effect-iteration</c>, then wraps the image in a media component with class
        ///         <c>eb-image-effect-rotate</c> (RotateEffect.css).
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> Works well with <c>ZoomHoverEffect</c> on a parent container. Avoid stacking with
        ///         <c>RotateRandomEffect</c> or <c>RotateSlightlyEffect</c> — multiple rotate effects on the same image conflict.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> For a slow, meditative spin use <c>deg: 360</c> and a long CSS animation duration (controlled in
        ///         the stylesheet). For a half-turn flip, use <c>deg: 180</c> with <c>infinite: false</c>. Combine
        ///         <c>scale: 1.1</c> with a slow spin for a living-poster feel. Keep <c>deg</c> positive for clockwise; negate in
        ///         the CSS or use a negative value if counter-clockwise is needed.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @Html.ImageRender("/images/logo-icon.png").SetWidth(80, UnitSize.px).RotateEffect(deg: 360, scale: 1.0, infinite: true)
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder RotateEffect(this ImageRenderBuilder builder, int deg = 360, double scale = 1.0, bool infinite = true)
        {
            builder.SetStyle("--eb-rotate-deg", $"{deg.ToString(CultureInfo.InvariantCulture)}deg");
            builder.SetStyle("--eb-image-effect-scale", scale.ToString(CultureInfo.InvariantCulture));
            builder.SetStyle("--eb-image-effect-iteration", infinite ? "infinite" : "1");

            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/RotateEffect.css");
            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-rotate"));
        }

        #endregion
    }
}