#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj StackingCardsEffectBuilder.cs create at 2026/05/07
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
    /// Fluent builder for a stacking-cards scroll effect: cards are laid out vertically and each one
    /// sticks in place as the user scrolls, building a visible stack where previous cards peek above
    /// the current one.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Use cases:</b> feature showcases, step-by-step storytelling, pricing tiers, timeline milestones,
    /// and any sequence where each item deserves full visual focus before the next one arrives.
    /// </para>
    /// <para>
    /// <b>How it works:</b> each card uses <c>position: sticky</c> with a <c>top</c> offset that increases
    /// by <see cref="SetPeek"/> pixels per card, and a <c>z-index</c> that increases with the card index
    /// so later cards appear on top. A configurable vertical gap between cards controls how much the user
    /// must scroll before the next card arrives.
    /// </para>
    /// <para>
    /// <b>Tips:</b> use 3–6 cards for the best balance. Set <see cref="SetStickyTop"/> to match your
    /// fixed navbar height. Increase <c>SetScrollGap</c> for cards with longer content so the user
    /// has time to read before the next card slides in.
    /// </para>
    /// <para>
    /// <b>Performance:</b> pure CSS — no JavaScript, no IntersectionObserver, no scroll events.
    /// </para>
    /// </remarks>
    [Documented]
    public sealed class StackingCardsEffectBuilder : IHtmlContent
    {
        private readonly IHtmlHelper _html;
        private readonly List<IHtmlContent> _cards = new();

        private int _cardHeightPx = 380;
        private int _peekPx = 16;
        private int _stickyTopPx = 80;
        private int _gapPx = 48;

        /// <summary>
        /// Initializes a new instance of the <see cref="StackingCardsEffectBuilder"/> class.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to register effect assets.</param>
        public StackingCardsEffectBuilder(IHtmlHelper html)
        {
            _html = html;
        }

        /// <summary>Adds a card using a Razor template delegate — preferred in <c>.cshtml</c> views.</summary>
        /// <param name="template">Razor template whose output fills the full card area.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public StackingCardsEffectBuilder AddCard(Func<dynamic, IHtmlContent> template)
        {
            _cards.Add(template(null!));
            return this;
        }

        /// <summary>Adds a card using pre-built <see cref="IHtmlContent"/>.</summary>
        /// <param name="content">HTML content for the card.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public StackingCardsEffectBuilder AddCard(IHtmlContent content)
        {
            _cards.Add(content);
            return this;
        }

        /// <summary>
        /// Adds a standard gradient card with a centred icon, title, subtitle and an optional button.
        /// Use this overload for the common layout; use the template overload for fully custom content.
        /// </summary>
        /// <param name="icon">Bootstrap icon class, e.g. <c>"bi-search"</c>.</param>
        /// <param name="title">Main heading displayed below the icon.</param>
        /// <param name="subtitle">Secondary text displayed below the title.</param>
        /// <param name="gradientStart">CSS color for the top-left of the gradient, e.g. <c>"#667eea"</c>.</param>
        /// <param name="gradientEnd">CSS color for the bottom-right of the gradient, e.g. <c>"#764ba2"</c>.</param>
        /// <param name="buttonUrl">Optional URL for the call-to-action button. Pass <c>null</c> to hide it.</param>
        /// <param name="buttonLabel">Button label text (default: <c>"Learn more"</c>).</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public StackingCardsEffectBuilder AddCard(
            string icon, string title, string subtitle,
            string gradientStart, string gradientEnd,
            string? buttonUrl = null, string buttonLabel = "Learn more")
        {
            var enc = HtmlEncoder.Default;
            var sb  = new System.Text.StringBuilder();
            sb.Append($"""<div class="w-100 h-100 d-flex flex-column align-items-center justify-content-center gap-3 text-center px-4" style="background:linear-gradient(135deg,{enc.Encode(gradientStart)},{enc.Encode(gradientEnd)});">""");
            sb.Append($"""<i class="bi {enc.Encode(icon)} text-white" style="font-size:3rem;"></i>""");
            sb.Append("<div>");
            sb.Append($"""<h2 class="text-white">{enc.Encode(title)}</h2>""");
            sb.Append($"""<h5 class="fw-normal text-white-50 mb-0">{enc.Encode(subtitle)}</h5>""");
            sb.Append("</div>");
            if (buttonUrl != null)
                sb.Append($"""<a href="{enc.Encode(buttonUrl)}" class="btn btn-outline-light">{enc.Encode(buttonLabel)}</a>""");
            sb.Append("</div>");
            _cards.Add(new HtmlString(sb.ToString()));
            return this;
        }

        /// <summary>Sets the height of each card in pixels (default: 380).</summary>
        /// <param name="px">Card height in pixels.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public StackingCardsEffectBuilder SetCardHeight(int px)
        {
            _cardHeightPx = px;
            return this;
        }

        /// <summary>
        /// Sets how many pixels of a previous card remain visible above the current one (default: 24).
        /// Each card's <c>top</c> offset is incremented by this value so the stack builds visually.
        /// </summary>
        /// <param name="px">Peek height in pixels.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public StackingCardsEffectBuilder SetPeek(int px)
        {
            _peekPx = px;
            return this;
        }

        /// <summary>
        /// Sets the <c>top</c> base offset in pixels at which the first card sticks (default: 80).
        /// Match this value to your fixed navbar height to avoid cards sliding underneath it.
        /// </summary>
        /// <param name="px">Base sticky top in pixels.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public StackingCardsEffectBuilder SetStickyTop(int px)
        {
            _stickyTopPx = px;
            return this;
        }

        /// <summary>
        /// Sets the visual gap between cards in pixels (default: 48).
        /// This controls the spacing between grid rows — keep it small for a tight stack.
        /// </summary>
        /// <param name="px">Gap in pixels.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public StackingCardsEffectBuilder SetGap(int px)
        {
            _gapPx = px;
            return this;
        }

        /// <summary>
        /// Writes the complete effect markup to the provided output writer.
        /// </summary>
        /// <param name="writer">The writer receiving generated HTML.</param>
        /// <param name="encoder">The encoder used to encode generated HTML.</param>
        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            if (_cards.Count == 0) return;

            PageInformation page = PageRegistry.GetOrCreatePageInformation(_html.ViewContext.HttpContext);
            page.SetStylesheet("/css/innerEffects/StackingCardsEffect.css");

            var style = $"--eb-stk-height:{_cardHeightPx}px;--eb-stk-peek:{_peekPx}px;--eb-stk-top:{_stickyTopPx}px;--eb-stk-gap:{_gapPx}px;--eb-stk-n:{_cards.Count};";

            writer.Write($"<div class=\"eb-stacking\" style=\"{style}\">");
            for (int i = 0; i < _cards.Count; i++)
            {
                writer.Write($"<div class=\"eb-stk-card\" style=\"--i:{i}\">");
                writer.Write("<div class=\"eb-stk-inner\">");
                _cards[i].WriteTo(writer, encoder);
                writer.Write("</div>");
                writer.Write("</div>");
            }
            writer.Write("</div>");
        }
    }
}
