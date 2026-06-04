#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Provides the entry-point extension method to create a <see cref="FilmstripRevealEffectBuilder" />
    ///     from an <see cref="IHtmlHelper" />.
    /// </summary>
    [Documented]
    public static class FilmstripRevealEffect_InnerExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates a <see cref="FilmstripRevealEffectBuilder" /> — a full-viewport cinematic hero
        ///     that opens with a filmstrip loading animation: all images scroll across the screen as
        ///     a strip, the center image scales to fullscreen, then the hero reveals the title
        ///     (word-by-word) and a parallax thumbnail slideshow.
        /// </summary>
        /// <param name="html">The current Razor <see cref="IHtmlHelper" /> instance.</param>
        /// <returns>A new <see cref="FilmstripRevealEffectBuilder" /> ready for configuration.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> portfolio landing pages, product launch heroes, brand reveal sections.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> on page load the section is hidden; when fonts are ready a GSAP
        ///         timeline scrolls all images as a strip, scales the center one to fullscreen, then reveals
        ///         the title word-by-word and the thumbnail navigation.
        ///         Clicking a thumbnail cross-fades between slides with a parallax wipe.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @(Html.FilmstripRevealBuilder()
        ///     .SetTitle("Nous aimons les pixels")
        ///     .SetBackgroundColor("#1a1a1a")
        ///     .SetForegroundColor("#f4f4f4")
        ///     .AddSlide("/img/hero1.jpg", "Produit 1")
        ///     .AddSlide("/img/hero2.jpg", "Produit 2")
        ///     .AddSlide("/img/hero3.jpg", "Produit 3"))
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public static FilmstripRevealEffectBuilder FilmstripRevealBuilder(this IHtmlHelper html)
            => new(html);

        #endregion
    }
}