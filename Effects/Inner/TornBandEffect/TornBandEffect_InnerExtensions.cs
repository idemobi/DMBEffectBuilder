#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj TornBandEffect_InnerExtensions.cs create at 2026/04/16
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides the entry-point extension method to create a <see cref="TornBandEffectBuilder"/>
    /// from an <see cref="IHtmlHelper"/>.
    /// </summary>
    [Documented]
    public static class TornBandEffect_InnerExtensions
    {
        /// <summary>
        /// Creates a <see cref="TornBandEffectBuilder"/> — an animated gradient band with a procedurally
        /// torn SVG edge that wraps a content block. The bottom band is rendered on
        /// <see cref="TornBandEffectBuilder.Begin"/>; the top band is rendered when the <c>@using</c>
        /// scope exits and calls <see cref="IDisposable.Dispose"/>.
        /// </summary>
        /// <param name="html">The current Razor <see cref="IHtmlHelper"/> instance.</param>
        /// <returns>A new <see cref="TornBandEffectBuilder"/> ready for configuration.</returns>
        /// <remarks>
        /// <para>
        /// <b>Use cases:</b> chapter separators, editorial section breaks, hero dividers,
        /// landing-page CTA sections, thematic zone transitions, game-over or reward screens.
        /// </para>
        /// <para>
        /// <b>How it works:</b> an SVG polygon with randomised vertices produces the torn-paper silhouette.
        /// The band is filled with an animated CSS gradient that cycles between two colours.
        /// The tilt angle shifts the band diagonally; the direction controls whether the slope goes
        /// left-to-right or right-to-left.
        /// </para>
        /// <para>
        /// <b>Combinations:</b> nest any Razor content inside the <c>@using</c> block — the torn bands
        /// frame it top and bottom. Works well with <see cref="VideoSectionEffect_InnerExtensions"/> or
        /// Bootstrap grid sections placed between the bands.
        /// </para>
        /// <para>
        /// <b>Tips:</b> use <see cref="TornBandEffectBuilder.SetTilt"/> to pin the diagonal angle
        /// instead of randomising it on each page load — useful for consistent design.
        /// Increase <see cref="TornBandEffectBuilder.SetTornPoints"/> (default 30) for a rougher,
        /// more jagged look. Keep band height between 80 px and 160 px for best visual balance.
        /// </para>
        /// <para>
        /// <b>Example:</b>
        /// <code>
        /// @using (Html.TornBandBuilder()
        ///     .SetGradient("#FFD700", "#FF8C00", 90m, 5m)
        ///     .SetBandHeight(100)
        ///     .Begin())
        /// {
        ///     &lt;div class="py-4 text-center"&gt;
        ///         &lt;h2&gt;Chapter II — The Reckoning&lt;/h2&gt;
        ///     &lt;/div&gt;
        /// }
        /// </code>
        /// </para>
        /// </remarks>
        /// <seealso cref="TornBandEffectBuilder"/>
        [Documented]
        public static TornBandEffectBuilder TornBandBuilder(this IHtmlHelper html)
        {
            return new TornBandEffectBuilder(html.ViewContext.Writer, html);
        }
    }
}
