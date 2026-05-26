#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj BootstrapBackgroundEffectDebugModel.cs create at 2026/04/07 21:04:27
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using DMBBootstrapBuilder;
using DMBPageBuilder;

#endregion

namespace DMBEffectBuilder
{
    [DebugModel("Bootstrap background", CodePattern = ".BootstrapBackgroundEffect(VariantStyle.{0}, {1})")]
    public sealed class BootstrapBackgroundEffectDebugModel
    {
        #region Instance fields and properties

        [DebugProperty(Label = "Auto text color")]
        public bool AutoTextColor { get; set; }

        [DebugProperty(Ignore = true)] public IReadOnlyList<string> AvailableVariants { get; set; } = Array.Empty<string>();

        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        [DebugProperty(Label = "Background", Target = DebugTarget.Class, ValuePrefix = "bg-")]
        public VariantStyle Variant { get; set; }

        #endregion
    }
}