#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj PanelCardEffect_InnerExtensions.cs create at 2026/05/07
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides the entry-point extension method to create a <see cref="PanelCardEffectBuilder"/>
    /// from an <see cref="IHtmlHelper"/>.
    /// </summary>
    [Documented]
    public static class PanelCardEffect_InnerExtensions
    {
        /// <summary>
        /// Creates a <see cref="PanelCardEffectBuilder"/> — a rounded gradient card with pill tab navigation
        /// that switches between informational content panels with a fade transition.
        /// Each panel carries its own HSL gradient background, auto-assigned from a built-in palette
        /// or overridden per panel.
        /// </summary>
        /// <param name="html">The current Razor <see cref="IHtmlHelper"/> instance.</param>
        /// <returns>A new <see cref="PanelCardEffectBuilder"/> ready for configuration.</returns>
        /// <remarks>
        /// <para>
        /// <b>Use cases:</b> feature showcases, step-by-step process cards, product highlights,
        /// onboarding flows, any scenario that benefits from compact tabbed navigation inside a
        /// visually prominent card.
        /// </para>
        /// <para>
        /// <b>How it works:</b> each <see cref="PanelCardEffectBuilder.AddPanel"/> call registers a
        /// content panel with its own gradient background derived from an HSL hue value. Tab buttons are
        /// overlaid at the top of the card with a frosted-glass style (<c>backdrop-filter: blur</c>).
        /// Clicking a tab fades in the matching panel via a CSS opacity transition; JavaScript handles
        /// the active-class toggling.
        /// </para>
        /// <para>
        /// <b>Palette:</b> when no <c>hue</c> is provided, panels are auto-assigned from a built-in
        /// sequence: purple (260°), pink (320°), blue (205°), green (145°) — matching the gradient
        /// palette used by <see cref="StackingCardsEffect_InnerExtensions"/> and
        /// <see cref="StickyScrollEffect_InnerExtensions"/>.
        /// </para>
        /// <para>
        /// <b>Tips:</b> use <c>eb-pc-badge</c>, <c>eb-pc-title</c> and <c>eb-pc-desc</c> utility classes
        /// for consistent white typography inside panels. Keep tab labels short (one or two words) so they
        /// fit on a single line at all viewport widths.
        /// </para>
        /// <para>
        /// <b>Example:</b>
        /// <code>
        /// @(Html.PanelCardBuilder()
        ///     .SetHeight(420)
        ///     .AddPanel("Discovery", @&lt;div class="d-flex flex-column align-items-center text-center gap-3"&gt;
        ///         &lt;i class="bi bi-search text-white" style="font-size:3rem;"&gt;&lt;/i&gt;
        ///         &lt;h2 class="eb-pc-title"&gt;01 — Discovery&lt;/h2&gt;
        ///         &lt;p class="eb-pc-desc"&gt;Understanding your goals before writing a single line of code.&lt;/p&gt;
        ///     &lt;/div&gt;)
        ///     .AddPanel("Design", @&lt;div&gt;...&lt;/div&gt;)
        ///     .AddPanel("Launch", @&lt;div&gt;...&lt;/div&gt;))
        /// </code>
        /// </para>
        /// </remarks>
        /// <seealso cref="PanelCardEffectBuilder"/>
        /// <seealso cref="FeatureTabEffect_InnerExtensions"/>
        /// <seealso cref="StackingCardsEffect_InnerExtensions"/>
        [Documented]
        public static PanelCardEffectBuilder PanelCardBuilder(this IHtmlHelper html)
            => new PanelCardEffectBuilder(html);
    }
}
