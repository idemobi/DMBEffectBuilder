#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using Microsoft.AspNetCore.Html;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>A named content panel for the Panel Card effect, with an optional gradient hue override.</summary>
    public sealed class PanelCardPanel
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets the HTML content rendered inside the panel body.
        /// </summary>
        public IHtmlContent Content { get; }

        /// <summary>
        ///     Gets the optional hue override used by the panel gradient.
        /// </summary>
        public int? Hue { get; }

        /// <summary>
        ///     Gets the label rendered for the panel selector.
        /// </summary>
        public string Label { get; }

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="PanelCardPanel" /> class.
        /// </summary>
        /// <param name="label">The label rendered for the panel selector.</param>
        /// <param name="content">The HTML content rendered inside the panel body.</param>
        /// <param name="hue">An optional hue override used by the panel gradient.</param>
        public PanelCardPanel(string label, IHtmlContent content, int? hue = null)
        {
            Label = label;
            Content = content;
            Hue = hue;
        }

        #endregion
    }
}