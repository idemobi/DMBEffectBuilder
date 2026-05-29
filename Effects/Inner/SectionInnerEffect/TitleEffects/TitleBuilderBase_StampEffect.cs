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

        private bool _stamp;
        private decimal _stampDuration = 0.6m;

        #endregion

        #region Instance methods

        /// <summary>
        ///     Enables the stamp title effect for the generated section heading.
        /// </summary>
        /// <param name="durationSeconds">The duration seconds value used by the title effect.</param>
        /// <returns>The current title builder instance for fluent chaining.</returns>
        public TSelf SetStampEffect(decimal durationSeconds = 0.6m)
        {
            _stamp = true;
            _stampDuration = durationSeconds;
            return (TSelf)this;
        }

        #endregion
    }
}