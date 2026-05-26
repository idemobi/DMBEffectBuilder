#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj StagedRevealEffectItem.cs create at 2026/04/27
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBEffectBuilder
{
    public class StagedRevealEffectItem
    {
        /// <summary>The type of content held by this item.</summary>
        public StagedRevealEffectContentType ContentType { get; private set; }

        /// <summary>Image URL, set when <see cref="ContentType"/> is <see cref="StagedRevealEffectContentType.Image"/>.</summary>
        public string? ImageSrc    { get; private set; }

        /// <summary>Alt text for the image.</summary>
        public string? AltText     { get; private set; }

        /// <summary>Text content, set when <see cref="ContentType"/> is <see cref="StagedRevealEffectContentType.Text"/>.</summary>
        public string? TextContent { get; private set; }

        private StagedRevealEffectItem() { }

        /// <summary>Creates an item that displays an image.</summary>
        public static StagedRevealEffectItem FromImage(string src, string alt = "")
            => new() { ContentType = StagedRevealEffectContentType.Image, ImageSrc = src, AltText = alt };

        /// <summary>Creates an item that displays text.</summary>
        public static StagedRevealEffectItem FromText(string text)
            => new() { ContentType = StagedRevealEffectContentType.Text, TextContent = text };
    }
}
