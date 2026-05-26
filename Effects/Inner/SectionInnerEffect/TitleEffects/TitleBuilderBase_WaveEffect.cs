#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj TitleBuilderBase_WaveEffect.cs create at 2026/04/15
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    public abstract partial class TitleBuilderBase<TParent, TSelf>
        where TSelf : TitleBuilderBase<TParent, TSelf>
    {
        #region Instance fields and properties

        private bool _wave;
        private decimal _waveDuration = 1m;

        #endregion

        #region Instance methods

        public TSelf SetWaveEffect(decimal durationSeconds = 1m)
        {
            _wave = true;
            _waveDuration = durationSeconds;
            return (TSelf)this;
        }

        #endregion
    }
}
