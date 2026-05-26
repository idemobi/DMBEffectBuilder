#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj CardFlipEffectCard.cs create at 2026/05/06
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using Microsoft.AspNetCore.Html;

namespace DMBEffectBuilder
{
    internal class CardFlipEffectCard
    {
        internal IHtmlContent Front { get; }
        internal IHtmlContent Back { get; }

        internal CardFlipEffectCard(IHtmlContent front, IHtmlContent back)
        {
            Front = front;
            Back = back;
        }
    }
}
