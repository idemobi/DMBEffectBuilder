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

        private string _splitBottom = "#ff6ecf";

        private bool _splitColor;
        private string _splitTop = "var(--bs-body-color, #212529)";

        #endregion

        #region Instance methods

        /// <summary>
        ///     Enables the split color title effect for the generated section heading.
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