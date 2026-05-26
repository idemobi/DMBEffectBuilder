#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder FlipEffect_ImageExtension.cs create at 2026/04/23
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
    /// Provides an extension method to apply a 3D flip effect to images using the PageBuilder framework.
    /// This class enhances the rendering capabilities of <see cref="ImageRenderBuilder"/> by integrating CSS 3D transforms
    /// and animations to create interactive flip effects on hover with customizable axis and rotation point.
    /// </summary>
    [Documented]
    public static class FlipEffect_ImageExtension
    {
        /// <summary>Flips the image along a chosen axis (horizontal, vertical, or both) using a CSS 3D rotation on hover, with a configurable rotation point.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder"/> to apply the effect to.</param>
        /// <param name="axis">The axis of rotation: <c>FlipAxis.X</c> (top-bottom flip), <c>FlipAxis.Y</c> (left-right flip), or <c>FlipAxis.XY</c> (point rotation). Defaults to <c>FlipAxis.Y</c>.</param>
        /// <param name="offset">Position of the rotation pivot along the active axis (0.0 = start edge to 1.0 = end edge). Clamped to [0, 1]. Defaults to 0.5 (center).</param>
        /// <returns>The modified <see cref="ImageRenderBuilder"/> instance with the effect applied.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> Card-flip UI patterns, interactive gallery tiles, game asset previews, and any hover interaction that benefits from a tactile 3D turn gesture.</para>
        /// <para><b>How it works:</b> Sets the CSS variable <c>--eb-image-flip-effect-offset</c> and applies wrapper classes <c>eb-image-effect-flip</c> plus <c>eb-image-effect-flip-axis-{axis}</c> via <c>FlipEffect.css</c>. The image itself receives <c>eb-image-flip-target</c>. Uses CSS <c>rotateX</c>, <c>rotateY</c>, or both.</para>
        /// <para><b>Combinations:</b> Works independently as a strong standalone interaction. Avoid combining with <c>DistortEffect</c> or <c>GlitchEffect</c> as simultaneous geometric transforms produce unpredictable results.</para>
        /// <para><b>Tips:</b> Use <c>FlipAxis.Y</c> (default) for the most natural card-flip feel. Adjust <paramref name="offset"/> away from 0.5 to create an off-center hinge, such as a page-turn from the left edge (offset: 0.0).</para>
        /// <para><b>Example:</b>
        /// <code>
        /// @Html.ImageRender("/images/photo.jpg").SetWidth(100, UnitSize.percent).FlipEffect(axis: FlipAxis.Y, offset: 0.5)
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder FlipEffect(this ImageRenderBuilder builder, FlipAxis axis = FlipAxis.Y, double offset = 0.5)
        {
            // Validate offset is within 0-1 range
            offset = Math.Max(0, Math.Min(1, offset));

            builder.SetStyle("--eb-image-flip-effect-offset", offset.ToString(CultureInfo.InvariantCulture));

            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/FlipEffect.css");
            return builder.MarkAsEffect()
                .InWrapperComponent(w => w
                    .AddClass("eb-image-effect-flip")
                    .AddClass($"eb-image-effect-flip-axis-{axis.ToString().ToLowerInvariant()}"))
                .InMediaComponent(img => img.AddClass("eb-image-flip-target"));
        }
    }
}

