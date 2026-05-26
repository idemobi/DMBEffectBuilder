#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj TitleBuilderBase_TwistedEffect.cs create at 2026/04/15
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    public abstract partial class TitleBuilderBase<TParent, TSelf>
        where TSelf : TitleBuilderBase<TParent, TSelf>
    {
        #region Instance fields and properties

        private bool _twisted;
        private decimal _twistAngle = -3m;

        #endregion

        #region Instance methods

        public TSelf SetTwistedEffect(decimal angle = -3m)
        {
            _twisted = true;
            _twistAngle = angle;
            return (TSelf)this;
        }

        #endregion
    }
}
