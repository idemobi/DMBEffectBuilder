#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj StickyScrollEffect_InnerExtensions.cs create at 2026/05/06
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides the entry-point extension method to create a <see cref="StickyScrollEffectBuilder"/>
    /// from an <see cref="IHtmlHelper"/>.
    /// </summary>
    [Documented]
    public static class StickyScrollEffect_InnerExtensions
    {
        /// <summary>
        /// Creates a <see cref="StickyScrollEffectBuilder"/> — a two-column sticky scroll layout where the
        /// left visual panel locks in place while numbered steps scroll on the right, sliding to the matching
        /// visual on each step change.
        /// </summary>
        /// <param name="html">The current Razor <see cref="IHtmlHelper"/> instance.</param>
        /// <returns>A new <see cref="StickyScrollEffectBuilder"/> ready for configuration.</returns>
        /// <remarks>
        /// <para>
        /// <b>Use cases:</b> product feature walkthroughs, onboarding sequences, step-by-step processes, and any
        /// context where each text step maps to a distinct visual — product tours, how-it-works sections,
        /// interactive documentation.
        /// </para>
        /// <para>
        /// <b>How it works:</b> renders a two-column CSS grid. The left column uses <c>position: sticky</c> and
        /// <c>overflow: hidden</c> to lock the visual panel in place; all visuals are stacked with
        /// <c>position: absolute</c> and moved off-screen via <c>transform: translateY</c>. An
        /// <c>IntersectionObserver</c> watches each right-side step and, when it enters the central zone of
        /// the viewport, slides the matching visual into place with a cubic-bezier transition.
        /// </para>
        /// <para>
        /// <b>Combinations:</b> the visual slot accepts any <see cref="IHtmlContent"/> — images, gradient divs,
        /// code blocks, or rendered helpers. Use the <c>@&lt;div&gt;...&lt;/div&gt;</c> Razor template syntax
        /// for inline markup. Wrap the builder in a <c>container-lg</c> div or a full-width
        /// <c>SectionBuilder</c> for a hero-style section.
        /// </para>
        /// <para>
        /// <b>Tips:</b> 3–6 steps give the best balance. Call <see cref="StickyScrollEffectBuilder.SetStickyOffset"/>
        /// to match your fixed navbar height; call <see cref="StickyScrollEffectBuilder.SetItemMinHeight"/> to
        /// ensure each step has enough scroll space to trigger cleanly.
        /// </para>
        /// <para>
        /// <b>Example:</b>
        /// <code>
        /// @(Html.StickyScrollBuilder()
        ///     .SetVisualHeight(420)
        ///     .SetStickyOffset(80)
        ///     .AddStep(
        ///         @&lt;div class="w-100 h-100 d-flex align-items-center justify-content-center rounded-3"
        ///               style="background:linear-gradient(135deg,#667eea,#764ba2);"&gt;
        ///             &lt;i class="bi bi-lightning-charge text-white" style="font-size:4rem;"&gt;&lt;/i&gt;
        ///         &lt;/div&gt;,
        ///         "Intelligent autocompletion",
        ///         "Understands the full context of your project to suggest completions that fit.",
        ///         "bi-lightning-charge")
        ///     .AddStep(
        ///         @&lt;img src="/images/refactoring.png" class="img-fluid rounded-3" alt="Refactoring" /&gt;,
        ///         "Smart refactoring",
        ///         "Renames and restructures across every file that references the symbol.",
        ///         "bi-arrow-left-right"))
        /// </code>
        /// </para>
        /// <para>
        /// <b>Performance:</b> powered by a single <c>IntersectionObserver</c> — no scroll-event polling.
        /// Scales linearly with step count; keep heavy visuals (videos, canvases) to a minimum.
        /// </para>
        /// </remarks>
        /// <seealso cref="StickyScrollEffectBuilder"/>
        [Documented]
        public static StickyScrollEffectBuilder StickyScrollBuilder(this IHtmlHelper html)
            => new StickyScrollEffectBuilder(html);
    }
}
