#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj SectionInnerBuilderExtensions.cs create at 2026/04/13
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides the entry-point extension method to create a <see cref="DMBEffectBuilder.SectionInnerEffectBuilder"/>
    /// from an <see cref="IHtmlHelper"/>.
    /// </summary>
    [Documented]
    public static class SectionInnerEffect_InnerExtensions
    {
        #region Static methods

        /// <summary>
        /// Creates a <see cref="DMBEffectBuilder.SectionInnerEffectBuilder"/> — a fluent builder that renders
        /// a structured inner container with an optional header row, a descriptive subtitle and an overlay
        /// mode for content layered on top of a background image or colour.
        /// </summary>
        /// <param name="html">The current Razor <see cref="IHtmlHelper"/> instance.</param>
        /// <returns>A new <see cref="DMBEffectBuilder.SectionInnerEffectBuilder"/> ready for configuration.</returns>
        /// <remarks>
        /// <para>
        /// <b>Use cases:</b> standardising the inner layout of every section — consistent padding,
        /// a title/button header row, an optional descriptive line, and a scoped content area.
        /// Use the overlay variant when the section sits on top of a video, gradient or image background.
        /// </para>
        /// <para>
        /// <b>How it works:</b> the builder emits a wrapper <c>&lt;div&gt;</c> with configurable Bootstrap
        /// utility classes. When a header is set via <see cref="DMBEffectBuilder.SectionInnerEffectBuilder.SetHeader"/>,
        /// a flex row containing the title and an optional action button is prepended. In overlay mode the
        /// text colours are inverted for contrast against dark backgrounds.
        /// </para>
        /// <para>
        /// <b>Combinations:</b> place inside any <see cref="VideoSectionEffect_InnerExtensions"/> or
        /// Bootstrap section to get a consistent content shell. Combine with
        /// <see cref="TornBandEffect_InnerExtensions"/> when the section needs decorative edge bands.
        /// </para>
        /// <para>
        /// <b>Tips:</b> use the overlay variant (<c>SetOverlay()</c>) whenever the section background
        /// is dark or image-based so that titles and body text remain legible. The header row automatically
        /// aligns the title to the left and the action button to the right — no manual flex utilities needed.
        /// </para>
        /// <para>
        /// <b>Example:</b>
        /// <code>
        /// @using (Html.SectionInnerEffectBuilder()
        ///     .SetHeader("Our Features", actionButton)
        ///     .SetSubtitle("Everything you need to build faster.")
        ///     .Begin())
        /// {
        ///     &lt;p&gt;Section content goes here.&lt;/p&gt;
        /// }
        /// </code>
        /// </para>
        /// </remarks>
        /// <seealso cref="DMBEffectBuilder.SectionInnerEffectBuilder"/>
        [Documented]
        public static SectionInnerEffectBuilder SectionInnerEffectBuilder(this IHtmlHelper html)
        {
            return new SectionInnerEffectBuilder(html.ViewContext.Writer, html);
        }

        #endregion
    }
}
