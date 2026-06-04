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
    ///     Provides the entry-point extension method to create a <see cref="RetroTvEffectBuilder" />
    ///     from an <see cref="IHtmlHelper" />.
    /// </summary>
    [Documented]
    public static class RetroTvEffect_InnerExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates a <see cref="RetroTvEffectBuilder" /> — a retro CRT television widget that cycles
        ///     through configurable channels, each displaying either a solid color palette or a full-screen image,
        ///     with authentic scanlines, phosphor glow and a channel-switch animation.
        /// </summary>
        /// <param name="html">The current Razor <see cref="IHtmlHelper" /> instance.</param>
        /// <returns>A new <see cref="RetroTvEffectBuilder" /> ready for configuration.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> color palette showcases, portfolio chapter reveals, mood boards,
        ///         game intro screens, brand identity sections.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> renders a canvas-based CRT screen inside a 3D wooden cabinet.
        ///         Color channels display the hex value and animated RGB bars; image channels fill the
        ///         screen with the image and overlay the name and description. All channels share the
        ///         same scanlines, phosphor-glow and static-burst switch animation.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @(Html.RetroTvBuilder()
        ///     .SetBrand("Idémobi")
        ///     .SetAutoAdvance(6000)
        ///     .AddColorChannel("Storm", "#2e3d4f", "cumulonimbus at dusk")
        ///     .AddColorChannel("Clay",  "#9c7c5e", "exposed earth")
        ///     .AddImageChannel("/img/hero.jpg", "Chapter I", "The beginning"))
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public static RetroTvEffectBuilder RetroTvBuilder(this IHtmlHelper html)
            => new(html);

        #endregion
    }
}