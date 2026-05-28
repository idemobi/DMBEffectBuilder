#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj PanelCardPanel.cs create at 2026/05/07
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using Microsoft.AspNetCore.Html;

namespace DMBEffectBuilder
{
    /// <summary>A named content panel for the Panel Card effect, with an optional gradient hue override.</summary>
    public sealed class PanelCardPanel
    {
        /// <summary>
        /// Gets the label rendered for the panel selector.
        /// </summary>
        public string Label   { get; }

        /// <summary>
        /// Gets the HTML content rendered inside the panel body.
        /// </summary>
        public IHtmlContent Content { get; }

        /// <summary>
        /// Gets the optional hue override used by the panel gradient.
        /// </summary>
        public int? Hue { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelCardPanel"/> class.
        /// </summary>
        /// <param name="label">The label rendered for the panel selector.</param>
        /// <param name="content">The HTML content rendered inside the panel body.</param>
        /// <param name="hue">An optional hue override used by the panel gradient.</param>
        public PanelCardPanel(string label, IHtmlContent content, int? hue = null)
        {
            Label   = label;
            Content = content;
            Hue     = hue;
        }
    }
}
