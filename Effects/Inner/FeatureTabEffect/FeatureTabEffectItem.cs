#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj FeatureTabEffectItem.cs create at 2026/05/06
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using Microsoft.AspNetCore.Html;

namespace DMBEffectBuilder
{
    internal class FeatureTabEffectItem
    {
        internal IHtmlContent Visual { get; }
        internal string Title { get; }
        internal string? Description { get; }

        internal FeatureTabEffectItem(IHtmlContent visual, string title, string? description)
        {
            Visual = visual;
            Title = title;
            Description = description;
        }
    }
}
