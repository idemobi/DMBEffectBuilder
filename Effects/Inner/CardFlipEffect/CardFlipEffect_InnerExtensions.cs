#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj CardFlipEffect_InnerExtensions.cs create at 2026/05/06
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides the entry-point extension method to create a <see cref="CardFlipEffectBuilder"/>
    /// from an <see cref="IHtmlHelper"/>.
    /// </summary>
    [Documented]
    public static class CardFlipEffect_InnerExtensions
    {
        /// <summary>
        /// Creates a <see cref="CardFlipEffectBuilder"/> — a grid of cards that flip in 3D to reveal
        /// a back face, triggered on hover or click.
        /// </summary>
        /// <param name="html">The current Razor <see cref="IHtmlHelper"/> instance.</param>
        /// <returns>A new <see cref="CardFlipEffectBuilder"/> ready for configuration.</returns>
        /// <remarks>
        /// <para>
        /// <b>Use cases:</b> team member cards (photo front / bio back), feature cards (icon front /
        /// description back), FAQ cards (question front / answer back).
        /// </para>
        /// <para>
        /// <b>Example:</b>
        /// <code>
        /// @(Html.CardFlipBuilder()
        ///     .SetTrigger(CardFlipTrigger.Hover)
        ///     .SetCardHeight(260)
        ///     .SetColumns(3)
        ///     .AddCard(
        ///         @&lt;div class="w-100 h-100 d-flex flex-column align-items-center justify-content-center
        ///                       rounded-3 bg-primary text-white"&gt;
        ///             &lt;i class="bi bi-code-slash mb-2" style="font-size:2.5rem;"&gt;&lt;/i&gt;
        ///             &lt;span class="fw-bold fs-5"&gt;Development&lt;/span&gt;
        ///         &lt;/div&gt;,
        ///         @&lt;div class="w-100 h-100 d-flex flex-column align-items-center justify-content-center
        ///                       rounded-3 bg-dark text-white p-4 text-center"&gt;
        ///             &lt;p&gt;Clean, well-tested code delivered in regular sprints.&lt;/p&gt;
        ///         &lt;/div&gt;))
        /// </code>
        /// </para>
        /// </remarks>
        /// <seealso cref="CardFlipEffectBuilder"/>
        [Documented]
        public static CardFlipEffectBuilder CardFlipBuilder(this IHtmlHelper html)
            => new CardFlipEffectBuilder(html);
    }
}
