#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using DMBBootstrapBuilder;
using DMBPageBuilder;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Provides extension methods to apply a Bootstrap theme color as section background.
    /// </summary>
    public static class BootstrapBackgroundEffect_SectionExtensions
    {
        #region Static methods

        /// <summary>
        ///     Applies a Bootstrap semantic background color to the section using the specified <see cref="VariantStyle" />.
        /// </summary>
        /// <param name="section">The section builder to apply the effect to.</param>
        /// <param name="variant">The Bootstrap color variant to use as background (e.g. Primary, Secondary, Danger).</param>
        /// <param name="autoTextColor">
        ///     When <c>true</c>, automatically applies a contrasting text color class. Defaults to
        ///     <c>false</c>.
        /// </param>
        /// <returns>The same <see cref="SectionBuilder" /> instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="section" /> is <c>null</c>.</exception>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> any section that needs a solid, theme-aware background — navigation bars,
        ///         alert banners, feature highlights, footer sections. The simplest and most reliable background effect.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> applies a Bootstrap <c>bg-{variant}</c> class and optionally a matching
        ///         <c>text-{variant}</c> contrast class, fully respecting the active Bootstrap theme (including dark mode).
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> this is the recommended base when pairing with FX &amp; Ambiance overlays
        ///         such as <c>SpotlightEffect</c>, <c>RainEffect</c>, <c>PulseRingEffect</c> or <c>GlowPulseEffect</c>
        ///         on dark sections.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> set <c>autoTextColor: true</c> to automatically apply a readable text color
        ///         that contrasts with the chosen variant, so you don't have to set it manually on child elements.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.SectionBuilder()
        ///     .BootstrapBackgroundEffect(VariantStyle.Dark, autoTextColor: true)
        ///     .SpotlightEffect(color: "#ffc832", opacity: 0.25m, sizePx: 300, autoMove: true)
        /// </code>
        ///     </para>
        /// </remarks>
        /// <seealso cref="AnimatedGradientEffect_SectionExtensions" />
        /// <seealso cref="AuroraEffect_SectionExtensions" />
        [Documented]
        public static SectionBuilder BootstrapBackgroundEffect(
            this SectionBuilder section,
            VariantStyle variant,
            bool autoTextColor = false
        )
        {
            if (section == null)
            {
                throw new ArgumentNullException(nameof(section));
            }

            string? bgClass = GetBackgroundCssClass(variant);
            if (!string.IsNullOrWhiteSpace(bgClass))
            {
                section.AddClass(bgClass);
            }

            if (autoTextColor)
            {
                string? textClass = GetTextCssClass(variant);
                if (!string.IsNullOrWhiteSpace(textClass))
                {
                    section.AddClass(textClass);
                }
            }

            #if DEBUG
            InjectDebugPanel(section, variant, autoTextColor);
            #endif

            return section;
        }

        private static string? GetBackgroundCssClass(VariantStyle variant)
        {
            return variant switch
            {
                VariantStyle.Primary => "bg-primary",
                VariantStyle.Secondary => "bg-secondary",
                // VariantStyle.Tertiary => "bg-tertiary",
                VariantStyle.Success => "bg-success",
                VariantStyle.Warning => "bg-warning",
                VariantStyle.Danger => "bg-danger",
                VariantStyle.Info => "bg-info",
                VariantStyle.Light => "bg-light",
                VariantStyle.Dark => "bg-dark",
                _ => null
            };
        }

        private static string? GetTextCssClass(VariantStyle variant)
        {
            return variant switch
            {
                VariantStyle.Primary => "text-light",
                VariantStyle.Secondary => "text-light",
                // VariantStyle.Tertiary => "text-light",
                VariantStyle.Success => "text-light",
                VariantStyle.Danger => "text-light",
                VariantStyle.Dark => "text-light",
                VariantStyle.Warning => "text-dark",
                VariantStyle.Info => "text-dark",
                VariantStyle.Light => "text-dark",
                _ => null
            };
        }

        #if DEBUG
        private static void InjectDebugPanel(
            SectionBuilder section,
            VariantStyle variant,
            bool autoTextColor
        )
        {
            section.EnsureId("bootstrap_bg");

            string sectionId = section.AttributeValue("id")
                               ?? throw new InvalidOperationException("Section id missing.");

            var model = new BootstrapBackgroundEffectDebugModel
            {
                SectionId = sectionId,
                Variant = variant,
                AutoTextColor = autoTextColor,
                AvailableVariants = new[]
                {
                    "primary", "secondary", "success", "warning", "danger", "info", "light", "dark"
                }
            };

            string initialBgClass = GetBackgroundCssClass(variant) ?? "";
            if (!string.IsNullOrWhiteSpace(initialBgClass)) section.SetAttribute("data-debug-class-variant", initialBgClass);

            section.AddDebugPanel(model);
        }
        #endif

        #endregion
    }
}