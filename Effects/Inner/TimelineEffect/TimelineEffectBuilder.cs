#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj TimelineEffectBuilder.cs create at 2026/05/06
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
    /// Fluent builder for a horizontal timeline: numbered dots connected by a progress line,
    /// clicking a dot activates the corresponding step and fades its content into view below.
    /// Add steps with <see cref="AddStep(string,string,string)"/>, enable auto-advance with
    /// <see cref="SetAutoPlay"/>, and tint the accent with <see cref="SetAccentColor"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Use cases:</b> how-it-works sections, onboarding flows, process explanations, project
    /// roadmaps, and any sequential content where order matters.
    /// </para>
    /// <para>
    /// <b>How it works:</b> renders a track of clickable dots above a panel zone. The progress line
    /// fills to the position of the active dot. Each panel fades in via <c>opacity</c> transitions.
    /// When auto-play is enabled, steps advance on a configurable interval and wrap back to the first.
    /// </para>
    /// <para>
    /// <b>Combinations:</b> steps can include an optional visual panel (any <see cref="IHtmlContent"/>)
    /// that displays alongside the description. Use the <c>@&lt;div&gt;...&lt;/div&gt;</c> Razor
    /// syntax for inline visuals. Works well inside a <c>container-lg</c> or a full-width section.
    /// </para>
    /// <para>
    /// <b>Tips:</b> 3–6 steps work best. Keep labels short (2–4 words) so they fit comfortably
    /// below each dot. Set <see cref="SetAutoPlay"/> to 0 to disable auto-advance (default).
    /// </para>
    /// </remarks>
    [Documented]
    public sealed class TimelineEffectBuilder : IHtmlContent
    {
        private readonly IHtmlHelper _html;
        private readonly List<TimelineEffectStep> _steps = new();
        private decimal _transitionDuration = 0.4m;
        private int _autoPlayMs = 0;
        private string _accentColor = "var(--bs-primary,#0d6efd)";

        public TimelineEffectBuilder(IHtmlHelper html)
        {
            _html = html;
        }

        /// <summary>Adds a step with a title and optional description — no visual panel.</summary>
        /// <param name="title">Step title shown in the dot label and the content area.</param>
        /// <param name="description">Optional body text shown below the title when the step is active.</param>
        /// <param name="icon">Optional Bootstrap icon class (e.g. <c>bi-rocket</c>) shown inside the dot instead of the step number.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public TimelineEffectBuilder AddStep(string title, string? description = null, string? icon = null)
        {
            _steps.Add(new TimelineEffectStep(null, title, description, icon));
            return this;
        }

        /// <summary>Adds a step with a Razor template delegate as the visual panel — preferred in <c>.cshtml</c> views.</summary>
        /// <param name="template">Razor template delegate (<c>@&lt;div&gt;...&lt;/div&gt;</c>) for the visual.</param>
        /// <param name="title">Step title shown in the dot label and the content area.</param>
        /// <param name="description">Optional body text shown below the title when the step is active.</param>
        /// <param name="icon">Optional Bootstrap icon class shown inside the dot instead of the step number.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public TimelineEffectBuilder AddStep(Func<dynamic, IHtmlContent> template, string title, string? description = null, string? icon = null)
        {
            _steps.Add(new TimelineEffectStep(template(null!), title, description, icon));
            return this;
        }

        /// <summary>Adds a step with pre-built <see cref="IHtmlContent"/> as the visual panel.</summary>
        /// <param name="visual">HTML content for the visual panel displayed alongside the description.</param>
        /// <param name="title">Step title shown in the dot label and the content area.</param>
        /// <param name="description">Optional body text shown below the title when the step is active.</param>
        /// <param name="icon">Optional Bootstrap icon class shown inside the dot instead of the step number.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public TimelineEffectBuilder AddStep(IHtmlContent visual, string title, string? description = null, string? icon = null)
        {
            _steps.Add(new TimelineEffectStep(visual, title, description, icon));
            return this;
        }

        /// <summary>Sets the duration of the fade transition between steps in seconds (default: 0.4).</summary>
        /// <param name="seconds">Duration in seconds for the <c>opacity</c> transition.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public TimelineEffectBuilder SetTransitionDuration(decimal seconds)
        {
            _transitionDuration = seconds;
            return this;
        }

        /// <summary>Enables auto-advance: steps cycle automatically on the given interval (default: 0 = disabled).</summary>
        /// <param name="ms">Interval in milliseconds between auto-advances. Pass <c>0</c> to disable.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public TimelineEffectBuilder SetAutoPlay(int ms)
        {
            _autoPlayMs = ms;
            return this;
        }

        /// <summary>Sets the accent color used for active dots and the progress line (default: <c>var(--bs-primary)</c>).</summary>
        /// <param name="color">Any valid CSS color value.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public TimelineEffectBuilder SetAccentColor(string color)
        {
            _accentColor = color;
            return this;
        }

        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            if (_steps.Count == 0) return;

            PageInformation page = PageRegistry.GetOrCreatePageInformation(_html.ViewContext.HttpContext);
            page.SetStylesheet("/css/innerEffects/TimelineEffect.css");
            page.SetScriptFile("/js/innerEffects/TimelineEffect.js");

            var ci = CultureInfo.InvariantCulture;
            var style = $"--eb-tl-duration:{_transitionDuration.ToString(ci)}s;--eb-tl-color:{_accentColor};--eb-tl-autoplay:{_autoPlayMs};--eb-tl-step-count:{_steps.Count};";

            writer.Write($"<div class=\"eb-timeline\" style=\"{style}\">");

            writer.Write("<div class=\"eb-tl-track\">");
            writer.Write("<div class=\"eb-tl-line\"><div class=\"eb-tl-fill\"></div></div>");
            writer.Write("<div class=\"eb-tl-dots\">");
            for (int i = 0; i < _steps.Count; i++)
            {
                var step = _steps[i];
                string activeClass = i == 0 ? " is-active" : string.Empty;
                writer.Write($"<button class=\"eb-tl-dot{activeClass}\" data-index=\"{i}\" title=\"{HtmlEncoder.Default.Encode(step.Title)}\">");
                writer.Write("<div class=\"eb-tl-dot-circle\">");
                if (!string.IsNullOrEmpty(step.Icon))
                    writer.Write($"<i class=\"bi {HtmlEncoder.Default.Encode(step.Icon)}\"></i>");
                else
                    writer.Write($"<span>{i + 1}</span>");
                writer.Write("</div>");
                writer.Write($"<span class=\"eb-tl-dot-label\">{HtmlEncoder.Default.Encode(step.Title)}</span>");
                writer.Write("</button>");
            }
            writer.Write("</div>");
            writer.Write("</div>");

            writer.Write("<div class=\"eb-tl-panels\">");
            for (int i = 0; i < _steps.Count; i++)
            {
                var step = _steps[i];
                string activeClass = i == 0 ? " is-active" : string.Empty;
                string visualClass = step.Visual != null ? " has-visual" : string.Empty;
                writer.Write($"<div class=\"eb-tl-panel{activeClass}{visualClass}\" data-index=\"{i}\">");

                if (step.Visual != null)
                {
                    writer.Write("<div class=\"eb-tl-panel-visual\">");
                    step.Visual.WriteTo(writer, encoder);
                    writer.Write("</div>");
                }

                writer.Write("<div class=\"eb-tl-panel-text\">");
                writer.Write($"<h3 class=\"eb-tl-title\">{HtmlEncoder.Default.Encode(step.Title)}</h3>");
                if (!string.IsNullOrEmpty(step.Description))
                    writer.Write($"<p class=\"eb-tl-desc\">{HtmlEncoder.Default.Encode(step.Description)}</p>");
                writer.Write("</div>");

                writer.Write("</div>");
            }
            writer.Write("</div>");

            writer.Write("</div>");
        }
    }
}
