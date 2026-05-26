#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj KenBurnsSlowEffect_ImageExtension.cs create at 2026/04/21
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
    /// Provides an extension method to apply a Ken Burns slow effect to images using the PageBuilder framework.
    /// This class enhances the rendering capabilities of <see cref="ImageRenderBuilder"/> by integrating a specific CSS effect.
    /// </summary>
    [Documented]
    public static class KenBurnsSlowEffect_ImageExtension
    {
        /// <summary>Applies a continuous slow Ken Burns zoom-and-pan animation to the image in a looping CSS keyframe.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder"/> to apply the effect to.</param>
        /// <param name="scale">The zoom scale factor reached at the end of the zoom-in phase of the animation. Default is 1.08 (8% zoom).</param>
        /// <returns>The modified <see cref="ImageRenderBuilder"/> instance with the effect applied.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> Hero banners, full-width landscape photos, background images in section headers, and any large image that should convey a sense of life or movement without distracting from foreground content. Works especially well when the image is used as a decorative backdrop.</para>
        /// <para><b>How it works:</b> Sets the CSS variable <c>--eb-image-effect-scale</c> and adds the class <c>eb-image-effect-ken-burns-slow</c> to the media wrapper via <c>KenBurnsSlowEffect.css</c>. The CSS defines a looping <c>@keyframes</c> animation that slowly scales the image from 1.0 to <paramref name="scale"/> while gently shifting the transform-origin to simulate a pan.</para>
        /// <para><b>Combinations:</b> This is a passive, continuous animation — it does not respond to hover — so it layers cleanly with hover-based effects like <c>GlowHoverEffect</c> or <c>LiftHoverEffect</c>. Avoid pairing with <c>PulseEffect</c>, which also loops a scale animation and will produce a conflicting keyframe fight.</para>
        /// <para><b>Tips:</b> Keep <paramref name="scale"/> between 1.05 and 1.15 for a natural documentary feel; values above 1.20 make the zoom too aggressive and reveal image edges on smaller containers. Always set <c>object-fit: cover</c> on the inner image so cropped edges are never visible during the pan. The animation speed is controlled by the CSS; to slow it down further, override <c>animation-duration</c> on the <c>.eb-image-effect-ken-burns-slow</c> class.</para>
        /// <para><b>Example:</b>
        /// <code>
        /// @Html.ImageRender("/images/landscape.jpg").SetWidth(100, UnitSize.percent).SetHeight(400, UnitSize.px)
        ///     .InMediaComponent(m => m.SetStyle("object-fit", "cover"))
        ///     .KenBurnsSlowEffect(scale: 1.12)
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder KenBurnsSlowEffect(this ImageRenderBuilder builder, double scale = 1.08)
        {
            builder.SetStyle("--eb-image-effect-scale", scale.ToString(CultureInfo.InvariantCulture));

            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/KenBurnsSlowEffect.css");
            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-ken-burns-slow"));
        }
    }
}
