#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj TitleBuilderBase_BlurRevealEffect.cs create at 2026/04/15
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    /// <inheritdoc/>
    public abstract partial class TitleBuilderBase<TParent, TSelf>
        where TSelf : TitleBuilderBase<TParent, TSelf>
    {
        #region Instance fields and properties

        private bool _blurReveal;
        private decimal _blurStart = 12m;
        private decimal _blurDuration = 1.5m;

        #endregion

        #region Instance methods

        /// <summary>
        /// Enables the blur reveal title effect for the generated section heading.
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
