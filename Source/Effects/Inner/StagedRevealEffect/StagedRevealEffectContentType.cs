#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBEffectBuilder
{
    /// <summary>Describes the type of content held by a <see cref="StagedRevealEffectItem" />.</summary>
    public enum StagedRevealEffectContentType
    {
        /// <summary>The item renders an <c>&lt;img&gt;</c> tag from a URL.</summary>
        Image,

        /// <summary>The item renders plain encoded text.</summary>
        Text
    }
}