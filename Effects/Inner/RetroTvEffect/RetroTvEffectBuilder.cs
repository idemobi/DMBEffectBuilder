#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.Text;
using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Fluent builder that renders a retro CRT television widget with configurable channels.
    ///     Each channel is either a solid color with metadata or a full-screen image with overlay text.
    /// </summary>
    [Documented]
    public sealed class RetroTvEffectBuilder : IHtmlContent
    {
        #region Static methods

        private static string Js(string s)
            => '"' + s.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "") + '"';

        private static (int r, int g, int b) ParseHex(string hex)
        {
            hex = hex.TrimStart('#');
            if (hex.Length == 3) hex = $"{hex[0]}{hex[0]}{hex[1]}{hex[1]}{hex[2]}{hex[2]}";
            return (Convert.ToInt32(hex[..2], 16), Convert.ToInt32(hex[2..4], 16), Convert.ToInt32(hex[4..6], 16));
        }

        private static string Scale(int r, int g, int b, float f)
        {
            int cr = Math.Clamp((int)(r * f), 0, 255);
            int cg = Math.Clamp((int)(g * f), 0, 255);
            int cb = Math.Clamp((int)(b * f), 0, 255);
            return $"#{cr:x2}{cg:x2}{cb:x2}";
        }

        #endregion

        #region Instance fields and properties

        private int _autoAdvanceMs = 6000;
        private string _brand = "Idémobi";
        private string? _cabinetColor = null;
        private readonly List<RetroTvChannel> _channels = [];
        private readonly IHtmlHelper _html;

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="RetroTvEffectBuilder" /> class.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to register effect assets.</param>
        public RetroTvEffectBuilder(IHtmlHelper html) => _html = html;

        #endregion

        #region Instance methods

        /// <summary>Adds a pre-built <see cref="RetroTvChannel" /> instance.</summary>
        [Documented]
        public RetroTvEffectBuilder AddChannel(RetroTvChannel channel)
        {
            _channels.Add(channel);
            return this;
        }

        /// <summary>Adds a solid-color channel that displays the color, its hex value and animated RGB bars.</summary>
        [Documented]
        public RetroTvEffectBuilder AddColorChannel(string name, string hexColor, string desc = "")
        {
            _channels.Add(RetroTvChannel.FromColor(name, hexColor, desc));
            return this;
        }

        /// <summary>Adds an image channel that fills the screen with the image and overlays the name and description.</summary>
        [Documented]
        public RetroTvEffectBuilder AddImageChannel(string imageSrc, string name, string desc = "")
        {
            _channels.Add(RetroTvChannel.FromImage(imageSrc, name, desc));
            return this;
        }

        private string BuildJson()
        {
            var sb = new StringBuilder("[");
            for (int i = 0; i < _channels.Count; i++)
            {
                if (i > 0) sb.Append(',');
                var ch = _channels[i];
                sb.Append('{');
                sb.Append($"\"ch\":{i + 1}");
                sb.Append($",\"name\":{Js(ch.Name)}");
                sb.Append($",\"desc\":{Js(ch.Desc)}");
                if (ch.Color is not null) sb.Append($",\"color\":{Js(ch.Color)}");
                if (ch.ImageSrc is not null) sb.Append($",\"image\":{Js(ch.ImageSrc)}");
                sb.Append('}');
            }

            sb.Append(']');
            return sb.ToString();
        }

        /// <summary>
        ///     Sets the auto-advance interval in milliseconds. Pass <c>0</c> to disable auto-advance.
        ///     Default: <c>6000</c> (6 seconds).
        /// </summary>
        [Documented]
        public RetroTvEffectBuilder SetAutoAdvance(int ms)
        {
            _autoAdvanceMs = Math.Max(0, ms);
            return this;
        }

        /// <summary>Sets the brand name displayed below the screen. Default: <c>"Idémobi"</c>.</summary>
        [Documented]
        public RetroTvEffectBuilder SetBrand(string brand)
        {
            _brand = brand;
            return this;
        }

        /// <summary>
        ///     Sets the cabinet color from a hex value. All gradient shades (top, mid, bottom, bezel)
        ///     are computed automatically from this base color.
        /// </summary>
        /// <param name="hexColor">Base hex color, e.g. <c>"#2e3d4f"</c>.</param>
        [Documented]
        public RetroTvEffectBuilder SetCabinetColor(string hexColor)
        {
            _cabinetColor = hexColor;
            return this;
        }

        #region From interface IHtmlContent

        /// <summary>
        ///     Writes the complete effect markup to the provided output writer.
        /// </summary>
        /// <param name="writer">The writer receiving generated HTML.</param>
        /// <param name="encoder">The encoder used to encode generated HTML.</param>
        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            PageInformation page = PageRegistry.GetOrCreatePageInformation(_html.ViewContext.HttpContext);
            page.SetStylesheet("/css/innerEffects/RetroTvEffect.css");
            page.SetScriptFile("/js/innerEffects/RetroTvEffect.js");

            var brand = encoder.Encode(_brand);
            var autoAttr = $"""data-auto="{_autoAdvanceMs}" """;
            var json = BuildJson();

            var colorStyle = "";
            if (_cabinetColor is not null)
            {
                var (r, g, b) = ParseHex(_cabinetColor);
                float luma = (r * 0.299f + g * 0.587f + b * 0.114f) / 255f;
                var (br, bg, bb) = luma > 0.35f ? (30, 30, 40) : (200, 191, 173);
                colorStyle = $"style=\"" +
                             $"--eb-tv-cab-a:{Scale(r, g, b, 1.45f)};" +
                             $"--eb-tv-cab-b:{Scale(r, g, b, 1.0f)};" +
                             $"--eb-tv-cab-c:{Scale(r, g, b, 0.62f)};" +
                             $"--eb-tv-bzl-a:{Scale(r, g, b, 0.48f)};" +
                             $"--eb-tv-bzl-b:{Scale(r, g, b, 0.32f)};" +
                             $"--eb-tv-shd-a:{Scale(r, g, b, 0.38f)};" +
                             $"--eb-tv-shd-b:{Scale(r, g, b, 0.26f)};" +
                             $"--eb-tv-btn-r:{br};" +
                             $"--eb-tv-btn-g:{bg};" +
                             $"--eb-tv-btn-b:{bb};" +
                             "\"";
            }

            writer.Write($"""<div class="eb-retrotv" {autoAttr}{colorStyle}>""");

            writer.Write("""<div class="eb-retrotv-cabinet">""");

            writer.Write("""<div class="eb-retrotv-bezel">""");
            writer.Write("""<div class="eb-retrotv-screen" tabindex="0">""");
            writer.Write("""<canvas class="eb-retrotv-canvas"></canvas>""");
            writer.Write("""<div class="eb-retrotv-scanlines"></div>""");
            writer.Write("""<div class="eb-retrotv-glow"></div>""");
            writer.Write("""<div class="eb-retrotv-glass"></div>""");
            writer.Write("</div>"); // screen
            writer.Write("</div>"); // bezel

            writer.Write("""<div class="eb-retrotv-controls">""");
            writer.Write($"""<div class="eb-retrotv-brand">{brand}</div>""");
            writer.Write("""<div class="eb-retrotv-indicator">""");
            writer.Write("""<div class="eb-retrotv-led"></div>""");
            writer.Write("""<span class="eb-retrotv-onair">ON AIR</span>""");
            writer.Write("</div>");
            writer.Write("""<div class="eb-retrotv-knobs">""");
            writer.Write("""<div class="eb-retrotv-knob" data-action="prev"></div>""");
            writer.Write("""<div class="eb-retrotv-knob eb-retrotv-knob-lg" data-action="next"></div>""");
            writer.Write("</div>");
            writer.Write("</div>"); // controls

            writer.Write("</div>"); // cabinet

            writer.Write("""<div class="eb-retrotv-strip"></div>""");
            writer.Write($"""<script type="application/json" class="eb-retrotv-channels">{json}</script>""");
            writer.Write("</div>");
        }

        #endregion

        #endregion
    }
}