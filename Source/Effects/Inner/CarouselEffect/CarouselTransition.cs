#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

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