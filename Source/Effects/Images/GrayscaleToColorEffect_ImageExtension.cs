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
    ///     Provides an extension method to apply a grayscale-to-color effect to images using the PageBuilder framework.
    ///     This class enhances the rendering capabilities by allowing developers to easily add visual effects to images
    ///     within their web pages.
    /// </summary>
    /// <remarks>
    ///     The GrayscaleToColorEffect_ImageExtension class is a static utility class that extends the functionality of
    ///     ImageRenderBuilder. It leverages the PageInformation and PageRegistry classes to manage stylesheet inclusion,
    ///     ensuring that the necessary CSS for the grayscale-to-color effect is loaded and applied to the image.
    ///     This extension method is particularly useful in scenarios where developers want to dynamically apply visual effects
    ///     to images without manually managing CSS or inline styles. By using this method, developers can focus on the
    ///     content and structure of their web pages while leaving the styling to the framework.
    ///     The method operates within the PageBuilder rendering model, which is designed to facilitate the construction
    ///     and rendering of HTML content in a structured and efficient manner. It integrates seamlessly with other components
    ///     of the PageBuilder framework, such as HtmlTagBuilder and CustomClassesExtensions, to provide a comprehensive
    ///     solution for web page development.
    ///     The lifecycle implications of using this method include the registration and management of stylesheets through
    ///     PageInformation, ensuring that the required CSS is loaded at the appropriate time. This method also contributes
    ///     to the rendering pipeline by marking the image as an effect and wrapping it in a media component with the
    ///     appropriate CSS class.
    ///     Developers should be aware that this method is intended for use with ImageRenderBuilder instances and requires
    ///     a valid HttpContext to function correctly. It is recommended to use this method in conjunction with other
    ///     PageBuilder
    ///     components to achieve a cohesive and visually appealing web page design.
    ///     For more information on the PageBuilder framework, refer to the <see cref="PageInformation" /> and
    ///     <see cref="HtmlTagBuilder{TBuilder}" /> documentation.
    ///     Example usage:
    ///     var imageBuilder = new ImageRenderBuilder(writer, htmlHelper, "path/to/image.jpg", "alt text");
    ///     imageBuilder.GrayscaleToColorEffect(amount: 0.8, hoverAmount: 0.1);
    /// </remarks>
    [Documented]
    public static class GrayscaleToColorEffect_ImageExtension
    {
        #region Static methods

        /// <summary>Renders the image in grayscale by default and animates it to full color when the user hovers.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder" /> to apply the effect to.</param>
        /// <param name="amount">
        ///     The grayscale intensity in the resting (non-hovered) state (0.0 = full color, 1.0 = fully
        ///     desaturated). Default is 1.0.
        /// </param>
        /// <param name="hoverAmount">The grayscale intensity when the image is hovered (0.0 = full color). Default is 0.0.</param>
        /// <returns>The modified <see cref="ImageRenderBuilder" /> instance with the effect applied.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> Gallery grids and card listings where most images should read as a neutral backdrop until
        ///         the user actively explores one — the color reveal is a clear affordance that the image is interactive. Also
        ///         effective for "before/after" demonstrations or historical photo sections.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> Sets CSS variables <c>--eb-image-effect-grayscale</c> (resting state) and
        ///         <c>--eb-image-effect-hover-grayscale</c> (hover state), then adds the class
        ///         <c>eb-image-effect-grayscale-to-color</c> to the media wrapper via <c>GrayscaleToColorEffect.css</c>. The CSS
        ///         applies a smooth <c>filter: grayscale()</c> transition between both variables.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> Stacks well with <c>LiftHoverEffect</c> — the color reveal and the physical lift
        ///         reinforce each other as a compound hover cue. Do not combine with <c>GrayscaleHoverEffect</c> or
        ///         <c>RainbowEffect</c> since all three drive the same <c>filter</c> property.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> Keep <paramref name="hoverAmount" /> at 0.0 for maximum contrast. If you want a partial reveal
        ///         effect (color but still slightly muted), try <paramref name="hoverAmount" /> = 0.2–0.3. Setting both parameters
        ///         to the same value produces no visible transition and should be avoided.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @Html.ImageRender("/images/photo.jpg").SetWidth(100, UnitSize.percent).SetHeight(180, UnitSize.px)
        ///     .InMediaComponent(m => m.SetStyle("object-fit", "cover"))
        ///     .GrayscaleToColorEffect(amount: 1.0, hoverAmount: 0.0)
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder GrayscaleToColorEffect(this ImageRenderBuilder builder, double amount = 1.0, double hoverAmount = 0.0)
        {
            builder.SetStyle("--eb-image-effect-grayscale", amount.ToString(CultureInfo.InvariantCulture));
            builder.SetStyle("--eb-image-effect-hover-grayscale", hoverAmount.ToString(CultureInfo.InvariantCulture));

            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/GrayscaleToColorEffect.css");
            return builder.MarkAsEffect().InMediaComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-grayscale-to-color"));
        }

        #endregion
    }
}