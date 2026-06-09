#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.Globalization;
using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Fluent builder for a carousel: slides cycle automatically or on user interaction, with
    ///     optional arrow buttons and dot indicators.
    ///     Add slides with <see cref="AddSlide(Func{TResult})" />, enable auto-play with
    ///     <see cref="SetAutoPlay" />, and choose the animation with <see cref="SetTransition" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <b>Use cases:</b> hero image rotators, testimonial showcases, screenshot galleries,
    ///         feature highlights, and any content that benefits from sequential presentation.
    ///     </para>
    ///     <para>
    ///         <b>How it works:</b> in <see cref="CarouselTransition.Slide" /> mode the track translates
    ///         horizontally via <c>transform: translateX</c>. In <see cref="CarouselTransition.Fade" /> mode
    ///         slides are stacked with <c>position: absolute</c> and cross-fade via <c>opacity</c>.
    ///         An <c>IntersectionObserver</c> pauses auto-play when the carousel leaves the viewport.
    ///         Touch swipe is supported on mobile.
    ///     </para>
    ///     <para>
    ///         <b>Tips:</b> use <see cref="SetHeight" /> to enforce a consistent panel height across slides.
    ///         In <see cref="CarouselTransition.Fade" /> mode all slides must have the same height since they
    ///         are stacked; in <see cref="CarouselTransition.Slide" /> mode content can overflow vertically
    ///         if <see cref="SetHeight" /> is not set.
    ///     </para>
    /// </remarks>
    [Documented]
    public sealed class CarouselEffectBuilder : IHtmlContent
    {
        #region Instance fields and properties

        private int _autoPlayMs = 0;
        private int _heightPx = 0;
        private readonly IHtmlHelper _html;
        private bool _showArrows = true;
        private bool _showDots = true;
        private readonly List<IHtmlContent> _slides = new();
        private CarouselTransition _transition = CarouselTransition.Slide;
        private decimal _transitionDuration = 0.5m;

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CarouselEffectBuilder" /> class.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to register effect assets.</param>
        public CarouselEffectBuilder(IHtmlHelper html)
        {
            _html = html;
        }

        #endregion

        #region Instance methods

        /// <summary>Adds a slide using a Razor template delegate — preferred in <c>.cshtml</c> views.</summary>
        /// <param name="template">Razor template delegate (<c>@&lt;div&gt;...&lt;/div&gt;</c>) for the slide content.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CarouselEffectBuilder AddSlide(Func<dynamic, IHtmlContent> template)
        {
            _slides.Add(template(null!));
            return this;
        }

        /// <summary>Adds a slide using pre-built <see cref="IHtmlContent" />.</summary>
        /// <param name="content">HTML content for the slide.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CarouselEffectBuilder AddSlide(IHtmlContent content)
        {
            _slides.Add(content);
            return this;
        }

        /// <summary>
        ///     Adds a standard gradient slide with a centred icon, title and subtitle.
        ///     Use this overload for the common icon-title-subtitle layout; use the template overload for fully custom content.
        /// </summary>
        /// <param name="icon">Bootstrap icon class, e.g. <c>"bi-lightning-charge"</c>.</param>
        /// <param name="title">Main heading displayed below the icon.</param>
        /// <param name="subtitle">Secondary text displayed below the title.</param>
        /// <param name="gradientStart">CSS color for the top-left of the gradient, e.g. <c>"#667eea"</c>.</param>
        /// <param name="gradientEnd">CSS color for the bottom-right of the gradient, e.g. <c>"#764ba2"</c>.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CarouselEffectBuilder AddSlide(
            string icon,
            string title,
            string subtitle,
            string gradientStart,
            string gradientEnd
        )
        {
            var enc = HtmlEncoder.Default;
            _slides.Add(new HtmlString(
                $"""
                 <div class="w-100 h-100 d-flex flex-column align-items-center justify-content-center text-center rounded-3"
                      style="background:linear-gradient(135deg,{enc.Encode(gradientStart)},{enc.Encode(gradientEnd)});">
                     <i class="bi {enc.Encode(icon)} text-white mb-3" style="font-size:3rem;"></i>
                     <h3 class="text-white mb-2">{enc.Encode(title)}</h3>
                     <h6 class="fw-normal text-white-50 mb-0">{enc.Encode(subtitle)}</h6>
                 </div>
                 """));
            return this;
        }

        /// <summary>Enables auto-advance: slides cycle on the given interval (default: 0 = disabled).</summary>
        /// <param name="ms">
        ///     Interval in milliseconds. Pass <c>0</c> to disable. Auto-play pauses when the carousel leaves the
        ///     viewport.
        /// </param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CarouselEffectBuilder SetAutoPlay(int ms)
        {
            _autoPlayMs = ms;
            return this;
        }

        /// <summary>
        ///     Sets the fixed height of the carousel in pixels. When 0 (default) the height adapts to the active slide
        ///     content.
        /// </summary>
        /// <param name="px">Height in pixels, or <c>0</c> for auto.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CarouselEffectBuilder SetHeight(int px)
        {
            _heightPx = px;
            return this;
        }

        /// <summary>Sets the transition animation between slides (default: <see cref="CarouselTransition.Slide" />).</summary>
        /// <param name="transition">Slide (horizontal translate) or Fade (cross-fade in place).</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CarouselEffectBuilder SetTransition(CarouselTransition transition)
        {
            _transition = transition;
            return this;
        }

        /// <summary>Sets the transition duration in seconds (default: 0.5).</summary>
        /// <param name="seconds">Animation duration in seconds.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CarouselEffectBuilder SetTransitionDuration(decimal seconds)
        {
            _transitionDuration = seconds;
            return this;
        }

        /// <summary>Shows or hides the previous/next arrow buttons (default: <c>true</c>).</summary>
        /// <param name="show"><c>true</c> to show arrows, <c>false</c> to hide them.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CarouselEffectBuilder ShowArrows(bool show = true)
        {
            _showArrows = show;
            return this;
        }

        /// <summary>Shows or hides the dot indicators below the carousel (default: <c>true</c>).</summary>
        /// <param name="show"><c>true</c> to show dots, <c>false</c> to hide them.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CarouselEffectBuilder ShowDots(bool show = true)
        {
            _showDots = show;
            return this;
        }

        #region From interface IHtmlContent

        /// <summary>
        ///     Writes the complete effect markup to the provided output writer.
        /// </summary>
        /// <param name="writer">The writer receiving generated HTML.</param>
        /// <param name="encoder">The encoder used to encode generated HTML.</param>
        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            if (_slides.Count == 0) return;

            PageInformation page = PageRegistry.GetOrCreatePageInformation(_html.ViewContext.HttpContext);
            page.SetStylesheet("/css/innerEffects/CarouselEffect.css");
            page.SetScriptFile("/js/innerEffects/CarouselEffect.js");

            var ci = CultureInfo.InvariantCulture;
            var heightStyle = _heightPx > 0 ? $"--eb-ca-height:{_heightPx}px;" : string.Empty;
            var style = $"{heightStyle}--eb-ca-duration:{_transitionDuration.ToString(ci)}s;--eb-ca-autoplay:{_autoPlayMs};";
            var transitionClass = _transition == CarouselTransition.Fade ? "fade-transition" : "slide-transition";

            writer.Write($"<div class=\"eb-carousel {transitionClass}\" style=\"{style}\">");

            writer.Write("<div class=\"eb-ca-viewport\">");
            writer.Write("<div class=\"eb-ca-track\">");
            for (int i = 0; i < _slides.Count; i++)
            {
                string activeClass = i == 0 ? " is-active" : string.Empty;
                writer.Write($"<div class=\"eb-ca-slide{activeClass}\" data-index=\"{i}\">");
                _slides[i].WriteTo(writer, encoder);
                writer.Write("</div>");
            }

            writer.Write("</div>");
            writer.Write("</div>");

            if (_showArrows)
            {
                writer.Write("<button class=\"eb-ca-arrow eb-ca-prev\" aria-label=\"Previous\"><i class=\"bi bi-chevron-left\"></i></button>");
                writer.Write("<button class=\"eb-ca-arrow eb-ca-next\" aria-label=\"Next\"><i class=\"bi bi-chevron-right\"></i></button>");
            }

            if (_showDots && _slides.Count > 1)
            {
                writer.Write("<div class=\"eb-ca-dots\">");
                for (int i = 0; i < _slides.Count; i++)
                {
                    string activeClass = i == 0 ? " is-active" : string.Empty;
                    writer.Write($"<button class=\"eb-ca-dot{activeClass}\" data-index=\"{i}\" aria-label=\"Slide {i + 1}\"></button>");
                }

                writer.Write("</div>");
            }

            writer.Write("</div>");
        }

        #endregion

        #endregion
    }
}