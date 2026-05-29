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
    ///     Provides the entry-point extension method to create a <see cref="FeatureTabEffectBuilder" />
    ///     from an <see cref="IHtmlHelper" />.
    /// </summary>
    [Documented]
    public static class FeatureTabEffect_InnerExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates a <see cref="FeatureTabEffectBuilder" /> — a feature-tab layout with clickable titles
        ///     on the left and a cross-fading visual panel on the right.
        /// </summary>
        /// <param name="html">The current Razor <see cref="IHtmlHelper" /> instance.</param>
        /// <returns>A new <see cref="FeatureTabEffectBuilder" /> ready for configuration.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> product feature showcases, tool capability listings, and any context where
        ///         each feature maps to a screenshot or illustration — IDE feature pages, SaaS landing pages,
        ///         onboarding tours.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> renders a two-column CSS grid. The left column stacks clickable items;
        ///         the active item gains a highlighted background and its description expands. The right column
        ///         cross-fades between visuals via <c>opacity</c> transitions on click.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @(Html.FeatureTabBuilder()
        ///     .SetPanelHeight(460)
        ///     .AddItem(
        ///         @&lt;img src="/images/refactor.png" class="img-fluid rounded-3" alt="Refactoring" /&gt;,
        ///         "Project-wide refactorings",
        ///         "Rename, move, extract — across your whole project with a single shortcut.")
        ///     .AddItem(
        ///         @&lt;img src="/images/debugger.png" class="img-fluid rounded-3" alt="Debugger" /&gt;,
        ///         "Integrated debugger",
        ///         "Step through code, inspect variables, and evaluate expressions in real time."))
        /// </code>
        ///     </para>
        ///     <para>
        ///         <b>Performance:</b> pure CSS transitions driven by a single click listener — no animation loop.
        ///     </para>
        /// </remarks>
        /// <seealso cref="FeatureTabEffectBuilder" />
        [Documented]
        public static FeatureTabEffectBuilder FeatureTabBuilder(this IHtmlHelper html)
            => new FeatureTabEffectBuilder(html);

        #endregion
    }
}