#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj TitleBuilderBase_ColorCycleEffect.cs create at 2026/04/15
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    /// <inheritdoc/>
    public abstract partial class TitleBuilderBase<TParent, TSelf>
        where TSelf : TitleBuilderBase<TParent, TSelf>
    {
        #region Instance fields and properties

        private bool _colorCycle;
        private decimal _colorCycleSpeed = 4m;
        private string _colorCycleBase = "#ff6ecf";

        #endregion

        #region Instance methods

        /// <summary>
        /// Enables the color cycle title effect for the generated section heading.
        /// </summary>
        /// <param name="baseColor">The base color value used by the title effect.</param>
        /// <param name="speedSeconds">The speed seconds value used by the title effect.</param>
        /// <returns>The current title builder instance for fluent chaining.</returns>
        public TSelf SetColorCycleEffect(string baseColor = "#ff6ecf", decimal speedSeconds = 4m)
        {
            _colorCycle = true;
            _colorCycleBase = baseColor;
            _colorCycleSpeed = speedSeconds;
            return (TSelf)this;
        }

        #endregion
    }
}
