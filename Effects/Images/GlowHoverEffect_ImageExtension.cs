#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj GlowHoverEffect_ImageExtension.cs create at 2026/04/21
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using DMBBootstrapBuilder;
using DMBPageBuilder;
using DMBServerWebHelper;
using System.Drawing;
using System.Globalization;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides an extension method to apply a glow hover effect to images using the PageBuilder framework.
    /// This class enhances the rendering capabilities of <see cref="ImageRenderBuilder"/> by integrating CSS styles
    /// and HTML class attributes that create a visual effect when an image is hovered over.
    /// </summary>
    /// <remarks>
    /// The GlowHoverEffect_ImageExtension class is designed to work within the PageBuilder rendering model,
    /// specifically targeting images. It leverages the <see cref="PageInformation"/> class to manage CSS
    /// stylesheet inclusion and modifies the HTML structure of images through the <see cref="ImageRenderBuilder"/>
    /// class.
    /// The extension method <see cref="GlowHoverEffect"/> is intended to be called on an instance of
    /// <see cref="ImageRenderBuilder"/>. It sets the necessary CSS stylesheet and applies a specific
    /// HTML class to enable the glow hover effect.
    /// This method is part of the rendering pipeline, where it contributes to the visual presentation
    /// of images by enhancing their interactivity. It is particularly useful in scenarios where a subtle
    /// visual cue is needed to indicate that an image is interactive or clickable.
    /// The method interacts with <see cref="HtmlBuilderBase{TBuilder}"/> by adding a custom class to the
    /// image element, which is then processed during the rendering process.
    /// Usage example:
    /// <code>
    /// var imageBuilder = new ImageRenderBuilder(writer, htmlHelper, "image.jpg", "alt text");
    /// imageBuilder.GlowHoverEffect();
    /// </code>
    /// This will apply the glow hover effect to the image, enhancing its appearance when hovered over.
    /// </remarks>
    [Documented]
    public static class GlowHoverEffect_ImageExtension
    {
        /// <summary>Adds a colored outer glow around the image that becomes visible when the user hovers over it.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder"/> to apply the effect to.</param>
        /// <param name="color">The RGBA color of the glow. Defaults to a semi-transparent Bootstrap primary blue (<c>rgba(64,13,110,253)</c>).</param>
        /// <param name="size">The spread radius of the glow in <paramref name="unit"/>. Default is 28.</param>
        /// <param name="unit">The unit of measurement for <paramref name="size"/>. Default is <see cref="UnitSize.px"/>.</param>
        /// <returns>The modified <see cref="ImageRenderBuilder"/> instance with the effect applied.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> Card thumbnails, clickable gallery images, profile avatars, and any interactive image that benefits from a visual hover cue without JavaScript.</para>
        /// <para><b>How it works:</b> Sets CSS variables <c>--eb-image-effect-glow-color</c> and <c>--eb-image-effect-glow-size</c> on the image element, then adds the class <c>eb-image-effect-glow-hover</c> to its media wrapper via <c>GlowHoverEffect.css</c>. The CSS applies a <c>box-shadow</c> transition on <c>:hover</c> to produce the glow.</para>
        /// <para><b>Combinations:</b> Works well alongside <c>LiftHoverEffect</c> — the lift physically separates the image while the glow frames it. Avoid stacking with <c>NeonFrameEffect</c>: both effects compete for the outer-border space and the two <c>box-shadow</c> values clash visually.</para>
        /// <para><b>Tips:</b> Use an alpha value below 160 for the glow color to keep it soft; a fully opaque color looks harsh. For branding, pass a tinted variant of your primary accent color. Sizes between 20–40 px feel natural; above 60 px the glow bleeds into adjacent elements — add layout margin to compensate.</para>
        /// <para><b>Example:</b>
        /// <code>
        /// @Html.ImageRender("/images/photo.jpg").SetWidth(100, UnitSize.percent).SetHeight(180, UnitSize.px)
        ///     .InMediaComponent(m => m.SetStyle("object-fit", "cover"))
        ///     .GlowHoverEffect(Color.FromArgb(128, 13, 110, 253), 35)
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder GlowHoverEffect(this ImageRenderBuilder builder, Color? color = null, int size = 28, UnitSize unit = UnitSize.px)
        {
            Color actualColor = color ?? Color.FromArgb(64, 13, 110, 253);
            builder.SetStyle("--eb-image-effect-glow-color", actualColor.ToRgba());
            builder.SetStyle("--eb-image-effect-glow-size", $"{size.ToString(CultureInfo.InvariantCulture)}{unit.GetCss()}");

            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/GlowHoverEffect.css");
            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-glow-hover"));
        }
    }
}
