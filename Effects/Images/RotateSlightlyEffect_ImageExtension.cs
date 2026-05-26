#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj RotateSlightlyEffect_ImageExtension.cs create at 2026/04/21
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
    /// Provides an extension method to apply a slight rotate effect to images using the PageBuilder framework.
    /// </summary>
    [Documented]
    public static class RotateSlightlyEffect_ImageExtension
    {
        /// <summary>Applies a fixed, subtle tilt to the image, giving it a relaxed, hand-placed appearance at rest and optionally correcting on hover.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder"/> to apply the effect to.</param>
        /// <param name="deg">Static rotation angle in degrees applied at rest (default -2 — a gentle counter-clockwise lean).</param>
        /// <param name="hoverDeg">Rotation angle applied when the user hovers (default 0 — straightens the image on hover).</param>
        /// <param name="scale">Base scale factor at rest (default 1.0 — no scaling).</param>
        /// <param name="hoverScale">Scale factor on hover (default 1.0 — no change).</param>
        /// <returns>The modified <see cref="ImageRenderBuilder"/> instance with the effect applied.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> Decorative images in blog posts, profile pictures with a casual tone, card-style UI elements, or anywhere a perfectly straight image would feel too rigid. Unlike <c>RotateRandomEffect</c>, the angle is deterministic — every page load looks identical, which is better for design consistency.</para>
        /// <para><b>How it works:</b> Writes CSS variables <c>--eb-image-effect-deg</c>, <c>--eb-image-effect-hover-deg</c>, <c>--eb-image-effect-scale</c>, and <c>--eb-image-effect-hover-scale</c> directly on the image element, then wraps it in a component with class <c>eb-image-effect-rotate-slightly</c> (RotateSlightlyEffect.css).</para>
        /// <para><b>Combinations:</b> Works very well with <c>SoftShadowEffect</c> to simulate a physical print. Can be layered with <c>VignetteEffect</c> for a vintage photograph feel. Avoid combining with <c>RotateEffect</c> or <c>RotateRandomEffect</c>.</para>
        /// <para><b>Tips:</b> Stay within ±6 degrees for a natural look; beyond that the tilt reads as a deliberate design choice rather than a subtle accent. Use <c>hoverDeg: 0, hoverScale: 1.05</c> to create a "pick up the photo" hover interaction. A positive <c>deg</c> leans right, negative leans left — mix both in a grid for visual rhythm.</para>
        /// <para><b>Example:</b>
        /// <code>
        /// @Html.ImageRender("/images/photo.jpg").SetWidth(100, UnitSize.percent).RotateSlightlyEffect(deg: -3, hoverDeg: 0, hoverScale: 1.05)
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder RotateSlightlyEffect(this ImageRenderBuilder builder, double deg = -2, double hoverDeg = 0, double scale = 1.0, double hoverScale = 1.0)
        {
            builder.SetStyle("--eb-image-effect-deg", $"{deg.ToString(CultureInfo.InvariantCulture)}deg");
            builder.SetStyle("--eb-image-effect-hover-deg", $"{hoverDeg.ToString(CultureInfo.InvariantCulture)}deg");
            builder.SetStyle("--eb-image-effect-scale", scale.ToString(CultureInfo.InvariantCulture));
            builder.SetStyle("--eb-image-effect-hover-scale", hoverScale.ToString(CultureInfo.InvariantCulture));

            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/RotateSlightlyEffect.css");
            return builder.MarkAsEffect().InWrapperComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-rotate-slightly"));
        }
    }
}
