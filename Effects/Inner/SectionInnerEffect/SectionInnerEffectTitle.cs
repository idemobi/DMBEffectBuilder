#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj SectionInnerTitle.cs create at 2026/04/14
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using System.Text;

namespace DMBEffectBuilder
{
    public sealed class SectionInnerEffectTitle : TitleBuilderBase<SectionInnerEffectBuilder, SectionInnerEffectTitle>
    {
        #region Instance constructors

        public SectionInnerEffectTitle(SectionInnerEffectBuilder parent) : base(parent, parent.HtmlHelper)
        {
        }

        #endregion

        #region Instance methods

        public override SectionInnerEffectBuilder Build()
        {
            var sb = new StringBuilder();
            sb.Append("<div class=\"eb-section-inner-header\">");
            sb.Append(BuildCoreTitleHtml());
            sb.Append("</div>");

            _parent.WriteHeader(sb.ToString());
            return _parent;
        }

        #endregion
    }
}
