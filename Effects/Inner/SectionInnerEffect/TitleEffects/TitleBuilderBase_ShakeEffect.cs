#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj TitleBuilderBase_ShakeEffect.cs create at 2026/04/15
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    /// <inheritdoc/>
    public abstract partial class TitleBuilderBase<TParent, TSelf>
        where TSelf : TitleBuilderBase<TParent, TSelf>
    {
        #region Instance fields and properties

        private bool _shake;
        private decimal _shakeSpeed = 4m;

        #endregion

        #region Instance methods

        /// <summary>
        /// Enables the shake title effect for the generated section heading.
        /// </summary>
        /// <param name="speedSeconds">The speed seconds value used by the title effect.</param>
        /// <returns>The current title builder instance for fluent chaining.</returns>
        public TSelf SetShakeEffect(decimal speedSeconds = 4m)
        {
            _shake = true;
            _shakeSpeed = speedSeconds;
            return (TSelf)this;
        }

        #endregion
    }
}
