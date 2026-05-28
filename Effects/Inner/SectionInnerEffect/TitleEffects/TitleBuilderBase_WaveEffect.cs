#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj TitleBuilderBase_WaveEffect.cs create at 2026/04/15
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    /// <inheritdoc/>
    public abstract partial class TitleBuilderBase<TParent, TSelf>
        where TSelf : TitleBuilderBase<TParent, TSelf>
    {
        #region Instance fields and properties

        private bool _wave;
        private decimal _waveDuration = 1m;

        #endregion

        #region Instance methods

        /// <summary>
        /// Enables the wave title effect for the generated section heading.
        /// </summary>
        /// <param name="durationSeconds">The duration seconds value used by the title effect.</param>
        /// <returns>The current title builder instance for fluent chaining.</returns>
        public TSelf SetWaveEffect(decimal durationSeconds = 1m)
        {
            _wave = true;
            _waveDuration = durationSeconds;
            return (TSelf)this;
        }

        #endregion
    }
}
