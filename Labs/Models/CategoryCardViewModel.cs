#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;

#endregion

namespace DMBEffectBuilderLabs.Models
{
    /// <summary>
    ///     Describes a section effect category card rendered by the DMBEffectBuilder labs pages.
    /// </summary>
    public class CategoryCardViewModel
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the short category description displayed below the preview.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the Bootstrap icon class used in the category overlay.
        /// </summary>
        public string Icon { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the preview transformations applied to sample <see cref="SectionBuilder" /> instances.
        /// </summary>
        public Func<SectionBuilder, SectionBuilder>[] Previews { get; set; } = [];

        /// <summary>
        ///     Gets or sets the action name opened when the category is selected.
        /// </summary>
        public string RouteAction { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the secondary tagline displayed below the category description.
        /// </summary>
        public string Tagline { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the category title.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        #endregion
    }
}