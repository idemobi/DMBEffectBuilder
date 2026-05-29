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
    ///     Provides the entry-point extension method to create a <see cref="MosaicGridEffectBuilder" />
    ///     from an <see cref="IHtmlHelper" />.
    /// </summary>
    [Documented]
    public static class MosaicGridEffect_InnerExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates a <see cref="MosaicGridEffectBuilder" /> — a 3×3 image grid where hovering a cell
        ///     expands its column and row using a GSAP Flip layout transition.
        /// </summary>
        /// <param name="html">The current Razor <see cref="IHtmlHelper" /> instance.</param>
        /// <returns>A new <see cref="MosaicGridEffectBuilder" /> ready for configuration.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> portfolio galleries, team photo grids, product showcases,
        ///         destination or location collections.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> renders a CSS grid with six layout-class variants
        ///         (<c>rows-3-1-1</c>, <c>rows-1-3-1</c>, <c>rows-1-1-3</c> and their column equivalents).
        ///         On cell hover the matching row and column classes are swapped and GSAP Flip animates
        ///         from the previous layout to the new one. On mouse leave the grid resets to the center
        ///         cell expanded. Exactly 9 items are required.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @(Html.MosaicGridBuilder()
        /// .AddItem("/images/test/forest1.png", "Forêt — l'aube")
        /// .AddItem("/images/test/fight.png",   "Combat — le duel")
        /// .AddItem("/images/test/forest2.png", "Forêt — brume matinale")
        /// .AddItem("/images/test/forest3.png", "Forêt — sous les frondaisons")
        /// .AddItem("/images/test/terranova.png", "Terra Nova — monde ouvert")
        /// .AddItem("/images/test/forest4.png", "Forêt — lumière rasante")
        /// .AddItem("/images/test/forest5.png", "Forêt — sentier secret")
        /// .AddItem("/images/test/rest.png",    "Repos — après la bataille")
        /// .AddItem("/images/test/forest6.png", "Forêt — ombre et lumière"))
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public static MosaicGridEffectBuilder MosaicGridBuilder(this IHtmlHelper html)
            => new(html);

        #endregion
    }
}