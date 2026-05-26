#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj TimelineEffect_InnerExtensions.cs create at 2026/05/06
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides the entry-point extension method to create a <see cref="TimelineEffectBuilder"/>
    /// from an <see cref="IHtmlHelper"/>.
    /// </summary>
    [Documented]
    public static class TimelineEffect_InnerExtensions
    {
        /// <summary>
        /// Creates a <see cref="TimelineEffectBuilder"/> — a horizontal timeline with numbered dots,
        /// a progress line, and a fade-in content panel for the active step.
        /// </summary>
        /// <param name="html">The current Razor <see cref="IHtmlHelper"/> instance.</param>
        /// <returns>A new <see cref="TimelineEffectBuilder"/> ready for configuration.</returns>
        /// <remarks>
        /// <para>
        /// <b>Use cases:</b> how-it-works sections, onboarding flows, process explanations, project
        /// roadmaps, and any sequential content where order matters.
        /// </para>
        /// <para>
        /// <b>Example:</b>
        /// <code>
        /// @(Html.TimelineBuilder()
        ///     .SetAutoPlay(3000)
        ///     .AddStep("Discovery", "We start by understanding your goals, constraints and audience.", "bi-search")
        ///     .AddStep("Design", "Wireframes and prototypes aligned with your brand.", "bi-pencil-square")
        ///     .AddStep("Development", "Clean, well-tested code delivered in regular sprints.", "bi-code-slash")
        ///     .AddStep("Launch", "Deployment, monitoring, and handover documentation.", "bi-rocket"))
        /// </code>
        /// </para>
        /// </remarks>
        /// <seealso cref="TimelineEffectBuilder"/>
        [Documented]
        public static TimelineEffectBuilder TimelineBuilder(this IHtmlHelper html)
            => new TimelineEffectBuilder(html);
    }
}
