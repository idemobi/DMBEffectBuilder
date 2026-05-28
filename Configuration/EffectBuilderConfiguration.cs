#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj EffectBuilderConfiguration.cs create at 2026/04/07 21:04:27
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using DMBServerWebHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    /// Configures the embedded static assets and host integration for the effect builder package.
    /// </summary>
    [Serializable]
    public class EffectBuilderConfiguration : WebGenericConfiguration<EffectBuilderConfiguration>, IServerWebConfig
    {
        #region Static constructors and destructors

        static EffectBuilderConfiguration()
        {
        }

        #endregion

        #region Instance methods

        #region From interface IServerWebConfig

        /// <summary>
        /// Registers the static file post-configuration used to expose embedded effect assets.
        /// </summary>
        /// <param name="appBuilder">The application builder receiving package services.</param>
        /// <param name="configBuilder">The host configuration builder.</param>
        /// <param name="configRoot">The resolved host configuration root.</param>
        public override void AfterConfiguration(IHostApplicationBuilder appBuilder, IConfigurationBuilder configBuilder, IConfigurationRoot configRoot)
        {
            appBuilder.Services.ConfigureOptions<EffectBuilderConfigureOptions>();
        }

        /// <summary>
        /// Indicates whether this package contributes API description endpoints.
        /// </summary>
        /// <returns><see langword="false"/> because this package exposes Razor visual effects rather than HTTP APIs.</returns>
        public override bool ApiDescription()
        {
            return false;
        }

        /// <summary>
        /// Runs before configuration binding; DMBEffectBuilder currently has no pre-binding work.
        /// </summary>
        /// <param name="appBuilder">The application builder receiving configuration.</param>
        /// <param name="configBuilder">The host configuration builder.</param>
        /// <param name="configRoot">The resolved host configuration root.</param>
        public override void BeforeConfiguration(IHostApplicationBuilder appBuilder, IConfigurationBuilder configBuilder, IConfigurationRoot configRoot)
        {
        }

        /// <summary>
        /// Indicates whether this package requires an external configuration file or appsettings section.
        /// </summary>
        /// <returns><see langword="false"/> because the embedded asset defaults are sufficient.</returns>
        public override bool NeedsConfigFileOrAppSettings()
        {
            return false;
        }

        /// <summary>
        /// Populates fake configuration values for diagnostics; this package currently has no fake data to generate.
        /// </summary>
        public override void RandomFake()
        {
        }

        #endregion

        #endregion
    }
}
