#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj TitleBuilderBase_ScrambleEffect.cs create at 2026/04/15
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    /// <inheritdoc/>
    public abstract partial class TitleBuilderBase<TParent, TSelf>
        where TSelf : TitleBuilderBase<TParent, TSelf>
    {
        #region Instance fields and properties

        private bool _scramble;
        private decimal _scrambleDuration = 2m;

        #endregion

        #region Instance methods

        /// <summary>
        /// Enables the scramble title effect for the generated section heading.
        /// </summary>
        /// <param name="durationSeconds">The duration seconds value used by the title effect.</param>
        /// <returns>The current title builder instance for fluent chaining.</returns>
        public TSelf SetScrambleEffect(decimal durationSeconds = 2m)
        {
            _scramble = true;
            _scrambleDuration = durationSeconds;
            return (TSelf)this;
        }

        #endregion
    }
}
