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
    ///     Fluent builder for a sticky-scroll layout: the left panel stays fixed while numbered steps scroll on the right,
    ///     and the visual slides to match the active step on each change.
    ///     Use <see cref="AddStep(Func{TResult},string,string,string)" /> in Razor views or
    ///     <see cref="AddStep(IHtmlContent,string,string,string)" /> in code, tune sizing with
    ///     <see cref="SetVisualHeight" /> and <see cref="SetStickyOffset" />.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <b>Use cases:</b> product feature walkthroughs, onboarding sequences, step-by-step processes, comparison
    ///         layouts, and any context where each text step maps to a distinct visual illustration.
    ///     </para>
    ///     <para>
    ///         <b>How it works:</b> renders a two-column CSS grid. The left column uses <c>position: sticky</c> and
    ///         <c>overflow: hidden</c> to lock the visual panel in place; all visuals are stacked with
    ///         <c>position: absolute</c> and moved off-screen via <c>transform: translateY</c>. An
    ///         <c>IntersectionObserver</c> watches each right-side step item and, when one enters the central zone of
    ///         the viewport (±40 % dead zone), slides the matching visual into position with a cubic-bezier transition.
    ///     </para>
    ///     <para>
    ///         <b>Combinations:</b> the visual slot accepts any <see cref="IHtmlContent" /> — images, gradient divs,
    ///         code blocks, or rendered helpers. Wrap in a <c>container-lg</c> div or a full-width
    ///         <c>SectionBuilder</c> for a hero-style treatment.
    ///     </para>
    ///     <para>
    ///         <b>Tips:</b> 3–6 steps give the best balance. Set <see cref="SetItemMinHeight" /> to at least
    ///         <c>50</c> vh so each step has enough scroll space to trigger cleanly. Increase <see cref="SetStickyOffset" />
    ///         to match your fixed navbar height so the panel does not slide underneath it.
    ///     </para>
    ///     <para>
    ///         <b>Performance:</b> powered by a single <c>IntersectionObserver</c> — no scroll-event polling.
    ///         Scales linearly with step count; keep heavy visuals (videos, canvases) to a minimum.
    ///     </para>
    /// </remarks>
    [Documented]
    public sealed class StickyScrollEffectBuilder : IHtmlContent
    {
        #region Instance fields and properties

        private readonly IHtmlHelper _html;
        private int _itemMinHeightVh = 50;
        private readonly List<StickyScrollEffectStep> _steps = new();
        private int _stickyOffsetPx = 80;
        private decimal _transitionDuration = 0.5m;
        private int _visualHeightPx = 400;

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="StickyScrollEffectBuilder" /> class.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to register effect assets.</param>
        public StickyScrollEffectBuilder(IHtmlHelper html)
        {
            _html = html;
        }

        #endregion

        #region Instance methods

        /// <summary>Adds a step to the layout using pre-built <see cref="IHtmlContent" /> as the visual panel.</summary>
        /// <param name="visual">HTML content for the left sticky panel — any <see cref="IHtmlContent" /> implementation.</param>
        /// <param name="title">Short heading displayed above the description on the right side.</param>
        /// <param name="description">Body text explaining the step, displayed below the title.</param>
        /// <param name="icon">Optional Bootstrap icon class (e.g. <c>bi-lightning-charge</c>) rendered above the title.</param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> programmatically built visuals, server-rendered partials, or content produced outside a Razor
        ///         view.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> prefer the <see cref="AddStep(Func{dynamic,IHtmlContent},string,string,string)" />
        ///         overload
        ///         in <c>.cshtml</c> views for cleaner inline markup using the <c>@&lt;div&gt;...&lt;/div&gt;</c> template syntax.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// IHtmlContent visual = Html.Raw("&lt;img src='/img/step1.png' class='img-fluid rounded-3' /&gt;");
        /// Html.StickyScrollBuilder()
        ///     .AddStep(visual, "Step title", "Step description.", "bi-1-circle")
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public StickyScrollEffectBuilder AddStep(IHtmlContent visual, string title, string description, string? icon = null)
        {
            _steps.Add(new StickyScrollEffectStep(visual, title, description, icon));
            return this;
        }

        /// <summary>
        ///     Adds a step using a Razor template delegate as the visual panel — the preferred overload in <c>.cshtml</c>
        ///     views.
        /// </summary>
        /// <param name="template">
        ///     Razor template delegate (<c>@&lt;div&gt;...&lt;/div&gt;</c>) producing the HTML for the sticky
        ///     panel.
        /// </param>
        /// <param name="title">Short heading displayed above the description on the right side.</param>
        /// <param name="description">Body text explaining the step, displayed below the title.</param>
        /// <param name="icon">Optional Bootstrap icon class (e.g. <c>bi-lightning-charge</c>) rendered above the title.</param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> any <c>.cshtml</c> view where the visual is defined as inline Razor markup — styled divs,
        ///         images, icon layouts, or other helper-rendered components.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> the Razor compiler turns <c>@&lt;element&gt;...&lt;/element&gt;</c> into a
        ///         <c>Func&lt;dynamic, IHtmlContent&gt;</c>; the builder invokes it immediately with a <c>null</c> model
        ///         argument and stores the resulting <see cref="IHtmlContent" />.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @(Html.StickyScrollBuilder()
        ///     .SetVisualHeight(420)
        ///     .AddStep(
        ///         @&lt;div class="w-100 h-100 d-flex align-items-center justify-content-center rounded-3"
        ///               style="background:linear-gradient(135deg,#667eea,#764ba2);"&gt;
        ///             &lt;i class="bi bi-lightning-charge text-white" style="font-size:4rem;"&gt;&lt;/i&gt;
        ///         &lt;/div&gt;,
        ///         "Intelligent autocompletion",
        ///         "Understands the full context of your project.",
        ///         "bi-lightning-charge"))
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public StickyScrollEffectBuilder AddStep(Func<dynamic, IHtmlContent> template, string title, string description, string? icon = null)
        {
            _steps.Add(new StickyScrollEffectStep(template(null!), title, description, icon));
            return this;
        }

        /// <summary>
        ///     Adds a standard gradient step: the visual panel shows an icon on a gradient background with a short label;
        ///     the right column displays the step title, description and icon.
        ///     Use this overload for the common gradient-visual layout; use the template overload for fully custom visuals.
        /// </summary>
        /// <param name="icon">
        ///     Bootstrap icon class shown in both the visual panel and the right-side step header, e.g.
        ///     <c>"bi-lightning-charge"</c>.
        /// </param>
        /// <param name="visualLabel">Short label shown inside the visual panel below the icon, e.g. <c>"Autocompletion"</c>.</param>
        /// <param name="title">Step heading displayed in the right column.</param>
        /// <param name="description">Body text displayed below the title in the right column.</param>
        /// <param name="gradientStart">CSS color for the top-left of the visual gradient, e.g. <c>"#667eea"</c>.</param>
        /// <param name="gradientEnd">CSS color for the bottom-right of the visual gradient, e.g. <c>"#764ba2"</c>.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public StickyScrollEffectBuilder AddStep(
            string icon,
            string visualLabel,
            string title,
            string description,
            string gradientStart,
            string gradientEnd
        )
        {
            var enc = HtmlEncoder.Default;
            var visual = new HtmlString(
                $"""
                 <div class="w-100 h-100 d-flex flex-column align-items-center justify-content-center text-center"
                      style="background:linear-gradient(135deg,{enc.Encode(gradientStart)},{enc.Encode(gradientEnd)});">
                     <i class="bi {enc.Encode(icon)} text-white mb-3" style="font-size:4rem;"></i>
                     <h4 class="text-white mb-0">{enc.Encode(visualLabel)}</h4>
                 </div>
                 """);
            _steps.Add(new StickyScrollEffectStep(visual, title, description, icon));
            return this;
        }

        /// <summary>Sets the minimum height of each scrollable step item as a viewport height percentage (default: 50).</summary>
        /// <param name="vh">Minimum height in <c>vh</c> units controlling how much vertical scroll space each step occupies.</param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> increase to <c>70</c> or more when steps have short descriptions so the user has
        ///         enough scroll distance to see each visual clearly. Decrease for very long descriptions that
        ///         naturally occupy more vertical space.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> if the active visual changes too quickly (steps skip), increase this value.
        ///         The <c>IntersectionObserver</c> fires when a step item crosses the central 20 % of the viewport,
        ///         so each item must be tall enough to remain visible for a moment as the user scrolls.
        ///     </para>
        /// </remarks>
        [Documented]
        public StickyScrollEffectBuilder SetItemMinHeight(int vh)
        {
            _itemMinHeightVh = vh;
            return this;
        }

        /// <summary>Sets the <c>top</c> CSS offset in pixels at which the visual panel sticks when scrolling (default: 80).</summary>
        /// <param name="px">Distance in pixels from the top of the viewport to the sticky panel's resting position.</param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> match this value to the height of your fixed navbar so the sticky panel never slides
        ///         underneath it.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> set to <c>0</c> or a small value such as <c>20</c> when there is no fixed header.
        ///         The default of <c>80</c> fits a standard Bootstrap navbar.
        ///     </para>
        /// </remarks>
        [Documented]
        public StickyScrollEffectBuilder SetStickyOffset(int px)
        {
            _stickyOffsetPx = px;
            return this;
        }

        /// <summary>Sets the duration of the slide animation between visuals in seconds (default: 0.5).</summary>
        /// <param name="seconds">Duration in seconds for the <c>transform: translateY</c> transition.</param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Tips:</b> values between <c>0.3</c> and <c>0.7</c> feel natural. Below <c>0.2</c> the change
        ///         is abrupt; above <c>1.0</c> it feels sluggish when the user scrolls quickly through multiple steps.
        ///         The transition uses a <c>cubic-bezier(0.4, 0, 0.2, 1)</c> easing for a material-style feel.
        ///     </para>
        /// </remarks>
        [Documented]
        public StickyScrollEffectBuilder SetTransitionDuration(decimal seconds)
        {
            _transitionDuration = seconds;
            return this;
        }

        /// <summary>Sets the height in pixels of the sticky visual panel (default: 400).</summary>
        /// <param name="px">Height of the left sticky panel in pixels.</param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> adjust to match the natural dimensions of your visual content — taller for
        ///         landscape images or data charts, shorter for compact icon illustrations.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> values between 350 and 600 px work well for most desktop layouts. The panel
        ///         always fills its container width; only the height is controlled here. On mobile the layout
        ///         collapses to a single column and the panel becomes <c>position: relative</c>.
        ///     </para>
        /// </remarks>
        [Documented]
        public StickyScrollEffectBuilder SetVisualHeight(int px)
        {
            _visualHeightPx = px;
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
            if (_steps.Count == 0) return;

            PageInformation page = PageRegistry.GetOrCreatePageInformation(_html.ViewContext.HttpContext);
            page.SetStylesheet("/css/innerEffects/StickyScrollEffect.css");
            page.SetScriptFile("/js/innerEffects/StickyScrollEffect.js");

            var ci = CultureInfo.InvariantCulture;
            var style = $"--eb-ss-offset:{_stickyOffsetPx}px;--eb-ss-height:{_visualHeightPx}px;--eb-ss-duration:{_transitionDuration.ToString(ci)}s;--eb-ss-min-height:{_itemMinHeightVh}vh;";

            writer.Write($"<div class=\"eb-sticky-scroll\" style=\"{style}\">");

            writer.Write("<div class=\"eb-ss-left\">");
            for (int i = 0; i < _steps.Count; i++)
            {
                writer.Write($"<div class=\"eb-ss-visual\" data-index=\"{i}\">");
                _steps[i].Visual.WriteTo(writer, encoder);
                writer.Write("</div>");
            }

            writer.Write("</div>");

            writer.Write("<div class=\"eb-ss-right\">");
            for (int i = 0; i < _steps.Count; i++)
            {
                var step = _steps[i];
                writer.Write($"<div class=\"eb-ss-item\" data-index=\"{i}\">");
                if (!string.IsNullOrEmpty(step.Icon)) writer.Write($"<div class=\"eb-ss-icon\"><i class=\"bi {HtmlEncoder.Default.Encode(step.Icon)}\"></i></div>");
                writer.Write($"<h3 class=\"eb-ss-title\">{HtmlEncoder.Default.Encode(step.Title)}</h3>");
                writer.Write($"<p class=\"eb-ss-desc\">{HtmlEncoder.Default.Encode(step.Description)}</p>");
                writer.Write("</div>");
            }

            writer.Write("</div>");

            writer.Write("</div>");
        }

        #endregion

        #endregion
    }
}