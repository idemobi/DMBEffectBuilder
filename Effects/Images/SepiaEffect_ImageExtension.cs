#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj SepiaEffect_ImageExtension.cs create at 2026/04/21
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
    /// Provides an extension method to apply a sepia effect to images using the PageBuilder framework.
    /// This class enhances the <see cref="ImageRenderBuilder"/> by adding CSS styles and managing
    /// stylesheet inclusion through the <see cref="PageInformation"/> object.
    /// </summary>
    [Documented]
    public static class SepiaEffect_ImageExtension
    {
        /// <summary>Applies a CSS sepia filter to the image, with independent intensity and contrast values for rest and hover states.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder"/> to apply the effect to.</param>
        /// <param name="amount">Sepia intensity at rest as a percentage (0–100); default 0 — full color at rest.</param>
        /// <param name="hoverAmount">Sepia intensity on hover as a percentage (0–100); default 100 — fully desaturated to warm brown on hover.</param>
        /// <param name="contrast">CSS contrast multiplier at rest; default 1.0 — no adjustment.</param>
        /// <param name="hoverContrast">CSS contrast multiplier on hover; default 1.2 — slightly punchier when sepia kicks in.</param>
        /// <returns>The modified <see cref="ImageRenderBuilder"/> instance with the effect applied.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> Portfolio thumbnails that reveal color on hover, historical or archival image galleries, hero images with a muted-by-default tone, or any context where you want color to feel like a reward for interaction.</para>
        /// <para><b>How it works:</b> Sets CSS variables <c>--eb-image-effect-sepia</c>, <c>--eb-image-effect-hover-sepia</c>, <c>--eb-image-effect-contrast</c>, and <c>--eb-image-effect-hover-contrast</c> on the image element, then wraps it in a media component with class <c>eb-image-effect-sepia</c> (SepiaEffect.css). Transition timing is defined in the stylesheet.</para>
        /// <para><b>Combinations:</b> Pairs naturally with <c>ZoomHoverEffect</c> for a combined "color-and-zoom" reveal. <c>VignetteEffect</c> adds mood when <c>amount</c> is non-zero. Avoid combining with <c>SepiaToColorEffect</c> — they control the same CSS variables.</para>
        /// <para><b>Tips:</b> Use <c>amount: 80, hoverAmount: 0</c> to invert the default — sepia at rest, color on hover (a more dramatic reveal). Raise <c>hoverContrast</c> to 1.3–1.4 to make colors pop vividly on hover. Keep <c>amount: 0</c> (default) for the subtle "color unlocks on hover" pattern that works on image grids.</para>
        /// <para><b>Example:</b>
        /// <code>
        /// @Html.ImageRender("/images/photo.jpg").SetWidth(100, UnitSize.percent).SepiaEffect(amount: 80, hoverAmount: 0, hoverContrast: 1.3)
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder SepiaEffect(this ImageRenderBuilder builder, int amount = 0, int hoverAmount = 100, double contrast = 1.0, double hoverContrast = 1.2)
        {
            builder.SetStyle("--eb-image-effect-sepia", $"{amount.ToString(CultureInfo.InvariantCulture)}%");
            builder.SetStyle("--eb-image-effect-hover-sepia", $"{hoverAmount.ToString(CultureInfo.InvariantCulture)}%");
            builder.SetStyle("--eb-image-effect-contrast", $"{contrast.ToString(CultureInfo.InvariantCulture)}");
            builder.SetStyle("--eb-image-effect-hover-contrast", $"{hoverContrast.ToString(CultureInfo.InvariantCulture)}");
            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/SepiaEffect.css");
            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-sepia"));
        }
    }
}
