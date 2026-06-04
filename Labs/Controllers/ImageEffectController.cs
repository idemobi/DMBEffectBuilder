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
    ///     Provides live examples for image visual effects.
    /// </summary>
    public class ImageEffectController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the image concatenation effect example page.
        /// </summary>
        /// <returns>The concatenation example view.</returns>
        public IActionResult Concatenation()
        {
            return View();
        }

        /// <summary>
        ///     Renders the image effect overview page.
        /// </summary>
        /// <returns>The image effect overview view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Renders the photo hover effect example page.
        /// </summary>
        /// <returns>The photo hover example view.</returns>
        public IActionResult PhotoHover()
        {
            return View();
        }

        /// <summary>
        ///     Renders the image stylisation effect example page.
        /// </summary>
        /// <returns>The stylisation example view.</returns>
        public IActionResult Stylise()
        {
            return View();
        }

        #endregion
    }
}