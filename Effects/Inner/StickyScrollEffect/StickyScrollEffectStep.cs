#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj StickyScrollEffectStep.cs create at 2026/05/06
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using Microsoft.AspNetCore.Html;

namespace DMBEffectBuilder
{
    internal class StickyScrollEffectStep
    {
        internal IHtmlContent Visual { get; }
        internal string Title { get; }
        internal string Description { get; }
        internal string? Icon { get; }

        internal StickyScrollEffectStep(IHtmlContent visual, string title, string description, string? icon)
        {
            Visual = visual;
            Title = title;
            Description = description;
            Icon = icon;
        }
    }
}
