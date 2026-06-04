#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Defines the axis or rotation point for the 3D flip effect on images.
    /// </summary>
    public enum FlipAxis
    {
        /// <summary>
        ///     Flip the image horizontally (rotates around the vertical Y axis).
        ///     The offset parameter controls the horizontal position of the rotation axis (0 = left, 0.5 = center, 1 = right).
        /// </summary>
        X,

        /// <summary>
        ///     Flip the image vertically (rotates around the horizontal X axis).
        ///     The offset parameter controls the vertical position of the rotation axis (0 = top, 0.5 = center, 1 = bottom).
        /// </summary>
        Y,

        /// <summary>
        ///     Flip the image in 3D around a specific point (uses both X and Y offset).
        ///     The offset parameter controls both horizontal and vertical position of the rotation point (0.5 = center, values map
        ///     to percentage position).
        /// </summary>
        XY
    }
}