#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj CarouselTransition.cs create at 2026/05/06
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    /// <summary>Defines the transition animation used between carousel slides.</summary>
    public enum CarouselTransition
    {
        /// <summary>Slides translate horizontally — the classic carousel movement.</summary>
        Slide,

        /// <summary>Slides cross-fade in place — good for full-bleed images or mixed content sizes.</summary>
        Fade
    }
}
