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
    /// <summary>
    /// Provides debug panel values for the bootstrap background section effect.
    /// </summary>
    [DebugModel("Bootstrap background", CodePattern = ".BootstrapBackgroundEffect(VariantStyle.{0}, {1})")]
    public sealed class BootstrapBackgroundEffectDebugModel
    {
        #region Instance fields and properties

        /// <summary>
        /// Gets or sets a value indicating whether text color should be adjusted automatically for contrast.
        /// </summary>
        [DebugProperty(Label = "Auto text color")]
        public bool AutoTextColor { get; set; }

        /// <summary>
        /// Gets or sets the selectable variants values exposed by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)] public IReadOnlyList<string> AvailableVariants { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Bootstrap variant applied to the section background.
        /// </summary>
        [DebugProperty(Label = "Background", Target = DebugTarget.Class, ValuePrefix = "bg-")]
        public VariantStyle Variant { get; set; }

        #endregion
    }
}