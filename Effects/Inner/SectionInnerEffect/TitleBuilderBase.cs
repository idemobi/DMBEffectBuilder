#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.Text;
using System.Text.Encodings.Web;
using DMBPageBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Provides fluent title configuration shared by section inner effect title builders.
    /// </summary>
    /// <typeparam name="TParent">The parent builder returned by <see cref="Build" />.</typeparam>
    /// <typeparam name="TSelf">The concrete title builder type used for fluent chaining.</typeparam>
    public abstract partial class TitleBuilderBase<TParent, TSelf>
        where TSelf : TitleBuilderBase<TParent, TSelf>
    {
        #region Instance fields and properties

        /// <summary>
        ///     Provides the Razor HTML helper used to register static assets for the title effect.
        /// </summary>
        protected readonly IHtmlHelper _html;

        /// <summary>
        ///     Stores the parent builder that receives the generated title markup.
        /// </summary>
        protected readonly TParent _parent;

        private string _title = string.Empty;

        #endregion

        #region Instance constructors and destructors

        #region Instance constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TitleBuilderBase{TParent, TSelf}" /> class.
        /// </summary>
        /// <param name="parent">The parent builder that receives the generated title markup.</param>
        /// <param name="html">The HTML helper used to register static assets.</param>
        protected TitleBuilderBase(TParent parent, IHtmlHelper html)
        {
            _parent = parent;
            _html = html;
        }

        #endregion

        #endregion

        #region Instance methods

        /// <summary>
        ///     Finalizes the title markup and returns the parent builder.
        /// </summary>
        /// <returns>The parent builder receiving the generated title markup.</returns>
        public abstract TParent Build();

        /// <summary>
        ///     Builds the encoded title HTML and applies the configured title effect classes and CSS variables.
        /// </summary>
        /// <returns>The generated title markup.</returns>
        /// <exception cref="InvalidOperationException">Thrown when mutually exclusive title effects are combined.</exception>
        protected string BuildCoreTitleHtml()
        {
            if (_typewriter && _letterCollapse) throw new InvalidOperationException("SetTypewriterEffect and SetLetterCollapseEffect cannot be used together.");
            if (_wave && _typewriter) throw new InvalidOperationException("SetWaveEffect and SetTypewriterEffect cannot be used together.");
            if (_wave && _letterCollapse) throw new InvalidOperationException("SetWaveEffect and SetLetterCollapseEffect cannot be used together.");
            if (_scramble && _typewriter) throw new InvalidOperationException("SetScrambleEffect and SetTypewriterEffect cannot be used together.");
            if (_neonGlow && _gradient) throw new InvalidOperationException("SetNeonGlowEffect and SetGradientEffect cannot be used together.");
            if (_outline && _gradient) throw new InvalidOperationException("SetOutlineEffect and SetGradientEffect cannot be used together.");
            if (_slideUp && _wave) throw new InvalidOperationException("SetSlideUpEffect and SetWaveEffect cannot be used together.");
            if (_blurReveal && _colorCycle) throw new InvalidOperationException("SetBlurRevealEffect and SetColorCycleEffect cannot be used together.");

            PageInformation page = PageRegistry.GetOrCreatePageInformation(_html.ViewContext.HttpContext);
            page.SetStylesheet("/css/innerEffects/SectionInnerTitleEffects.css");

            if (_typewriter || _scramble) page.SetScriptFile("/js/innerEffects/SectionInnerTitleEffects.js");

            var ci = System.Globalization.CultureInfo.InvariantCulture;
            string encodedTitle = HtmlEncoder.Default.Encode(_title);
            int charCount = encodedTitle.Length;
            string typewriterDuration = _typewriterDuration.ToString(ci);
            string letterCollapseDuration = _letterCollapseDuration.ToString(ci);

            var h1Style = new StringBuilder();
            var h1Classes = new StringBuilder();

            if (_twisted) h1Style.Append($"transform:rotate({_twistAngle.ToString(ci)}deg);");

            if (_gradient)
            {
                h1Style.Append($"background:linear-gradient({_gradientAngle.ToString(ci)}deg,{_gradientStart},{_gradientEnd});");
                h1Style.Append("-webkit-background-clip:text;");
                h1Style.Append("-webkit-text-fill-color:transparent;");
                h1Style.Append("background-clip:text;");
            }

            if (_typewriter)
            {
                h1Style.Append("overflow:hidden;white-space:nowrap;border-right:3px solid;width:0;");
                h1Classes.Append($"eb-typewriter eb-tw-{Guid.NewGuid():N}");
            }

            if (_letterCollapse) h1Style.Append($"animation:eb-letter-collapse {letterCollapseDuration}s ease-out forwards;");

            if (_scramble) h1Classes.Append("eb-scramble");

            if (_neonGlow)
            {
                h1Classes.Append(" eb-title-neon");
                h1Style.Append($"--eb-neon-color:{_neonColor};");
                h1Style.Append($"--eb-neon-speed:{_neonSpeed.ToString(ci)}s;");
            }

            if (_glitchText) h1Classes.Append(" eb-title-glitch");

            if (_blurReveal)
            {
                h1Classes.Append(" eb-title-blur-reveal");
                h1Style.Append($"--eb-blur-start:{_blurStart.ToString(ci)}px;");
                h1Style.Append($"--eb-blur-duration:{_blurDuration.ToString(ci)}s;");
            }

            if (_colorCycle)
            {
                h1Classes.Append(" eb-title-color-cycle");
                h1Style.Append($"--eb-color-cycle-base:{_colorCycleBase};");
                h1Style.Append($"--eb-color-cycle-speed:{_colorCycleSpeed.ToString(ci)}s;");
            }

            if (_outline)
            {
                h1Classes.Append(" eb-title-outline");
                h1Style.Append($"--eb-outline-color:{_outlineColor};");
                h1Style.Append($"--eb-outline-width:{_outlineWidth.ToString(ci)}px;");
                h1Style.Append($"--eb-outline-speed:{_outlineSpeed.ToString(ci)}s;");
            }

            if (_shake)
            {
                h1Classes.Append(" eb-title-shake");
                h1Style.Append($"--eb-shake-speed:{_shakeSpeed.ToString(ci)}s;");
            }

            if (_splitColor)
            {
                h1Classes.Append(" eb-title-split-color");
                h1Style.Append($"--eb-split-top:{_splitTop};");
                h1Style.Append($"--eb-split-bottom:{_splitBottom};");
            }

            if (_stamp)
            {
                h1Classes.Append(" eb-title-stamp");
                h1Style.Append($"--eb-stamp-duration:{_stampDuration.ToString(ci)}s;");
            }

            h1Style.Append("display:inline-block;");

            string titleContent;
            if (_wave)
            {
                var letters = new StringBuilder();
                decimal delay = 0m;
                foreach (char c in _title)
                {
                    string encoded = c == ' ' ? "&nbsp;" : HtmlEncoder.Default.Encode(c.ToString());
                    letters.Append($"<span class=\"eb-wave-letter\" style=\"--wave-duration:{_waveDuration.ToString(ci)}s;animation-delay:{delay.ToString(ci)}s;\">{encoded}</span>");
                    delay += 0.08m;
                }

                titleContent = letters.ToString();
            }
            else if (_slideUp)
            {
                var letters = new StringBuilder();
                decimal delay = 0m;
                foreach (char c in _title)
                {
                    string encoded = c == ' ' ? "&nbsp;" : HtmlEncoder.Default.Encode(c.ToString());
                    letters.Append($"<span class=\"eb-slide-letter\" style=\"--eb-slide-duration:{_slideDuration.ToString(ci)}s;animation-delay:{delay.ToString(ci)}s;\">{encoded}</span>");
                    delay += 0.05m;
                }

                titleContent = letters.ToString();
            }
            else
            {
                titleContent = encodedTitle;
            }

            var extraAttrs = new StringBuilder();
            if (_typewriter) extraAttrs.Append($" data-typewriter-chars=\"{charCount}\" data-typewriter-duration=\"{typewriterDuration}\"");
            if (_scramble) extraAttrs.Append($" data-scramble-text=\"{encodedTitle}\" data-scramble-duration=\"{_scrambleDuration.ToString(ci)}\"");
            if (_glitchText)
            {
                extraAttrs.Append($" data-text=\"{encodedTitle}\"");
                h1Style.Append($"--eb-glitch-color1:{_glitchColor1};");
                h1Style.Append($"--eb-glitch-color2:{_glitchColor2};");
                h1Style.Append($"--eb-glitch-speed:{_glitchSpeed.ToString(ci)}s;");
            }

            if (_splitColor && !_glitchText) extraAttrs.Append($" data-text=\"{encodedTitle}\"");

            var sb = new StringBuilder();
            sb.Append("<div class=\"eb-section-inner-title\">");
            sb.Append($"<h1 class=\"{h1Classes}\" style=\"{h1Style}\"{extraAttrs}>{titleContent}</h1>");
            sb.Append("</div>");

            return sb.ToString();
        }

        /// <summary>
        ///     Sets the title text rendered by the title effect builder.
        /// </summary>
        /// <param name="title">The title text to render.</param>
        /// <returns>The current title builder instance for fluent chaining.</returns>
        public TSelf SetTitle(string title)
        {
            _title = title;
            return (TSelf)this;
        }

        #endregion
    }
}