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
    ///     Fluent builder for a 3D cylinder carousel: cards are arranged on a rotating cylinder using CSS
    ///     perspective and 3D transforms. The shape, filter, and hover behaviour are all configurable.
    ///     Add cards with <see cref="AddCard(Func{dynamic,IHtmlContent})" /> or <see cref="AddCard(IHtmlContent)" />,
    ///     choose an orientation with <see cref="SetShape" />, and optionally apply a
    ///     <see cref="SetFilter">filter</see> or enable <see cref="SetHoverClearFilter">hover interactions</see>.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <b>Use cases:</b> portfolio galleries, product showcases, team photos, cover-flow style navigation,
    ///         and any collection where visual depth adds interest.
    ///     </para>
    ///     <para>
    ///         <b>How it works:</b> each card is rotated around the Y axis by its slot angle
    ///         (<c>i × 360° / n</c>), then translated along its local Z axis to form a cylinder.
    ///         A negative Z shift (<see cref="CylinderShapeMode.Concave" />) curves the cylinder away from
    ///         the viewer so the front card is largest; a positive shift (<see cref="CylinderShapeMode.Convex" />)
    ///         curves it toward the viewer. The whole container spins with a CSS animation.
    ///         <see cref="CylinderShapeMode.Flat" /> switches to a 2D horizontal marquee instead.
    ///     </para>
    ///     <para>
    ///         <b>Tips:</b> use <see cref="SetFilter" /> with <see cref="SetHoverClearFilter" /> to create a
    ///         "colour reveal on hover" effect. Combine <see cref="PauseOnHover(bool)" /> with
    ///         <see cref="SetHoverScale" /> so the user can pause and inspect a card. Keep card count between
    ///         6 and 16 for best visual results — too few cards look sparse, too many overlap at the sides.
    ///     </para>
    ///     <para>
    ///         <b>Performance:</b> the rotation is a pure CSS animation with no JavaScript. Hover scale uses a
    ///         CSS <c>@property</c>-animated custom property, so no layout thrashing occurs.
    ///     </para>
    /// </remarks>
    [Documented]
    public sealed class CylinderCarouselEffectBuilder : IHtmlContent
    {
        #region Instance fields and properties

        private string _aspectRatio = "7/10";
        private readonly List<IHtmlContent> _cards = new();
        private string _cardWidth = "17.5em";
        private CylinderFilterMode _filter = CylinderFilterMode.None;
        private bool _hoverClearFilter;
        private bool _hoverScale;
        private readonly IHtmlHelper _html;
        private bool _pauseOnHover;
        private string? _perspectiveOverride;

        private CylinderShapeMode _shape = CylinderShapeMode.Concave;
        private int _speedSeconds = 32;

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CylinderCarouselEffectBuilder" /> class.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to register effect assets.</param>
        public CylinderCarouselEffectBuilder(IHtmlHelper html)
        {
            _html = html;
        }

        #endregion

        #region Instance methods

        /// <summary>Adds a card using a Razor template delegate — preferred in <c>.cshtml</c> views.</summary>
        /// <param name="template">Razor template whose output becomes the card content.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CylinderCarouselEffectBuilder AddCard(Func<dynamic, IHtmlContent> template)
        {
            _cards.Add(template(null!));
            return this;
        }

        /// <summary>Adds a card using pre-built <see cref="IHtmlContent" />.</summary>
        /// <param name="content">HTML content for the card.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CylinderCarouselEffectBuilder AddCard(IHtmlContent content)
        {
            _cards.Add(content);
            return this;
        }

        /// <summary>Adds an image card from a URL, without writing any HTML.</summary>
        /// <param name="src">URL or path to the image.</param>
        /// <param name="alt">Alt text for accessibility (default: empty).</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CylinderCarouselEffectBuilder AddImageCard(string src, string alt = "")
        {
            _cards.Add(new HtmlString($"<img src=\"{HtmlEncoder.Default.Encode(src)}\" alt=\"{HtmlEncoder.Default.Encode(alt)}\" />"));
            return this;
        }

        private string BuildClasses()
        {
            var sb = new System.Text.StringBuilder("eb-cylinder");
            sb.Append(_shape switch
            {
                CylinderShapeMode.Convex => " shape-convex",
                CylinderShapeMode.Flat => " shape-flat",
                _ => " shape-concave"
            });
            if (_filter != CylinderFilterMode.None)
                sb.Append(_filter switch
                {
                    CylinderFilterMode.Grayscale => " filter-grayscale",
                    CylinderFilterMode.Sepia => " filter-sepia",
                    CylinderFilterMode.Blur => " filter-blur",
                    CylinderFilterMode.Dim => " filter-dim",
                    CylinderFilterMode.Invert => " filter-invert",
                    CylinderFilterMode.HueRotate => " filter-huerotate",
                    CylinderFilterMode.Saturate => " filter-saturate",
                    _ => string.Empty
                });
            if (_hoverScale) sb.Append(" hover-scale");
            if (_hoverClearFilter) sb.Append(" hover-clear");
            if (_pauseOnHover) sb.Append(" pause-hover");
            return sb.ToString();
        }

        private string BuildStyle()
        {
            var sb = new System.Text.StringBuilder();
            sb.Append($"--eb-cy-n:{_cards.Count};");
            sb.Append($"--eb-cy-dur:{_speedSeconds}s;");
            sb.Append($"--eb-cy-w:{_cardWidth};");
            sb.Append($"--eb-cy-ratio:{_aspectRatio};");
            if (_perspectiveOverride != null) sb.Append($"--eb-cy-persp:{_perspectiveOverride};");
            return sb.ToString();
        }

        /// <summary>
        ///     Pauses the rotation animation when the user's cursor is anywhere inside the carousel (default: false).
        ///     Useful when combined with <see cref="SetHoverScale" /> so users can inspect cards without them moving away.
        /// </summary>
        /// <param name="enable">Pass <c>false</c> to disable.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CylinderCarouselEffectBuilder PauseOnHover(bool enable = true)
        {
            _pauseOnHover = enable;
            return this;
        }

        /// <summary>Sets the card aspect ratio (default: <c>"7/10"</c>).</summary>
        /// <param name="ratio">CSS aspect-ratio string, e.g. <c>"1/1"</c>, <c>"16/9"</c>, <c>"3/4"</c>.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CylinderCarouselEffectBuilder SetAspectRatio(string ratio)
        {
            _aspectRatio = ratio;
            return this;
        }

        /// <summary>Sets the card width as a CSS value (default: <c>"17.5em"</c>). Accepts any CSS length unit.</summary>
        /// <param name="cssValue">CSS length string, e.g. <c>"280px"</c>, <c>"20em"</c>, <c>"18rem"</c>.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CylinderCarouselEffectBuilder SetCardWidth(string cssValue)
        {
            _cardWidth = cssValue;
            return this;
        }

        /// <summary>Applies a CSS filter to all cards (default: <see cref="CylinderFilterMode.None" />).</summary>
        /// <param name="filter">The filter to apply. Combine with <see cref="SetHoverClearFilter" /> for a colour-reveal effect.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CylinderCarouselEffectBuilder SetFilter(CylinderFilterMode filter)
        {
            _filter = filter;
            return this;
        }

        /// <summary>
        ///     Removes the card filter on hover, revealing the original colours (default: false).
        ///     Most effective when combined with <see cref="SetFilter" />.
        /// </summary>
        /// <param name="enable">Pass <c>false</c> to disable.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CylinderCarouselEffectBuilder SetHoverClearFilter(bool enable = true)
        {
            _hoverClearFilter = enable;
            return this;
        }

        /// <summary>
        ///     Enables a subtle scale-up effect when the user hovers over a card (default: false).
        ///     Uses a CSS <c>@property</c>-animated custom property for smooth transition without JavaScript.
        /// </summary>
        /// <param name="enable">Pass <c>false</c> to disable.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CylinderCarouselEffectBuilder SetHoverScale(bool enable = true)
        {
            _hoverScale = enable;
            return this;
        }

        /// <summary>
        ///     Sets the CSS perspective depth for 3D modes (default: <c>"35em"</c>).
        ///     Smaller values produce a more dramatic 3D effect; larger values flatten the perspective.
        ///     Has no effect in <see cref="CylinderShapeMode.Flat" /> mode.
        /// </summary>
        /// <param name="cssValue">CSS perspective value, e.g. <c>"35em"</c>, <c>"600px"</c>.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CylinderCarouselEffectBuilder SetPerspective(string cssValue)
        {
            _perspectiveOverride = cssValue;
            return this;
        }

        /// <summary>
        ///     Sets the cylinder orientation (default: <see cref="CylinderShapeMode.Concave" />).
        /// </summary>
        /// <param name="shape">
        ///     <see cref="CylinderShapeMode.Concave" /> — opening faces viewer, center card is largest.<br />
        ///     <see cref="CylinderShapeMode.Convex" /> — back faces viewer, side cards are larger.<br />
        ///     <see cref="CylinderShapeMode.Flat" /> — flat horizontal marquee, no 3D depth.
        /// </param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CylinderCarouselEffectBuilder SetShape(CylinderShapeMode shape)
        {
            _shape = shape;
            return this;
        }

        /// <summary>Sets the rotation speed as a full-cycle duration in seconds (default: 32).</summary>
        /// <param name="durationSeconds">Duration of one full rotation. Higher = slower.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public CylinderCarouselEffectBuilder SetSpeed(int durationSeconds)
        {
            _speedSeconds = durationSeconds;
            return this;
        }

        private void Write3DContent(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<div class=\"eb-cy-scene\"><div class=\"eb-cy-a3d\">");
            for (int i = 0; i < _cards.Count; i++)
            {
                writer.Write($"<div class=\"eb-cy-card\" style=\"--i:{i}\">");
                _cards[i].WriteTo(writer, encoder);
                writer.Write("</div>");
            }

            writer.Write("</div></div>");
        }

        private void WriteFlatContent(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<div class=\"eb-cy-flat-track\">");
            // Duplicated for seamless loop: animation translates -50% of total width = one full set
            foreach (var card in _cards)
            {
                writer.Write("<div class=\"eb-cy-card\">");
                card.WriteTo(writer, encoder);
                writer.Write("</div>");
            }

            foreach (var card in _cards)
            {
                writer.Write("<div class=\"eb-cy-card\">");
                card.WriteTo(writer, encoder);
                writer.Write("</div>");
            }

            writer.Write("</div>");
        }

        #region From interface IHtmlContent

        /// <summary>
        ///     Writes the complete effect markup to the provided output writer.
        /// </summary>
        /// <param name="writer">The writer receiving generated HTML.</param>
        /// <param name="encoder">The encoder used to encode generated HTML.</param>
        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            if (_cards.Count == 0) return;

            PageInformation page = PageRegistry.GetOrCreatePageInformation(_html.ViewContext.HttpContext);
            page.SetStylesheet("/css/innerEffects/CylinderCarouselEffect.css");

            writer.Write($"<div class=\"{BuildClasses()}\" style=\"{BuildStyle()}\">");

            if (_shape == CylinderShapeMode.Flat)
                WriteFlatContent(writer, encoder);
            else
                Write3DContent(writer, encoder);

            writer.Write("</div>");
        }

        #endregion

        #endregion
    }
}