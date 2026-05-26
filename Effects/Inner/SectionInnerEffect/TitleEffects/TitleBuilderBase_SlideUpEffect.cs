#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj TitleBuilderBase_SlideUpEffect.cs create at 2026/04/15
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    public abstract partial class TitleBuilderBase<TParent, TSelf>
        where TSelf : TitleBuilderBase<TParent, TSelf>
    {
        #region Instance fields and properties

        private bool _slideUp;
        private decimal _slideDuration = 0.6m;

        #endregion

        #region Instance methods

        public TSelf SetSlideUpEffect(decimal durationSeconds = 0.6m)
        {
            _slideUp = true;
            _slideDuration = durationSeconds;
            return (TSelf)this;
        }

        #endregion
    }
}
