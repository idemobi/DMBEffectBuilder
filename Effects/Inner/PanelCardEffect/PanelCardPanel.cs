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
        public string Label   { get; }
        public IHtmlContent Content { get; }
        public int? Hue { get; }

        public PanelCardPanel(string label, IHtmlContent content, int? hue = null)
        {
            Label   = label;
            Content = content;
            Hue     = hue;
        }
    }
}
