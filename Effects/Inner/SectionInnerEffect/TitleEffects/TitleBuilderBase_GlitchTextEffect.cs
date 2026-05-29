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

        private string _glitchColor1 = "#ff005c";
        private string _glitchColor2 = "#00f5d4";
        private decimal _glitchSpeed = 3m;

        private bool _glitchText;

        #endregion

        #region Instance methods

        /// <summary>
        ///     Enables the glitch text title effect for the generated section heading.
        /// </summary>
        /// <param name="color1">The color1 value used by the title effect.</param>
        /// <param name="color2">The color2 value used by the title effect.</param>
        /// <param name="speedSeconds">The speed seconds value used by the title effect.</param>
        /// <returns>The current title builder instance for fluent chaining.</returns>
        public TSelf SetGlitchTextEffect(string color1 = "#ff005c", string color2 = "#00f5d4", decimal speedSeconds = 3m)
        {
            _glitchText = true;
            _glitchColor1 = color1;
            _glitchColor2 = color2;
            _glitchSpeed = speedSeconds;
            return (TSelf)this;
        }

        #endregion
    }
}