#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Fluent builder that renders a section inner container with an optional header title and overlay layout.
    /// </summary>
    [Documented]
    public sealed class SectionInnerEffectBuilder : HtmlTagBuilder<SectionInnerEffectBuilder>, IDisposable, ICanUseCustomClasses, ICanUseMargin
    {
        #region Instance fields and properties

        private string? _header
        {
            get => GetInternal<string?>("_header", null);
            set => SetInternal("_header", value);
        }

        private bool _innerDisposed
        {
            get => GetInternal("_innerDisposed", false);
            set => SetInternal("_innerDisposed", value);
        }

        private bool _innerStarted
        {
            get => GetInternal("_innerStarted", false);
            set => SetInternal("_innerStarted", value);
        }

        private bool _overlay
        {
            get => GetInternal("_overlay", false);
            set => SetInternal("_overlay", value);
        }

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SectionInnerEffectBuilder" /> class.
        /// </summary>
        /// <param name="writer">The writer that receives the generated section inner markup.</param>
        /// <param name="html">The Razor HTML helper used to register effect assets.</param>
        public SectionInnerEffectBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "div";
            this.AddClass("eb-section-inner");
        }

        #endregion

        #region Instance methods

        /// <summary>
        ///     Opens the section inner effect wrapper and writes the configured header placement.
        /// </summary>
        /// <returns>The current <see cref="SectionInnerEffectBuilder" /> instance for fluent chaining.</returns>
        public new SectionInnerEffectBuilder Begin()
        {
            if (_innerStarted)
            {
                return this;
            }

            base.Begin();
            _innerStarted = true;

            if (!string.IsNullOrEmpty(_header) && !_overlay)
            {
                _textWriter.Write(_header);
            }

            _textWriter.Write("""<div class="eb-section-inner-body">""");

            if (!string.IsNullOrEmpty(_header) && _overlay)
            {
                _textWriter.Write("""<div class="eb-section-inner-header-overlay">""");
                _textWriter.Write(_header);
                _textWriter.Write("</div>");
            }

            return this;
        }

        /// <inheritdoc />
        protected override SectionInnerEffectBuilder CreateInstance()
        {
            return new SectionInnerEffectBuilder(_textWriter, _htmlHelper);
        }

        /// <inheritdoc />
        protected override void InternalClone(SectionInnerEffectBuilder source)
        {
            base.InternalClone(source);
            _header = source._header;
            _overlay = source._overlay;
            _innerStarted = false;
            _innerDisposed = false;
        }

        /// <summary>Creates and returns a title builder scoped to this section inner container.</summary>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> call when you need a structured header area (title, subtitle, buttons) above or overlaying
        ///         the section body.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> returns a new <see cref="SectionInnerEffectTitle" /> whose <c>Build</c> method
        ///         serialises the header HTML and passes it back to this builder via <c>WriteHeader</c>. The header is rendered
        ///         either before the body div (default) or inside an overlay wrapper when <see cref="SetOverlay" /> is active.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> call <see cref="SetOverlay" /> before <see cref="Begin" /> to change header placement;
        ///         the title builder chain must end with <c>Build()</c> to register the header HTML.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> the title builder is one-shot — creating a second instance overwrites the first header;
        ///         configure everything in a single chain before calling <c>Build()</c>.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.SectionInnerEffectBuilder()
        ///     .SectionInnerEffectTitle()
        ///         .SetTitle("Our Services")
        ///         .Build()
        ///     .Begin()
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public SectionInnerEffectTitle SectionInnerEffectTitle()
        {
            return new SectionInnerEffectTitle(this);
        }

        /// <summary>
        ///     Switches the title header to overlay mode, positioning it on top of the section body content instead of above
        ///     it.
        /// </summary>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> use when the section body contains a full-bleed background image or visual element and the
        ///         title should float over it rather than push it down.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> adds the CSS class <c>eb-section-inner-overlay</c> to the container and causes
        ///         <c>Begin</c> to render the header inside a <c>.eb-section-inner-header-overlay</c> div that is positioned
        ///         absolutely over the body div.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> call before <see cref="Begin" />; has no effect after the container has been opened.
        ///         Pairs well with a full-height body that contains an image or a parallax component.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> ensure the body content has enough height to give the overlaid title room to breathe; consider a
        ///         semi-transparent background on the title for legibility over busy visuals.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.SectionInnerEffectBuilder()
        ///     .SectionInnerEffectTitle().SetTitle("Welcome").Build()
        ///     .SetOverlay()
        ///     .Begin()
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public SectionInnerEffectBuilder SetOverlay()
        {
            _overlay = true;
            this.AddClass("eb-section-inner-overlay");
            return this;
        }

        internal SectionInnerEffectBuilder WriteHeader(string html)
        {
            _header = html;
            return this;
        }

        #region From interface IDisposable

        /// <summary>
        ///     Closes the section inner effect body and root wrapper.
        /// </summary>
        public new void Dispose()
        {
            if (_innerDisposed || !_innerStarted)
            {
                return;
            }

            _textWriter.Write("</div>");
            End();
            _innerDisposed = true;
            _innerStarted = false;
        }

        #endregion

        #endregion
    }
}