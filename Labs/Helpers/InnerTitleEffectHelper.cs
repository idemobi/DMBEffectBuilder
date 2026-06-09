#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBEffectBuilderLabs.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBEffectBuilderLabs.Helpers
{
    /// <summary>
    ///     Provides Razor helpers for rendering inner title effect preview cards.
    /// </summary>
    public static class InnerTitleEffectHelper
    {
        #region Static methods

        /// <summary>
        ///     Renders the inner title effect preview card partial asynchronously.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to render the partial view.</param>
        /// <param name="model">The card view model to render.</param>
        /// <returns>The rendered card content.</returns>
        public static Task<IHtmlContent> InnerTitleEffectCardAsync(this IHtmlHelper html, InnerTitleEffectCardViewModel model)
        {
            return html.PartialAsync("~/Views/Shared/_InnerTitleEffectCard.cshtml", model);
        }

        #endregion
    }
}