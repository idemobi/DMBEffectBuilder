#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBEffectBuilder
{
    /// <summary>Direction from which the fan cards slide into view when the component enters the viewport.</summary>
    public enum FanEffectEntranceDirection
    {
        /// <summary>No entrance animation — cards are visible immediately.</summary>
        None,

        /// <summary>Cards slide in from the left edge.</summary>
        Left,

        /// <summary>Cards slide in from the right edge.</summary>
        Right,

        /// <summary>Cards slide in from the top edge.</summary>
        Top
    }
}