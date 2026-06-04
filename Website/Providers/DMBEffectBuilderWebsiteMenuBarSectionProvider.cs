#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.IO;
using DMBBootstrapBuilder;
using DMBEffectBuilderLabs.Navigation;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBEffectBuilderWebsite;

internal sealed class DMBEffectBuilderWebsiteMenuBarSectionProvider : IMenuBarSectionProvider
{
    #region Instance fields and properties

    #region From interface IMenuBarSectionProvider

    /// <summary>
    ///     Gets the provider order used when composing the local website navbar.
    /// </summary>
    public int Order => 100;

    #endregion

    #endregion

    #region Instance methods

    #region From interface IMenuBarSectionProvider

    /// <summary>
    ///     Builds the local website navbar modules.
    /// </summary>
    /// <param name="writer">The current response writer.</param>
    /// <param name="html">The current Razor HTML helper.</param>
    /// <returns>The menu module result containing DMBEffectBuilder labs navigation links.</returns>
    public MenuBarModuleResult Build(TextWriter writer, IHtmlHelper html)
    {
        MenuBarModuleResult result = new();

        result.ActionList.Add(DMBEffectBuilderLabsNavigationAgent.CreateMenuGroup());

        return result;
    }

    /// <summary>
    ///     Determines whether the provider is enabled for the current request.
    /// </summary>
    /// <param name="html">The current Razor HTML helper.</param>
    /// <returns><see langword="true"/> for all local DMBEffectBuilder website requests.</returns>
    public bool IsEnabled(IHtmlHelper html)
    {
        return true;
    }

    #endregion

    #endregion
}
