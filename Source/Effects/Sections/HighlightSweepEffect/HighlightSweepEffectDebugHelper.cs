#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using Microsoft.AspNetCore.Html;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Provides debug helper hooks for the highlight sweep section effect.
    /// </summary>
    public static class HighlightSweepEffectDebugHelper
    {
        #region Static methods

        /// <summary>
        ///     Renders the optional custom debug form for the highlight sweep effect.
        /// </summary>
        /// <param name="model">The debug model used by the form.</param>
        /// <returns>The rendered debug form content, or an empty <see cref="HtmlString" /> when no custom form is required.</returns>
        public static IHtmlContent RenderForm(HighlightSweepEffectDebugModel model)
        {
            /*var sb = new StringBuilder();
            string sectionIdJs = JavaScriptEncoder.Default.Encode(model.SectionId);
            string opacityInputId = $"{model.SectionId}_sweep_opacity";
            string opacityInputIdJs = JavaScriptEncoder.Default.Encode(opacityInputId);

            sb.Append("$<div>");
            sb.Append("$<label>Opacity: ");
            sb.Append("$<input id='{opacityInputId}' type='range min='0' max='1' step='0.1' value='{model.Opacity}'/>");
            sb.Append("$</label>");
            sb.Append("$</div>");

            return new HtmlString(sb.ToString());*/
            return HtmlString.Empty;
        }

        #endregion
    }
}