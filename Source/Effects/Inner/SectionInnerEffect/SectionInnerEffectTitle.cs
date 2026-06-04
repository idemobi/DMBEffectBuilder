#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.Text;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Builds and registers the title markup for a <see cref="SectionInnerEffectBuilder" />.
    /// </summary>
    public sealed class SectionInnerEffectTitle : TitleBuilderBase<SectionInnerEffectBuilder, SectionInnerEffectTitle>
    {
        #region Instance constructors and destructors

        #region Instance constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SectionInnerEffectTitle" /> class.
        /// </summary>
        /// <param name="parent">The parent section inner effect builder that receives the generated title.</param>
        public SectionInnerEffectTitle(SectionInnerEffectBuilder parent)
            : base(parent, parent.HtmlHelper)
        {
        }

        #endregion

        #endregion

        #region Instance methods

        /// <inheritdoc />
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