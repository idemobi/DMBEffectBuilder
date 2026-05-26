#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj CylinderShapeMode.cs create at 2026/05/07
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    /// <summary>Defines the spatial orientation of the cylinder carousel.</summary>
    public enum CylinderShapeMode
    {
        /// <summary>Opening of the cylinder faces the viewer — center cards appear closest and largest (natural perspective). Default.</summary>
        Concave,

        /// <summary>Back of the cylinder faces the viewer — side cards are closer to the viewer and appear larger; the center card recedes.</summary>
        Convex,

        /// <summary>Flat horizontal marquee — all cards face the viewer with no 3D depth, continuous scroll animation.</summary>
        Flat
    }
}
