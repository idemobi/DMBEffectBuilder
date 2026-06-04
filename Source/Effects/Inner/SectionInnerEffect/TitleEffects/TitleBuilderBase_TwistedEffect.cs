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

        private decimal _twistAngle = -3m;

        private bool _twisted;

        #endregion

        #region Instance methods

        /// <summary>
        ///     Enables the twisted title effect for the generated section heading.
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