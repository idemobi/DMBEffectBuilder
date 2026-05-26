#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj RotateRandomEffect_ImageExtension.cs create at 2026/04/21
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using System;
using System.Globalization;
using DMBBootstrapBuilder;
using DMBPageBuilder;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides an extension method to apply a random rotate effect to images using the PageBuilder framework.
    /// </summary>
    [Documented]
    public static class RotateRandomEffect_ImageExtension
    {
        /// <summary>Tilts the image by a random angle chosen at page-load time, giving each render a unique, hand-placed look.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder"/> to apply the effect to.</param>
        /// <param name="fromDeg">Lower bound of the random rotation range in degrees (default -4).</param>
        /// <param name="toDeg">Upper bound of the random rotation range in degrees (default 4).</param>
        /// <param name="scale">Base scale factor applied before any interaction (default 1.0 — no scaling).</param>
        /// <param name="hoverScale">Scale factor applied when the user hovers over the image (default 1.0 — no change).</param>
        /// <param name="hoverDeg">Fixed rotation applied on hover, overriding the random angle (default 0.0 — returns to straight).</param>
        /// <returns>The modified <see cref="ImageRenderBuilder"/> instance with the effect applied.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> Photo galleries, scrapbook-style layouts, polaroid grids, or any design where a "casually scattered" aesthetic is desired. Each server-side render picks a new angle so repeated page loads vary the composition naturally.</para>
        /// <para><b>How it works:</b> Picks a random <c>double</c> between <paramref name="fromDeg"/> and <paramref name="toDeg"/> using <c>Random.Shared</c> at render time, writes it to the CSS variable <c>--eb-rotate-random-angle</c> on the wrapper element, and adds class <c>eb-image-effect-rotate-random</c> (RotateRandomEffect.css). Scale and hover values are stored in matching CSS variables on the same wrapper.</para>
        /// <para><b>Combinations:</b> Pairs naturally with <c>SoftShadowEffect</c> to reinforce the physical-print illusion. Avoid combining with <c>RotateEffect</c> (spinning animation) or <c>TiltParallaxEffect</c> — the competing transforms will fight each other.</para>
        /// <para><b>Tips:</b> Keep the range narrow (±3–6 deg) for a realistic scattered-photos look. Use a wider range (±15–30 deg) for a playful, chaotic feel. Set <c>hoverDeg: 0</c> and <c>hoverScale: 1.05</c> to let the user "straighten and lift" the photo on hover — a satisfying micro-interaction.</para>
        /// <para><b>Example:</b>
        /// <code>
        /// @Html.ImageRender("/images/photo.jpg").SetWidth(200, UnitSize.px).RotateRandomEffect(fromDeg: -6, toDeg: 6, hoverScale: 1.05, hoverDeg: 0)
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder RotateRandomEffect(this ImageRenderBuilder builder, double fromDeg = -4d, double toDeg = 4d, double scale = 1.0d, double hoverScale = 1.0d, double hoverDeg = 0.0d)
        {
            double angle = fromDeg + ((toDeg - fromDeg) * Random.Shared.NextDouble());
            string angleStr = angle.ToString("0.##", CultureInfo.InvariantCulture);

            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/RotateRandomEffect.css");

            return builder.MarkAsEffect().InWrapperComponent(wrapperBuilder =>
                wrapperBuilder.AddClass("eb-image-effect-rotate-random")
                    .SetStyle("--eb-rotate-random-angle", $"{angleStr}deg")
                    .SetStyle("--eb-image-effect-scale", scale.ToString(CultureInfo.InvariantCulture))
                    .SetStyle("--eb-image-effect-hover-scale", hoverScale.ToString(CultureInfo.InvariantCulture))
                    .SetStyle("--eb-image-effect-hover-deg", $"{hoverDeg.ToString(CultureInfo.InvariantCulture)}deg")
            );
        }
    }
}
