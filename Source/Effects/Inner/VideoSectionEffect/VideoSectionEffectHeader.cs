#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.Text;
using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Fluent title configurator for a <see cref="VideoSectionEffectBuilder" />.
    ///     Set the title text, optional subtitle, optional button, then call <see cref="Build" /> to return to the parent
    ///     builder.
    /// </summary>
    [Documented]
    public sealed class VideoSectionEffectHeader : TitleBuilderBase<VideoSectionEffectBuilder, VideoSectionEffectHeader>
    {
        #region Instance fields and properties

        private string _buttonText = string.Empty;
        private string _buttonUrl = string.Empty;
        private VariantStyle _buttonVariant = VariantStyle.Primary;
        private bool _hasButton;

        private string _subtitle = string.Empty;

        #endregion

        #region Instance constructors and destructors

        #region Instance constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="VideoSectionEffectHeader" /> class.
        /// </summary>
        /// <param name="parent">The video section builder that receives the rendered header markup.</param>
        public VideoSectionEffectHeader(VideoSectionEffectBuilder parent)
            : base(parent, parent.HtmlHelper)
        {
        }

        #endregion

        #endregion

        #region Instance methods

        /// <inheritdoc />
        public override VideoSectionEffectBuilder Build()
        {
            PageInformation page = PageRegistry.GetOrCreatePageInformation(_html.ViewContext.HttpContext);
            page.SetStylesheet("/css/innerEffects/VideoSectionEffect.css");

            var sb = new StringBuilder();
            sb.Append("<div class=\"eb-section-inner-header\">");
            sb.Append(BuildCoreTitleHtml());

            if (!string.IsNullOrEmpty(_subtitle))
            {
                string encodedSubtitle = HtmlEncoder.Default.Encode(_subtitle);
                sb.Append($"<p class=\"eb-video-section-subtitle\">{encodedSubtitle}</p>");
            }

            if (_hasButton)
            {
                string encodedText = HtmlEncoder.Default.Encode(_buttonText);
                string encodedUrl = HtmlEncoder.Default.Encode(_buttonUrl);
                string variantClass = "btn btn" + _buttonVariant.GetOldSuffixCssClass();
                sb.Append($"<a href=\"{encodedUrl}\" class=\"{variantClass} eb-video-section-btn\">{encodedText}</a>");
            }

            sb.Append("</div>");

            _parent.WriteHeader(sb.ToString());
            return _parent;
        }

        /// <summary>Adds a call-to-action button rendered below the subtitle in the video section header.</summary>
        /// <param name="text">Button label text. HTML-encoded before output.</param>
        /// <param name="url">The href target of the button anchor. HTML-encoded before output.</param>
        /// <param name="variant">
        ///     Bootstrap variant style controlling the button colour. Default:
        ///     <see cref="VariantStyle.Primary" />.
        /// </param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> place a prominent action link — "Learn more", "Shop now", "Watch trailer" — directly in the
        ///         hero video header to drive user navigation.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> HTML-encodes both <paramref name="text" /> and <paramref name="url" />, then emits an
        ///         <c>&lt;a&gt;</c> tag with Bootstrap button classes (<c>btn btn-*</c>) and the additional class
        ///         <c>eb-video-section-btn</c> for layout styling.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> rendered after the subtitle (if any); the button is omitted entirely when this method is
        ///         not called. Pair with <see cref="SetSubtitle" /> for a classic hero pattern: title → tagline → CTA button.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> <see cref="VariantStyle.Light" /> or an outline light button style often reads better than solid
        ///         dark buttons over a video background; always test contrast with the overlay on and off.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.VideoSectionEffectBuilder("/video/hero.mp4")
        ///     .VideoSectionTitle()
        ///         .SetTitle("Discover")
        ///         .SetSubtitle("Explore our full catalogue")
        ///         .SetButton(text: "Browse now", url: "/catalogue", variant: VariantStyle.Light)
        ///         .Build()
        ///     .SetOverlay()
        ///     .Begin()
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public VideoSectionEffectHeader SetButton(string text, string url, VariantStyle variant = VariantStyle.Primary)
        {
            _hasButton = true;
            _buttonText = text;
            _buttonUrl = url;
            _buttonVariant = variant;
            return this;
        }

        /// <summary>Sets an optional subtitle rendered directly below the main title in the video section header.</summary>
        /// <param name="subtitle">Plain text subtitle. HTML-encoded before output. Default: empty (no subtitle rendered).</param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> add a short supporting sentence that elaborates on the main title — a tagline, date, or
        ///         location line.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> HTML-encodes the value via <c>HtmlEncoder.Default</c> and wraps it in a
        ///         <c>&lt;p class="eb-video-section-subtitle"&gt;</c> element appended after the title markup.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> rendered between the title (from the base class) and the button (from
        ///         <see cref="SetButton" />); omit entirely if no subtitle is needed — the element is not emitted when the value
        ///         is empty.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> keep subtitle text concise (one sentence); longer text may overflow on small viewports when the
        ///         video section has a fixed height.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.VideoSectionEffectBuilder("/video/hero.mp4")
        ///     .VideoSectionTitle()
        ///         .SetTitle("Our Story")
        ///         .SetSubtitle("A journey through innovation")
        ///         .Build()
        ///     .Begin()
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public VideoSectionEffectHeader SetSubtitle(string subtitle)
        {
            _subtitle = subtitle;
            return this;
        }

        #endregion
    }
}