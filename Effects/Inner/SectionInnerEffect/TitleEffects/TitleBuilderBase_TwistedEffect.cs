#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj TitleBuilderBase_TwistedEffect.cs create at 2026/04/15
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    /// <inheritdoc/>
    public abstract partial class TitleBuilderBase<TParent, TSelf>
        where TSelf : TitleBuilderBase<TParent, TSelf>
    {
        #region Instance fields and properties

        private bool _twisted;
        private decimal _twistAngle = -3m;

        #endregion

        #region Instance methods

        /// <summary>
        /// Enables the twisted title effect for the generated section heading.
        /// </summary>
        /// <param name="angle">The angle value used by the title effect.</param>
        /// <returns>The current title builder instance for fluent chaining.</returns>
        public TSelf SetTwistedEffect(decimal angle = -3m)
        {
            _twisted = true;
            _twistAngle = angle;
            return (TSelf)this;
        }

        #endregion
    }
}
