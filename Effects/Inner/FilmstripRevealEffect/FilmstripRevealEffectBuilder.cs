#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj FilmstripRevealEffectBuilder.cs create at 2026/05/11
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBEffectBuilder
{
    /// <summary>
    /// Fluent builder that renders a full-viewport hero with a cinematic loading animation:
    /// images scroll as a strip, the center image scales to fullscreen, then the hero reveals
    /// with the title and a thumbnail slideshow navigation.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Use cases:</b> portfolio landing pages, product launch heroes, brand reveal sections,
    /// and any hero that benefits from a dramatic cinematic entrance.
    /// </para>
    /// <para>
    /// <b>How it works:</b> on scroll-in the section is hidden; when fonts are ready a GSAP timeline
    /// scrolls all images across the screen as a strip, scales the center one to fullscreen, then
    /// reveals the title word-by-word and the thumbnail navigation.
    /// Clicking a thumbnail cross-fades between slides with a parallax wipe.
    /// The animation replays automatically each time the user scrolls back into view.
    /// </para>
    /// <para>
    /// <b>Tips:</b> use an odd number of slides (3, 5, 7) so the center slide is a natural focal
    /// point. Match <see cref="SetBackgroundColor"/> to the dominant tone of your images for a
    /// seamless scale transition. Use <see cref="SetForegroundColor"/> for the title and thumbnail
    /// borders — light on dark backgrounds, dark on light ones.
    /// </para>
    /// <para>
    /// <b>Performance:</b> GSAP and SplitText are loaded on demand from CDN the first time the
    /// effect is encountered; subsequent instances on the same page reuse the cached scripts.
    /// </para>
    /// </remarks>
    [Documented]
    public sealed class FilmstripRevealEffectBuilder : IHtmlContent
    {
        private readonly IHtmlHelper              _html;
        private readonly List<FilmstripRevealSlide> _slides = [];
        private string  _title   = "Idémobi";
        private string  _bgColor = "#eaeaea";
        private string  _fgColor = "#1a1a1a";

        public FilmstripRevealEffectBuilder(IHtmlHelper html) => _html = html;

        /// <summary>Sets the hero title displayed after the loading animation. Default: <c>"Idémobi"</c>.</summary>
        /// <param name="title">Visible heading text, revealed word-by-word by GSAP SplitText.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public FilmstripRevealEffectBuilder SetTitle(string title)
        {
            _title = title;
            return this;
        }

        /// <summary>
        /// Sets the background color used for the filmstrip fade overlay and the section background.
        /// Default: <c>"#eaeaea"</c>.
        /// </summary>
        /// <param name="hex">Hex color string, e.g. <c>"#1a1a1a"</c>.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public FilmstripRevealEffectBuilder SetBackgroundColor(string hex)
        {
            _bgColor = hex;
            return this;
        }

        /// <summary>
        /// Sets the foreground color used for the title text and thumbnail borders.
        /// Default: <c>"#1a1a1a"</c>.
        /// </summary>
        /// <param name="hex">Hex color string, e.g. <c>"#f4f4f4"</c>.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public FilmstripRevealEffectBuilder SetForegroundColor(string hex)
        {
            _fgColor = hex;
            return this;
        }

        /// <summary>
        /// Adds an image slide. The center slide (index <c>Count / 2</c>) is the one that scales
        /// to fullscreen during the loading animation and is shown as the initial active slide.
        /// </summary>
        /// <param name="imageSrc">URL or path to the image.</param>
        /// <param name="alt">Alt text for accessibility. Default: empty string.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public FilmstripRevealEffectBuilder AddSlide(string imageSrc, string alt = "")
        {
            _slides.Add(new FilmstripRevealSlide(imageSrc, alt));
            return this;
        }

        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            PageInformation page = PageRegistry.GetOrCreatePageInformation(_html.ViewContext.HttpContext);
            page.SetStylesheet("/css/innerEffects/FilmstripRevealEffect.css");
            page.SetScriptFile("/js/innerEffects/FilmstripRevealEffect.js");

            if (_slides.Count == 0) return;

            int centerIdx = _slides.Count / 2;
            var style     = $"""style="--eb-fstrip-bg:{encoder.Encode(_bgColor)};--eb-fstrip-fg:{encoder.Encode(_fgColor)}" """;

            writer.Write("""<div class="eb-fstrip-wrapper">""");
            writer.Write($"""<section data-slideshow="wrap" class="eb-fstrip-header is--loading is--hidden" {style}>""");

            // ── Slider ────────────────────────────────────────────────────
            writer.Write("""<div class="eb-fstrip-header__slider">""");
            writer.Write("""<div class="eb-fstrip-header__slider-list">""");
            for (int i = 0; i < _slides.Count; i++)
            {
                var s       = _slides[i];
                var current = i == centerIdx ? " is--current" : "";
                var src     = encoder.Encode(s.Src);
                var alt     = encoder.Encode(s.Alt);
                writer.Write($"""<div data-slideshow="slide" class="eb-fstrip-header__slider-slide{current}">""");
                writer.Write($"""<img class="eb-fstrip-header__slider-slide-inner" src="{src}" alt="{alt}" data-slideshow="parallax" draggable="false">""");
                writer.Write("</div>");
            }
            writer.Write("</div>"); // slider-list
            writer.Write("</div>"); // slider

            // ── Loader ────────────────────────────────────────────────────
            writer.Write("""<div class="eb-fstrip-loader">""");
            writer.Write("""<div class="eb-fstrip-loader__wrap">""");
            writer.Write("""<div class="eb-fstrip-loader__groups">""");

            writer.Write("""<div class="eb-fstrip-loader__group is--duplicate">""");
            foreach (var s in _slides) WriteLoaderSingle(writer, encoder, s, false, false);
            writer.Write("</div>");

            writer.Write("""<div class="eb-fstrip-loader__group is--relative">""");
            for (int i = 0; i < _slides.Count; i++)
                WriteLoaderSingle(writer, encoder, _slides[i], i == centerIdx, i != centerIdx);
            writer.Write("</div>");

            writer.Write("</div>"); // groups
            writer.Write("""<div class="eb-fstrip-loader__fade"></div>""");
            writer.Write("""<div class="eb-fstrip-loader__fade is--duplicate"></div>""");
            writer.Write("</div>"); // wrap
            writer.Write("</div>"); // loader

            // ── Content ───────────────────────────────────────────────────
            writer.Write("""<div class="eb-fstrip-header__content">""");

            writer.Write("""<div class="eb-fstrip-header__center">""");
            writer.Write($"""<h1 class="eb-fstrip-header__h1">{encoder.Encode(_title)}</h1>""");
            writer.Write("</div>");

            writer.Write("""<div class="eb-fstrip-header__bottom">""");
            writer.Write("""<div class="eb-fstrip-header__slider-nav">""");
            for (int i = 0; i < _slides.Count; i++)
            {
                var s       = _slides[i];
                var current = i == centerIdx ? " is--current" : "";
                var src     = encoder.Encode(s.Src);
                var alt     = encoder.Encode(s.Alt);
                writer.Write($"""<div data-slideshow="thumb" class="eb-fstrip-header__slider-nav-btn{current}">""");
                writer.Write($"""<img loading="eager" src="{src}" alt="{alt}" class="eb-fstrip-loader__cover-img">""");
                writer.Write("</div>");
            }
            writer.Write("</div>"); // slider-nav
            writer.Write("</div>"); // bottom

            writer.Write("</div>"); // content
            writer.Write("</section>");
            writer.Write("</div>"); // wrapper
        }

        private static void WriteLoaderSingle(TextWriter writer, HtmlEncoder encoder,
            FilmstripRevealSlide slide, bool isScaling, bool isScaleDown)
        {
            var src        = encoder.Encode(slide.Src);
            var alt        = encoder.Encode(slide.Alt);
            var mediaClass = "eb-fstrip-loader__media" + (isScaling ? " is--scaling is--radius" : "");
            var imgClass   = "eb-fstrip-loader__cover-img" + (isScaleDown ? " is--scale-down" : "");
            writer.Write("""<div class="eb-fstrip-loader__single">""");
            writer.Write($"""<div class="{mediaClass}">""");
            writer.Write($"""<img loading="eager" src="{src}" alt="{alt}" class="{imgClass}">""");
            writer.Write("</div>");
            writer.Write("</div>");
        }
    }
}
