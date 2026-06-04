#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using System.Linq;
using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Provides extension methods to apply a lava lamp blob animation to a <see cref="SectionBuilder" />.
    /// </summary>
    public static class LavaLampEffect_SectionExtensions
    {
        #region Static methods

        #if DEBUG
        private static void InjectDebugPanel(SectionBuilder section, string backgroundColor, int count, int blur)
        {
            string sectionId = section.AttributeValue("id") ?? throw new InvalidOperationException("Section id could not be resolved.");
            section.AddDebugPanel(new LavaLampEffectDebugModel
            {
                SectionId = sectionId,
                BackgroundColor = backgroundColor,
                Count = count,
                Blur = blur
            });
        }
        #endif

        /// <summary>
        ///     Applies an animated lava lamp blob effect to the section, rendering floating colored blobs on a dark background.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="backgroundColor">The section background color in CSS format. Defaults to <c>#0d0d0d</c>.</param>
        /// <param name="colors">Array of CSS hex colors for the blobs. Defaults to pink, cyan, yellow.</param>
        /// <param name="count">Number of animated blobs. Must be greater than 0. Defaults to <c>5</c>.</param>
        /// <param name="blur">Blur radius applied to the blobs in pixels. Must be 0 or greater. Defaults to <c>30</c>.</param>
        /// <returns>The same <see cref="SectionBuilder" /> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="section" /> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="backgroundColor" /> is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown when <paramref name="count" /> is less than or equal to 0, or
        ///     <paramref name="blur" /> is negative.
        /// </exception>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> bold hero banners, music platforms, art-focused or entertainment pages.
        ///         Creates a psychedelic, organic background that demands attention.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> renders blurred colored blobs that float and morph using CSS keyframe
        ///         animations. A high-contrast CSS <c>filter: contrast()</c> is applied to the container,
        ///         making the blobs fuse and separate organically when they overlap — the classic lava lamp effect.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> use standalone — the effect already manages both the background color
        ///         and the blob layer. Layering other background effects on top will break the contrast filter.
        ///         <c>ScanlineEffect</c> can be added for a retro CRT twist.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> keep the <c>backgroundColor</c> very dark (near black) — the contrast filter
        ///         requires a dark background to produce clean blob edges. Use 3–6 vivid, saturated colors
        ///         for the blobs. A <c>blur</c> of 25–40 px gives smooth blobs; higher values create
        ///         a more diffuse cloud effect.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.SectionBuilder()
        ///     .LavaLampEffect(backgroundColor: "#0d0d0d",
        ///                     colors: new[] { "#ff6ecf", "#00f5d4", "#efff5c" },
        ///                     count: 5, blur: 30)
        /// </code>
        ///     </para>
        ///     <para>
        ///         <b>Performance:</b> each blob is a CSS-animated DOM element subject to a global
        ///         <c>filter: contrast()</c> on the container, which triggers GPU compositing.
        ///         Keep <c>count</c> at or below 6 on pages targeting low-end mobile devices.
        ///     </para>
        /// </remarks>
        /// <seealso cref="BubbleEffect_SectionExtensions" />
        [Documented]
        public static SectionBuilder LavaLampEffect(
            this SectionBuilder section,
            string backgroundColor = "#0d0d0d",
            string[]? colors = null,
            int count = 5,
            int blur = 30
        )
        {
            if (section == null) throw new ArgumentNullException(nameof(section));
            if (string.IsNullOrWhiteSpace(backgroundColor)) throw new ArgumentException("Background color cannot be null or empty.", nameof(backgroundColor));
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count), "Count must be greater than 0.");
            if (blur < 0) throw new ArgumentOutOfRangeException(nameof(blur), "Blur must be 0 or greater.");

            colors ??= new[] { "#ff6ecf", "#00f5d4", "#efff5c" };

            var encodedColors = string.Join(",", colors.Select(c => HtmlEncoder.Default.Encode(c)));

            section.EnsureId("lavalamp");
            section.AddClass("eb-section-effect-lavalamp");
            section.SetStyle("background-color", backgroundColor);
            section.SetStyle("--eb-lavalamp-bg", backgroundColor);
            section.SetAttribute("data-lavalamp-count", count.ToString());
            section.SetAttribute("data-lavalamp-colors", encodedColors);
            section.SetAttribute("data-lavalamp-blur", blur.ToString());

            PageInformation page = PageRegistry.GetOrCreatePageInformation(section.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/sectionEffects/LavaLampEffect.css");
            page.SetScriptFile("/js/sectionEffects/LavaLampEffect.js");

            #if DEBUG
            InjectDebugPanel(section, backgroundColor, count, blur);
            #endif

            return section;
        }

        #endregion
    }
}