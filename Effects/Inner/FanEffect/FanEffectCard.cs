#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBEffectBuilder.csproj FanEffectCard.cs create at 2026/04/29
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using System.Text;
using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;

namespace DMBEffectBuilder
{
    /// <summary>A single card displayed inside a <see cref="FanEffectBuilder"/>.</summary>
    public sealed class FanEffectCard
    {
        /// <summary>Optional image URL shown as the card background.</summary>
        public string? ImageSrc { get; private set; }

        /// <summary>Alt text for the image.</summary>
        public string? AltText { get; private set; }

        /// <summary>Optional HTML content rendered inside the card body.</summary>
        public IHtmlContent? Content { get; private set; }

        private FanEffectCard() { }

        /// <summary>Creates a card with a background image and no overlay content.</summary>
        /// <param name="src">Image URL used as the card background.</param>
        /// <param name="alt">Alt text for the image.</param>
        /// <returns>A new <see cref="FanEffectCard"/> instance.</returns>
        [Documented]
        public static FanEffectCard FromImage(string src, string alt = "")
            => new() { ImageSrc = src, AltText = alt };

        /// <summary>Creates a card with arbitrary pre-built HTML content and no background image.</summary>
        /// <param name="content">HTML content rendered inside the card body.</param>
        /// <returns>A new <see cref="FanEffectCard"/> instance.</returns>
        [Documented]
        public static FanEffectCard FromContent(IHtmlContent content)
            => new() { Content = content };

        /// <summary>Creates a card with a background image and overlaid HTML content.</summary>
        /// <param name="src">Image URL used as the card background.</param>
        /// <param name="content">HTML content rendered on top of the image.</param>
        /// <param name="alt">Alt text for the image.</param>
        /// <returns>A new <see cref="FanEffectCard"/> instance.</returns>
        [Documented]
        public static FanEffectCard FromImageAndContent(string src, IHtmlContent content, string alt = "")
            => new() { ImageSrc = src, AltText = alt, Content = content };

        /// <summary>Creates a card with a background image and a Razor template delegate rendered on top — the preferred overload in <c>.cshtml</c> views.</summary>
        /// <param name="src">Image URL used as the card background.</param>
        /// <param name="template">Razor template delegate (<c>@&lt;div&gt;...&lt;/div&gt;</c>) producing the overlay HTML.</param>
        /// <param name="alt">Alt text for the image.</param>
        /// <returns>A new <see cref="FanEffectCard"/> instance.</returns>
        [Documented]
        public static FanEffectCard FromImageAndContent(string src, Func<dynamic, IHtmlContent> template, string alt = "")
            => new() { ImageSrc = src, AltText = alt, Content = template(null!) };

        /// <summary>Creates a card with a Razor template delegate as content and no background image — the preferred overload in <c>.cshtml</c> views.</summary>
        /// <param name="template">Razor template delegate (<c>@&lt;div&gt;...&lt;/div&gt;</c>) producing the card HTML.</param>
        /// <returns>A new <see cref="FanEffectCard"/> instance.</returns>
        [Documented]
        public static FanEffectCard FromContent(Func<dynamic, IHtmlContent> template)
            => new() { Content = template(null!) };

        /// <summary>
        /// Creates a character card: a background image with an overlaid badge and name.
        /// Use this overload for the standard character-selection layout.
        /// </summary>
        /// <param name="src">Image URL used as the card background.</param>
        /// <param name="alt">Alt text for the image.</param>
        /// <param name="badgeVariant">Bootstrap variant color for the badge.</param>
        /// <param name="badgeName">Short label inside the badge, e.g. <c>"Mage"</c>.</param>
        /// <param name="characterName">Character name displayed below the badge.</param>
        /// <returns>A new <see cref="FanEffectCard"/> instance.</returns>
        [Documented]
        public static FanEffectCard Character(
            string src, string alt,
            VariantStyle badgeVariant, string badgeName, string characterName)
        {
            var enc = HtmlEncoder.Default;
            var content = new HtmlString(
                $"""<div><span class="badge bg-{VariantToBg(badgeVariant)} mb-1">{enc.Encode(badgeName)}</span><h5 class="text-white mb-0">{enc.Encode(characterName)}</h5></div>""");
            return new() { ImageSrc = src, AltText = alt, Content = content };
        }

        /// <summary>
        /// Creates a text-only character card with a badge, name and optional subtitle — no background image.
        /// Use this overload when no illustration is available.
        /// </summary>
        /// <param name="badgeVariant">Bootstrap variant color for the badge.</param>
        /// <param name="badgeName">Short label inside the badge, e.g. <c>"Warrior"</c>.</param>
        /// <param name="characterName">Character name displayed prominently.</param>
        /// <param name="subtitle">Optional subtitle line, e.g. a class or guild name.</param>
        /// <returns>A new <see cref="FanEffectCard"/> instance.</returns>
        [Documented]
        public static FanEffectCard CharacterText(
            VariantStyle badgeVariant, string badgeName, string characterName, string? subtitle = null)
        {
            var enc = HtmlEncoder.Default;
            var sb = new StringBuilder();
            sb.Append("""<div class="d-flex flex-column align-items-center justify-content-center h-100 gap-2 text-center px-3">""");
            sb.Append($"""<span class="badge bg-{VariantToBg(badgeVariant)}">{enc.Encode(badgeName)}</span>""");
            sb.Append($"""<h4 class="mb-0">{enc.Encode(characterName)}</h4>""");
            if (subtitle != null)
                sb.Append($"""<h6 class="fw-normal small text-muted mb-0">{enc.Encode(subtitle)}</h6>""");
            sb.Append("</div>");
            return new() { Content = new HtmlString(sb.ToString()) };
        }

        /// <summary>
        /// Creates a playing-card style card with a badge (rank) and a card name.
        /// Use this overload for card-hand or deck layouts.
        /// </summary>
        /// <param name="badgeText">Rank label displayed in the badge, e.g. <c>"A"</c>, <c>"10"</c>, <c>"K"</c>.</param>
        /// <param name="badgeVariant">Bootstrap variant color for the badge.</param>
        /// <param name="cardName">Card name displayed below the badge, e.g. <c>"Ace"</c>, <c>"King"</c>.</param>
        /// <returns>A new <see cref="FanEffectCard"/> instance.</returns>
        [Documented]
        public static FanEffectCard PlayingCard(string badgeText, VariantStyle badgeVariant, string cardName)
        {
            var enc = HtmlEncoder.Default;
            var content = new HtmlString(
                $"""<div class="d-flex flex-column align-items-center justify-content-center h-100 gap-2 text-center px-2"><span class="badge bg-{VariantToBg(badgeVariant)}">{enc.Encode(badgeText)}</span><h5 class="mb-0">{enc.Encode(cardName)}</h5></div>""");
            return new() { Content = content };
        }

        private static string VariantToBg(VariantStyle variant) => variant switch
        {
            VariantStyle.Primary   => "primary",
            VariantStyle.Secondary => "secondary",
            VariantStyle.Success   => "success",
            VariantStyle.Danger    => "danger",
            VariantStyle.Warning   => "warning",
            VariantStyle.Info      => "info",
            VariantStyle.Light     => "light",
            VariantStyle.Dark      => "dark",
            _                      => "secondary"
        };
    }
}
