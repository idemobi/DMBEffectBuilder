#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj FanEffectBuilder.cs create at 2026/04/29
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
    /// Fluent builder that renders a fan (éventail) of cards.
    /// Cards are spread and rotated automatically based on their count;
    /// hovering a card brings it to the foreground.
    /// </summary>
    [Documented]
    public sealed class FanEffectBuilder : IHtmlContent
    {
        private readonly IHtmlHelper         _html;
        private readonly List<FanEffectCard> _cards = [];
        private int                          _cardWidthPx           = 200;
        private int                          _cardHeightPx          = 280;
        private int                          _containerHeightPx     = 360;
        private int                          _topWidthPx            = 150;
        private int                          _bottomWidthPx         = 0;
        private FanEffectEntranceDirection   _entranceDirection     = FanEffectEntranceDirection.None;
        private int                          _entranceDelayStepMs   = 80;
        private int                          _bottomOffsetPx        = 20;

        public FanEffectBuilder(IHtmlHelper html)
        {
            _html = html;
        }

        /// <summary>Adds a single card to the fan layout.</summary>
        /// <param name="card">The <see cref="FanEffectCard"/> instance to append.</param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> when cards are built individually, e.g. from a loop with conditional logic per card.</para>
        /// <para><b>How it works:</b> appends the card to the internal list; rotation and position are computed at render time based on the total card count.</para>
        /// <para><b>Combinations:</b> use together with <see cref="AddCards"/> freely — both append to the same list. Call before <see cref="SetEntrance"/> so entrance stagger indices are correct.</para>
        /// <para><b>Tips:</b> add all cards before rendering; the fan geometry depends on the final count.</para>
        /// <para><b>Example:</b>
        /// <code>
        /// Html.FanEffectBuilder()
        ///     .AddCard(new FanEffectCard { ImageSrc = "/img/card1.jpg" })
        ///     .AddCard(new FanEffectCard { ImageSrc = "/img/card2.jpg" })
        ///     .WriteTo(writer, encoder)
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public FanEffectBuilder AddCard(FanEffectCard card)
        {
            _cards.Add(card);
            return this;
        }

        /// <summary>Adds multiple cards to the fan layout in a single call.</summary>
        /// <param name="cards">One or more <see cref="FanEffectCard"/> instances to append.</param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> when the full set of cards is already available as an array or a collection initialiser.</para>
        /// <para><b>How it works:</b> delegates to <c>List.AddRange</c> — identical to calling <see cref="AddCard"/> for each element in order.</para>
        /// <para><b>Combinations:</b> can be mixed with <see cref="AddCard"/>; all cards share the same spread geometry regardless of how they were added.</para>
        /// <para><b>Tips:</b> prefer this over multiple <see cref="AddCard"/> calls when cards are already in an array for cleaner chaining syntax.</para>
        /// <para><b>Example:</b>
        /// <code>
        /// Html.FanEffectBuilder()
        ///     .AddCards(
        ///         new FanEffectCard { ImageSrc = "/img/a.jpg" },
        ///         new FanEffectCard { ImageSrc = "/img/b.jpg" },
        ///         new FanEffectCard { ImageSrc = "/img/c.jpg" }
        ///     )
        ///     .WriteTo(writer, encoder)
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public FanEffectBuilder AddCards(params FanEffectCard[] cards)
        {
            _cards.AddRange(cards);
            return this;
        }

        /// <summary>Sets the width and height of every card in the fan in pixels.</summary>
        /// <param name="widthPx">Card width in pixels. Default: <c>200</c>. Clamped to a minimum of 1.</param>
        /// <param name="heightPx">Card height in pixels. Default: <c>280</c>. Clamped to a minimum of 1.</param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> when the default 200 × 280 cards need to match a specific image aspect ratio or fit inside a constrained layout.</para>
        /// <para><b>How it works:</b> writes <c>--eb-fan-card-w</c> and <c>--eb-fan-card-h</c> CSS custom properties on the container; the spread angle calculation also uses <paramref name="heightPx"/> as the lever arm.</para>
        /// <para><b>Combinations:</b> always call before <see cref="SetSpreadByWidth"/> when the spread geometry depends on card height; update <see cref="SetContainerHeight"/> and <see cref="SetBottomOffset"/> to match taller or shorter cards.</para>
        /// <para><b>Tips:</b> portrait orientation (height &gt; width) gives the most realistic hand-of-cards appearance; square or landscape cards look more like a spread of tiles.</para>
        /// <para><b>Example:</b>
        /// <code>
        /// Html.FanEffectBuilder()
        ///     .SetCardSize(widthPx: 160, heightPx: 240)
        ///     .AddCards(card1, card2, card3)
        ///     .WriteTo(writer, encoder)
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public FanEffectBuilder SetCardSize(int widthPx, int heightPx)
        {
            _cardWidthPx  = Math.Max(1, widthPx);
            _cardHeightPx = Math.Max(1, heightPx);
            return this;
        }

        /// <summary>Sets how far the card rotation anchor sits above the bottom of the container in pixels.</summary>
        /// <param name="px">Offset from the container bottom in pixels. Default: <c>20</c>. Clamped to a minimum of 0.</param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> use when cards at steep rotation angles are clipped by the container edge and need to be lifted higher.</para>
        /// <para><b>How it works:</b> sets the <c>--eb-fan-bottom</c> CSS custom property, which shifts the pivot point upward so that the card corners stay within the container bounds.</para>
        /// <para><b>Combinations:</b> the minimum safe value for a given spread is approximately <c>(cardWidth / 2) × sin(maxAngle)</c>; recalculate after changing <see cref="SetCardSize"/> or <see cref="SetSpreadByWidth"/>.</para>
        /// <para><b>Tips:</b> start with the default 20 px and increase only when clipping is visible; a large offset wastes vertical space at the bottom.</para>
        /// <para><b>Example:</b>
        /// <code>
        /// Html.FanEffectBuilder()
        ///     .SetBottomOffset(px: 40)
        ///     .SetSpreadByWidth(topWidthPx: 300, bottomWidthPx: 0)
        ///     .AddCards(card1, card2, card3, card4, card5)
        ///     .WriteTo(writer, encoder)
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public FanEffectBuilder SetBottomOffset(int px)
        {
            _bottomOffsetPx = Math.Max(0, px);
            return this;
        }

        /// <summary>Sets the total height of the fan container in pixels.</summary>
        /// <param name="heightPx">Container height in pixels. Default: <c>360</c>. Clamped to a minimum of 1.</param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> adjust when the default 360 px container is too tall for a compact section or too short to show taller cards without clipping.</para>
        /// <para><b>How it works:</b> writes <c>--eb-fan-h</c> CSS custom property on the container div, which drives the overall height of the fan widget.</para>
        /// <para><b>Combinations:</b> set this in tandem with <see cref="SetCardSize"/> — a good starting point is card height plus the bottom offset plus a few pixels of breathing room.</para>
        /// <para><b>Tips:</b> if hovering a card causes it to overflow the container, increase this value or reduce the card height.</para>
        /// <para><b>Example:</b>
        /// <code>
        /// Html.FanEffectBuilder()
        ///     .SetCardSize(widthPx: 180, heightPx: 260)
        ///     .SetContainerHeight(heightPx: 320)
        ///     .AddCards(card1, card2)
        ///     .WriteTo(writer, encoder)
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public FanEffectBuilder SetContainerHeight(int heightPx)
        {
            _containerHeightPx = Math.Max(1, heightPx);
            return this;
        }

        /// <summary>Controls the fan spread shape by specifying the total horizontal width occupied by cards at their top and bottom edges.</summary>
        /// <param name="topWidthPx">Total horizontal span of all card tops in pixels. Default: <c>150</c>.</param>
        /// <param name="bottomWidthPx">Total horizontal span of all card bottoms in pixels. Default: <c>0</c> (converging at a single anchor point).</param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> the primary control for shaping the fan — use a large top width and zero bottom width for a dramatic hand-of-cards look, or equal values for a flat tiled row.</para>
        /// <para><b>How it works:</b> for each card the builder computes the horizontal position at top and bottom using linear interpolation across the spread, then derives the rotation angle via <c>arcsin(deltaX / cardHeight)</c>.</para>
        /// <para><b>Combinations:</b> recalculate <see cref="SetBottomOffset"/> when increasing spread to avoid clipping rotated corners; pair with <see cref="SetContainerHeight"/> to give enough vertical room.</para>
        /// <para><b>Tips:</b> <paramref name="topWidthPx"/> = card count × card width produces a barely-overlapping flat fan; half that value gives moderate overlap; setting <paramref name="bottomWidthPx"/> &gt; 0 opens the bottom and creates an arc shape.</para>
        /// <para><b>Example:</b>
        /// <code>
        /// Html.FanEffectBuilder()
        ///     .SetSpreadByWidth(topWidthPx: 200, bottomWidthPx: 0)
        ///     .AddCards(card1, card2, card3, card4, card5)
        ///     .WriteTo(writer, encoder)
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public FanEffectBuilder SetSpreadByWidth(int topWidthPx, int bottomWidthPx = 0)
        {
            _topWidthPx    = Math.Max(0, topWidthPx);
            _bottomWidthPx = Math.Max(0, bottomWidthPx);
            return this;
        }

        /// <summary>Enables a staggered entrance animation that fires when the fan scrolls into the viewport.</summary>
        /// <param name="direction">The direction cards slide in from. Use <see cref="FanEffectEntranceDirection.None"/> to disable the animation.</param>
        /// <param name="delayStepMs">Milliseconds between each successive card animation start. Default: <c>80</c>. Clamped to a minimum of 0.</param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> adds visual polish on page load or scroll; a left/right entrance direction reinforces the card-dealing metaphor.</para>
        /// <para><b>How it works:</b> sets the <c>data-entrance</c> attribute on the container and a <c>--eb-fan-delay</c> CSS custom property on each card with a cumulative offset; the JS observer triggers the animation on intersection.</para>
        /// <para><b>Combinations:</b> works independently of <see cref="SetSpreadByWidth"/> and <see cref="SetCardSize"/>; call after adding all cards so the stagger indices match the final card order.</para>
        /// <para><b>Tips:</b> 60–100 ms per step feels natural for 3–6 cards; reduce the step for larger fans to avoid a long wait for the last card; set <paramref name="delayStepMs"/> to 0 for a simultaneous entrance.</para>
        /// <para><b>Example:</b>
        /// <code>
        /// Html.FanEffectBuilder()
        ///     .AddCards(card1, card2, card3)
        ///     .SetEntrance(direction: FanEffectEntranceDirection.Left, delayStepMs: 100)
        ///     .WriteTo(writer, encoder)
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public FanEffectBuilder SetEntrance(FanEffectEntranceDirection direction, int delayStepMs = 80)
        {
            _entranceDirection   = direction;
            _entranceDelayStepMs = Math.Max(0, delayStepMs);
            return this;
        }

        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            PageInformation page = PageRegistry.GetOrCreatePageInformation(_html.ViewContext.HttpContext);
            page.SetStylesheet("/css/innerEffects/FanEffect.css");
            page.SetScriptFile("/js/innerEffects/FanEffect.js");

            var ci    = CultureInfo.InvariantCulture;
            var count = _cards.Count;

            var containerStyle = $"--eb-fan-card-w:{_cardWidthPx}px;--eb-fan-card-h:{_cardHeightPx}px;--eb-fan-h:{_containerHeightPx}px;--eb-fan-bottom:{_bottomOffsetPx}px;";
            var entranceAttr   = _entranceDirection != FanEffectEntranceDirection.None
                ? $""" data-entrance="{_entranceDirection.ToString().ToLowerInvariant()}" """
                : " ";

            writer.Write($"""<div class="eb-fan" style="{containerStyle}"{entranceAttr}>""");

            for (int i = 0; i < count; i++)
            {
                var card = _cards[i];

                decimal t       = count > 1 ? (decimal)i / (count - 1) : 0m;
                decimal bottomX = -(decimal)_bottomWidthPx / 2m + t * _bottomWidthPx;
                decimal topX    = -(decimal)_topWidthPx    / 2m + t * _topWidthPx;
                decimal deltaX  = topX - bottomX;

                double  sinVal   = Math.Clamp((double)deltaX / _cardHeightPx, -1.0, 1.0);
                decimal angleDeg = (decimal)(Math.Asin(sinVal) * 180.0 / Math.PI);

                var cardStyle = $"--eb-fan-rotate:{angleDeg.ToString(ci)}deg;--eb-fan-x:{bottomX.ToString(ci)}px;--eb-fan-z:{i + 1};";
                if (_entranceDirection != FanEffectEntranceDirection.None)
                    cardStyle += $"--eb-fan-delay:{i * _entranceDelayStepMs}ms;";

                writer.Write($"""<div class="eb-fan-card" style="{cardStyle}" data-index="{i}">""");

                if (card.ImageSrc is not null)
                {
                    var src = encoder.Encode(card.ImageSrc);
                    var alt = encoder.Encode(card.AltText ?? "");
                    writer.Write($"""<img src="{src}" alt="{alt}" class="eb-fan-card-img" />""");
                }

                if (card.Content is not null)
                {
                    writer.Write("""<div class="eb-fan-card-body">""");
                    card.Content.WriteTo(writer, encoder);
                    writer.Write("</div>");
                }

                writer.Write("</div>");
            }

            writer.Write("</div>");
        }
    }
}
