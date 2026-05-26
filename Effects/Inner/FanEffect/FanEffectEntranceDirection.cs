#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj FanEffectEntranceDirection.cs create at 2026/05/04
// ©2024-2026 idéMobi SARL FRANCE

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
