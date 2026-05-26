#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj AnimatedGradientEffectDebugModel.cs create at 2026/04/07 21:04:27
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;

namespace DMBEffectBuilder
{
    [DebugModel("Animated gradient", CodePattern = ".AnimatedGradientEffect(\"{0}\", \"{1}\", {2}m, {3}m, GradientAnimationCurve.{4})")]
    public sealed class AnimatedGradientEffectDebugModel
    {
        #region Instance fields and properties

        [DebugProperty(Label = "Angle", HelpText = "Gradient angle in degrees.", Target = DebugTarget.DataAttribute, TargetName = "data-gradient-angle")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-animated-gradient-angle", ValueSuffix = "deg")]
        public decimal Angle { get; set; } = 90m;

        [DebugProperty(InputType = DebugInputType.Hidden, Target = DebugTarget.DataAttribute, TargetName = "data-animated-gradient")]
        public string AnimatedGradientFlag { get; set; } = "true";

        [DebugProperty(Label = "Color end", HelpText = "Ending color of the gradient.", Target = DebugTarget.DataAttribute, TargetName = "data-gradient-end")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-animated-gradient-end")]
        public string ColorEnd { get; set; } = "#0000ff";

        [DebugProperty(Label = "Color start", HelpText = "Starting color of the gradient.", Target = DebugTarget.DataAttribute, TargetName = "data-gradient-start")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-animated-gradient-start")]
        public string ColorStart { get; set; } = "#ff0000";

        [DebugProperty(Label = "Curve", Target = DebugTarget.DataAttribute, TargetName = "data-gradient-curve")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-animated-gradient-curve")]
        public GradientAnimationCurve Curve { get; set; } = GradientAnimationCurve.EaseInOut;

        [DebugProperty(Label = "Duration (s)", HelpText = "Animation duration in seconds.", Target = DebugTarget.DataAttribute, TargetName = "data-gradient-duration")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-animated-gradient-duration", ValueSuffix = "s")]
        public decimal DurationSeconds { get; set; } = 8m;

        [DebugProperty(Ignore = true)] public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}