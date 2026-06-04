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
    ///     Provides documentation pages for <see cref="DMBEffectBuilder.EffectBuilderConfiguration" />.
    /// </summary>
    public class EffectBuilderController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the EffectBuilder architecture page.
        /// </summary>
        /// <returns>The architecture view.</returns>
        public IActionResult Architecture()
        {
            SetTitle("EffectBuilder - Architecture");
            SetDescription("EffectBuilder architecture");
            SetKeywords("EffectBuilder", "DMBEffectBuilder", "Architecture", "ASP.NET Core", "CSS");
            return View();
        }

        /// <summary>
        ///     Renders the EffectBuilder getting started page.
        /// </summary>
        /// <returns>The getting started view.</returns>
        public IActionResult GettingStarted()
        {
            SetTitle("EffectBuilder - Getting Started");
            SetDescription("EffectBuilder getting started guide");
            SetKeywords("EffectBuilder", "DMBEffectBuilder", "Getting Started", "NuGet", "ASP.NET Core");
            return View();
        }

        /// <summary>
        ///     Renders the EffectBuilder introduction page.
        /// </summary>
        /// <returns>The introduction view.</returns>
        public IActionResult Introduction()
        {
            SetTitle("EffectBuilder - Introduction");
            SetDescription("EffectBuilder");
            SetKeywords("EffectBuilder", "DMBEffectBuilder", "NuGet", "ASP.NET Core");
            return View();
        }

        /// <summary>
        ///     Renders the EffectBuilder rendering pipeline page.
        /// </summary>
        /// <returns>The rendering pipeline view.</returns>
        public IActionResult RenderingPipeline()
        {
            SetTitle("EffectBuilder - Rendering Pipeline");
            SetDescription("EffectBuilder rendering pipeline");
            SetKeywords("EffectBuilder", "DMBEffectBuilder", "Rendering Pipeline", "ASP.NET Core", "CSS");
            return View();
        }

        #endregion
    }
}