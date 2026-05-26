#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj TitleBuilderBase_StampEffect.cs create at 2026/04/15
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    public abstract partial class TitleBuilderBase<TParent, TSelf>
        where TSelf : TitleBuilderBase<TParent, TSelf>
    {
        #region Instance fields and properties

        private bool _stamp;
        private decimal _stampDuration = 0.6m;

        #endregion

        #region Instance methods

        public TSelf SetStampEffect(decimal durationSeconds = 0.6m)
        {
            _stamp = true;
            _stampDuration = durationSeconds;
            return (TSelf)this;
        }

        #endregion
    }
}
