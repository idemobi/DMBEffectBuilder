#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj TitleBuilderBase_LetterCollapseEffect.cs create at 2026/04/15
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    /// <inheritdoc/>
    public abstract partial class TitleBuilderBase<TParent, TSelf>
        where TSelf : TitleBuilderBase<TParent, TSelf>
    {
        #region Instance fields and properties

        private bool _letterCollapse;
        private decimal _letterCollapseDuration = 1m;

        #endregion

        #region Instance methods

        /// <summary>
        /// Enables the letter collapse title effect for the generated section heading.
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
