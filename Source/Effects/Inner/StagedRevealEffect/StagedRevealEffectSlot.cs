#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

using System;
using System.Collections.Generic;

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Represents one column in a <see cref="StagedRevealEffectBuilder" />.
    ///     A slot holds one or more direction groups displayed simultaneously,
    ///     each occupying an equal fraction of the slot height.
    /// </summary>
    public sealed class StagedRevealEffectSlot
    {
        #region Instance methods

        /// <summary>
        ///     Adds a group of items that enter from <paramref name="direction" /> and cycle through each other.
        ///     Multiple groups within the same slot are stacked vertically and always visible simultaneously.
        /// </summary>
        public StagedRevealEffectSlot AddGroup(StagedRevealEffectDirection direction, params StagedRevealEffectItem[] items)
        {
            Groups.Add(new Group(direction, items));
            return this;
        }

        /// <summary>Sets the opacity of all elements inside this slot (0.0 = invisible, 1.0 = fully opaque).</summary>
        public StagedRevealEffectSlot SetOpacity(decimal opacity)
        {
            Opacity = Math.Clamp(opacity, 0m, 1m);
            return this;
        }

        #endregion

        #region Nested type: Group

        #region Private types

        internal sealed record Group(StagedRevealEffectDirection Direction, StagedRevealEffectItem[] Items);

        #endregion

        #endregion

        #region Instance fields

        internal List<Group> Groups { get; } = [];
        internal decimal? Opacity { get; private set; }

        #endregion
    }
}