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
    ///     Fluent builder that renders a 3×3 mosaic grid where hovering a cell expands its column
    ///     and row using GSAP Flip for smooth layout transitions. Requires exactly 9 items.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <b>Use cases:</b> portfolio galleries, team photo grids, product showcases,
    ///         destination or location collections, and any set of 9 images that benefit from
    ///         an interactive focus effect.
    ///     </para>
    ///     <para>
    ///         <b>How it works:</b> the grid has six layout-class variants
    ///         (<c>rows-3-1-1</c>, <c>rows-1-3-1</c>, <c>rows-1-1-3</c> and their column counterparts).
    ///         On cell hover the matching row and column classes are swapped; GSAP Flip records the
    ///         previous layout, applies the new one, then animates all cells from their old positions
    ///         to the new ones. On mouse leave the grid resets to the center cell expanded.
    ///     </para>
    ///     <para>
    ///         <b>Tips:</b> choose your most impactful image for position 5 (center cell, column 1 row 1)
    ///         as it is the default expanded cell. Portrait-oriented images work best since the grid
    ///         height is fixed and cells are taller than they are wide.
    ///     </para>
    ///     <para>
    ///         <b>Performance:</b> GSAP and the Flip plugin are loaded on demand from CDN; the layout
    ///         change itself is a CSS class swap with no manual DOM measurements.
    ///     </para>
    /// </remarks>
    [Documented]
    public sealed class MosaicGridEffectBuilder : IHtmlContent
    {
        #region Instance fields and properties

        private string _gap = "15px";
        private readonly IHtmlHelper _html;
        private readonly List<MosaicGridItem> _items = [];
        private string _maxHeight = "800px";

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MosaicGridEffectBuilder" /> class.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to register effect assets.</param>
        public MosaicGridEffectBuilder(IHtmlHelper html) => _html = html;

        #endregion

        #region Instance methods

        /// <summary>
        ///     Adds an image cell to the grid. Items fill left-to-right, top-to-bottom
        ///     (columns 0→1→2, then next row). Exactly 9 calls are required; the builder
        ///     renders nothing if the count differs.
        /// </summary>
        /// <param name="src">URL or path of the image.</param>
        /// <param name="alt">Alt text for accessibility. Default: empty string.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public MosaicGridEffectBuilder AddItem(string src, string alt = "")
        {
            _items.Add(new MosaicGridItem(src, alt));
            return this;
        }

        /// <summary>
        ///     Sets the gap between grid cells. Accepts any valid CSS length value.
        ///     Default: <c>"15px"</c>.
        /// </summary>
        /// <param name="gap">CSS gap value, e.g. <c>"10px"</c> or <c>"1rem"</c>.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public MosaicGridEffectBuilder SetGap(string gap)
        {
            _gap = gap;
            return this;
        }

        /// <summary>
        ///     Sets the maximum height of the grid container. Accepts any valid CSS length value.
        ///     Default: <c>"800px"</c>.
        /// </summary>
        /// <param name="maxHeight">CSS max-height value, e.g. <c>"600px"</c> or <c>"80vh"</c>.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public MosaicGridEffectBuilder SetMaxHeight(string maxHeight)
        {
            _maxHeight = maxHeight;
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
            page.SetStylesheet("/css/innerEffects/MosaicGridEffect.css");
            page.SetScriptFile("/js/innerEffects/MosaicGridEffect.js");

            if (_items.Count != 9) return;

            var style = $"""style="--eb-mosaic-gap:{encoder.Encode(_gap)};--eb-mosaic-max-h:{encoder.Encode(_maxHeight)}" """;

            writer.Write($"""<div class="eb-mosaic-container cols-1-3-1 rows-1-3-1" {style}>""");

            for (int i = 0; i < 9; i++)
            {
                int col = i % 3;
                int row = i / 3;
                bool center = col == 1 && row == 1;
                var src = encoder.Encode(_items[i].Src);
                var alt = encoder.Encode(_items[i].Alt);
                var cls = "eb-mosaic-item" + (center ? " eb-mosaic-center" : "");

                writer.Write($"""<div data-column="{col}" data-row="{row}" class="{cls}">""");
                writer.Write($"""<img src="{src}" alt="{alt}" loading="lazy">""");
                writer.Write("""<div class="eb-mosaic-overlay"></div>""");
                writer.Write("</div>");
            }

            writer.Write("</div>");
        }

        #endregion

        #endregion
    }
}