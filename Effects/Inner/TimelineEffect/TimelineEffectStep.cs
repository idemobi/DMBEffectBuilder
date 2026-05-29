#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using Microsoft.AspNetCore.Html;

#endregion

namespace DMBEffectBuilder
{
    internal class TimelineEffectStep
    {
        #region Instance fields and properties

        internal string? Description { get; }
        internal string? Icon { get; }
        internal string Title { get; }
        internal IHtmlContent? Visual { get; }

        #endregion

        #region Instance constructors and destructors

        internal TimelineEffectStep(IHtmlContent? visual, string title, string? description, string? icon)
        {
            Visual = visual;
            Title = title;
            Description = description;
            Icon = icon;
        }

        #endregion
    }
}