#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj CylinderCarouselEffect_InnerExtensions.cs create at 2026/05/07
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides the entry-point extension method to create a <see cref="CylinderCarouselEffectBuilder"/>
    /// from an <see cref="IHtmlHelper"/>.
    /// </summary>
    [Documented]
    public static class CylinderCarouselEffect_InnerExtensions
    {
        /// <summary>
        /// Creates a <see cref="CylinderCarouselEffectBuilder"/> — a 3D cylinder carousel where cards
        /// are arranged on a rotating cylinder using CSS perspective and <c>rotateY</c> / <c>translateZ</c>
        /// transforms, with configurable shape, filter, and hover effects.
        /// </summary>
        /// <param name="html">The current Razor <see cref="IHtmlHelper"/> instance.</param>
        /// <returns>A new <see cref="CylinderCarouselEffectBuilder"/> ready for configuration.</returns>
        /// <remarks>
        /// <para>
        /// <b>Use cases:</b> portfolio galleries, product showcases, team photos, cover-flow
        /// navigation, and any image collection where visual depth adds impact.
        /// </para>
        /// <para>
        /// <b>Shape modes:</b>
        /// <see cref="CylinderShapeMode.Concave"/> (default) — cylinder opening faces the viewer,
        /// center card is largest;
        /// <see cref="CylinderShapeMode.Convex"/> — cylinder back faces the viewer, side cards are
        /// larger;
        /// <see cref="CylinderShapeMode.Flat"/> — flat horizontal marquee, no 3D depth.
        /// </para>
        /// <para>
        /// <b>Combinations:</b> pair <see cref="CylinderCarouselEffectBuilder.SetFilter"/> with
        /// <see cref="CylinderCarouselEffectBuilder.SetHoverClearFilter"/> for a colour-reveal-on-hover
        /// effect. Add <see cref="CylinderCarouselEffectBuilder.PauseOnHover"/> and
        /// <see cref="CylinderCarouselEffectBuilder.SetHoverScale"/> so users can inspect cards
        /// without them rotating away.
        /// </para>
        /// <para>
        /// <b>Performance:</b> the rotation is a pure CSS animation. Hover scale uses a CSS
        /// <c>@property</c>-animated custom property — zero JavaScript required.
        /// </para>
        /// <para>
        /// <b>Example:</b>
        /// <code>
        /// @(Html.CylinderCarouselBuilder()
        ///     .SetShape(CylinderShapeMode.Concave)
        ///     .SetFilter(CylinderFilterMode.Grayscale)
        ///     .SetHoverClearFilter()
        ///     .SetHoverScale()
        ///     .PauseOnHover()
        ///     .SetSpeed(24)
        ///     .AddCard(@&lt;img src="/img/photo1.jpg" alt="photo 1" /&gt;)
        ///     .AddCard(@&lt;img src="/img/photo2.jpg" alt="photo 2" /&gt;))
        /// </code>
        /// </para>
        /// </remarks>
        /// <seealso cref="CylinderCarouselEffectBuilder"/>
        [Documented]
        public static CylinderCarouselEffectBuilder CylinderCarouselBuilder(this IHtmlHelper html)
            => new CylinderCarouselEffectBuilder(html);
    }
}
