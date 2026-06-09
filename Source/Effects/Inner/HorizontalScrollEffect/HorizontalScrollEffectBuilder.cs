#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Fluent builder for a horizontal-scroll section: the panel sticks in place while the user
    ///     scrolls vertically, and the inner track translates horizontally in proportion to scroll progress.
    ///     Add slides with <see cref="AddSlide(Func{TResult})" />, set the panel height with
    ///     <see cref="SetHeight" /> and optionally constrain item width with <see cref="SetItemWidth" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <b>Use cases:</b> product feature tours, portfolio galleries, step-by-step journeys, timeline
    ///         reveals, and any sequence where each item deserves full visual attention before the next one.
    ///     </para>
    ///     <para>
    ///         <b>How it works:</b> an outer div accumulates extra height equal to the total horizontal scroll
    ///         distance. Inside it, a sticky panel locks in the viewport. A scroll listener maps the vertical
    ///         progress of the outer div to a <c>translateX</c> on the inner track. Item width defaults to
    ///         <c>100vw</c> (full viewport) so each slide fills the screen; override with <see cref="SetItemWidth" />
    ///         for narrower layouts.
    ///     </para>
    ///     <para>
    ///         <b>Tips:</b> use 4–6 slides for a comfortable scroll distance. Call <see cref="SetStickyTop" />
    ///         to match your fixed navbar height so the panel does not slide underneath it. Avoid placing
    ///         interactive elements inside slides — the user cannot hover/click while actively scrolling.
    ///     </para>
    ///     <para>
    ///         <b>Performance:</b> powered by a passive <c>scroll</c> event listener and a single
    ///         <c>resize</c> observer — no animation loop.
    ///     </para>
    ///     <para>
    ///         <b>Example:</b>
    ///         <code>
    /// @(Html.HorizontalScrollBuilder()
    ///     .SetHeight(500)
    ///     .SetStickyTop(80)
    ///     .AddSlide("bi-geo-alt", "Tokyo",   "Lost in the neon maze.", "#f72585", "#7209b7")
    ///     .AddSlide("bi-sun",     "Sahara",  "Dunes beyond the horizon.", "#fb8500", "#ffb703")
    ///     .AddSlide("bi-water",   "Maldives","Crystal lagoons.", "#0077b6", "#00b4d8"))
    /// </code>
    ///     </para>
    /// </remarks>
    [Documented]
    public sealed class HorizontalScrollEffectBuilder : IHtmlContent
    {
        #region Instance fields and properties

        private int _heightPx = 500;
        private readonly IHtmlHelper _html;
        private readonly List<IHtmlContent> _items = new();
        private int? _itemWidthPx = null;
        private int _stickyTopPx = 0;

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HorizontalScrollEffectBuilder" /> class.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to register effect assets.</param>
        public HorizontalScrollEffectBuilder(IHtmlHelper html)
        {
            _html = html;
        }

        #endregion

        #region Instance methods

        /// <summary>Adds a slide using a Razor template delegate — preferred in <c>.cshtml</c> views.</summary>
        /// <param name="template">Razor template delegate producing the slide HTML.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public HorizontalScrollEffectBuilder AddSlide(Func<dynamic, IHtmlContent> template)
        {
            _items.Add(template(null!));
            return this;
        }

        /// <summary>Adds a slide using pre-built <see cref="IHtmlContent" />.</summary>
        /// <param name="content">HTML content for the slide.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public HorizontalScrollEffectBuilder AddSlide(IHtmlContent content)
        {
            _items.Add(content);
            return this;
        }

        /// <summary>
        ///     Adds a standard gradient slide with a centred icon, title and subtitle.
        ///     Use this overload for the common icon-title-subtitle layout; use the template overload for fully custom content.
        /// </summary>
        /// <param name="icon">Bootstrap icon class, e.g. <c>"bi-geo-alt"</c>.</param>
        /// <param name="title">Main heading displayed below the icon.</param>
        /// <param name="subtitle">Secondary text displayed below the title.</param>
        /// <param name="gradientStart">CSS color for the top-left of the gradient, e.g. <c>"#f72585"</c>.</param>
        /// <param name="gradientEnd">CSS color for the bottom-right of the gradient, e.g. <c>"#7209b7"</c>.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public HorizontalScrollEffectBuilder AddSlide(
            string icon,
            string title,
            string subtitle,
            string gradientStart,
            string gradientEnd
        )
        {
            var enc = HtmlEncoder.Default;
            _items.Add(new HtmlString(
                $"""
                 <div class="w-100 h-100 d-flex flex-column align-items-center justify-content-center gap-3"
                      style="background:linear-gradient(135deg,{enc.Encode(gradientStart)},{enc.Encode(gradientEnd)});">
                     <i class="bi {enc.Encode(icon)} text-white" style="font-size:3.5rem;"></i>
                     <div class="text-center px-5">
                         <h2 class="text-white mb-2">{enc.Encode(title)}</h2>
                         <h5 class="fw-normal text-white-50 mb-0">{enc.Encode(subtitle)}</h5>
                     </div>
                 </div>
                 """));
            return this;
        }

        /// <summary>Sets the height of the sticky panel in pixels (default: 500).</summary>
        /// <param name="px">Panel height in pixels.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public HorizontalScrollEffectBuilder SetHeight(int px)
        {
            _heightPx = px;
            return this;
        }

        /// <summary>
        ///     Sets the width of each slide in pixels (default: full viewport width via <c>100vw</c>).
        ///     Override when embedding the effect inside a constrained container rather than full-width.
        /// </summary>
        /// <param name="px">Slide width in pixels.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public HorizontalScrollEffectBuilder SetItemWidth(int px)
        {
            _itemWidthPx = px;
            return this;
        }

        /// <summary>
        ///     Sets the <c>top</c> CSS offset in pixels at which the sticky panel rests (default: 0).
        ///     Match this value to your fixed navbar height to prevent the panel sliding underneath it.
        /// </summary>
        /// <param name="px">Distance from the top of the viewport in pixels.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public HorizontalScrollEffectBuilder SetStickyTop(int px)
        {
            _stickyTopPx = px;
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
            if (_items.Count == 0) return;

            PageInformation page = PageRegistry.GetOrCreatePageInformation(_html.ViewContext.HttpContext);
            page.SetStylesheet("/css/innerEffects/HorizontalScrollEffect.css");
            page.SetScriptFile("/js/innerEffects/HorizontalScrollEffect.js");

            var itemWidthCss = _itemWidthPx.HasValue
                ? $"{_itemWidthPx.Value}px"
                : "100vw";

            var style = $"--eb-hs-height:{_heightPx}px;--eb-hs-item-width:{itemWidthCss};--eb-hs-top:{_stickyTopPx}px;";

            writer.Write($"<div class=\"eb-hscroll-outer\" style=\"{style}\">");
            writer.Write("<div class=\"eb-hscroll-sticky\">");
            writer.Write("<div class=\"eb-hscroll-track\">");

            foreach (var item in _items)
            {
                writer.Write("<div class=\"eb-hscroll-item\">");
                item.WriteTo(writer, encoder);
                writer.Write("</div>");
            }

            writer.Write("</div>");
            writer.Write("</div>");
            writer.Write("</div>");
        }

        #endregion

        #endregion
    }
}