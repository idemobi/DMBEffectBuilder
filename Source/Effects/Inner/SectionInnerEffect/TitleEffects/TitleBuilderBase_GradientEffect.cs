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

        private bool _gradient;
        private decimal _gradientAngle = 90m;
        private string _gradientEnd = "#efff5c";
        private string _gradientStart = "#ff6ecf";

        #endregion

        #region Instance methods

        /// <summary>
        ///     Enables the gradient title effect for the generated section heading.
        /// </summary>
        /// <param name="colorStart">The color start value used by the title effect.</param>
        /// <param name="colorEnd">The color end value used by the title effect.</param>
        /// <param name="angle">The angle value used by the title effect.</param>
        /// <returns>The current title builder instance for fluent chaining.</returns>
        public TSelf SetGradientEffect(string colorStart = "#ff6ecf", string colorEnd = "#efff5c", decimal angle = 90m)
        {
            _gradient = true;
            _gradientStart = colorStart;
            _gradientEnd = colorEnd;
            _gradientAngle = angle;
            return (TSelf)this;
        }

        #endregion
    }
}