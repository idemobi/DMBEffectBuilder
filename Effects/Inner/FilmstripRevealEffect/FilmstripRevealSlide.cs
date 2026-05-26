#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj FilmstripRevealSlide.cs create at 2026/05/11
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    /// <summary>A single image slide for <see cref="FilmstripRevealEffectBuilder"/>.</summary>
    public sealed class FilmstripRevealSlide
    {
        public string Src { get; }
        public string Alt { get; }

        public FilmstripRevealSlide(string src, string alt = "")
        {
            Src = src;
            Alt = alt;
        }
    }
}
