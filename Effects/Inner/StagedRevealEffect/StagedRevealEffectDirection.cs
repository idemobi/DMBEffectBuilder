#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj StagedRevealDirection.cs create at 2026/04/27
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    /// <summary>Edge from which a slot slides into the staged-reveal container.</summary>
    public enum StagedRevealEffectDirection
    {
        /// <summary>Slot occupies the left column and slides in from the left edge.</summary>
        Left,
        /// <summary>Slot occupies the right column and slides in from the right edge.</summary>
        Right,
        /// <summary>Slot occupies the top row and slides in from the top edge.</summary>
        Top,
        /// <summary>Slot occupies the bottom row and slides in from the bottom edge.</summary>
        Bottom
    }
}
