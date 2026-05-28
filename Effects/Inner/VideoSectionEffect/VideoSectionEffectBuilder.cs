#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBBootstrapBuilder.csproj VideoSectionEffectBuilder.cs create at 2026/04/15
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBEffectBuilder
{
    /// <summary>
    /// Fluent builder that renders a full-width video background section.
    /// Chain <see cref="SetOverlay"/> to darken the video, then <see cref="VideoSectionHeader"/> to add a title block on top.
    /// </summary>
    [Documented]
    public sealed class VideoSectionEffectBuilder : HtmlTagBuilder<VideoSectionEffectBuilder>, IDisposable, ICanUseHeight, ICanUseCustomClasses, ICanUseMargin
    {
        #region Instance fields and properties

        private bool _videoDisposed
        {
            get => GetInternal("_videoDisposed", false);
            set => SetInternal("_videoDisposed", value);
        }

        private bool _videoStarted
        {
            get => GetInternal("_videoStarted", false);
            set => SetInternal("_videoStarted", value);
        }

        private string? _header
        {
            get => GetInternal<string?>("_header", null);
            set => SetInternal("_header", value);
        }

        private bool _overlay
        {
            get => GetInternal("_overlay", false);
            set => SetInternal("_overlay", value);
        }

        private string _videoUrl
        {
            get => GetInternal("_videoUrl", string.Empty);
            set => SetInternal("_videoUrl", value);
        }

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoSectionEffectBuilder"/> class.
        /// </summary>
        /// <param name="writer">The writer that receives the generated video section markup.</param>
        /// <param name="html">The Razor HTML helper used to register effect assets.</param>
        /// <param name="videoUrl">The URL of the background video to render.</param>
        public VideoSectionEffectBuilder(TextWriter writer, IHtmlHelper html, string videoUrl)
            : base(writer, html)
        {
            _videoUrl = videoUrl ?? string.Empty;
            _tag = "div";
            This().AddClass("eb-video-section");
        }

        #endregion

        #region Instance methods

        /// <inheritdoc/>
        protected override VideoSectionEffectBuilder CreateInstance()
        {
            return new VideoSectionEffectBuilder(_textWriter, _htmlHelper, _videoUrl);
        }

        /// <inheritdoc/>
        protected override void InternalClone(VideoSectionEffectBuilder source)
        {
            base.InternalClone(source);

            _videoStarted = false;
            _videoDisposed = false;
            _header = source._header;
            _overlay = source._overlay;
            _videoUrl = source._videoUrl;
        }

        /// <summary>Opens a title configuration chain for the video section's header block.</summary>
        /// <returns>A <see cref="VideoSectionEffectHeader"/> for configuring the title, subtitle, and optional call-to-action button.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> call whenever the video section needs a visible text header — a main title, an optional subtitle, and an optional button to drive user action.</para>
        /// <para><b>How it works:</b> returns a new <see cref="VideoSectionEffectHeader"/> instance; calling <c>Build()</c> on it serialises the header HTML and registers it on this builder via <c>WriteHeader</c>. On <see cref="Begin"/>, the header is emitted either before the body div or inside an overlay wrapper depending on <see cref="SetOverlay"/>.</para>
        /// <para><b>Combinations:</b> configure <see cref="SetOverlay"/> before <see cref="Begin"/> to control header placement; chain <c>SetSubtitle</c> and <c>SetButton</c> on the title builder before calling <c>Build()</c> to return here.</para>
        /// <para><b>Tips:</b> the title builder is single-use — a second call replaces the previously registered header; complete the entire chain in one go.</para>
        /// <para><b>Example:</b>
        /// <code>
        /// Html.VideoSectionEffectBuilder("/video/hero.mp4")
        ///     .VideoSectionHeader()
        ///         .SetTitle("Our Story")
        ///         .SetSubtitle("Watch and discover")
        ///         .SetButton("Learn more", "/about")
        ///         .Build()
        ///     .SetOverlay()
        ///     .Begin()
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public VideoSectionEffectHeader VideoSectionHeader()
        {
            return new VideoSectionEffectHeader(this);
        }

        /// <summary>Adds a semi-transparent dark overlay between the video background and the title content to improve text legibility.</summary>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        /// <para><b>Use cases:</b> use whenever the video is bright, colourful, or visually busy and white/light-coloured title text would be hard to read without a darkening layer.</para>
        /// <para><b>How it works:</b> sets an internal flag that causes <c>Begin</c> to render the header inside a <c>.eb-video-section-overlay</c> div, which the stylesheet styles with a semi-transparent dark background positioned over the video element.</para>
        /// <para><b>Combinations:</b> call before <see cref="Begin"/> — the flag is read at render time and has no effect afterwards. Works alongside <see cref="VideoSectionHeader"/> regardless of call order.</para>
        /// <para><b>Tips:</b> the overlay opacity is controlled by the stylesheet; if the default darkening is too heavy or too light, override <c>.eb-video-section-overlay</c> in a custom CSS file rather than duplicating the builder.</para>
        /// <para><b>Example:</b>
        /// <code>
        /// Html.VideoSectionEffectBuilder("/video/background.mp4")
        ///     .VideoSectionHeader().SetTitle("Hero Title").Build()
        ///     .SetOverlay()
        ///     .Begin()
        /// </code>
        /// </para>
        /// </remarks>
        [Documented]
        public VideoSectionEffectBuilder SetOverlay()
        {
            _overlay = true;
            return this;
        }

        internal VideoSectionEffectBuilder WriteHeader(string html)
        {
            _header = html;
            return this;
        }

        /// <summary>
        /// Opens the video section wrapper and writes the video background layer.
        /// </summary>
        /// <returns>The current <see cref="VideoSectionEffectBuilder"/> instance for fluent chaining.</returns>
        public new VideoSectionEffectBuilder Begin()
        {
            if (_videoStarted)
            {
                return this;
            }

            base.Begin();
            _videoStarted = true;

            WriteInnerContent(_textWriter, HtmlEncoder.Default);

            return this;
        }

        void IDisposable.Dispose()
        {
            if (_videoDisposed || !_videoStarted)
            {
                return;
            }

            _textWriter.Write("</div>");
            End();

            _videoDisposed = true;
            _videoStarted = false;
        }

        /// <inheritdoc/>
        protected override void WriteToCore(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write($"<{GetTag()}{BuildAttributes()}>");
            WriteInnerContent(writer, encoder);
            writer.Write("</div>");
            writer.Write($"</{GetTag()}>");
        }

        private void WriteInnerContent(TextWriter writer, HtmlEncoder encoder)
        {
            string encodedUrl = encoder.Encode(_videoUrl);

            writer.Write($"""
                          <video class="eb-video-section-bg" autoplay muted loop playsinline>
                              <source src="{encodedUrl}" type="video/mp4" />
                          </video>
                          """);

            if (!string.IsNullOrEmpty(_header) && !_overlay)
            {
                writer.Write(_header);
            }

            writer.Write("""<div class="eb-video-section-body">""");

            if (!string.IsNullOrEmpty(_header) && _overlay)
            {
                writer.Write("""<div class="eb-video-section-overlay">""");
                writer.Write(_header);
                writer.Write("</div>");
            }
        }

        #endregion
    }
}
