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
    ///     Provides live examples for section visual effects.
    /// </summary>
    public class SectionEffectController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the animated gradient effect example page.
        /// </summary>
        /// <returns>The animated gradient example view.</returns>
        public IActionResult AnimatedGradient()
        {
            return View();
        }

        /// <summary>
        ///     Renders the background effect example page.
        /// </summary>
        /// <returns>The background effect example view.</returns>
        public IActionResult Background()
        {
            return View();
        }

        /// <summary>
        ///     Renders the FX ambiance effect example page.
        /// </summary>
        /// <returns>The FX ambiance example view.</returns>
        public IActionResult FxAmbiance()
        {
            return View();
        }

        /// <summary>
        ///     Renders the highlight sweep effect example page with a set of opacity variations.
        /// </summary>
        /// <returns>The highlight sweep example view.</returns>
        public IActionResult HighlightSweep()
        {
            var variations = new List<decimal> { 0.1m, 0.3m, 0.5m, 0.7m, 0.9m };
            return View(variations);
        }

        /// <summary>
        ///     Renders the section effect overview page.
        /// </summary>
        /// <returns>The section effect overview view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Renders the shape edge effect example page.
        /// </summary>
        /// <returns>The shape edge example view.</returns>
        public IActionResult ShapeEdge()
        {
            return View();
        }

        #endregion
    }
}