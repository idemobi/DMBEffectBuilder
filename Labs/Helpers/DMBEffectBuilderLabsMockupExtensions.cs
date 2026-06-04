#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using DMBPageBuilder;

#endregion

namespace DMBEffectBuilderLabs
{
    /// <summary>
    ///     BlockBuilder extensions for UI mock-up display helpers used in InnerSectionEffect index pages.
    /// </summary>
    public static class LabsMockupExtensions
    {
        #region BlockBuilder — pill tab bar

        /// <summary>
        ///     Horizontal pill tab bar container — flex row with gap and padding, no shrink.
        /// </summary>
        public static BlockBuilder AsMockupTabBar(this BlockBuilder builder)
            => builder.AddClass("d-flex gap-2 p-2 flex-shrink-0");

        /// <summary>
        ///     Active pill tab — primary colour, white text, semi-bold.
        /// </summary>
        public static BlockBuilder AsActivePill(this BlockBuilder builder)
            => builder.AddClass("px-3 py-1 rounded-pill text-white small fw-semibold");

        /// <summary>
        ///     Inactive pill tab — small text, colour applied via inline style.
        /// </summary>
        public static BlockBuilder AsInactivePill(this BlockBuilder builder)
            => builder.AddClass("px-3 py-1 rounded-pill small");

        #endregion

        #region BlockBuilder — mock-up panels

        /// <summary>
        ///     Mock-up panel — flex row, grows to fill space, rounded, padded.
        /// </summary>
        public static BlockBuilder AsMockupPanel(this BlockBuilder builder)
            => builder.AddClass("flex-grow-1 mx-3 mb-3 rounded-3 d-flex align-items-center gap-3 px-4");

        /// <summary>
        ///     Circular icon container — centered flex, circle shape, no shrink.
        /// </summary>
        public static BlockBuilder AsMockupCircle(this BlockBuilder builder)
            => builder.AddClass("d-flex align-items-center justify-content-center rounded-circle flex-shrink-0");

        /// <summary>
        ///     White bold title inside a mock-up card — white text, bold, small bottom margin.
        /// </summary>
        public static BlockBuilder AsMockupTitle(this BlockBuilder builder)
            => builder.AddClass("text-white fw-bold mb-1");

        #endregion

        #region BlockBuilder — layered card stages

        /// <summary>
        ///     Dark stage container for layered card mock-ups — relative position, overflow hidden.
        /// </summary>
        public static BlockBuilder AsMockupDarkStage(this BlockBuilder builder)
            => builder.AddClass("position-relative overflow-hidden");

        /// <summary>
        ///     Top-most layered card spanning full width — absolute positioning, flex row, full horizontal span.
        /// </summary>
        public static BlockBuilder AsMockupLayerTop(this BlockBuilder builder)
            => builder.AddClass("position-absolute rounded-3 start-0 end-0 d-flex align-items-center gap-2 px-3");

        /// <summary>
        ///     Middle layered card — absolute positioning, flex row, width constrained via inline style.
        /// </summary>
        public static BlockBuilder AsMockupLayerMid(this BlockBuilder builder)
            => builder.AddClass("position-absolute rounded-3 d-flex align-items-center gap-2 px-3");

        /// <summary>
        ///     Back-most layered card — absolute positioning, shape only, dimensions via inline style.
        /// </summary>
        public static BlockBuilder AsMockupLayerBack(this BlockBuilder builder)
            => builder.AddClass("position-absolute rounded-3");

        #endregion
    }
}