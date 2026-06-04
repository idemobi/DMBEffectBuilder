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

        private bool _letterCollapse;
        private decimal _letterCollapseDuration = 1m;

        #endregion

        #region Instance methods

        /// <summary>
        ///     Enables the letter collapse title effect for the generated section heading.
        /// </summary>
        /// <param name="durationSeconds">The duration seconds value used by the title effect.</param>
        /// <returns>The current title builder instance for fluent chaining.</returns>
        public TSelf SetLetterCollapseEffect(decimal durationSeconds = 1m)
        {
            _letterCollapse = true;
            _letterCollapseDuration = durationSeconds;
            return (TSelf)this;
        }

        #endregion
    }
}