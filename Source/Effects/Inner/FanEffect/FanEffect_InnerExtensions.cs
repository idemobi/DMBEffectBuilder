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
    ///     Provides the entry-point extension method to create a <see cref="FanEffectBuilder" />
    ///     from an <see cref="IHtmlHelper" />.
    /// </summary>
    [Documented]
    public static class FanEffect_InnerExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates a <see cref="FanEffectBuilder" /> — a fan (éventail) layout where cards are spread,
        ///     rotated and stacked automatically. Hovering a card raises it to the foreground and dims the others.
        /// </summary>
        /// <param name="html">The current Razor <see cref="IHtmlHelper" /> instance.</param>
        /// <returns>A new <see cref="FanEffectBuilder" /> ready for configuration.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> character selection screens, card-game hands, tarot or oracle reveals,
        ///         pricing plan showcases, mission boards, leaderboard rosters, ability pickers.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> each card is absolutely positioned and rotated so that the card bottoms
        ///         converge at a shared anchor point. The spread width is controlled by the gap between
        ///         <c>topWidthPx</c> and <c>bottomWidthPx</c> — a larger difference produces a wider fan;
        ///         equal values produce a flat, upright row. Hover lifts the active card with a CSS transform
        ///         and reduces the opacity of siblings.
        ///     </para>
        ///     <para>
        ///         <b>Fan shapes:</b>
        ///         <c>bottomWidthPx: 0</c> creates a pure fan where all cards share a single bottom point.
        ///         <c>topWidthPx == bottomWidthPx</c> produces a flat parallel layout (leaderboard, gallery row).
        ///         Any value in between gives a tapered accordion.
        ///     </para>
        ///     <para>
        ///         <b>Entrances:</b> use <see cref="FanEffectBuilder.SetEntrance" /> to animate cards sliding in
        ///         from a chosen edge on load, with a configurable stagger delay between each card.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> keep card count between 3 and 7 for legible hover interaction.
        ///         For larger counts (e.g. a full 13-card suit) reduce <c>delayStepMs</c> to keep the entrance snappy.
        ///         Cards can hold images, text, badges or any Razor content.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @(Html.FanBuilder()
        ///     .SetCardSize(widthPx: 180, heightPx: 280)
        ///     .SetSpreadByWidth(topWidthPx: 600, bottomWidthPx: 0)
        ///     .SetContainerHeight(340)
        ///     .SetEntrance(FanEffectEntranceDirection.Left, delayStepMs: 80)
        ///     .AddCards(
        ///         FanEffectCard.FromImageAndContent("/img/mage.png", @&lt;div&gt;Kaïros&lt;/div&gt;, "Mage"),
        ///         FanEffectCard.FromImageAndContent("/img/warrior.png", @&lt;div&gt;Aldric&lt;/div&gt;, "Warrior"),
        ///         FanEffectCard.FromImageAndContent("/img/rogue.png", @&lt;div&gt;Sable&lt;/div&gt;, "Rogue")))
        /// </code>
        ///     </para>
        /// </remarks>
        /// <seealso cref="FanEffectBuilder" />
        [Documented]
        public static FanEffectBuilder FanBuilder(this IHtmlHelper html)
            => new FanEffectBuilder(html);

        #endregion
    }
}