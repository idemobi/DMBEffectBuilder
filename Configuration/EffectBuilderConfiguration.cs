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

        public override void AfterConfiguration(IHostApplicationBuilder appBuilder, IConfigurationBuilder configBuilder, IConfigurationRoot configRoot)
        {
            appBuilder.Services.ConfigureOptions<EffectBuilderConfigureOptions>();
        }

        public override bool ApiDescription()
        {
            return false;
        }

        public override void BeforeConfiguration(IHostApplicationBuilder appBuilder, IConfigurationBuilder configBuilder, IConfigurationRoot configRoot)
        {
        }

        public override bool NeedsConfigFileOrAppSettings()
        {
            return false;
        }

        public override void RandomFake()
        {
        }

        #endregion

        #endregion
    }
}