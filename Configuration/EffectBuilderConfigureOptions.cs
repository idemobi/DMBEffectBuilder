#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj EffectBuilderConfigureOptions.cs create at 2026/04/07 21:04:27
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    /// Adds DMBEffectBuilder embedded static assets to ASP.NET Core static file options.
    /// </summary>
    public class EffectBuilderConfigureOptions : IPostConfigureOptions<StaticFileOptions>
    {
        #region Constants

        const string K_BasePath = "wwwroot";

        #endregion

        #region Instance fields and properties

        private IWebHostEnvironment Environment { get; }

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EffectBuilderConfigureOptions"/> class.
        /// </summary>
        /// <param name="environment">The web host environment that supplies the host web root file provider.</param>
        public EffectBuilderConfigureOptions(IWebHostEnvironment environment)
        {
            Environment = environment;
        }

        #endregion

        #region Instance methods

        #region From interface IPostConfigureOptions<StaticFileOptions>

        /// <summary>
        /// Adds the package manifest embedded file provider to the configured static file provider chain.
        /// </summary>
        /// <param name="name">The options name being configured.</param>
        /// <param name="options">The static file options to update.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="name"/> or <paramref name="options"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">Thrown when no host or existing static file provider is available.</exception>
        public void PostConfigure(string? name, StaticFileOptions options)
        {
            name = name ?? throw new ArgumentNullException(nameof(name));
            options = options ?? throw new ArgumentNullException(nameof(options));

            options.ContentTypeProvider = options.ContentTypeProvider ?? new FileExtensionContentTypeProvider();
            if (options.FileProvider == null && Environment.WebRootFileProvider == null)
            {
                throw new InvalidOperationException("Missing FileProvider.");
            }

            options.FileProvider = options.FileProvider ?? Environment.WebRootFileProvider;
            var tFilesProvider = new ManifestEmbeddedFileProvider(GetType().Assembly, K_BasePath);
            options.FileProvider = new CompositeFileProvider(options.FileProvider, tFilesProvider);
        }

        #endregion

        #endregion
    }
}
