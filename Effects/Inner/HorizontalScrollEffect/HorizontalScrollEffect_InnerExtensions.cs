#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj HorizontalScrollEffect_InnerExtensions.cs create at 2026/05/13
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides the entry-point extension method to create a <see cref="HorizontalScrollEffectBuilder"/>
    /// from an <see cref="IHtmlHelper"/>.
    /// </summary>
    [Documented]
    public static class HorizontalScrollEffect_InnerExtensions
    {
        /// <summary>
        /// Creates a <see cref="HorizontalScrollEffectBuilder"/> — a section where vertical scrolling
        /// drives a horizontal journey through full-width slides.
        /// </summary>
        /// <param name="html">The current Razor <see cref="IHtmlHelper"/> instance.</param>
        /// <returns>A new <see cref="HorizontalScrollEffectBuilder"/> ready for configuration.</returns>
        /// <remarks>
        /// <para>
        /// <b>Use cases:</b> product feature tours, portfolio galleries, step-by-step journeys and
        /// any sequence where each item deserves full visual attention before the next one.
        /// </para>
        /// <para>
        /// <b>How it works:</b> an outer div is made tall enough to accumulate the full horizontal
        /// scroll distance. Inside it a sticky panel locks in the viewport. A passive scroll listener
        /// maps vertical progress to a <c>translateX</c> on the inner track.
        /// </para>
        /// <para>
        /// <b>Example:</b>
        /// <code>
        /// @(Html.HorizontalScrollBuilder()
        ///     .SetHeight(500)
        ///     .SetStickyTop(80)
        ///     .AddSlide("bi-geo-alt", "Tokyo",    "Lost in the neon maze.",        "#f72585", "#7209b7")
        ///     .AddSlide("bi-sun",     "Sahara",   "Dunes beyond the horizon.",     "#fb8500", "#ffb703")
        ///     .AddSlide("bi-water",   "Maldives", "Crystal lagoons.",              "#0077b6", "#00b4d8"))
        /// </code>
        /// </para>
        /// </remarks>
        /// <seealso cref="HorizontalScrollEffectBuilder"/>
        [Documented]
        public static HorizontalScrollEffectBuilder HorizontalScrollBuilder(this IHtmlHelper html)
            => new HorizontalScrollEffectBuilder(html);
    }
}
