#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBEffectBuilder
{
    /// <summary>A single image slide for <see cref="FilmstripRevealEffectBuilder" />.</summary>
    public sealed class FilmstripRevealSlide
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets the alternative text rendered for the slide image.
        /// </summary>
        public string Alt { get; }

        /// <summary>
        ///     Gets the image source URL rendered for the slide.
        /// </summary>
        public string Src { get; }

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="FilmstripRevealSlide" /> class.
        /// </summary>
        /// <param name="src">The image source URL rendered for the slide.</param>
        /// <param name="alt">The alternative text rendered for the slide image.</param>
        public FilmstripRevealSlide(string src, string alt = "")
        {
            Src = src;
            Alt = alt;
        }

        #endregion
    }
}