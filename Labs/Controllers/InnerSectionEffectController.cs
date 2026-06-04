#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace DMBEffectBuilderLabs.Controllers
{
    /// <summary>
    ///     Provides live examples for inner section visual effects.
    /// </summary>
    public class InnerSectionEffectController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the card effects example page.
        /// </summary>
        /// <returns>The card effects example view.</returns>
        public IActionResult Cards()
        {
            return View();
        }

        /// <summary>
        ///     Renders the carousel effects example page.
        /// </summary>
        /// <returns>The carousel effects example view.</returns>
        public IActionResult Carousels()
        {
            return View();
        }

        /// <summary>
        ///     Renders the inner section effect overview page.
        /// </summary>
        /// <returns>The inner section effect overview view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Renders the layout decoration effects example page.
        /// </summary>
        /// <returns>The layout decoration example view.</returns>
        public IActionResult LayoutDecoration()
        {
            return View();
        }

        /// <summary>
        ///     Renders the navigation effects example page.
        /// </summary>
        /// <returns>The navigation effects example view.</returns>
        public IActionResult Navigation()
        {
            return View();
        }

        /// <summary>
        ///     Renders the scroll effects example page.
        /// </summary>
        /// <returns>The scroll effects example view.</returns>
        public IActionResult Scroll()
        {
            return View();
        }

        /// <summary>
        ///     Renders the text effects example page.
        /// </summary>
        /// <returns>The text effects example view.</returns>
        public IActionResult TextEffects()
        {
            return View();
        }

        /// <summary>
        ///     Renders the video background effect example page.
        /// </summary>
        /// <returns>The video effect example view.</returns>
        public IActionResult Video()
        {
            return View();
        }

        #endregion
    }
}