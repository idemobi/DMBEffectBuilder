#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.Globalization;
using System.Text;
using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Fluent builder that renders a staged-reveal container: one or more image/content slots
    ///     slide in from the edges of a CSS grid, framing a configurable center zone.
    ///     Attach slots with <see cref="AddSlot(StagedRevealEffectDirection, StagedRevealEffectItem[])" />,
    ///     set the center with <see cref="SetCenterContent(Func{dynamic, IHtmlContent})" />,
    ///     and tune timing with <see cref="SetSpeed" />.
    /// </summary>
    [Documented]
    public sealed class StagedRevealEffectBuilder : IHtmlContent
    {
        #region Instance fields and properties

        private int _arcRadiusPx = 0;
        private IHtmlContent? _centerContent;
        private int _diagonalOffsetPx = 0;
        private int _gapPx = 4;
        private int _holdMs = 2500;
        private readonly IHtmlHelper _html;
        private bool _leftTopIn = true;
        private bool _rightTopIn = true;
        private readonly Dictionary<StagedRevealEffectDirection, StagedRevealEffectSlot> _slots = new();
        private readonly decimal _stopPercent;
        private int _transitionMs = 700;

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="StagedRevealEffectBuilder" /> class.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to register effect assets.</param>
        /// <param name="stopPercent">The reveal stop position, expressed as a percentage.</param>
        public StagedRevealEffectBuilder(IHtmlHelper html, decimal stopPercent = 50m)
        {
            _html = html;
            _stopPercent = Math.Clamp(stopPercent, 0m, 100m);
        }

        #endregion

        #region Instance methods

        /// <summary>Adds a slot at the specified grid position containing a single cycling group of items.</summary>
        /// <param name="position">The grid edge (Left, Right, Top, or Bottom) where the slot appears.</param>
        /// <param name="items">One or more <see cref="StagedRevealEffectItem" /> instances that cycle inside the slot.</param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> the quick-setup overload — use when you only need one group of images or text panels
        ///         cycling in a slot without extra configuration.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> delegates to the
        ///         <see cref="AddSlot(StagedRevealEffectDirection, Action{StagedRevealEffectSlot})" /> overload, creating a single
        ///         group assigned to the same direction as the slot.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> for multiple groups or custom opacity on a slot, use the
        ///         <c>Action&lt;StagedRevealEffectSlot&gt;</c> overload instead. Left and right slots support arc and diagonal
        ///         edge cuts via <see cref="SetArcEdge" /> and <see cref="SetDiagonalCut" />.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> each direction can only have one slot; calling this method twice with the same
        ///         <paramref name="position" /> replaces the previous slot.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.StagedRevealEffectBuilder()
        ///     .AddSlot(StagedRevealEffectDirection.Left,
        ///         StagedRevealEffectItem.FromImage("/img/a.jpg"),
        ///         StagedRevealEffectItem.FromImage("/img/b.jpg"))
        ///     .WriteTo(writer, encoder)
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public StagedRevealEffectBuilder AddSlot(StagedRevealEffectDirection position, params StagedRevealEffectItem[] items)
            => AddSlot(position, slot => slot.AddGroup(position, items));

        /// <summary>
        ///     Adds a slot at the specified grid position and exposes the slot for advanced configuration such as multiple
        ///     item groups or custom opacity.
        /// </summary>
        /// <param name="position">The grid edge (Left, Right, Top, or Bottom) where the slot is placed.</param>
        /// <param name="configure">
        ///     An action delegate that receives the <see cref="StagedRevealEffectSlot" /> to configure (add
        ///     groups, set opacity, etc.).
        /// </param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> use when a slot needs multiple cycling groups with different slide-in directions, or when
        ///         the slot's opacity needs to be reduced to blend with the background.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> creates a new <see cref="StagedRevealEffectSlot" />, passes it to
        ///         <paramref name="configure" />, then stores it in the slot dictionary keyed by <paramref name="position" />.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> can be combined freely with the simpler <c>params</c> overload for other positions;
        ///         apply <see cref="SetArcEdge" /> or <see cref="SetDiagonalCut" /> to shape the inner edges of left/right slots.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> calling this twice with the same <paramref name="position" /> overwrites the previous slot —
        ///         keep each direction unique.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.StagedRevealEffectBuilder()
        ///     .AddSlot(StagedRevealEffectDirection.Right, slot =>
        ///     {
        ///         slot.AddGroup(StagedRevealEffectDirection.Right,
        ///             StagedRevealEffectItem.FromImage("/img/x.jpg"),
        ///             StagedRevealEffectItem.FromImage("/img/y.jpg"));
        ///         slot.Opacity = 0.85m;
        ///     })
        ///     .WriteTo(writer, encoder)
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public StagedRevealEffectBuilder AddSlot(StagedRevealEffectDirection position, Action<StagedRevealEffectSlot> configure)
        {
            var slot = new StagedRevealEffectSlot();
            configure(slot);
            _slots[position] = slot;
            return this;
        }

        /// <summary>
        ///     Cuts a concave arc into the inner edge of the left and right slots, creating a curved frame around the center
        ///     zone.
        /// </summary>
        /// <param name="radiusPx">
        ///     Horizontal depth of the arc bite in pixels. Default: <c>0</c> (straight vertical edge). Clamped
        ///     to a minimum of 0.
        /// </param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> adds a decorative curved cutout on the inner edge of left and right slots, drawing the eye
        ///         toward the center content.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> writes <c>--eb-sr-arc</c> as a CSS custom property; the stylesheet uses it as a
        ///         <c>clip-path</c> or border-radius offset to bow the inner edge inward. The arc always spans the full height of
        ///         the slot.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> mutually exclusive in practice with <see cref="SetDiagonalCut" /> — applying both
        ///         produces a combined shape but may look unintentional; choose one edge style per layout.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> a radius of 30–60 px on a 300 px wide slot gives a subtle curve; values above half the slot
        ///         width start to look like a pointed arch.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.StagedRevealEffectBuilder()
        ///     .SetArcEdge(radiusPx: 40)
        ///     .AddSlot(StagedRevealEffectDirection.Left, item1)
        ///     .AddSlot(StagedRevealEffectDirection.Right, item2)
        ///     .WriteTo(writer, encoder)
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public StagedRevealEffectBuilder SetArcEdge(int radiusPx)
        {
            _arcRadiusPx = Math.Max(0, radiusPx);
            return this;
        }

        /// <summary>Sets the center zone content from a raw HTML string.</summary>
        /// <param name="html">A developer-controlled HTML string. Must not contain unsanitised user input.</param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> use when center content is a short, known-safe HTML snippet generated in code (e.g. a
        ///         pre-built button or heading tag).
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> wraps the string in an <see cref="Microsoft.AspNetCore.Html.HtmlString" /> and assigns
        ///         it as the center content; it is written inside <c>.eb-sr-center-content</c> without further encoding.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> mutually exclusive with the other <c>SetCenterContent</c> overloads — the last call
        ///         wins. Prefer the <c>Func&lt;dynamic, IHtmlContent&gt;</c> overload for Razor markup.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> never pass raw user input to this overload; use the <see cref="IHtmlContent" /> overload with a
        ///         properly encoded content object instead.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.StagedRevealEffectBuilder()
        ///     .SetCenterContent("&lt;h2 class=\"hero-title\"&gt;Welcome&lt;/h2&gt;")
        ///     .WriteTo(writer, encoder)
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public StagedRevealEffectBuilder SetCenterContent(string html)
        {
            _centerContent = new HtmlString(html);
            return this;
        }

        /// <summary>Sets the center zone content from an <see cref="IHtmlContent" /> instance.</summary>
        /// <param name="content">Any <see cref="IHtmlContent" /> value, such as the output of another builder or a partial view.</param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> use when center content is produced by another builder (e.g. a Bootstrap card builder) or
        ///         returned from a helper method that already implements <see cref="IHtmlContent" />.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> stores the reference directly; <c>WriteTo</c> calls <c>content.WriteTo</c> inside the
        ///         <c>.eb-sr-center-content</c> div, delegating encoding to the content object itself.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> mutually exclusive with the other <c>SetCenterContent</c> overloads — the last call
        ///         wins. This is the safest overload when the content is user-provided and already encoded.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> prefer this over the raw string overload whenever possible, as encoding responsibility stays
        ///         with the content object.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// var card = Html.CardBuilder().SetTitle("Feature").Build();
        /// Html.StagedRevealEffectBuilder()
        ///     .SetCenterContent(card)
        ///     .WriteTo(writer, encoder)
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public StagedRevealEffectBuilder SetCenterContent(IHtmlContent content)
        {
            _centerContent = content;
            return this;
        }

        /// <summary>
        ///     Sets the center zone content via a Razor template delegate, enabling full Razor markup including other
        ///     builders.
        /// </summary>
        /// <param name="template">
        ///     A Razor template delegate (e.g. an <c>@&lt;text&gt;...&lt;/text&gt;</c> block) that returns the
        ///     center content as <see cref="IHtmlContent" />.
        /// </param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> the most expressive overload — use it when the center zone contains mixed Razor markup,
        ///         multiple elements, or nested builders that cannot be expressed as a plain string or pre-built content object.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> invokes <paramref name="template" /> immediately with a <c>null</c> model argument to
        ///         materialise the content, then stores the resulting <see cref="IHtmlContent" /> exactly like the
        ///         <see cref="IHtmlContent" /> overload.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> mutually exclusive with the other <c>SetCenterContent</c> overloads — the last call
        ///         wins. Well-suited as the last configuration step before rendering.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> the delegate is called once at configuration time, not at render time; avoid relying on mutable
        ///         state that changes after the call.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.StagedRevealEffectBuilder()
        ///     .SetCenterContent(@&lt;div class="hero-text"&gt;
        ///         &lt;h2&gt;Title&lt;/h2&gt;
        ///         &lt;p&gt;Subtitle&lt;/p&gt;
        ///     &lt;/div&gt;)
        ///     .WriteTo(writer, encoder)
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public StagedRevealEffectBuilder SetCenterContent(Func<dynamic, IHtmlContent> template)
        {
            _centerContent = template(null!);
            return this;
        }

        /// <summary>Cuts the inner edge of the left and right slots diagonally, creating a slanted frame around the center zone.</summary>
        /// <param name="offsetPx">
        ///     Pixel shift between the top and bottom corners of each slot's inner edge. Default: <c>0</c>
        ///     (straight vertical edge). Clamped to a minimum of 0.
        /// </param>
        /// <param name="leftTopIn">
        ///     When <c>true</c>, the top corner of the left slot's inner edge shifts inward; <c>false</c>
        ///     shifts the bottom corner inward instead. Default: <c>true</c>.
        /// </param>
        /// <param name="rightTopIn">Same inward-shift logic applied to the right slot. Default: <c>true</c>.</param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> creates a dynamic, angled frame effect that draws attention inward — useful for cinematic
        ///         or action-oriented section designs.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> when <paramref name="offsetPx" /> &gt; 0, adds the CSS class <c>eb-sr-has-diag</c> to
        ///         the container and emits four CSS custom properties (<c>--eb-sr-dl-top</c>, <c>--eb-sr-dl-bot</c>,
        ///         <c>--eb-sr-dr-top</c>, <c>--eb-sr-dr-bot</c>) that drive <c>clip-path</c> polygons on left and right slots.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> avoid mixing with <see cref="SetArcEdge" /> on the same layout as both modify the inner
        ///         slot edge; pairs well with <see cref="SetGap(int)" /> set to 0 for a sharp, gapless diagonal seam.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> matching <paramref name="leftTopIn" /> and <paramref name="rightTopIn" /> both to <c>true</c>
        ///         produces a symmetrical V-frame pointing inward at the top; mixing them creates an asymmetric tilt. An offset of
        ///         40–80 px is clearly visible without being too extreme.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.StagedRevealEffectBuilder()
        ///     .SetDiagonalCut(offsetPx: 60, leftTopIn: true, rightTopIn: true)
        ///     .SetGap(gapPx: 0)
        ///     .AddSlot(StagedRevealEffectDirection.Left, item1)
        ///     .AddSlot(StagedRevealEffectDirection.Right, item2)
        ///     .WriteTo(writer, encoder)
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public StagedRevealEffectBuilder SetDiagonalCut(int offsetPx, bool leftTopIn = true, bool rightTopIn = true)
        {
            _diagonalOffsetPx = Math.Max(0, offsetPx);
            _leftTopIn = leftTopIn;
            _rightTopIn = rightTopIn;
            return this;
        }

        /// <summary>Sets the gap in pixels between the sliding slots and the center zone.</summary>
        /// <param name="gapPx">
        ///     Gap size in pixels. Default: <c>4</c>. Clamped to a minimum of 0. Pass <c>0</c> for seamless,
        ///     borderless edges.
        /// </param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> use a small gap (2–6 px) for a clean separated look; set to 0 when images should bleed
        ///         directly into the center zone.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> writes the <c>--eb-sr-gap</c> CSS custom property which the grid layout uses as
        ///         column/row gap between tracks.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> gap is independent of arc and diagonal cuts — <see cref="SetArcEdge" /> and
        ///         <see cref="SetDiagonalCut" /> shape the edge geometry but do not affect spacing.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> matching the gap colour to the page background makes the grid seams invisible; a contrasting gap
        ///         can act as a deliberate border line.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.StagedRevealEffectBuilder()
        ///     .SetGap(gapPx: 0)
        ///     .AddSlot(StagedRevealEffectDirection.Left, item1)
        ///     .WriteTo(writer, encoder)
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public StagedRevealEffectBuilder SetGap(int gapPx)
        {
            _gapPx = Math.Max(0, gapPx);
            return this;
        }

        /// <summary>Controls how fast the panels slide in and how long each item remains visible before cycling.</summary>
        /// <param name="transitionMs">
        ///     Duration of the slide-in animation in milliseconds. Default: <c>700</c>. Clamped to a
        ///     minimum of 0.
        /// </param>
        /// <param name="holdMs">
        ///     Time each item stays fully visible before the next one takes over, in milliseconds. Default:
        ///     <c>2500</c>. Clamped to a minimum of 0.
        /// </param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> tune pacing to match content density — fast transitions for high-energy sections, slow
        ///         transitions for reading-heavy or showcase content.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> writes <c>data-transition</c> and <c>data-hold</c> attributes on the container div; the
        ///         JS driver reads these to schedule CSS class toggles for each item.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> the total cycle time per item is approximately <c>transitionMs + holdMs</c>; multiply by
        ///         the item count to estimate the full rotation period. Adjust <see cref="SetGap" /> for spacing without affecting
        ///         timing.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> very short hold times (&lt; 1000 ms) may prevent users from reading text content; 2000–4000 ms
        ///         is a comfortable range for image galleries.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.StagedRevealEffectBuilder()
        ///     .SetSpeed(transitionMs: 500, holdMs: 3000)
        ///     .AddSlot(StagedRevealEffectDirection.Left, item1, item2)
        ///     .WriteTo(writer, encoder)
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public StagedRevealEffectBuilder SetSpeed(int transitionMs = 700, int holdMs = 2500)
        {
            _transitionMs = Math.Max(0, transitionMs);
            _holdMs = Math.Max(0, holdMs);
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
            PageInformation page = PageRegistry.GetOrCreatePageInformation(_html.ViewContext.HttpContext);
            page.SetStylesheet("/css/innerEffects/StagedRevealEffect.css");
            page.SetScriptFile("/js/innerEffects/StagedRevealEffect.js");

            var ci = CultureInfo.InvariantCulture;
            var stop = _stopPercent.ToString(ci);

            var style = new StringBuilder($"--eb-sr-stop:{stop}%;");
            style.Append($"--eb-sr-transition:{_transitionMs}ms;");
            style.Append($"--eb-sr-gap:{_gapPx}px;");
            style.Append($"--eb-sr-arc:{_arcRadiusPx}px;");
            if (_diagonalOffsetPx > 0)
            {
                style.Append($"--eb-sr-dl-top:{(_leftTopIn ? _diagonalOffsetPx : 0)}px;");
                style.Append($"--eb-sr-dl-bot:{(_leftTopIn ? 0 : _diagonalOffsetPx)}px;");
                style.Append($"--eb-sr-dr-top:{(_rightTopIn ? _diagonalOffsetPx : 0)}px;");
                style.Append($"--eb-sr-dr-bot:{(_rightTopIn ? 0 : _diagonalOffsetPx)}px;");
            }

            if (_slots.ContainsKey(StagedRevealEffectDirection.Left)) style.Append("--eb-sr-left:var(--eb-sr-stop);");
            if (_slots.ContainsKey(StagedRevealEffectDirection.Right)) style.Append("--eb-sr-right:var(--eb-sr-stop);");
            if (_slots.ContainsKey(StagedRevealEffectDirection.Top)) style.Append("--eb-sr-top:var(--eb-sr-stop);");
            if (_slots.ContainsKey(StagedRevealEffectDirection.Bottom)) style.Append("--eb-sr-bottom:var(--eb-sr-stop);");

            var containerClass = _diagonalOffsetPx > 0 ? "eb-staged-reveal eb-sr-has-diag" : "eb-staged-reveal";
            writer.Write($"""<div class="{containerClass}" style="{style}" data-transition="{_transitionMs}" data-hold="{_holdMs}">""");

            if (_centerContent is not null)
            {
                writer.Write("""<div class="eb-sr-center-content">""");
                _centerContent.WriteTo(writer, encoder);
                writer.Write("</div>");
            }

            foreach (var kvp in _slots)
            {
                var position = kvp.Key.ToString().ToLowerInvariant();
                var slot = kvp.Value;
                var slotStyle = $"grid-area:{position};";
                if (slot.Opacity.HasValue) slotStyle += $"opacity:{slot.Opacity.Value.ToString(ci)};";

                writer.Write($"""<div class="eb-staged-reveal-slot slot-{position}" style="{slotStyle}">""");

                foreach (var group in slot.Groups)
                {
                    var dir = group.Direction.ToString().ToLowerInvariant();
                    writer.Write($"""<div class="eb-staged-reveal-group" data-direction="{dir}">""");

                    foreach (var item in group.Items)
                    {
                        writer.Write("""<div class="eb-staged-reveal-item">""");

                        if (item.ContentType == StagedRevealEffectContentType.Image && item.ImageSrc is not null)
                        {
                            var src = encoder.Encode(item.ImageSrc);
                            var alt = encoder.Encode(item.AltText ?? "");
                            writer.Write($"""<img src="{src}" alt="{alt}" />""");
                        }
                        else if (item.ContentType == StagedRevealEffectContentType.Text && item.TextContent is not null)
                        {
                            writer.Write(encoder.Encode(item.TextContent));
                        }

                        writer.Write("</div>");
                    }

                    writer.Write("</div>");
                }

                writer.Write("</div>");
            }

            writer.Write("</div>");
        }

        #endregion

        #endregion
    }
}