#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj PolaroidEffect_ImageExtension.cs create at 2026/04/21
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
    /// Provides an extension method to apply a Polaroid effect to images using the PageBuilder framework.
    /// This class enhances the rendering capabilities of images by integrating a specific CSS stylesheet
    /// and applying a designated HTML class for styling.
    /// </summary>
    /// <remarks>
    /// The PolaroidEffect_ImageExtension class is designed to work within the PageBuilder rendering model,
    /// specifically targeting the ImageRenderBuilder. It contributes to the rendering pipeline by modifying
    /// the image's appearance through CSS and HTML class manipulation.
    /// The extension method <see cref="PolaroidEffect"/> modifies the image by setting a stylesheet
    /// and marking it as an effect. It also wraps the image in a media component with a specific CSS class
    /// to apply the Polaroid effect.
    /// This class is part of the DMBEffectBuilder namespace and leverages the PageRegistry to manage
    /// page-specific information, ensuring that the stylesheet is correctly associated with the current HTTP context.
    /// Usage:
    /// var imageBuilder = new ImageRenderBuilder(writer, htmlHelper, "image.jpg", "Sample Image");
    /// var polaroidImage = imageBuilder.PolaroidEffect();
    /// This will render an image with the Polaroid effect applied, using the specified CSS and HTML class.
    /// </remarks>
    [Documented]
    public static class PolaroidEffect_ImageExtension
    {
        /// <summary>Styles the image as a Polaroid photograph with a white frame, thicker bottom border, subtle shadow, and a slight rotation.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder"/> to apply the effect to.</param>
        /// <param name="padding">Uniform padding applied to all four sides of the Polaroid frame, in <paramref name="unit"/>. Default is 10.</param>
        /// <param name="unit">The unit of measurement for both <paramref name="padding"/> and <paramref name="bottomPadding"/>. Default is <see cref="UnitSize.px"/>.</param>
        /// <param name="bottomPadding">Extra padding on the bottom edge that creates the characteristic wide Polaroid tab. Default is 26.</param>
        /// <param name="backgroundColor">The fill color of the Polaroid frame. Defaults to white (<c>Color.White</c>).</param>
        /// <param name="borderColor">The color of the thin border around the frame. Defaults to a very light transparent black (<c>rgba(0,0,0,0.08)</c>).</param>
        /// <param name="shadowColor">The color of the drop shadow cast by the frame. Defaults to a soft transparent black (<c>rgba(0,0,0,0.18)</c>).</param>
        /// <returns>The modified <see cref="ImageRenderBuilder"/> instance with the effect applied.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> Photography portfolios, nostalgic or vintage-themed galleries, travel blogs, testimonial sections with portrait photos, and any layout where an informal, handcrafted feel is desirable. Works best when several polaroid images are placed in a scattered or slightly rotated grid.</para>
        /// <para><b>How it works:</b> Sets CSS variables <c>--eb-image-effect-padding</c>, <c>--eb-image-effect-bottom-padding</c>, <c>--eb-image-effect-bg-color</c>, <c>--eb-image-effect-border-color</c>, and <c>--eb-image-effect-shadow-color</c>, then adds the class <c>eb-image-effect-polaroid</c> to the media wrapper via <c>PolaroidEffect.css</c>. The CSS applies <c>padding</c>, <c>border</c>, <c>box-shadow</c>, and a slight <c>rotate</c> transform to the wrapper.</para>
        /// <para><b>Combinations:</b> Combine with <c>LiftHoverEffect</c> to make individual polaroids lift on hover — this is particularly effective in a scattered grid. Avoid using with <c>NeonFrameEffect</c> or <c>GlowHoverEffect</c>, which clash with the delicate paper frame aesthetic.</para>
        /// <para><b>Tips:</b> Set a fixed image size before calling this method — the Polaroid frame adds <paramref name="padding"/> and <paramref name="bottomPadding"/> around the image, so the overall rendered size is larger than the image dimensions. Increase <paramref name="bottomPadding"/> to 40–50 px if you plan to overlay a caption inside the tab area via CSS. For a stack-of-photos look, apply alternating small positive and negative CSS <c>rotate</c> values to sibling wrappers manually.</para>
        /// <para><b>Example:</b>
        /// <code>
        /// @Html.ImageRender("/images/photo.jpg").SetWidth(220, UnitSize.px).SetHeight(160, UnitSize.px)
        ///     .PolaroidEffect(padding: 12, bottomPadding: 40, shadowColor: Color.FromArgb(60, 0, 0, 0))
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder PolaroidEffect(
            this ImageRenderBuilder builder,
            int padding = 10,
            UnitSize unit = UnitSize.px,
            int bottomPadding = 26,
            Color? backgroundColor = null,
            Color? borderColor = null,
            Color? shadowColor = null
        )
        {
            Color actualBackgroundColor = backgroundColor ?? Color.White;
            Color actualBorderColor = borderColor ?? Color.FromArgb(20, 0, 0, 0);
            Color actualShadowColor = shadowColor ?? Color.FromArgb(46, 0, 0, 0);

            builder.SetStyle("--eb-image-effect-padding", $"{padding.ToString(CultureInfo.InvariantCulture)}{unit.GetCss()}");
            builder.SetStyle("--eb-image-effect-bottom-padding", $"{bottomPadding.ToString(CultureInfo.InvariantCulture)}{unit.GetCss()}");
            builder.SetStyle("--eb-image-effect-bg-color", actualBackgroundColor.ToRgba());
            builder.SetStyle("--eb-image-effect-border-color", actualBorderColor.ToRgba());
            builder.SetStyle("--eb-image-effect-shadow-color", actualShadowColor.ToRgba());

            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/PolaroidEffect.css");
            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-polaroid"));
        }
    }
}
