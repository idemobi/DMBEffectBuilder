#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj StagedRevealEffectContentType.cs create at 2026/04/27
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    /// <summary>Describes the type of content held by a <see cref="StagedRevealItem"/>.</summary>
    public enum StagedRevealEffectContentType
    {
        /// <summary>The item renders an <c>&lt;img&gt;</c> tag from a URL.</summary>
        Image,
        /// <summary>The item renders plain encoded text.</summary>
        Text
    }
}
