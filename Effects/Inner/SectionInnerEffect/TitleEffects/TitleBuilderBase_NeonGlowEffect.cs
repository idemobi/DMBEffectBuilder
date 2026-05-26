#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj TitleBuilderBase_NeonGlowEffect.cs create at 2026/04/15
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    public abstract partial class TitleBuilderBase<TParent, TSelf>
        where TSelf : TitleBuilderBase<TParent, TSelf>
    {
        #region Instance fields and properties

        private bool _neonGlow;
        private string _neonColor = "#ff6ecf";
        private decimal _neonSpeed = 2m;

        #endregion

        #region Instance methods

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
