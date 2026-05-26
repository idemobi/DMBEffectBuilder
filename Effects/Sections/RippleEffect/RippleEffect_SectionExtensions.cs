#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj RippleEffect_SectionExtensions.cs create at 2026/04/17
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using System.Globalization;
using DMBBootstrapBuilder;
using DMBPageBuilder;

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides extension methods to apply a ripple wave effect to a <see cref="SectionBuilder"/>.
    /// </summary>
    public static class RippleEffect_SectionExtensions
    {
        #region Static methods

        /// <summary>
        /// Applies a ripple wave animation to the section, triggered on click or automatically.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="color">The ripple color in CSS hex format. Defaults to <c>#ffffff</c>.</param>
        /// <param name="opacity">Opacity of the ripple (0–1). Defaults to <c>0.5</c>.</param>
        /// <param name="speed">Expansion speed of the ripple in seconds. Must be greater than 0. Defaults to <c>3</c>.</param>
        /// <param name="autoMode">When <c>true</c>, ripples trigger automatically. When <c>false</c>, ripples trigger on click. Defaults to <c>false</c>.</param>
        /// <returns>The same <see cref="SectionBuilder"/> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="section"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="color"/> is null or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="opacity"/> is not between 0 and 1, or <paramref name="speed"/> is less than or equal to 0.</exception>
        /// <remarks>
        /// <para>
        /// <b>Use cases:</b> interactive CTAs, event pages, calm ambient backgrounds, water or
        /// sound-themed sections. In click mode, it rewards user interaction with a satisfying visual response.
        /// </para>
        /// <para>
        /// <b>How it works:</b> when the user clicks inside the section (or automatically at random
        /// intervals in auto mode), an expanding circular ring is rendered on a canvas and fades out
        /// as it grows. Multiple ripples can coexist simultaneously.
        /// </para>
        /// <para>
        /// <b>Combinations:</b> chain after any background effect — the ripple canvas is transparent
        /// and overlays everything. Works particularly well with <c>AnimatedGradientEffect</c> or
        /// <c>BootstrapBackgroundEffect</c> for interactive hero sections.
        /// </para>
        /// <para>
        /// <b>Tips:</b> set <c>autoMode: true</c> for a passive ambient effect with no user interaction needed.
        /// Set <c>autoMode: false</c> for sections where you want to invite the user to click.
        /// A <c>speed</c> of 2–4 s gives a natural expansion; faster values feel more energetic.
        /// White ripples work on most backgrounds; match the ripple color to a complementary hue
        /// for a more designed look.
        /// </para>
        /// <para>
        /// <b>Example:</b>
        /// <code>
        /// Html.SectionBuilder()
        ///     .AnimatedGradientEffect("#0066cc", "#004499", angle: 135m, durationSeconds: 6m)
        ///     .RippleEffect(color: "#ffffff", opacity: 0.5m, speed: 3, autoMode: true)
        /// </code>
        /// </para>
        /// <para>
        /// <b>Performance:</b> ripples are drawn on a canvas via <c>requestAnimationFrame</c>.
        /// In auto mode, new ripples are spawned at regular intervals — keep <c>speed</c> high enough
        /// that old ripples have time to fade before new ones appear, to avoid canvas overdraw.
        /// </para>
        /// </remarks>
        /// <seealso cref="PulseRingEffect_SectionExtensions"/>
        /// <seealso cref="ConfettiEffect_SectionExtensions"/>
        [Documented]
        public static SectionBuilder RippleEffect(
            this SectionBuilder section,
            string color = "#ffffff",
            decimal opacity = 0.5m,
            int speed = 3,
            bool autoMode = false)
        {
            if (section == null) throw new ArgumentNullException(nameof(section));
            if (string.IsNullOrWhiteSpace(color)) throw new ArgumentException("Color cannot be null or empty.", nameof(color));
            if (opacity < 0m || opacity > 1m) throw new ArgumentOutOfRangeException(nameof(opacity), "Opacity must be between 0 and 1.");
            if (speed <= 0) throw new ArgumentOutOfRangeException(nameof(speed), "Speed must be greater than 0.");

            var ci = CultureInfo.InvariantCulture;

            section.EnsureId("ripple");
            section.AddClass("eb-section-effect-ripple");
            section.SetAttribute("data-ripple-color",   color);
            section.SetAttribute("data-ripple-opacity", opacity.ToString(ci));
            section.SetAttribute("data-ripple-speed",   speed.ToString());
            section.SetAttribute("data-ripple-auto",    autoMode ? "true" : "false");

            PageInformation page = PageRegistry.GetOrCreatePageInformation(section.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/sectionEffects/RippleEffect.css");
            page.SetScriptFile("/js/sectionEffects/RippleEffect.js");

            #if DEBUG
            InjectDebugPanel(section, color, opacity, speed);
            #endif

            return section;
        }

        #if DEBUG
        private static void InjectDebugPanel(SectionBuilder section, string color, decimal opacity, int speed)
        {
            string sectionId = section.AttributeValue("id") ?? throw new InvalidOperationException("Section id could not be resolved.");
            section.AddDebugPanel(new RippleEffectDebugModel
            {
                SectionId = sectionId,
                Color     = color,
                Opacity   = opacity,
                Speed     = speed
            });
        }
        #endif

        #endregion
    }
}
