#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj TitleBuilderBase_GradientEffect.cs create at 2026/04/15
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    public abstract partial class TitleBuilderBase<TParent, TSelf>
        where TSelf : TitleBuilderBase<TParent, TSelf>
    {
        #region Instance fields and properties

        private bool _gradient;
        private string _gradientStart = "#ff6ecf";
        private string _gradientEnd = "#efff5c";
        private decimal _gradientAngle = 90m;

        #endregion

        #region Instance methods

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
