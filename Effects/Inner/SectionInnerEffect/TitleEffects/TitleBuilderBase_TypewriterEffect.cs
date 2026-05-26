#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj TitleBuilderBase_TypewriterEffect.cs create at 2026/04/15
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    public abstract partial class TitleBuilderBase<TParent, TSelf>
        where TSelf : TitleBuilderBase<TParent, TSelf>
    {
        #region Instance fields and properties

        private bool _typewriter;
        private decimal _typewriterDuration = 2m;

        #endregion

        #region Instance methods

        public TSelf SetTypewriterEffect(decimal durationSeconds = 2m)
        {
            _typewriter = true;
            _typewriterDuration = durationSeconds;
            return (TSelf)this;
        }

        #endregion
    }
}
