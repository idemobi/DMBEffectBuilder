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

        private string _neonColor = "#ff6ecf";

        private bool _neonGlow;
        private decimal _neonSpeed = 2m;

        #endregion

        #region Instance methods

        /// <summary>
        ///     Enables the neon glow title effect for the generated section heading.
        /// </summary>
        /// <param name="color">The color value used by the title effect.</param>
        /// <param name="speedSeconds">The speed seconds value used by the title effect.</param>
        /// <returns>The current title builder instance for fluent chaining.</returns>
        public TSelf SetNeonGlowEffect(string color = "#ff6ecf", decimal speedSeconds = 2m)
        {
            _neonGlow = true;
            _neonColor = color;
            _neonSpeed = speedSeconds;
            return (TSelf)this;
        }

        #endregion
    }
}