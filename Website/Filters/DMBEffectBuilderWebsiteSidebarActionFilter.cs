#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using DMBEffectBuilderLabs.Navigation;
using DMBPageBuilder;
using Microsoft.AspNetCore.Mvc.Filters;

#endregion

namespace DMBEffectBuilderWebsite;

internal sealed class DMBEffectBuilderWebsiteSidebarActionFilter : IActionFilter
{
    #region Instance methods

    #region From interface IActionFilter

    /// <summary>
    ///     Completes the action filter lifecycle after the action has executed.
    /// </summary>
    /// <param name="context">The current action executed context.</param>
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    /// <summary>
    ///     Injects the DMBEffectBuilder labs sidebar and breadcrumb into module pages.
    /// </summary>
    /// <param name="context">The current action execution context.</param>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.Controller is not RawBootstrapController controller)
        {
            return;
        }

        string? currentController = context.RouteData.Values["controller"]?.ToString();
        string? currentAction = context.RouteData.Values["action"]?.ToString();

        if (!DMBEffectBuilderLabsNavigationAgent.IsModuleController(currentController))
        {
            return;
        }

        string actionName = string.IsNullOrWhiteSpace(currentAction) ? "Introduction" : currentAction;

        controller.SetSidebar(DMBEffectBuilderLabsNavigationAgent.CreateSidebar(currentController, currentAction));
        controller.AddBreadcrumb(
            ActionItemFactory.Url("Home", "/", IconStruct.Bootstrap("bi-house")),
            ActionItemFactory.AspRoute("EffectBuilder", "Introduction")
                .SetTitle("DMBEffectBuilder")
                .SetIcon(IconStruct.Bootstrap("bi-stars")),
            ActionItemFactory.AspRoute(currentController ?? "EffectBuilder", actionName)
                .SetTitle(DMBEffectBuilderLabsNavigationAgent.ResolveActionTitle(currentController, actionName))
                .SetIcon(DMBEffectBuilderLabsNavigationAgent.ResolveActionIcon(currentController, actionName))
        );
    }

    #endregion

    #endregion
}
