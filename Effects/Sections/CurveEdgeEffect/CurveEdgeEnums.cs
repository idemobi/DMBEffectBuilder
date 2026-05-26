#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj CurveEdgeEnums.cs create at 2026/04/22
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    /// Specifies which edge of the section the curve is applied to.
    /// </summary>
    public enum CurveEdge
    {
        /// <summary>Apply curve to the top edge.</summary>
        Top,
        /// <summary>Apply curve to the bottom edge.</summary>
        Bottom,
        /// <summary>Apply curve to both top and bottom edges.</summary>
        Both
    }

    /// <summary>
    /// Specifies the curvature style.
    /// </summary>
    public enum Curvature
    {
        /// <summary>Curved outward (convex).</summary>
        Convex,
        /// <summary>Curved inward (concave).</summary>
        Concave
    }
}
