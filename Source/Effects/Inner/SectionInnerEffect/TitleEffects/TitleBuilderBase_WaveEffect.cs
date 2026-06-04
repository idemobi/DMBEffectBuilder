#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBEffectBuilder
{
    /// <inheritdoc />
    public abstract partial class TitleBuilderBase<TParent, TSelf>
        where TSelf : TitleBuilderBase<TParent, TSelf>
    {
        #region Instance fields and properties

        private bool _wave;
        private decimal _waveDuration = 1m;

        #endregion

        #region Instance methods

        /// <summary>
        ///     Enables the wave title effect for the generated section heading.
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