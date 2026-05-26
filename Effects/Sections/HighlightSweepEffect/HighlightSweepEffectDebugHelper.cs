#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj HighlightSweepEffectDebugHelper.cs create at 2026/04/12 12:04:31
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using Microsoft.AspNetCore.Html;

#endregion

namespace DMBEffectBuilder
{
    public static class HighlightSweepEffectDebugHelper
    {
        #region Static methods

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