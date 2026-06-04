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
    ///     Provides the entry-point extension method to create a <see cref="CarouselEffectBuilder" />
    ///     from an <see cref="IHtmlHelper" />.
    /// </summary>
    [Documented]
    public static class CarouselEffect_InnerExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates a <see cref="CarouselEffectBuilder" /> — a carousel with slide or fade transitions,
        ///     optional arrow buttons, dot indicators, and auto-play.
        /// </summary>
        /// <param name="html">The current Razor <see cref="IHtmlHelper" /> instance.</param>
        /// <returns>A new <see cref="CarouselEffectBuilder" /> ready for configuration.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> hero image rotators, testimonial showcases, screenshot galleries,
        ///         feature highlights, and any sequential content presentation.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @(Html.CarouselBuilder()
        ///     .SetHeight(400)
        ///     .SetAutoPlay(4000)
        ///     .SetTransition(CarouselTransition.Slide)
        ///     .AddSlide(@&lt;div class="w-100 h-100 d-flex align-items-center justify-content-center rounded-3"
        ///                    style="background:linear-gradient(135deg,#667eea,#764ba2);"&gt;
        ///                   &lt;span class="text-white fw-bold fs-2"&gt;Slide 1&lt;/span&gt;
        ///               &lt;/div&gt;)
        ///     .AddSlide(@&lt;div class="w-100 h-100 d-flex align-items-center justify-content-center rounded-3"
        ///                    style="background:linear-gradient(135deg,#f093fb,#f5576c);"&gt;
        ///                   &lt;span class="text-white fw-bold fs-2"&gt;Slide 2&lt;/span&gt;
        ///               &lt;/div&gt;))
        /// </code>
        ///     </para>
        /// </remarks>
        /// <seealso cref="CarouselEffectBuilder" />
        [Documented]
        public static CarouselEffectBuilder CarouselBuilder(this IHtmlHelper html)
            => new CarouselEffectBuilder(html);

        #endregion
    }
}