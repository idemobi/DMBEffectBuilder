#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using Microsoft.AspNetCore.Html;

#endregion

namespace DMBEffectBuilder
{
    internal class FeatureTabEffectItem
    {
        #region Instance fields and properties

        internal string? Description { get; }
        internal string Title { get; }
        internal IHtmlContent Visual { get; }

        #endregion

        #region Instance constructors and destructors

        internal FeatureTabEffectItem(IHtmlContent visual, string title, string? description)
        {
            Visual = visual;
            Title = title;
            Description = description;
        }

        #endregion
    }
}