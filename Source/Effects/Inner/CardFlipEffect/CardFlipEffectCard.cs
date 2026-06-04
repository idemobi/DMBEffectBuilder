#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using Microsoft.AspNetCore.Html;

#endregion

namespace DMBEffectBuilder
{
    internal class CardFlipEffectCard
    {
        #region Instance fields and properties

        internal IHtmlContent Back { get; }
        internal IHtmlContent Front { get; }

        #endregion

        #region Instance constructors and destructors

        internal CardFlipEffectCard(IHtmlContent front, IHtmlContent back)
        {
            Front = front;
            Back = back;
        }

        #endregion
    }
}