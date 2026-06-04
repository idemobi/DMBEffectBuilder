#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBEffectBuilder
{
    /// <summary>
    ///     Fluent builder that wraps a content block between two animated torn-band decorators.
    ///     The bottom band is written on <see cref="Begin" />; the top band is written on <see cref="Dispose" />.
    ///     Use inside a <c>@using</c> block so that <see cref="Dispose" /> is called automatically.
    /// </summary>
    [Documented]
    public sealed class TornBandEffectBuilder : IDisposable
    {
        #region Instance constructors and destructors

        #region Instance constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TornBandEffectBuilder" /> class.
        /// </summary>
        /// <param name="writer">The writer that receives the generated torn band markup.</param>
        /// <param name="html">The Razor HTML helper used to register effect assets.</param>
        public TornBandEffectBuilder(TextWriter writer, IHtmlHelper html)
        {
            _writer = writer;
            _html = html;
        }

        #endregion

        #endregion

        #region Instance methods

        /// <summary>Writes the bottom torn band to the output and opens the inner content wrapper div.</summary>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> always the first call after configuring the builder — it emits the bottom decorative band
        ///         and the opening tag for the section body.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> registers the required stylesheet via <c>PageRegistry</c>, renders the bottom
        ///         <c>eb-torn-band</c> div with its computed <c>clip-path</c> and gradient styles, then writes the opening
        ///         <c>eb-torn-band-content</c> div. Calling <c>Dispose</c> (via <c>@using</c>) closes the content div and renders
        ///         the mirror top band.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> all configuration methods (<see cref="SetGradient" />, <see cref="SetBandHeight" />,
        ///         <see cref="SetDirection" />, <see cref="SetTilt" />, <see cref="SetTornPoints" />) must be called before
        ///         <c>Begin</c>; they have no effect afterwards.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> always wrap in a Razor <c>@using</c> block to guarantee <c>Dispose</c> is called and the top
        ///         band is rendered — forgetting this leaves an unclosed div.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// @using (Html.TornBandEffectBuilder().SetGradient("#FFD700", "#FF8C00").Begin())
        /// {
        ///     &lt;p&gt;Content between the torn bands.&lt;/p&gt;
        /// }
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public TornBandEffectBuilder Begin()
        {
            if (_started) return this;
            _started = true;

            PageInformation page = PageRegistry.GetOrCreatePageInformation(_html.ViewContext.HttpContext);
            page.SetStylesheet("/css/innerEffects/TornBandEffect.css");

            WriteBand(DiagonalEdge.Bottom);
            _writer.Write("""<div class="eb-torn-band-content">""");

            return this;
        }

        /// <summary>Sets the height of each torn-paper band rendered above and below the content.</summary>
        /// <param name="height">Numeric height value. Default: <c>150</c>.</param>
        /// <param name="unit">CSS unit to append to the value. Default: <see cref="UnitSize.px" />.</param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> increase for a bold decorative separator; decrease for a subtle accent that does not take
        ///         too much vertical space.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> combines <paramref name="height" /> and <paramref name="unit" /> into a CSS
        ///         <c>height</c> property applied inline to each band div.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> larger bands pair better with a higher <see cref="SetTornPoints" /> count so the ragged
        ///         texture remains visible at the greater scale; relative units (<c>vh</c>) can adapt to viewport height.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> 100–200 px suits most desktop layouts; on mobile, 60–80 px prevents the bands from dominating
        ///         the viewport.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.TornBandEffectBuilder()
        ///     .SetBandHeight(height: 120, unit: UnitSize.px)
        ///     .Begin()
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public TornBandEffectBuilder SetBandHeight(int height, UnitSize unit = UnitSize.px)
        {
            _bandHeight = height;
            _bandUnit = unit;
            return this;
        }

        /// <summary>Sets the diagonal direction of the torn edge on both bands.</summary>
        /// <param name="direction">
        ///     The direction the diagonal runs across the band. Default:
        ///     <see cref="DiagonalDirection.LeftToRight" />.
        /// </param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> choose a direction that complements the visual flow of the page — a left-to-right slope can
        ///         reinforce a reading direction or match a slanted hero image.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> passes the value to <c>DiagonalEdgeHelper.BuildClipPath</c> which constructs a CSS
        ///         <c>clip-path polygon</c> with the appropriate slant applied to both the top and bottom bands.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> works independently of <see cref="SetTilt" />; the tilt value controls the steepness
        ///         while this controls which side is higher.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> <see cref="DiagonalDirection.LeftToRight" /> and <see cref="DiagonalDirection.RightToLeft" />
        ///         are mirror images — switch between them to match adjacent section slopes.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.TornBandEffectBuilder()
        ///     .SetDirection(DiagonalDirection.RightToLeft)
        ///     .SetTilt(12m)
        ///     .Begin()
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public TornBandEffectBuilder SetDirection(DiagonalDirection direction)
        {
            _direction = direction;
            return this;
        }

        /// <summary>Sets the animated gradient fill applied to both torn bands.</summary>
        /// <param name="colorStart">Starting CSS color of the gradient (e.g. <c>#FFD700</c>). Default: <c>#FFD700</c>.</param>
        /// <param name="colorEnd">Ending CSS color of the gradient (e.g. <c>#FF8C00</c>). Default: <c>#FF8C00</c>.</param>
        /// <param name="angle">Gradient angle in degrees. Default: <c>90</c> (left to right).</param>
        /// <param name="durationSeconds">Duration of one full animation cycle in seconds. Default: <c>5</c>.</param>
        /// <param name="curve">
        ///     Easing curve controlling gradient shift speed. Default:
        ///     <see cref="GradientAnimationCurve.EaseInOut" />.
        /// </param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> call to brand the torn bands with site colours, or to create a lively animated highlight
        ///         separating sections.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> injects CSS custom properties (<c>--gradient-start</c>, <c>--gradient-end</c>,
        ///         <c>--gradient-angle</c>, etc.) as inline styles and drives them with a <c>background-size: 200%</c> keyframe
        ///         animation.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> the gradient applies to both the top and bottom bands equally; combine with
        ///         <see cref="SetBandHeight" /> to make the colour band more prominent, and <see cref="SetTornPoints" /> to
        ///         control how rough the edges appear.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> complementary colours (e.g. warm gold to orange) work well; very short durations (&lt; 2 s) can
        ///         feel distracting — 4–8 s is a comfortable range.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.TornBandEffectBuilder()
        ///     .SetGradient(colorStart: "#1a73e8", colorEnd: "#0d47a1", angle: 135m, durationSeconds: 6m)
        ///     .Begin()
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public TornBandEffectBuilder SetGradient(
            string colorStart,
            string colorEnd,
            decimal angle = 90m,
            decimal durationSeconds = 5m,
            GradientAnimationCurve curve = GradientAnimationCurve.EaseInOut
        )
        {
            _colorStart = colorStart;
            _colorEnd = colorEnd;
            _gradientAngle = angle;
            _gradientDuration = durationSeconds;
            _gradientCurve = curve;
            return this;
        }

        /// <summary>Fixes the tilt angle of the diagonal tear edge in degrees, overriding the default random value.</summary>
        /// <param name="tilt">
        ///     Tilt angle in degrees. Clamped to the range <c>1–40°</c> at render time. Default: random value
        ///     between 8° and 18° per render.
        /// </param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> call when a consistent, reproducible slope is needed across page refreshes or server-side
        ///         renders — e.g. for screenshots, print layouts, or design review.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> stores the angle and passes it directly to <c>DiagonalEdgeHelper.BuildClipPath</c>,
        ///         bypassing the <c>Random.Shared</c> fallback that normally varies between 8° and 18°.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> pair with <see cref="SetDirection" /> to fully control the diagonal appearance; omit to
        ///         keep the lively random-per-render behaviour.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> values below 5° are nearly flat and may not read as a deliberate tear; values above 30° become
        ///         very steep and can clip unexpectedly at narrow viewports.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.TornBandEffectBuilder()
        ///     .SetTilt(tilt: 15m)
        ///     .SetDirection(DiagonalDirection.LeftToRight)
        ///     .Begin()
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public TornBandEffectBuilder SetTilt(decimal tilt)
        {
            _tilt = tilt;
            return this;
        }

        /// <summary>Sets the number of vertices used to generate the jagged torn edge on both bands.</summary>
        /// <param name="points">Number of torn-edge vertices. Default: <c>30</c>. Must be greater than 0.</param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <remarks>
        ///     <para>
        ///         <b>Use cases:</b> increase for a rougher, more organic paper-tear texture; decrease for a smoother, more
        ///         stylised wave.
        ///     </para>
        ///     <para>
        ///         <b>How it works:</b> passed to <c>DiagonalEdgeHelper.BuildClipPath</c>, which distributes this many
        ///         random-offset points along the diagonal edge to construct the <c>clip-path</c> polygon.
        ///     </para>
        ///     <para>
        ///         <b>Combinations:</b> more points are most noticeable on taller bands (see <see cref="SetBandHeight" />); with
        ///         very thin bands, extra points may not be visible and only add HTML size.
        ///     </para>
        ///     <para>
        ///         <b>Tips:</b> 15–20 points gives a clean stylised tear; 40–60 gives a highly irregular paper look; values
        ///         above 80 rarely add visible detail and increase markup size.
        ///     </para>
        ///     <para>
        ///         <b>Example:</b>
        ///         <code>
        /// Html.TornBandEffectBuilder()
        ///     .SetTornPoints(points: 50)
        ///     .SetBandHeight(height: 200)
        ///     .Begin()
        /// </code>
        ///     </para>
        /// </remarks>
        [Documented]
        public TornBandEffectBuilder SetTornPoints(int points)
        {
            _tornPoints = points;
            return this;
        }

        private void WriteBand(DiagonalEdge edge)
        {
            decimal resolvedTilt = _tilt ?? Random.Shared.Next(8, 18);
            resolvedTilt = Math.Clamp(resolvedTilt, 1m, 40m);

            string clipPath = DiagonalEdgeHelper.BuildClipPath(edge, _direction, resolvedTilt, _tornPoints);
            string height = $"{_bandHeight}{_bandUnit.GetCss()}";

            string encodedStart = HtmlEncoder.Default.Encode(_colorStart);
            string encodedEnd = HtmlEncoder.Default.Encode(_colorEnd);
            string curve = _gradientCurve.GetCss();
            string angle = _gradientAngle.ToString(CultureInfo.InvariantCulture);
            string duration = _gradientDuration.ToString(CultureInfo.InvariantCulture);

            var style = new StringBuilder();
            style.Append($"--gradient-start:{encodedStart};");
            style.Append($"--gradient-end:{encodedEnd};");
            style.Append($"--gradient-angle:{angle}deg;");
            style.Append($"--gradient-duration:{duration}s;");
            style.Append($"--gradient-curve:{curve};");
            style.Append($"background:linear-gradient(var(--gradient-angle),var(--gradient-start),var(--gradient-end));");
            style.Append($"background-size:200% 200%;");
            style.Append($"animation:eb-torn-band-gradient-shift var(--gradient-duration) var(--gradient-curve) infinite alternate;");
            style.Append($"height:{height};");
            style.Append($"clip-path:{clipPath};");

            _writer.Write($"""<div class="eb-torn-band" style="{style}"></div>""");
        }

        #region From interface IDisposable

        /// <summary>
        ///     Closes the content wrapper and writes the top torn band.
        /// </summary>
        public void Dispose()
        {
            if (_disposed || !_started) return;
            _disposed = true;

            _writer.Write("</div>");
            WriteBand(DiagonalEdge.Top);
        }

        #endregion

        #endregion

        #region Instance fields

        private readonly TextWriter _writer;
        private readonly IHtmlHelper _html;

        private bool _started;
        private bool _disposed;

        private string _colorStart = "#FFD700";
        private string _colorEnd = "#FF8C00";
        private decimal _gradientAngle = 90m;
        private decimal _gradientDuration = 5m;
        private GradientAnimationCurve _gradientCurve = GradientAnimationCurve.EaseInOut;

        private int _bandHeight = 150;
        private UnitSize _bandUnit = UnitSize.px;

        private DiagonalDirection _direction = DiagonalDirection.LeftToRight;
        private decimal? _tilt = null;
        private int _tornPoints = 30;

        #endregion
    }
}