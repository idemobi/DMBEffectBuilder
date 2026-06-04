#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Provides debug panel values for the animated gradient section effect.
    /// </summary>
    [DebugModel("Animated gradient", CodePattern = ".AnimatedGradientEffect(\"{0}\", \"{1}\", {2}m, {3}m, GradientAnimationCurve.{4})")]
    public sealed class AnimatedGradientEffectDebugModel
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the gradient angle in degrees.
        /// </summary>
        [DebugProperty(Label = "Angle", HelpText = "Gradient angle in degrees.", Target = DebugTarget.DataAttribute, TargetName = "data-gradient-angle")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-animated-gradient-angle", ValueSuffix = "deg")]
        public decimal Angle { get; set; } = 90m;

        /// <summary>
        ///     Gets or sets the marker value that enables animated gradient behavior in the debug panel.
        /// </summary>
        [DebugProperty(InputType = DebugInputType.Hidden, Target = DebugTarget.DataAttribute, TargetName = "data-animated-gradient")]
        public string AnimatedGradientFlag { get; set; } = "true";

        /// <summary>
        ///     Gets or sets the ending color used by the animated gradient.
        /// </summary>
        [DebugProperty(Label = "Color end", HelpText = "Ending color of the gradient.", Target = DebugTarget.DataAttribute, TargetName = "data-gradient-end")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-animated-gradient-end")]
        public string ColorEnd { get; set; } = "#0000ff";

        /// <summary>
        ///     Gets or sets the starting color used by the animated gradient.
        /// </summary>
        [DebugProperty(Label = "Color start", HelpText = "Starting color of the gradient.", Target = DebugTarget.DataAttribute, TargetName = "data-gradient-start")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-animated-gradient-start")]
        public string ColorStart { get; set; } = "#ff0000";

        /// <summary>
        ///     Gets or sets the animation curve used by the generated gradient.
        /// </summary>
        [DebugProperty(Label = "Curve", Target = DebugTarget.DataAttribute, TargetName = "data-gradient-curve")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-animated-gradient-curve")]
        public GradientAnimationCurve Curve { get; set; } = GradientAnimationCurve.EaseInOut;

        /// <summary>
        ///     Gets or sets the duration seconds value used by the effect debug panel.
        /// </summary>
        [DebugProperty(Label = "Duration (s)", HelpText = "Animation duration in seconds.", Target = DebugTarget.DataAttribute, TargetName = "data-gradient-duration")]
        [DebugPropertyTarget(DebugTarget.CssVariable, "--eb-animated-gradient-duration", ValueSuffix = "s")]
        public decimal DurationSeconds { get; set; } = 8m;

        /// <summary>
        ///     Gets or sets the generated section identifier used by the debug panel.
        /// </summary>
        [DebugProperty(Ignore = true)]
        public string SectionId { get; set; } = string.Empty;

        #endregion
    }
}