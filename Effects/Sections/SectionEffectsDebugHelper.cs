#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj SectionEffectsDebugHelper.cs create at 2026/04/07 21:04:27
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace DMBEffectBuilder
{
    public static class SectionEffectsDebugHelper
    {
        #region Static methods

        public static IReadOnlyList<string> GetEffectsImageUrls(IHtmlHelper html)
        {
            if (html == null)
                throw new ArgumentNullException(nameof(html));

            var env = html.ViewContext.HttpContext.RequestServices.GetService<IWebHostEnvironment>();

            if (env == null || string.IsNullOrWhiteSpace(env.WebRootPath))
                return Array.Empty<string>();

            string imagesPath = Path.Combine(env.WebRootPath, "images", "test");

            if (!Directory.Exists(imagesPath))
                return Array.Empty<string>();

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
