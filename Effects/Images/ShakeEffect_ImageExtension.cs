#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj ShakeEffect_ImageExtension.cs create at 2026/04/22
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
    /// Provides an extension method to apply a shake effect to images using the PageBuilder framework.
    /// This class enhances the rendering capabilities of <see cref="ImageRenderBuilder"/> by adding a shake animation
    /// that can be customized with a specified intensity and duration.
    /// </summary>
    [Documented]
    public static class ShakeEffect_ImageExtension
    {
        /// <summary>Makes the image vibrate continuously in a looping side-to-side shake animation, with configurable intensity and speed.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder"/> to apply the effect to.</param>
        /// <param name="intensity">Maximum pixel displacement per shake step (default 2 px — subtle tremor; increase for aggressive shaking).</param>
        /// <param name="duration">Duration of one full shake cycle in milliseconds (default 500 ms; lower values shake faster).</param>
        /// <returns>The modified <see cref="ImageRenderBuilder"/> instance with the effect applied.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> Alert or error state indicators, attention-grabbing call-to-action images, game-UI elements signaling failure or urgency, notification badges, or any image that should feel agitated or alive.</para>
        /// <para><b>How it works:</b> Writes <c>--eb-shake-intensity</c> (in px) and <c>--eb-image-effect-duration</c> (in ms) as CSS variables on the image element, then wraps it in a media component with classes <c>eb-image-effect-shake eb-image-effect-shake-var</c> (ShakeEffect.css). The animation loops indefinitely via CSS keyframes.</para>
        /// <para><b>Combinations:</b> Avoid layering with other motion effects like <c>RotateEffect</c> or <c>TiltParallaxEffect</c> — competing transforms create visual chaos. <c>VignetteEffect</c> can reinforce a "something is wrong" aesthetic. Works fine alongside static filters like <c>SepiaEffect</c>.</para>
        /// <para><b>Tips:</b> Use <c>intensity: 1, duration: 700</c> for a barely-perceptible tremor on decorative elements. Use <c>intensity: 5, duration: 150</c> for an aggressive, error-state rattle. Be mindful of accessibility — continuous motion can affect users with vestibular disorders; consider wrapping the render in a <c>prefers-reduced-motion</c> media query check.</para>
        /// <para><b>Example:</b>
        /// <code>
        /// @Html.ImageRender("/images/alert-icon.png").SetWidth(48, UnitSize.px).ShakeEffect(intensity: 3, duration: 300)
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder ShakeEffect(this ImageRenderBuilder builder, double intensity = 2, int duration = 500)
        {
            builder.SetStyle("--eb-shake-intensity", $"{intensity.ToString(CultureInfo.InvariantCulture)}px");
            builder.SetStyle("--eb-image-effect-duration", $"{duration}ms");
            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/ShakeEffect.css");
            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-shake eb-image-effect-shake-var"));
        }
    }
}
