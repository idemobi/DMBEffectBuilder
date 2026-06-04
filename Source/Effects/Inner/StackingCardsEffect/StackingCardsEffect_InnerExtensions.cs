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
    ///     Provides the entry-point extension method to create a <see cref="StackingCardsEffectBuilder" />
    ///     from an <see cref="IHtmlHelper" />.
    /// </summary>
    [Documented]
    public static class StackingCardsEffect_InnerExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates a <see cref="StackingCardsEffectBuilder" /> — a pure-CSS stacking scroll effect where
        ///     each card sticks in place as the user scrolls, building a visible stack with previous cards
        ///     peeking above the current one.
        /// </summary>
        /// <param name="html">The current Razor <see cref="IHtmlHelper" /> instance.</param>
        /// <returns>A new <see cref="StackingCardsEffectBuilder" /> ready for configuration.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> feature showcases, step-by-step storytelling, pricing tiers, timeline
        ///         milestones, and any sequence where each item deserves full visual focus before the next arrives.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> each card uses <c>position: sticky</c> with an incrementing <c>top</c>
        ///         offset and an increasing <c>z-index</c>. As the user scrolls, later cards slide up and cover
        ///         earlier ones, which remain visible as thin coloured strips at the top of the viewport.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @(Html.StackingCardsBuilder()
        ///     .SetCardHeight(400)
        ///     .SetScrollGap(70)
        ///     .AddCard(@&lt;div class="w-100 h-100 d-flex align-items-center justify-content-center"
        ///                   style="background:linear-gradient(135deg,#667eea,#764ba2);"&gt;
        ///         &lt;span class="text-white fw-bold fs-2"&gt;Step 1&lt;/span&gt;
        ///     &lt;/div&gt;)
        ///     .AddCard(@&lt;div class="w-100 h-100 d-flex align-items-center justify-content-center"
        ///                   style="background:linear-gradient(135deg,#f093fb,#f5576c);"&gt;
        ///         &lt;span class="text-white fw-bold fs-2"&gt;Step 2&lt;/span&gt;
        ///     &lt;/div&gt;))
        /// </code>
        ///     </para>
        ///     <para>
        ///         <b>Performance:</b> pure CSS — no JavaScript, no IntersectionObserver, no scroll events.
        ///     </para>
        /// </remarks>
        /// <seealso cref="StackingCardsEffectBuilder" />
        [Documented]
        public static StackingCardsEffectBuilder StackingCardsBuilder(this IHtmlHelper html)
            => new StackingCardsEffectBuilder(html);

        #endregion
    }
}