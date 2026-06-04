#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using DMBComponentBuilder;
using DMBEffectBuilder;
using DMBEffectBuilderLabs.Controllers;
using DMBEffectBuilderWebsite;
using DMBPageBuilder;
using DMBServerHelper;
using DMBServerWebHelper;

#endregion

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ServerHelperConfiguration.LoadCommonConfig(builder);
ServerHelperConfiguration.Config.CookiePrefix = "DEB";
ServerWebHelperConfiguration.LoadCommonConfig(builder);
PageBuilderConfiguration.LoadCommonConfig(builder);
BootstrapBuilderConfiguration.LoadCommonConfig(builder);
ComponentBuilderConfiguration.LoadCommonConfig(builder);
EffectBuilderConfiguration.LoadCommonConfig(builder);

var mvcBuilder = builder.Services.AddControllersWithViews();
mvcBuilder.AddApplicationPart(typeof(EffectBuilderController).Assembly);
mvcBuilder.AddMvcOptions(options => options.Filters.Add(new DMBEffectBuilderWebsiteSidebarActionFilter()));

builder.Services.AddTransient<IMenuBarSectionProvider, DMBEffectBuilderWebsiteMenuBarSectionProvider>();
builder.Services.AddTransient<IProfileBarSectionProvider, ThemeBarSectionProvider>();
builder.Services.AddTransient<IProfileBarSectionProvider, DebugBarSectionProvider>();

WebApplication app = builder.Build();

app.UseHttpsRedirection();

ServerWebHelperConfiguration.UseApp(app);

app.MapGet("/", context =>
{
    context.Response.Redirect("/EffectBuilder/Introduction");
    return Task.CompletedTask;
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=EffectBuilder}/{action=Introduction}/{id?}");

app.Run();
