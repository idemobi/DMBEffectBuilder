#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj TitleBuilderBase_SplitColorEffect.cs create at 2026/04/15
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    /// <inheritdoc/>
    public abstract partial class TitleBuilderBase<TParent, TSelf>
        where TSelf : TitleBuilderBase<TParent, TSelf>
    {
        #region Instance fields and properties

        private bool _splitColor;
        private string _splitTop = "var(--bs-body-color, #212529)";
        private string _splitBottom = "#ff6ecf";

        #endregion

        #region Instance methods

        /// <summary>
        /// Enables the split color title effect for the generated section heading.
        /// </summary>
        /// <param name="topColor">The top color value used by the title effect.</param>
        /// <param name="bottomColor">The bottom color value used by the title effect.</param>
        /// <returns>The current title builder instance for fluent chaining.</returns>
        public TSelf SetSplitColorEffect(string topColor = "#ffffff", string bottomColor = "#ff6ecf")
        {
            _splitColor = true;
            _splitTop = topColor;
            _splitBottom = bottomColor;
            return (TSelf)this;
        }

        #endregion
    }
}
