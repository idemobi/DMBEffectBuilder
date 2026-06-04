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

        private decimal _blurDuration = 1.5m;

        private bool _blurReveal;
        private decimal _blurStart = 12m;

        #endregion

        #region Instance methods

        /// <summary>
        ///     Enables the blur reveal title effect for the generated section heading.
        /// </summary>
        /// <param name="startBlurPx">The start blur px value used by the title effect.</param>
        /// <param name="durationSeconds">The duration seconds value used by the title effect.</param>
        /// <returns>The current title builder instance for fluent chaining.</returns>
        public TSelf SetBlurRevealEffect(decimal startBlurPx = 12m, decimal durationSeconds = 1.5m)
        {
            _blurReveal = true;
            _blurStart = startBlurPx;
            _blurDuration = durationSeconds;
            return (TSelf)this;
        }

        #endregion
    }
}