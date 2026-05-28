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
        /// <summary>
        /// Gets the image source URL rendered for the slide.
        /// </summary>
        public string Src { get; }

        /// <summary>
        /// Gets the alternative text rendered for the slide image.
        /// </summary>
        public string Alt { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilmstripRevealSlide"/> class.
        /// </summary>
        /// <param name="src">The image source URL rendered for the slide.</param>
        /// <param name="alt">The alternative text rendered for the slide image.</param>
        public FilmstripRevealSlide(string src, string alt = "")
        {
            Src = src;
            Alt = alt;
        }
    }
}
