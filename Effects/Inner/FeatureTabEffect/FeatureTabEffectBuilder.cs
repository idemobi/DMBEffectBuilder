#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj FeatureTabEffectBuilder.cs create at 2026/05/06
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
    /// Fluent builder for a feature-tab layout: a list of clickable titles on the left, the active item
    /// expands to reveal its description, and the right panel cross-fades to the matching visual.
    /// Add items with <see cref="AddItem(Func{dynamic,IHtmlContent},string,string)"/>, tune sizing with
    /// <see cref="SetPanelHeight"/> and the transition with <see cref="SetTransitionDuration"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Use cases:</b> product feature showcases, IDE/tool capability listings, onboarding tours, and
    /// any context where each feature maps to a screenshot or illustration.
    /// </para>
    /// <para>
    /// <b>How it works:</b> renders a two-column CSS grid. The left column stacks clickable feature items;
    /// the active item gains a highlighted background and its description slides open. The right column
    /// layers all visuals with <c>position: absolute</c> and cross-fades between them via
    /// <c>opacity</c> transitions driven by a click listener.
    /// </para>
    /// <para>
    /// <b>Combinations:</b> the visual slot accepts any <see cref="IHtmlContent"/> — screenshots, gradient
    /// divs, illustrations, or rendered helpers. Use the <c>@&lt;div&gt;...&lt;/div&gt;</c> Razor
    /// template syntax for inline markup.
    /// </para>
    /// <para>
    /// <b>Tips:</b> 4–8 items work best. Keep titles short (2–5 words) so the list stays scannable.
    /// Use a consistent visual size across all items so the panel dimensions do not jump on switch.
    /// </para>
    /// <para>
    /// <b>Performance:</b> pure CSS transitions driven by a single click listener per container — no
    /// animation loop. The cost is proportional to the number of visuals loaded in the DOM.
    /// </para>
    /// </remarks>
    [Documented]
    public sealed class FeatureTabEffectBuilder : IHtmlContent
    {
        private readonly IHtmlHelper _html;
        private readonly List<FeatureTabEffectItem> _items = new();
        private int _panelHeightPx = 420;
        private decimal _transitionDuration = 0.4m;

        public FeatureTabEffectBuilder(IHtmlHelper html)
        {
            _html = html;
        }

        /// <summary>Adds a feature item using a Razor template delegate as the visual panel — the preferred overload in <c>.cshtml</c> views.</summary>
        /// <param name="template">Razor template delegate (<c>@&lt;div&gt;...&lt;/div&gt;</c>) producing the HTML for the right panel.</param>
        /// <param name="title">Feature title displayed in the left list.</param>
        /// <param name="description">Optional description shown below the title when the item is active.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public FeatureTabEffectBuilder AddItem(Func<dynamic, IHtmlContent> template, string title, string? description = null)
        {
            _items.Add(new FeatureTabEffectItem(template(null!), title, description));
            return this;
        }

        /// <summary>Adds a feature item using pre-built <see cref="IHtmlContent"/> as the visual panel.</summary>
        /// <param name="visual">HTML content for the right panel.</param>
        /// <param name="title">Feature title displayed in the left list.</param>
        /// <param name="description">Optional description shown below the title when the item is active.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public FeatureTabEffectBuilder AddItem(IHtmlContent visual, string title, string? description = null)
        {
            _items.Add(new FeatureTabEffectItem(visual, title, description));
            return this;
        }

        /// <summary>
        /// Adds a feature item using a standard image-on-gradient visual.
        /// Use this overload for the common layout; use the template overload for fully custom visuals.
        /// </summary>
        /// <param name="imageSrc">URL or path to the image displayed in the right panel.</param>
        /// <param name="alt">Alt text for the image.</param>
        /// <param name="gradientStart">CSS color for the top-left of the gradient background, e.g. <c>"#1a1a2e"</c>.</param>
        /// <param name="gradientEnd">CSS color for the bottom-right of the gradient background, e.g. <c>"#16213e"</c>.</param>
        /// <param name="title">Feature title displayed in the left list.</param>
        /// <param name="description">Optional description shown below the title when the item is active.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public FeatureTabEffectBuilder AddItem(
            string imageSrc, string alt,
            string gradientStart, string gradientEnd,
            string title, string? description = null)
        {
            var enc = HtmlEncoder.Default;
            var visual = new HtmlString(
                $"""
                <div class="w-100 h-100 d-flex align-items-center justify-content-center"
                     style="background:linear-gradient(135deg,{enc.Encode(gradientStart)},{enc.Encode(gradientEnd)});">
                    <img src="{enc.Encode(imageSrc)}" class="img-fluid rounded-3 shadow-lg"
                         style="max-height:400px;" alt="{enc.Encode(alt)}" />
                </div>
                """);
            _items.Add(new FeatureTabEffectItem(visual, title, description));
            return this;
        }

        /// <summary>Sets the height in pixels of the right visual panel (default: 420).</summary>
        /// <param name="px">Height of the right panel in pixels.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public FeatureTabEffectBuilder SetPanelHeight(int px)
        {
            _panelHeightPx = px;
            return this;
        }

        /// <summary>Sets the duration of the cross-fade transition between visuals in seconds (default: 0.4).</summary>
        /// <param name="seconds">Duration in seconds for the <c>opacity</c> transition.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public FeatureTabEffectBuilder SetTransitionDuration(decimal seconds)
        {
            _transitionDuration = seconds;
            return this;
        }

        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            if (_items.Count == 0) return;

            PageInformation page = PageRegistry.GetOrCreatePageInformation(_html.ViewContext.HttpContext);
            page.SetStylesheet("/css/innerEffects/FeatureTabEffect.css");
            page.SetScriptFile("/js/innerEffects/FeatureTabEffect.js");

            var ci = CultureInfo.InvariantCulture;
            var style = $"--eb-ft-duration:{_transitionDuration.ToString(ci)}s;--eb-ft-height:{_panelHeightPx}px;";

            writer.Write($"<div class=\"eb-feature-tab\" style=\"{style}\">");

            writer.Write("<div class=\"eb-ft-left\">");
            for (int i = 0; i < _items.Count; i++)
            {
                var item = _items[i];
                string activeClass = i == 0 ? " is-active" : string.Empty;
                writer.Write($"<div class=\"eb-ft-item{activeClass}\" data-index=\"{i}\">");
                writer.Write($"<button class=\"eb-ft-trigger\">{HtmlEncoder.Default.Encode(item.Title)}</button>");
                if (!string.IsNullOrEmpty(item.Description))
                    writer.Write($"<div class=\"eb-ft-body\">{HtmlEncoder.Default.Encode(item.Description)}</div>");
                writer.Write("</div>");
            }
            writer.Write("</div>");

            writer.Write("<div class=\"eb-ft-right\">");
            for (int i = 0; i < _items.Count; i++)
            {
                string activeClass = i == 0 ? " is-active" : string.Empty;
                writer.Write($"<div class=\"eb-ft-panel{activeClass}\" data-index=\"{i}\">");
                _items[i].Visual.WriteTo(writer, encoder);
                writer.Write("</div>");
            }
            writer.Write("</div>");

            writer.Write("</div>");
        }
    }
}
