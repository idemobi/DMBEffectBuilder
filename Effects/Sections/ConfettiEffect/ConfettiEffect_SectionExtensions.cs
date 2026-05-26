#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj ConfettiEffect_SectionExtensions.cs create at 2026/04/21
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;
using DMBPageBuilder;

namespace DMBEffectBuilder
{
    /// <summary>
    /// Provides extension methods to apply an animated confetti particle effect to a <see cref="SectionBuilder"/>.
    /// </summary>
    public static class ConfettiEffect_SectionExtensions
    {
        #region Static methods

        /// <summary>
        /// Applies an animated falling confetti particle effect to the section.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="colors">Array of CSS hex colors for the confetti pieces. Defaults to a multicolor palette.</param>
        /// <param name="count">Number of confetti pieces. Must be greater than 0. Defaults to <c>60</c>.</param>
        /// <param name="speed">Fall speed multiplier. Must be greater than 0. Defaults to <c>1</c>.</param>
        /// <returns>The same <see cref="SectionBuilder"/> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="section"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="count"/> or <paramref name="speed"/> is less than or equal to 0.</exception>
        /// <remarks>
        /// <para>
        /// <b>Use cases:</b> celebration pages, success screens, promotions, gamification rewards,
        /// event announcements. Best used sparingly and contextually.
        /// </para>
        /// <para>
        /// <b>How it works:</b> renders colored rectangular pieces on a canvas element, each falling
        /// with a randomized rotation, speed, and horizontal drift. The animation loops continuously.
        /// </para>
        /// <para>
        /// <b>Combinations:</b> works best over a dark or neutral background such as
        /// <c>BootstrapBackgroundEffect(Dark)</c>. Avoid combining with heavy particle effects.
        /// </para>
        /// <para>
        /// <b>Tips:</b> a <c>count</c> of 40–80 gives a festive look without being too heavy.
        /// Use <c>speed: 0.5</c> for a gentle drift or <c>speed: 2</c> for an energetic burst.
        /// Customize <c>colors</c> to match your brand palette.
        /// </para>
        /// <para>
        /// <b>Example:</b>
        /// <code>
        /// Html.SectionBuilder()
        ///     .BootstrapBackgroundEffect(VariantStyle.Dark, true)
        ///     .ConfettiEffect(count: 60, speed: 1m)
        /// </code>
        /// </para>
        /// <para>
        /// <b>Performance:</b> confetti is rendered on a <c>canvas</c> element via <c>requestAnimationFrame</c>.
        /// Avoid combining with other canvas-based effects on the same section.
        /// </para>
        /// </remarks>
        /// <seealso cref="BootstrapBackgroundEffect_SectionExtensions"/>
        /// <seealso cref="RippleEffect_SectionExtensions"/>
        [Documented]
        public static SectionBuilder ConfettiEffect(this SectionBuilder section,
            string[] colors = null,
            int count = 60,
            decimal speed = 1m)
        {
            if (section == null) throw new ArgumentNullException(nameof(section));
            if (count <= 0) throw new ArgumentOutOfRangeException(nameof(count), "Count must be greater than 0.");
            if (speed <= 0m) throw new ArgumentOutOfRangeException(nameof(speed), "Speed must be greater than 0.");

            var effectiveColors = colors ?? new[] { "#ff6ecf", "#efff5c", "#00f5d4", "#ff9f43", "#a29bfe" };

            section.EnsureId("confetti");
            section.AddClass("eb-section-effect-confetti");
            for (int i = 0; i < effectiveColors.Length && i < 5; i++)
                section.SetAttribute($"data-confetti-color{i + 1}", effectiveColors[i]);
            section.SetAttribute("data-confetti-count", count.ToString());
            section.SetAttribute("data-confetti-speed", speed.ToString(System.Globalization.CultureInfo.InvariantCulture));

            PageInformation page = PageRegistry.GetOrCreatePageInformation(section.HtmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/sectionEffects/ConfettiEffect.css");
            page.SetScriptFile("/js/sectionEffects/ConfettiEffect.js");

            #if DEBUG
            InjectDebugPanel(section, effectiveColors, count, speed);
            #endif

            return section;
        }

        #if DEBUG
        private static void InjectDebugPanel(SectionBuilder section, string[] colors, int count, decimal speed)
        {
            string sectionId = section.AttributeValue("id") ?? throw new InvalidOperationException("Section id could not be resolved.");
            section.AddDebugPanel(new ConfettiEffectDebugModel
            {
                SectionId = sectionId,
                Color1    = colors.Length > 0 ? colors[0] : "#ff6ecf",
                Color2    = colors.Length > 1 ? colors[1] : "#efff5c",
                Color3    = colors.Length > 2 ? colors[2] : "#00f5d4",
                Color4    = colors.Length > 3 ? colors[3] : "#ff9f43",
                Color5    = colors.Length > 4 ? colors[4] : "#a29bfe",
                Count     = count,
                Speed     = speed
            });
        }
        #endif

        #endregion
    }
}
