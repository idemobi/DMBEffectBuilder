#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj DiagonalEdgeEnums.cs create at 2026/04/16
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    /// Defines diagonal edge options used by DMBEffectBuilder effects.
    /// </summary>
    public enum DiagonalEdge
    {
        /// <summary>
        /// Applies the bottom diagonal edge option.
        /// </summary>
        Bottom,
        /// <summary>
        /// Applies the top diagonal edge option.
        /// </summary>
        Top,
        /// <summary>
        /// Applies the both diagonal edge option.
        /// </summary>
        Both
    }

    /// <summary>
    /// Defines diagonal direction options used by DMBEffectBuilder effects.
    /// </summary>
    public enum DiagonalDirection
    {
        /// <summary>
        /// Applies the left to right diagonal direction option.
        /// </summary>
        LeftToRight,
        /// <summary>
        /// Applies the right to left diagonal direction option.
        /// </summary>
        RightToLeft
    }
}
