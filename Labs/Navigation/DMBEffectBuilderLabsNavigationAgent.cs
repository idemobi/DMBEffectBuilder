#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using DMBPageBuilder;

#endregion

namespace DMBEffectBuilderLabs.Navigation;

/// <summary>
///     Provides reusable navigation fragments for DMBEffectBuilder labs hosts.
/// </summary>
/// <remarks>
///     The agent only builds DMBEffectBuilder-specific menu and sidebar fragments. Host websites remain
///     responsible for assembling these fragments into their own navbar providers, sidebar filters, and
///     global navigation structures.
/// </remarks>
public static class DMBEffectBuilderLabsNavigationAgent
{
    #region Static methods

    /// <summary>
    ///     Creates an action item for a DMBEffectBuilder labs page.
    /// </summary>
    /// <param name="controller">The MVC controller name.</param>
    /// <param name="action">The MVC action name.</param>
    /// <param name="title">The action title shown in navigation UI.</param>
    /// <param name="icon">The Bootstrap Icons CSS class used by the action.</param>
    /// <param name="currentController">The current MVC controller name used to mark the action active.</param>
    /// <param name="currentAction">The current MVC action name used to mark the action active.</param>
    /// <returns>The configured <see cref="AspRouteActionItem" />.</returns>
    public static AspRouteActionItem CreateAction(
        string controller,
        string action,
        string title,
        string icon,
        string? currentController = null,
        string? currentAction = null
    )
    {
        bool active =
            string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase);

        return ActionItemFactory.AspRoute(controller, action)
            .SetTitle(title)
            .SetIcon(IconStruct.Bootstrap(icon))
            .SetActive(active);
    }

    /// <summary>
    ///     Creates the DMBEffectBuilder navbar menu group.
    /// </summary>
    /// <returns>The configured <see cref="GroupActionItem" /> containing DMBEffectBuilder labs page links.</returns>
    public static GroupActionItem CreateMenuGroup()
    {
        return ActionItemFactory.Group("DMBEffectBuilder", IconStruct.Bootstrap("bi-stars"))
            .AddItems(
                ActionItemFactory.Group("General", IconStruct.Bootstrap("bi-info-circle"))
                    .AddItems(
                        CreateAction("EffectBuilder", "Introduction", "Introduction", "bi-info-circle"),
                        CreateAction("EffectBuilder", "GettingStarted", "Getting Started", "bi-play-circle"),
                        CreateAction("EffectBuilder", "Architecture", "Architecture", "bi-diagram-3"),
                        CreateAction("EffectBuilder", "RenderingPipeline", "Rendering Pipeline", "bi-bezier2")
                    ),
                ActionItemFactory.Group("Image", IconStruct.Bootstrap("bi-image"))
                    .AddItems(
                        CreateAction("ImageEffect", "Index", "Catalog", "bi-collection"),
                        CreateAction("ImageEffect", "PhotoHover", "Photo & Hover", "bi-camera"),
                        CreateAction("ImageEffect", "Stylise", "Stylise", "bi-magic"),
                        CreateAction("ImageEffect", "Concatenation", "Concatenation", "bi-plus-circle")
                    ),
                ActionItemFactory.Group("Section", IconStruct.Bootstrap("bi-layout-text-window"))
                    .AddItems(
                        CreateAction("SectionEffect", "Index", "Catalog", "bi-collection"),
                        CreateAction("SectionEffect", "Background", "Background", "bi-image"),
                        CreateAction("SectionEffect", "FxAmbiance", "FX & Ambiance", "bi-stars"),
                        CreateAction("SectionEffect", "ShapeEdge", "Shape & Edge", "bi-pentagon")
                    ),
                ActionItemFactory.Group("Inner Section", IconStruct.Bootstrap("bi-layout-three-columns"))
                    .AddItems(
                        CreateAction("InnerSectionEffect", "Index", "Catalog", "bi-collection"),
                        CreateAction("InnerSectionEffect", "TextEffects", "Text Effects", "bi-type"),
                        CreateAction("InnerSectionEffect", "Video", "Video", "bi-camera-video"),
                        CreateAction("InnerSectionEffect", "Navigation", "Navigation", "bi-signpost-split"),
                        CreateAction("InnerSectionEffect", "Scroll", "Scroll", "bi-mouse"),
                        CreateAction("InnerSectionEffect", "Carousels", "Carousels", "bi-collection-play"),
                        CreateAction("InnerSectionEffect", "Cards", "Cards", "bi-credit-card-2-front"),
                        CreateAction("InnerSectionEffect", "LayoutDecoration", "Layout & Decoration", "bi-layout-wtf")
                    )
            );
    }

    /// <summary>
    ///     Creates the DMBEffectBuilder sidebar component.
    /// </summary>
    /// <param name="currentController">The current MVC controller name used to mark the active item.</param>
    /// <param name="currentAction">The current MVC action name used to mark the active item.</param>
    /// <param name="sidebarId">The HTML identifier applied to the sidebar component.</param>
    /// <param name="localStorageKey">The browser local-storage key used for sidebar state.</param>
    /// <returns>The configured <see cref="SideBarComponent" />.</returns>
    public static SideBarComponent CreateSidebar(
        string? currentController,
        string? currentAction,
        string sidebarId = "effect_builder_sidebar",
        string localStorageKey = "dmbeffectbuilder.labs.sidebar"
    )
    {
        SideBarComponent sidebar = new SideBarComponent()
            .WithId(sidebarId)
            .WithLocalStorageKey(localStorageKey)
            .WithAutoExpandActivePath()
            .WithRememberExpandedState();

        sidebar.AddSection(CreateSidebarSection(currentController, currentAction));

        return sidebar;
    }

    /// <summary>
    ///     Creates the DMBEffectBuilder sidebar section.
    /// </summary>
    /// <param name="currentController">The current MVC controller name used to mark the active item.</param>
    /// <param name="currentAction">The current MVC action name used to mark the active item.</param>
    /// <returns>The configured <see cref="SideBarSectionComponent" />.</returns>
    public static SideBarSectionComponent CreateSidebarSection(string? currentController, string? currentAction)
    {
        return new SideBarSectionComponent("DMBEffectBuilder")
            .Add(
                ActionItemFactory.Group("General", IconStruct.Bootstrap("bi-info-circle"))
                    .AddItems(
                        CreateAction("EffectBuilder", "Introduction", "Introduction", "bi-info-circle", currentController, currentAction),
                        CreateAction("EffectBuilder", "GettingStarted", "Getting Started", "bi-play-circle", currentController, currentAction),
                        CreateAction("EffectBuilder", "Architecture", "Architecture", "bi-diagram-3", currentController, currentAction),
                        CreateAction("EffectBuilder", "RenderingPipeline", "Rendering Pipeline", "bi-bezier2", currentController, currentAction)
                    ),
                ActionItemFactory.Group("Image", IconStruct.Bootstrap("bi-image"))
                    .AddItems(
                        CreateAction("ImageEffect", "Index", "Catalog", "bi-collection", currentController, currentAction),
                        CreateAction("ImageEffect", "PhotoHover", "Photo & Hover", "bi-camera", currentController, currentAction),
                        CreateAction("ImageEffect", "Stylise", "Stylise", "bi-magic", currentController, currentAction),
                        CreateAction("ImageEffect", "Concatenation", "Concatenation", "bi-plus-circle", currentController, currentAction)
                    ),
                ActionItemFactory.Group("Section", IconStruct.Bootstrap("bi-layout-text-window"))
                    .AddItems(
                        CreateAction("SectionEffect", "Index", "Catalog", "bi-collection", currentController, currentAction),
                        CreateAction("SectionEffect", "Background", "Background", "bi-image", currentController, currentAction),
                        CreateAction("SectionEffect", "FxAmbiance", "FX & Ambiance", "bi-stars", currentController, currentAction),
                        CreateAction("SectionEffect", "ShapeEdge", "Shape & Edge", "bi-pentagon", currentController, currentAction)
                    ),
                ActionItemFactory.Group("Inner Section", IconStruct.Bootstrap("bi-layout-three-columns"))
                    .AddItems(
                        CreateAction("InnerSectionEffect", "Index", "Catalog", "bi-collection", currentController, currentAction),
                        CreateAction("InnerSectionEffect", "TextEffects", "Text Effects", "bi-type", currentController, currentAction),
                        CreateAction("InnerSectionEffect", "Video", "Video", "bi-camera-video", currentController, currentAction),
                        CreateAction("InnerSectionEffect", "Navigation", "Navigation", "bi-signpost-split", currentController, currentAction),
                        CreateAction("InnerSectionEffect", "Scroll", "Scroll", "bi-mouse", currentController, currentAction),
                        CreateAction("InnerSectionEffect", "Carousels", "Carousels", "bi-collection-play", currentController, currentAction),
                        CreateAction("InnerSectionEffect", "Cards", "Cards", "bi-credit-card-2-front", currentController, currentAction),
                        CreateAction("InnerSectionEffect", "LayoutDecoration", "Layout & Decoration", "bi-layout-wtf", currentController, currentAction)
                    )
            );
    }

    /// <summary>
    ///     Determines whether a controller belongs to the DMBEffectBuilder labs module.
    /// </summary>
    /// <param name="controllerName">The MVC controller name to inspect.</param>
    /// <returns><see langword="true" /> when the controller belongs to DMBEffectBuilder labs.</returns>
    public static bool IsModuleController(string? controllerName)
    {
        return controllerName is not null &&
               (string.Equals(controllerName, "EffectBuilder", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(controllerName, "ImageEffect", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(controllerName, "SectionEffect", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(controllerName, "InnerSectionEffect", StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    ///     Resolves the Bootstrap icon for a DMBEffectBuilder labs action.
    /// </summary>
    /// <param name="controllerName">The MVC controller name to resolve.</param>
    /// <param name="actionName">The MVC action name to resolve.</param>
    /// <returns>The icon value represented as an <see cref="IconStruct" />.</returns>
    public static IconStruct ResolveActionIcon(string? controllerName, string? actionName)
    {
        return actionName switch
        {
            "GettingStarted" => IconStruct.Bootstrap("bi-play-circle"),
            "Architecture" => IconStruct.Bootstrap("bi-diagram-3"),
            "RenderingPipeline" => IconStruct.Bootstrap("bi-bezier2"),
            "PhotoHover" => IconStruct.Bootstrap("bi-camera"),
            "Stylise" => IconStruct.Bootstrap("bi-magic"),
            "Concatenation" => IconStruct.Bootstrap("bi-plus-circle"),
            "Background" => IconStruct.Bootstrap("bi-image"),
            "FxAmbiance" => IconStruct.Bootstrap("bi-stars"),
            "ShapeEdge" => IconStruct.Bootstrap("bi-pentagon"),
            "TextEffects" => IconStruct.Bootstrap("bi-type"),
            "Video" => IconStruct.Bootstrap("bi-camera-video"),
            "Navigation" => IconStruct.Bootstrap("bi-signpost-split"),
            "Scroll" => IconStruct.Bootstrap("bi-mouse"),
            "Carousels" => IconStruct.Bootstrap("bi-collection-play"),
            "Cards" => IconStruct.Bootstrap("bi-credit-card-2-front"),
            "LayoutDecoration" => IconStruct.Bootstrap("bi-layout-wtf"),
            "Index" => ResolveIndexIcon(controllerName),
            _ => IconStruct.Bootstrap("bi-info-circle")
        };
    }

    /// <summary>
    ///     Resolves the display title for a DMBEffectBuilder labs action.
    /// </summary>
    /// <param name="controllerName">The MVC controller name to resolve.</param>
    /// <param name="actionName">The MVC action name to resolve.</param>
    /// <returns>The display title for the action.</returns>
    public static string ResolveActionTitle(string? controllerName, string? actionName)
    {
        return actionName switch
        {
            "GettingStarted" => "Getting Started",
            "Architecture" => "Architecture",
            "RenderingPipeline" => "Rendering Pipeline",
            "PhotoHover" => "Photo & Hover",
            "Stylise" => "Stylise",
            "Concatenation" => "Concatenation",
            "Background" => "Background",
            "FxAmbiance" => "FX & Ambiance",
            "ShapeEdge" => "Shape & Edge",
            "TextEffects" => "Text Effects",
            "Video" => "Video",
            "Navigation" => "Navigation",
            "Scroll" => "Scroll",
            "Carousels" => "Carousels",
            "Cards" => "Cards",
            "LayoutDecoration" => "Layout & Decoration",
            "Index" => ResolveIndexTitle(controllerName),
            _ => "Introduction"
        };
    }

    private static IconStruct ResolveIndexIcon(string? controllerName)
    {
        return controllerName switch
        {
            "ImageEffect" => IconStruct.Bootstrap("bi-image"),
            "SectionEffect" => IconStruct.Bootstrap("bi-layout-text-window"),
            "InnerSectionEffect" => IconStruct.Bootstrap("bi-layout-three-columns"),
            _ => IconStruct.Bootstrap("bi-info-circle")
        };
    }

    private static string ResolveIndexTitle(string? controllerName)
    {
        return controllerName switch
        {
            "ImageEffect" or "SectionEffect" or "InnerSectionEffect" => "Catalog",
            _ => "Introduction"
        };
    }

    /// <summary>
    ///     Resolves the default controller for a DMBEffectBuilder labs module controller.
    /// </summary>
    /// <param name="controllerName">The MVC controller name to resolve.</param>
    /// <returns>The root module controller name.</returns>
    public static string ResolveModuleController(string? controllerName)
    {
        return "EffectBuilder";
    }

    /// <summary>
    ///     Resolves the default action for a DMBEffectBuilder labs module controller.
    /// </summary>
    /// <param name="controllerName">The MVC controller name to resolve.</param>
    /// <returns>The root module action name.</returns>
    public static string ResolveModuleDefaultAction(string? controllerName)
    {
        return "Introduction";
    }

    /// <summary>
    ///     Resolves the Bootstrap icon for a DMBEffectBuilder labs module controller.
    /// </summary>
    /// <param name="controllerName">The MVC controller name to resolve.</param>
    /// <returns>The icon value represented as an <see cref="IconStruct" />.</returns>
    public static IconStruct ResolveModuleIcon(string? controllerName)
    {
        return IconStruct.Bootstrap("bi-stars");
    }

    /// <summary>
    ///     Resolves the display title for a DMBEffectBuilder labs module controller.
    /// </summary>
    /// <param name="controllerName">The MVC controller name to resolve.</param>
    /// <returns>The module display title.</returns>
    public static string ResolveModuleTitle(string? controllerName)
    {
        return "DMBEffectBuilder";
    }

    #endregion
}