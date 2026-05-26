#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj LockedRevealEffect_ImageExtension.cs create at 2026/04/21
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
    /// Provides an extension method to apply a locked reveal effect to an image using the PageBuilder framework.
    /// This class enhances the rendering capabilities of images by integrating a specific CSS effect, thereby
    /// contributing to the visual richness of web pages.
    /// </summary>
    /// <remarks>
    /// The LockedRevealEffect_ImageExtension class is designed to work within the PageBuilder rendering model,
    /// specifically targeting images. It leverages the ImageRenderBuilder to apply a CSS class that triggers
    /// the locked reveal effect, which is defined in an external stylesheet.
    /// The extension method <see cref="LockedRevealEffect"/> modifies the ImageRenderBuilder instance by:
    /// - Setting the appropriate CSS stylesheet for the locked reveal effect.
    /// - Marking the image as an effect, which may influence how it is processed in the rendering pipeline.
    /// - Wrapping the image in a component with a specific CSS class to enable the effect.
    /// This method is particularly useful for developers who wish to enhance image presentation on their web pages
    /// without directly manipulating HTML or CSS. By using this extension, developers can leverage the PageBuilder's
    /// abstraction layer to apply complex visual effects in a clean and maintainable manner.
    /// The interaction with <see cref="HtmlBuilderBase{TBuilder}"/> is implicit through the ImageRenderBuilder,
    /// which inherits from HtmlTagBuilder. This allows for seamless integration with other components and builders
    /// within the PageBuilder framework.
    /// The method contributes to <see cref="PageInformation"/> by setting a stylesheet, ensuring that the necessary
    /// CSS rules are included in the page's resources.
    /// Usage example:
    /// <code>
    /// var imageBuilder = new ImageRenderBuilder(writer, htmlHelper, "image.jpg", "alt text");
    /// imageBuilder.LockedRevealEffect(lockCharacter: "🔑", size: "3rem", color: "#ffd700");
    /// </code>
    /// This will apply the locked reveal effect to the image, enhancing its visual presentation on the page.
    /// </remarks>
    [Documented]
    public static class LockedRevealEffect_ImageExtension
    {
        /// <summary>Covers the image with a dark overlay showing a lock icon, then reveals the image with an unlock animation on hover.</summary>
        /// <param name="builder">The <see cref="ImageRenderBuilder"/> to apply the effect to.</param>
        /// <param name="lockCharacter">The character or emoji rendered as the lock icon in the center of the overlay. Default is <c>"🔒"</c>.</param>
        /// <param name="size">The font size of <paramref name="lockCharacter"/> in <paramref name="unit"/>. Default is 2.</param>
        /// <param name="unit">The unit of measurement for <paramref name="size"/>. Default is <see cref="UnitSize.rem"/>.</param>
        /// <param name="color">The color of the lock character. Defaults to a medium gray (<c>rgb(184,184,184)</c>).</param>
        /// <param name="overlayColor">The background color of the overlay that hides the image. Defaults to near-opaque black (<c>rgba(0,0,0,0.99)</c>).</param>
        /// <returns>The modified <see cref="ImageRenderBuilder"/> instance with the effect applied.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> Premium or members-only content previews, gated gallery images, spoiler images on game or media sites, and any image where you want to tease the content while clearly communicating that access is restricted.</para>
        /// <para><b>How it works:</b> Sets CSS variables <c>--eb-image-effect-lock</c>, <c>--eb-image-effect-size</c>, <c>--eb-image-effect-color</c>, and <c>--eb-image-effect-overlay-color</c>, then wraps the image in a wrapper component (not a media component) with the class <c>eb-image-effect-locked-reveal</c> via <c>LockedRevealEffect.css</c>. On hover, the overlay fades out and the lock icon animates to an unlocked state, revealing the image beneath.</para>
        /// <para><b>Combinations:</b> Works standalone — adding hover effects like <c>GlowHoverEffect</c> beneath this is pointless since the image is hidden at rest. Once you have the reveal pattern, consider pairing with <c>KenBurnsSlowEffect</c> so the revealed image immediately feels alive.</para>
        /// <para><b>Tips:</b> The default near-opaque black overlay guarantees the image is completely hidden; if you want a subtle tease of the image through a translucent overlay, reduce the alpha of <paramref name="overlayColor"/> to around 200–220. Choose a <paramref name="lockCharacter"/> that fits your content's tone — a key emoji (<c>"🗝"</c>) works well for vintage or treasure themes, while a simple Unicode lock (<c>"🔐"</c>) suits corporate UIs. Ensure the container has a fixed height so the overlay covers the full image area.</para>
        /// <para><b>Example:</b>
        /// <code>
        /// @Html.ImageRender("/images/photo.jpg").SetWidth(100, UnitSize.percent).SetHeight(200, UnitSize.px)
        ///     .InMediaComponent(m => m.SetStyle("object-fit", "cover"))
        ///     .LockedRevealEffect(lockCharacter: "🔑", size: 3, color: Color.Gold)
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public static ImageRenderBuilder LockedRevealEffect(
            this ImageRenderBuilder builder,
            string lockCharacter = "🔒",
            float size = 2,
            UnitSize unit = UnitSize.rem,
            Color? color = null,
            Color? overlayColor = null
        )
        {
            PageInformation page = PageRegistry.GetOrCreatePageInformation(builder.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/imageEffects/LockedRevealEffect.css");

            Color actualColor = color ?? Color.FromArgb(184, 184, 184);
            Color actualOverlayColor = overlayColor ?? Color.FromArgb(252, 0, 0, 0);

            builder.SetStyle("--eb-image-effect-lock", $"\"{lockCharacter}\"");
            builder.SetStyle("--eb-image-effect-size", $"{size}{unit.GetCss()}");
            builder.SetStyle("--eb-image-effect-color", actualColor.ToRgba());
            builder.SetStyle("--eb-image-effect-overlay-color", actualOverlayColor.ToRgba());

            return builder.MarkAsEffect().InWrapperComponent(wrapperBuilder => wrapperBuilder.AddClass("eb-image-effect-locked-reveal"));
        }
    }
}
