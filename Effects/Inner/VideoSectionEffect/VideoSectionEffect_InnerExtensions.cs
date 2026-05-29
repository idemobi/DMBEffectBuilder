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
    ///     Provides the entry-point extension method to create a <see cref="VideoSectionEffectBuilder" />
    ///     from an <see cref="IHtmlHelper" />.
    /// </summary>
    [Documented]
    public static class VideoSectionEffect_InnerExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates a <see cref="VideoSectionEffectBuilder" /> — a full-width video background section
        ///     with an optional dark overlay and a centred title block rendered on top.
        /// </summary>
        /// <param name="html">The current Razor <see cref="IHtmlHelper" /> instance.</param>
        /// <param name="videoUrl">Relative or absolute URL of the MP4 video file used as the background.</param>
        /// <returns>A new <see cref="VideoSectionEffectBuilder" /> ready for configuration.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> cinematic hero sections, product launch pages, game trailers,
        ///         immersive landing banners, ambient background loops.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> a <c>&lt;video&gt;</c> element is positioned absolutely and covers the
        ///         container with <c>object-fit: cover</c>. The video is muted, auto-plays and loops silently.
        ///         <see cref="VideoSectionEffectBuilder.SetOverlay" /> adds a semi-transparent dark layer so that
        ///         text placed on top remains legible. <see cref="VideoSectionEffectBuilder.VideoSectionHeader" />
        ///         renders a centred title block above the overlay.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> pair with a <see cref="TornBandEffect_InnerExtensions" /> above or below
        ///         to create a torn-edge video band. Use Bootstrap utility classes on the title block for
        ///         custom typography and spacing.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> keep video files under 5 MB and encode at 1280 × 720 for fast loading.
        ///         Always set <see cref="VideoSectionEffectBuilder.SetOverlay" /> when placing light-coloured text
        ///         over bright or high-contrast footage. Use <c>loop + muted + autoplay</c> (the defaults) for
        ///         ambient background videos — browsers block auto-play for unmuted video.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @using (Html.VideoSectionBuilder("/videos/hero.mp4")
        ///     .SetOverlay(opacity: 0.55m)
        ///     .VideoSectionHeader("Welcome to Terra Nova", "An open world awaits.")
        ///     .Begin())
        /// {
        /// }
        /// </code>
        ///     </para>
        /// </remarks>
        /// <seealso cref="VideoSectionEffectBuilder" />
        [Documented]
        public static VideoSectionEffectBuilder VideoSectionBuilder(this IHtmlHelper html, string videoUrl)
        {
            return new VideoSectionEffectBuilder(html.ViewContext.Writer, html, videoUrl);
        }

        #endregion
    }
}