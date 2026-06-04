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
    ///     Provides an extension method to apply a pulse effect to images using the PageBuilder framework.
    ///     This class enhances the rendering capabilities of <see cref="ImageRenderBuilder" /> by adding a scale animation
    ///     that can be customized with a specified scale factor and duration.
    /// </summary>
    [Documented]
    public static class PulseEffect_ImageExtension
    {
        #region Static methods

        /// <summary>
        ///     Applies a continuous looping scale-pulse animation to the image, rhythmically growing and shrinking it in
        ///     place.
        /// </summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder" /> to apply the effect to.</param>
        /// <param name="scale">The maximum scale factor reached at the peak of each pulse cycle. Default is 1.05 (5% larger).</param>
        /// <param name="duration">
        ///     The total duration of one full pulse cycle, in milliseconds as a string. Default is
        ///     <c>"1000"</c> (1 second).
        /// </param>
        /// <returns>The modified <see cref="ImageRenderBuilder" /> instance with the effect applied.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> Attention-grabbing call-to-action images, notification icons, "new" badges overlaid on
        ///         images, avatar indicators in live/online status contexts, and any small image that needs to signal activity or
        ///         urgency without text.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> Sets CSS variables <c>--eb-pulse-scale</c> and <c>--eb-image-effect-duration</c> (the
        ///         duration is passed as a millisecond value and the stylesheet appends <c>ms</c>), then adds the classes
        ///         <c>eb-image-effect-pulse</c> and <c>eb-image-effect-pulse-var</c> to the media wrapper via
        ///         <c>PulseEffect.css</c>. The CSS drives a looping <c>@keyframes</c> animation that alternates the
        ///         <c>transform: scale()</c> between 1.0 and <paramref name="scale" />.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> Pairs well with <c>NeonFrameEffect</c> for a glowing pulsing beacon. Do not combine with
        ///         <c>KenBurnsSlowEffect</c> — both run looping scale keyframes simultaneously, which produces jittery,
        ///         unpredictable transforms.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> Keep <paramref name="scale" /> between 1.03 and 1.10 — values above 1.15 become distracting and
        ///         can shift surrounding layout elements if <c>overflow: hidden</c> is not set on the container. Slower pulses
        ///         (1500–2000 ms) feel calm and ambient; faster pulses (400–600 ms) convey urgency. Pass the duration as a plain
        ///         integer string — do not append <c>ms</c> or <c>s</c> yourself as the CSS already does this.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @Html.ImageRender("/images/icon.png").SetWidth(80, UnitSize.px).SetHeight(80, UnitSize.px)
        ///     .PulseEffect(scale: 1.08, duration: "1200")
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder PulseEffect(this ImageRenderBuilder builder, double scale = 1.05, string duration = "1000")
        {
            builder.SetStyle("--eb-pulse-scale", scale.ToString(CultureInfo.InvariantCulture));
            builder.SetStyle("--eb-image-effect-duration", $"{duration}ms");
            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/PulseEffect.css");
            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-pulse eb-image-effect-pulse-var"));
        }

        #endregion
    }
}