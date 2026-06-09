#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Provides shared helpers used by section effect debug panels.
    /// </summary>
    public static class SectionEffectsDebugHelper
    {
        #region Static methods

        /// <summary>
        ///     Returns image URLs from the host <c>wwwroot/images/test</c> folder for debug selectors.
        /// </summary>
        /// <param name="html">The current Razor HTML helper used to resolve the web host environment.</param>
        /// <returns>A sorted list of supported image URLs, or an empty list when the folder is unavailable.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="html" /> is <see langword="null" />.</exception>
        public static IReadOnlyList<string> GetEffectsImageUrls(IHtmlHelper html)
        {
            if (html == null) throw new ArgumentNullException(nameof(html));

            var env = html.ViewContext.HttpContext.RequestServices.GetService<IWebHostEnvironment>();

            if (env == null || string.IsNullOrWhiteSpace(env.WebRootPath)) return Array.Empty<string>();

            string imagesPath = Path.Combine(env.WebRootPath, "images", "test");

            if (!Directory.Exists(imagesPath)) return Array.Empty<string>();

            return Directory
                .EnumerateFiles(imagesPath)
                .Where(IsSupportedImageFile)
                .OrderBy(Path.GetFileName, StringComparer.OrdinalIgnoreCase)
                .Select(x => "/images/test/" + Path.GetFileName(x))
                .ToArray();
        }

        private static bool IsSupportedImageFile(string filePath)
        {
            string extension = Path.GetExtension(filePath);

            return extension.Equals(".png", StringComparison.OrdinalIgnoreCase) ||
                   extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) ||
                   extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                   extension.Equals(".webp", StringComparison.OrdinalIgnoreCase) ||
                   extension.Equals(".gif", StringComparison.OrdinalIgnoreCase) ||
                   extension.Equals(".svg", StringComparison.OrdinalIgnoreCase);
        }

        #endregion
    }
}