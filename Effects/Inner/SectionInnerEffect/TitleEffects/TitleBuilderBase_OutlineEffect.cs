#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj TitleBuilderBase_OutlineEffect.cs create at 2026/04/15
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    /// <inheritdoc/>
    public abstract partial class TitleBuilderBase<TParent, TSelf>
        where TSelf : TitleBuilderBase<TParent, TSelf>
    {
        #region Instance fields and properties

        private bool _outline;
        private string _outlineColor = "var(--bs-primary, #0d6efd)";
        private decimal _outlineWidth = 1m;
        private decimal _outlineSpeed = 2m;

        #endregion

        #region Instance methods

        /// <summary>
        /// Enables the outline title effect for the generated section heading.
        /// </summary>
        /// <param name="color">The color value used by the title effect.</param>
        /// <param name="widthPx">The width px value used by the title effect.</param>
        /// <param name="speedSeconds">The speed seconds value used by the title effect.</param>
        /// <returns>The current title builder instance for fluent chaining.</returns>
        public TSelf SetOutlineEffect(string color = "#ffffff", decimal widthPx = 1m, decimal speedSeconds = 2m)
        {
            _outline = true;
            _outlineColor = color;
            _outlineWidth = widthPx;
            _outlineSpeed = speedSeconds;
            return (TSelf)this;
        }

        #endregion
    }
}
