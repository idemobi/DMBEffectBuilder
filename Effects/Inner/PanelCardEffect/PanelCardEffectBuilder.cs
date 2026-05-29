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
    ///     Fluent builder for the Panel Card effect: a rounded gradient card with tab navigation.
    ///     Each tab switches to a different content panel; the gradient colour is derived from a hue value
    ///     (explicit or auto-assigned from a built-in palette).
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <b>Use cases:</b> feature showcases, step-by-step explanations, product highlights.
    ///     </para>
    ///     <para>
    ///         <b>How it works:</b> each <c>AddPanel</c> call registers a content panel with its own
    ///         gradient background. Tab buttons are overlaid at the top; clicking one fades in the matching panel.
    ///     </para>
    ///     <para>
    ///         <b>Tips:</b> use <c>eb-pc-badge</c>, <c>eb-pc-title</c> and <c>eb-pc-desc</c> utility classes
    ///         for consistent white typography inside panels.
    ///     </para>
    /// </remarks>
    [Documented]
    public sealed class PanelCardEffectBuilder : IHtmlContent
    {
        #region Static fields and properties

        private static readonly int[] DefaultHues = { 260, 320, 205, 145 };
        private static readonly int[] DefaultHues2 = { 295, 355, 240, 175 };

        #endregion

        #region Instance fields and properties

        private int _heightPx = 420;
        private readonly IHtmlHelper _html;
        private readonly List<PanelCardPanel> _panels = new();

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PanelCardEffectBuilder" /> class.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to register effect assets.</param>
        public PanelCardEffectBuilder(IHtmlHelper html)
        {
            _html = html;
        }

        #endregion

        #region Instance methods

        /// <summary>Adds a content panel. The first panel added is active by default.</summary>
        /// <param name="label">Tab button label.</param>
        /// <param name="content">Razor template delegate rendered inside the panel.</param>
        /// <param name="hue">Optional HSL base hue (0–360) for the gradient background. Auto-assigned if omitted.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public PanelCardEffectBuilder AddPanel(string label, Func<dynamic, IHtmlContent> content, int? hue = null)
        {
            _panels.Add(new PanelCardPanel(label, content(null!), hue));
            return this;
        }

        /// <summary>
        ///     Adds a standard panel with an icon, badge, heading, description and an optional button.
        ///     Use this overload for the common layout; use the template overload for fully custom content.
        /// </summary>
        /// <param name="label">Tab button label.</param>
        /// <param name="icon">Bootstrap icon class, e.g. <c>"bi-search"</c>.</param>
        /// <param name="badge">Short badge text displayed above the heading, e.g. <c>"Step 01"</c>.</param>
        /// <param name="heading">Main heading text.</param>
        /// <param name="description">Body description text.</param>
        /// <param name="buttonUrl">Optional URL for the button. Pass <c>null</c> to hide the button.</param>
        /// <param name="buttonLabel">Button label text. Default: <c>"Learn more"</c>.</param>
        /// <param name="hue">Optional HSL base hue (0–360) for the gradient background. Auto-assigned if omitted.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public PanelCardEffectBuilder AddPanel(
            string label,
            string icon,
            string badge,
            string heading,
            string description,
            string? buttonUrl = null,
            string buttonLabel = "Learn more",
            int? hue = null
        )
        {
            var enc = HtmlEncoder.Default;
            var sb = new System.Text.StringBuilder();
            sb.Append("""<div class="d-flex align-items-center gap-4 w-100 px-2">""");
            sb.Append($"""<i class="bi {enc.Encode(icon)} flex-shrink-0 text-white" style="font-size:3.5rem;"></i>""");
            sb.Append("<div>");
            sb.Append($"""<span class="eb-pc-badge mb-2 d-inline-block">{enc.Encode(badge)}</span>""");
            sb.Append($"""<h2 class="fw-bold text-white mb-1">{enc.Encode(heading)}</h2>""");
            sb.Append($"""<p class="fw-normal text-white-50 mb-3">{enc.Encode(description)}</p>""");
            if (buttonUrl != null) sb.Append($"""<a href="{enc.Encode(buttonUrl)}" class="btn btn-outline-light">{enc.Encode(buttonLabel)}</a>""");
            sb.Append("</div></div>");
            _panels.Add(new PanelCardPanel(label, new HtmlString(sb.ToString()), hue));
            return this;
        }

        /// <summary>Sets the height of the card in pixels (default: 420).</summary>
        /// <param name="px">Card height in pixels.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public PanelCardEffectBuilder SetHeight(int px)
        {
            _heightPx = px;
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
            page.SetStylesheet("/css/innerEffects/PanelCardEffect.css");
            page.SetScriptFile("/js/innerEffects/PanelCardEffect.js");

            writer.Write("<div class=\"eb-pcard\">");

            if (_panels.Count > 1)
            {
                writer.Write("<div class=\"eb-pc-tabs\">");
                for (int i = 0; i < _panels.Count; i++)
                {
                    var active = i == 0 ? " is-active" : string.Empty;
                    writer.Write($"<button class=\"eb-pc-tab{active}\" data-panel=\"{i}\">{HtmlEncoder.Default.Encode(_panels[i].Label)}</button>");
                }

                writer.Write("</div>");
            }

            writer.Write($"<div class=\"eb-pc-panels\" style=\"--eb-pc-height:{_heightPx}px;\">");
            for (int i = 0; i < _panels.Count; i++)
            {
                var active = i == 0 ? " is-active" : string.Empty;
                int hue = _panels[i].Hue ?? DefaultHues[i % DefaultHues.Length];
                int hue2 = _panels[i].Hue.HasValue ? hue + 40 : DefaultHues2[i % DefaultHues2.Length];
                writer.Write($"<div class=\"eb-pc-panel{active}\" data-panel=\"{i}\" style=\"--eb-pc-hue:{hue};--eb-pc-hue2:{hue2};\">");
                _panels[i].Content.WriteTo(writer, encoder);
                writer.Write("</div>");
            }

            writer.Write("</div>");

            writer.Write("</div>");
        }

        #endregion

        #endregion
    }
}