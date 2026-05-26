#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj CylinderFilterMode.cs create at 2026/05/07
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    /// <summary>CSS visual filter applied uniformly to all cards in the cylinder carousel.</summary>
    public enum CylinderFilterMode
    {
        /// <summary>No filter applied.</summary>
        None,

        /// <summary>Cards are desaturated to grayscale.</summary>
        Grayscale,

        /// <summary>Cards are tinted with a warm sepia tone.</summary>
        Sepia,

        /// <summary>Cards are blurred and dimmed.</summary>
        Blur,

        /// <summary>Cards are darkened — hover reveals full brightness.</summary>
        Dim,

        /// <summary>Cards have their colours inverted — negative photo effect.</summary>
        Invert,

        /// <summary>Card hues are shifted 180° — dreamlike / fantasy tint.</summary>
        HueRotate,

        /// <summary>Card colours are heavily saturated — vivid neon / pop effect.</summary>
        Saturate
    }
}
