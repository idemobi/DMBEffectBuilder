#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj CurtainEffectEnums.cs create at 2026/04/17
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    /// Controls where the two curtain panels stop.
    /// </summary>
    public enum CurtainStop
    {
        /// <summary>Left and right panels stop at exactly 50% — they touch in the center.</summary>
        Meet,

        /// <summary>Both panels go past 50% — they overlap in the center.</summary>
        Overlap,

        /// <summary>Both panels stop before 50% — a gap remains in the center.</summary>
        Gap
    }
}
