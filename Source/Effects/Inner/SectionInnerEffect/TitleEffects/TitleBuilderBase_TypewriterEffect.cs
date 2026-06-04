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

        private bool _typewriter;
        private decimal _typewriterDuration = 2m;

        #endregion

        #region Instance methods

        /// <summary>
        ///     Enables the typewriter title effect for the generated section heading.
        /// </summary>
        /// <param name="durationSeconds">The duration seconds value used by the title effect.</param>
        /// <returns>The current title builder instance for fluent chaining.</returns>
        public TSelf SetTypewriterEffect(decimal durationSeconds = 2m)
        {
            _typewriter = true;
            _typewriterDuration = durationSeconds;
            return (TSelf)this;
        }

        #endregion
    }
}