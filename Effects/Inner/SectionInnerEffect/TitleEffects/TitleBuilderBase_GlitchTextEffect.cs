#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj TitleBuilderBase_GlitchTextEffect.cs create at 2026/04/15
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    public abstract partial class TitleBuilderBase<TParent, TSelf>
        where TSelf : TitleBuilderBase<TParent, TSelf>
    {
        #region Instance fields and properties

        private bool _glitchText;
        private string _glitchColor1 = "#ff005c";
        private string _glitchColor2 = "#00f5d4";
        private decimal _glitchSpeed = 3m;

        #endregion

        #region Instance methods

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
