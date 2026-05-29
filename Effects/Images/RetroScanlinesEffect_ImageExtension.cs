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
    ///     Provides extension methods to apply retro scanline effects to images within the PageBuilder rendering model.
    ///     This class enhances the visual appearance of images by simulating the classic scanline effect found in older video
    ///     games and displays.
    /// </summary>
    /// <remarks>
    ///     The RetroScanlinesEffect_ImageExtension class is designed to work seamlessly with the PageBuilder framework,
    ///     allowing developers
    ///     to easily add a retro aesthetic to their web pages. The scanline effect is achieved by overlaying horizontal lines
    ///     on the image,
    ///     which can be customized in terms of color, intensity, and spacing.
    ///     This class extends the functionality of <see cref="HtmlBuilderBase{TBuilder}" /> by providing additional methods
    ///     that can be chained
    ///     with other builder operations to create complex and visually appealing layouts.
    ///     The lifecycle of this class is tied to the rendering pipeline of PageBuilder. It participates in the rendering
    ///     process by
    ///     modifying the image elements before they are written to the output stream. This ensures that the scanline effect is
    ///     applied
    ///     consistently across all supported platforms and devices.
    /// </remarks>
    [Documented]
    public static class RetroScanlinesEffect_ImageExtension
    {
        #region Static methods

        /// <summary>
        ///     Overlays evenly-spaced horizontal scanlines on the image to simulate a retro CRT monitor or old television
        ///     screen look.
        /// </summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder" /> to apply the effect to.</param>
        /// <param name="opacity">
        ///     The opacity of the scanline overlay in the resting (non-hovered) state (0.0–1.0). Default is
        ///     0.22.
        /// </param>
        /// <param name="hoverOpacity">The opacity of the scanline overlay when the image is hovered (0.0–1.0). Default is 0.35.</param>
        /// <param name="spacing">
        ///     The vertical distance between scanlines (one dark line + one gap), in <paramref name="unit" />.
        ///     Default is 4.
        /// </param>
        /// <param name="unit">The unit of measurement for <paramref name="spacing" />. Default is <see cref="UnitSize.px" />.</param>
        /// <param name="saturate">
        ///     The CSS <c>saturate()</c> filter value applied in the resting state. Default is 0.92 (slightly
        ///     desaturated).
        /// </param>
        /// <param name="hoverSaturate">
        ///     The CSS <c>saturate()</c> filter value applied on hover. Default is 1.08 (slightly
        ///     boosted).
        /// </param>
        /// <param name="contrast">The CSS <c>contrast()</c> filter value applied in the resting state. Default is 1.04.</param>
        /// <param name="hoverContrast">The CSS <c>contrast()</c> filter value applied on hover. Default is 1.08.</param>
        /// <returns>The modified <see cref="ImageRenderBuilder" /> instance with the effect applied.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> Retro gaming galleries, 8-bit or pixel-art themed sites, vintage computer or arcade museum
        ///         pages, developer portfolios with a hacker aesthetic, and any image section meant to evoke nostalgia for CRT
        ///         displays or early home computers.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> Sets eight CSS variables controlling the scanline overlay opacity, spacing, and image
        ///         filter values, then adds the class <c>eb-image-effect-retro-scanlines</c> to the media wrapper via
        ///         <c>RetroScanlinesEffect.css</c>. The CSS creates the scanline grid using a repeating <c>linear-gradient</c>
        ///         pseudo-element overlaid at the set <paramref name="spacing" />, and applies
        ///         <c>filter: saturate() contrast()</c> transitions between resting and hover states.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> Works naturally alongside <c>GrayscaleHoverEffect</c> for a full black-and-white CRT
        ///         experience — desaturate at rest and show color on hover while keeping the scanlines visible throughout. Avoid
        ///         pairing with <c>RainbowEffect</c> since both manipulate the <c>filter</c> property and will conflict.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> A <paramref name="spacing" /> of 2–3 px produces a dense, convincing CRT grid; 6–8 px gives a
        ///         more graphic, stylized stripe. Keep resting <paramref name="opacity" /> below 0.35 — heavier overlays start to
        ///         hide image detail. The hover state intentionally boosts <paramref name="hoverOpacity" /> and
        ///         <paramref name="hoverContrast" /> to heighten the "monitor waking up" feel; this pairing is deliberate and
        ///         worth keeping even if you adjust the base values.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @Html.ImageRender("/images/retro-screenshot.png").SetWidth(320, UnitSize.px).SetHeight(240, UnitSize.px)
        ///     .RetroScanlinesEffect(opacity: 0.28, spacing: 3, saturate: 0.85)
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder RetroScanlinesEffect(
            this ImageRenderBuilder builder,
            double opacity = 0.22,
            double hoverOpacity = 0.35,
            int spacing = 4,
            UnitSize unit = UnitSize.px,
            double saturate = 0.92,
            double hoverSaturate = 1.08,
            double contrast = 1.04,
            double hoverContrast = 1.08
        )
        {
            builder.SetStyle("--eb-image-effect-opacity", opacity.ToString(CultureInfo.InvariantCulture));
            builder.SetStyle("--eb-image-effect-hover-opacity", hoverOpacity.ToString(CultureInfo.InvariantCulture));
            builder.SetStyle("--eb-image-effect-spacing", $"{spacing.ToString(CultureInfo.InvariantCulture)}{unit.GetCss()}");
            builder.SetStyle("--eb-image-effect-saturate", saturate.ToString(CultureInfo.InvariantCulture));
            builder.SetStyle("--eb-image-effect-hover-saturate", hoverSaturate.ToString(CultureInfo.InvariantCulture));
            builder.SetStyle("--eb-image-effect-contrast", contrast.ToString(CultureInfo.InvariantCulture));
            builder.SetStyle("--eb-image-effect-hover-contrast", hoverContrast.ToString(CultureInfo.InvariantCulture));

            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/RetroScanlinesEffect.css");
            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-retro-scanlines"));
        }

        #endregion
    }
}