#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBEffectBuilder
{
    /// <summary>Defines the spatial orientation of the cylinder carousel.</summary>
    public enum CylinderShapeMode
    {
        /// <summary>
        ///     Opening of the cylinder faces the viewer — center cards appear closest and largest (natural perspective).
        ///     Default.
        /// </summary>
        Concave,

        /// <summary>
        ///     Back of the cylinder faces the viewer — side cards are closer to the viewer and appear larger; the center card
        ///     recedes.
        /// </summary>
        Convex,

        /// <summary>Flat horizontal marquee — all cards face the viewer with no 3D depth, continuous scroll animation.</summary>
        Flat
    }
}