#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj CardFlipEffectBuilder.cs create at 2026/05/06
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using System.Globalization;
using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBEffectBuilder
{
    /// <summary>
    /// Fluent builder for a card-flip grid: each card has a front face and a back face revealed by a
    /// 3D rotation, triggered on hover or click.
    /// Add cards with <see cref="AddCard(Func{dynamic,IHtmlContent},Func{dynamic,IHtmlContent})"/>,
    /// set the trigger with <see cref="SetTrigger"/>, and tune sizing with <see cref="SetCardHeight"/>
    /// and <see cref="SetColumns"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Use cases:</b> team member cards (photo front / bio back), feature cards (icon front /
    /// description back), FAQ cards (question front / answer back), pricing tiers.
    /// </para>
    /// <para>
    /// <b>How it works:</b> renders a CSS grid of cards. Each card uses CSS 3D transforms
    /// (<c>perspective</c>, <c>transform-style: preserve-3d</c>, <c>rotateY(180deg)</c>) to flip
    /// between faces. The front face is visible by default; the back face is pre-rotated 180° with
    /// <c>backface-visibility: hidden</c>. Hover mode is pure CSS; click mode adds an <c>is-flipped</c>
    /// class via a JS listener.
    /// </para>
    /// <para>
    /// <b>Tips:</b> both faces should have the same height — use <see cref="SetCardHeight"/> to enforce
    /// it. Avoid placing interactive elements on the back face when using hover trigger, as the card
    /// flips back on mouse-out.
    /// </para>
    /// </remarks>
    [Documented]
    public sealed class CardFlipEffectBuilder : IHtmlContent
    {
        private readonly IHtmlHelper _html;
        private readonly List<CardFlipEffectCard> _cards = new();
        private int _cardHeightPx = 240;
        private decimal _transitionDuration = 0.6m;
        private int _columns = 3;
        private CardFlipTrigger _trigger = CardFlipTrigger.Hover;

        public CardFlipEffectBuilder(IHtmlHelper html)
        {
            _html = html;
        }

        /// <summary>Adds a card using Razor template delegates for both faces — preferred in <c>.cshtml</c> views.</summary>
        /// <param name="front">Razor template for the front face.</param>
        /// <param name="back">Razor template for the back face.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CardFlipEffectBuilder AddCard(Func<dynamic, IHtmlContent> front, Func<dynamic, IHtmlContent> back)
        {
            _cards.Add(new CardFlipEffectCard(front(null!), back(null!)));
            return this;
        }

        /// <summary>Adds a card using pre-built <see cref="IHtmlContent"/> for both faces.</summary>
        /// <param name="front">HTML content for the front face.</param>
        /// <param name="back">HTML content for the back face.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CardFlipEffectBuilder AddCard(IHtmlContent front, IHtmlContent back)
        {
            _cards.Add(new CardFlipEffectCard(front, back));
            return this;
        }

        /// <summary>
        /// Adds a standard gradient card: front shows a centred icon and title on a gradient background;
        /// back shows the description on the reversed gradient.
        /// Use this overload for the common icon-title-description layout; use the template overload for fully custom faces.
        /// </summary>
        /// <param name="icon">Bootstrap icon class, e.g. <c>"bi-search"</c>.</param>
        /// <param name="title">Title displayed on the front face.</param>
        /// <param name="gradientStart">CSS color for the top-left of the gradient, e.g. <c>"#667eea"</c>.</param>
        /// <param name="gradientEnd">CSS color for the bottom-right of the gradient, e.g. <c>"#764ba2"</c>.</param>
        /// <param name="description">Optional description shown on the back face. Omit to leave the back empty.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CardFlipEffectBuilder AddCard(
            string icon, string title,
            string gradientStart, string gradientEnd,
            string? description = null)
        {
            var enc = HtmlEncoder.Default;
            var front = new HtmlString(
                $"""
                <div class="w-100 h-100 d-flex flex-column align-items-center justify-content-center text-center rounded-3"
                     style="background:linear-gradient(135deg,{enc.Encode(gradientStart)},{enc.Encode(gradientEnd)});">
                    <i class="bi {enc.Encode(icon)} text-white mb-3" style="font-size:2.5rem;"></i>
                    <h5 class="text-white mb-0">{enc.Encode(title)}</h5>
                </div>
                """);
            var back = new HtmlString(
                $"""
                <div class="w-100 h-100 d-flex flex-column align-items-center justify-content-center rounded-3 p-4 text-center"
                     style="background:linear-gradient(135deg,{enc.Encode(gradientEnd)},{enc.Encode(gradientStart)});">
                    <h6 class="fw-normal text-white mb-0">{enc.Encode(description ?? string.Empty)}</h6>
                </div>
                """);
            _cards.Add(new CardFlipEffectCard(front, back));
            return this;
        }

        /// <summary>Sets how the flip is triggered (default: <see cref="CardFlipTrigger.Hover"/>).</summary>
        /// <param name="trigger">Use <see cref="CardFlipTrigger.Hover"/> for mouse-over or <see cref="CardFlipTrigger.Click"/> for a toggle on click.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CardFlipEffectBuilder SetTrigger(CardFlipTrigger trigger)
        {
            _trigger = trigger;
            return this;
        }

        /// <summary>Sets the fixed height of every card in pixels (default: 240). Both faces share this height.</summary>
        /// <param name="px">Card height in pixels.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CardFlipEffectBuilder SetCardHeight(int px)
        {
            _cardHeightPx = px;
            return this;
        }

        /// <summary>Sets the number of columns in the card grid (default: 3).</summary>
        /// <param name="columns">Number of equal-width columns. Reduces to 2 on tablet and 1 on mobile.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CardFlipEffectBuilder SetColumns(int columns)
        {
            _columns = columns;
            return this;
        }

        /// <summary>Sets the flip animation duration in seconds (default: 0.6).</summary>
        /// <param name="seconds">Duration of the 3D rotation transition.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CardFlipEffectBuilder SetTransitionDuration(decimal seconds)
        {
            _transitionDuration = seconds;
            return this;
        }

        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            if (_cards.Count == 0) return;

            PageInformation page = PageRegistry.GetOrCreatePageInformation(_html.ViewContext.HttpContext);
            page.SetStylesheet("/css/innerEffects/CardFlipEffect.css");
            if (_trigger == CardFlipTrigger.Click)
                page.SetScriptFile("/js/innerEffects/CardFlipEffect.js");

            var ci = CultureInfo.InvariantCulture;
            var style = $"--eb-cf-height:{_cardHeightPx}px;--eb-cf-duration:{_transitionDuration.ToString(ci)}s;--eb-cf-cols:{_columns};";
            var triggerClass = _trigger == CardFlipTrigger.Hover ? "hover-trigger" : "click-trigger";

            writer.Write($"<div class=\"eb-card-flip-grid {triggerClass}\" style=\"{style}\">");

            foreach (var card in _cards)
            {
                writer.Write("<div class=\"eb-cf-card\">");
                writer.Write("<div class=\"eb-cf-inner\">");

                writer.Write("<div class=\"eb-cf-front\">");
                card.Front.WriteTo(writer, encoder);
                writer.Write("</div>");

                writer.Write("<div class=\"eb-cf-back\">");
                card.Back.WriteTo(writer, encoder);
                writer.Write("</div>");

                writer.Write("</div>");
                writer.Write("</div>");
            }

            writer.Write("</div>");
        }
    }
}
