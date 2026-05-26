#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj StagedRevealEffect_InnerExtensions.cs create at 2026/04/27
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides the entry-point extension method to create a <see cref="StagedRevealEffectBuilder"/>
    /// from an <see cref="IHtmlHelper"/>.
    /// </summary>
    [Documented]
    public static class StagedRevealEffect_InnerExtensions
    {
        /// <summary>
        /// Creates a <see cref="StagedRevealEffectBuilder"/> — a CSS-grid reveal effect where one to four
        /// image/content slots slide in from the edges of the container, framing a configurable center zone.
        /// </summary>
        /// <param name="html">The current Razor <see cref="IHtmlHelper"/> instance.</param>
        /// <param name="stopPercent">
        /// How far each slot penetrates into the container from its own edge, in percent (0–100).
        /// <c>50</c> = slots meet exactly at the center. Values above <c>50</c> cause overlap;
        /// values below leave a visible gap around the center zone.
        /// </param>
        /// <returns>A new <see cref="StagedRevealEffectBuilder"/> ready for configuration.</returns>
        /// <remarks>
        /// <para>
        /// <b>Use cases:</b> VS screens, fighting-game character reveals, cinematic title cards,
        /// open-world environment intros, map/zone selection screens, before/after comparisons,
        /// auto-cycling news or portfolio feeds.
        /// </para>
        /// <para>
        /// <b>How it works:</b> slots are placed in a CSS grid around a center cell.
        /// On load each slot translates from its edge toward the center and stops at
        /// <paramref name="stopPercent"/>. When multiple items are registered in a slot they cycle
        /// automatically at the interval set by <see cref="StagedRevealEffectBuilder.SetSpeed"/>.
        /// </para>
        /// <para>
        /// <b>Slot shapes:</b> use <see cref="StagedRevealEffectBuilder.SetArcEdge"/> to cut a concave
        /// arc into the inner edge of left/right slots, or
        /// <see cref="StagedRevealEffectBuilder.SetDiagonalCut"/> to slant those edges independently
        /// (Z-shape, V-shape, parallel cut).
        /// </para>
        /// <para>
        /// <b>Tips:</b> set <c>holdMs: 99999</c> to freeze the animation after the first reveal.
        /// Use <c>stopPercent</c> between 35 and 45 to leave a comfortable center zone for text.
        /// Left and right slots are the most common; top and bottom add a cinematic widescreen feel.
        /// </para>
        /// <para>
        /// <b>Example:</b>
        /// <code>
        /// @(Html.StagedRevealBuilder(stopPercent: 42m)
        ///     .SetSpeed(transitionMs: 600, holdMs: 99999)
        ///     .AddSlot(StagedRevealEffectDirection.Left,
        ///         StagedRevealEffectItem.FromImage("/img/hero-left.jpg", "Hero left"))
        ///     .SetCenterContent(@&lt;div class="text-center text-white"&gt;
        ///         &lt;h1&gt;VS&lt;/h1&gt;
        ///     &lt;/div&gt;)
        ///     .AddSlot(StagedRevealEffectDirection.Right,
        ///         StagedRevealEffectItem.FromImage("/img/hero-right.jpg", "Hero right")))
        /// </code>
        /// </para>
        /// </remarks>
        /// <seealso cref="StagedRevealEffectBuilder"/>
        [Documented]
        public static StagedRevealEffectBuilder StagedRevealBuilder(this IHtmlHelper html, decimal stopPercent = 50m)
            => new StagedRevealEffectBuilder(html, stopPercent);
    }
}
