# DMBEffectBuilder Project Map

## Purpose

Map the structure of `DMBEffectBuilder` so AI assistants can find the right files quickly.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBEffectBuilder`
- Project folder: `DMBEffectBuilder`
- Project role: reusable visual effect package for PageBuilder and BootstrapBuilder applications.
- Publication host: `labs_idemobi_com`

## Root files

- `AGENTS.md`: local AI instructions.
- `AI_CONTEXT.md`: project context for AI assistants.
- `DOCUMENTATION_RULES.md`: XML and reference documentation rules.
- `EXAMPLES_AND_TUTORIALS_RULES.md`: website page, example, and tutorial rules.
- `DRAWIO_DIAGRAM_RULES.md`: editable Draw.io diagram rules.
- `DELIVERY_CHECKLIST.md`: pre-delivery checklist.
- `ARCHITECTURE_DECISIONS.md`: durable architecture decisions.
- `LOCALIZATION_NOMENCLATURE.md`: localization key rules.
- `LOCAL_DEVELOPMENT_RUNBOOK.md`: local workflow guide.
- `TROUBLESHOOTING.md`: common issue guide.
- `GLOSSARY.md`: common term definitions.

## Source

- `Source/DMBEffectBuilder.csproj`: project file and package metadata.
- `Source/README.md`: package overview and documentation entry point.
- `Source/LICENSE.md`: package license file.
- `Source/DMBEffectBuilder.png`: package icon.
- `Source/DMBEffectBuilder.snk`: package signing key.

## Configuration

- `Source/Configuration/EffectBuilderConfiguration.cs`: default effect builder configuration.
- `Source/Configuration/EffectBuilderConfigureOptions.cs`: static file options configuration for embedded effect assets.

## Source/Effects/Images

- `Source/Effects/Images/BlurToSharpEffect_ImageExtension.cs`: blur-to-sharp image effect.
- `Source/Effects/Images/ColorFlickerEffect_ImageExtension.cs`: color flicker image effect.
- `Source/Effects/Images/ColorInvertEffect_ImageExtension.cs`: color inversion image effect.
- `Source/Effects/Images/DarkenToNormalEffect_ImageExtension.cs`: darken-to-normal image effect.
- `Source/Effects/Images/DistortEffect_ImageExtension.cs`: distortion image effect.
- `Source/Effects/Images/FadeHoverEffect_ImageExtension.cs`: fade hover image effect.
- `Source/Effects/Images/FlipEffect_ImageExtension.cs`: flip image effect.
- `Source/Effects/Images/FlipAxis.cs`: supported flip axis enum.
- `Source/Effects/Images/FocusSpotEffect_ImageExtension.cs`: focus spot image effect.
- `Source/Effects/Images/GhostEffect_ImageExtension.cs`: ghost image effect.
- `Source/Effects/Images/GlassRevealEffect_ImageExtension.cs`: glass reveal image effect.
- `Source/Effects/Images/GlitchEffect_ImageExtension.cs`: glitch image effect.
- `Source/Effects/Images/GlowHoverEffect_ImageExtension.cs`: glow hover image effect.
- `Source/Effects/Images/GrayscaleHoverEffect_ImageExtension.cs`: grayscale hover image effect.
- `Source/Effects/Images/GrayscaleToColorEffect_ImageExtension.cs`: grayscale-to-color image effect.
- `Source/Effects/Images/KenBurnsSlowEffect_ImageExtension.cs`: slow Ken Burns image effect.
- `Source/Effects/Images/LiftHoverEffect_ImageExtension.cs`: lift hover image effect.
- `Source/Effects/Images/LockedRevealEffect_ImageExtension.cs`: locked reveal image effect.
- `Source/Effects/Images/NeonFrameEffect_ImageExtension.cs`: neon frame image effect.
- `Source/Effects/Images/PolaroidEffect_ImageExtension.cs`: polaroid frame image effect.
- `Source/Effects/Images/PulseEffect_ImageExtension.cs`: pulse image effect.
- `Source/Effects/Images/RainbowEffect_ImageExtension.cs`: rainbow image effect.
- `Source/Effects/Images/RetroScanlinesEffect_ImageExtension.cs`: retro scanlines image effect.
- `Source/Effects/Images/RotateEffect_ImageExtension.cs`: rotation image effect.
- `Source/Effects/Images/RotateRandomEffect_ImageExtension.cs`: random rotation image effect.
- `Source/Effects/Images/RotateSlightlyEffect_ImageExtension.cs`: slight rotation image effect.
- `Source/Effects/Images/SepiaEffect_ImageExtension.cs`: sepia image effect.
- `Source/Effects/Images/SepiaToColorEffect_ImageExtension.cs`: sepia-to-color image effect.
- `Source/Effects/Images/ShakeEffect_ImageExtension.cs`: shake image effect.
- `Source/Effects/Images/ShineSweepEffect_ImageExtension.cs`: shine sweep image effect.
- `Source/Effects/Images/SoftShadowEffect_ImageExtension.cs`: soft shadow image effect.
- `Source/Effects/Images/TiltParallaxEffect_ImageExtension.cs`: tilt parallax image effect.
- `Source/Effects/Images/VignetteEffect_ImageExtension.cs`: vignette image effect.
- `Source/Effects/Images/ZoomHoverEffect_ImageExtension.cs`: zoom hover image effect.

## Source/Effects/Sections

- `Source/Effects/Sections/SectionEffectsDebugHelper.cs`: helper for inspecting or demonstrating section-level effect behavior.

## Managers

- `Source/Managers/Managers.cs`: shared manager or registration surface for effect-related behavior.

## Labs

- `Labs/DMBEffectBuilderLabs.csproj`: non-packable Razor project that provides DMBEffectBuilder presentation pages and examples to `labs_idemobi_com`.
- `Labs/Controllers`: MVC controllers for EffectBuilder, image effects, section effects, and inner section effects.
- `Labs/Views`: Razor pages and partials rendered by the host application through an MVC application part.
- `Labs/Models`: view models used by effect preview cards.
- `Labs/Helpers`: Razor helper extensions used by the Labs views without depending on `labs_idemobi_com`.
- `Labs/Navigation/DMBEffectBuilderLabsNavigationAgent.cs`: reusable navbar, sidebar, title, and icon fragments for local and final labs hosts.

## Website

- `Website/DMBEffectBuilderWebsite.csproj`: local ASP.NET Core host for testing DMBEffectBuilder labs pages independently.
- `Website/Program.cs`: local host startup, configuration, MVC application part, services, and default route.
- `Website/Filters`: local MVC filters that attach the DMBEffectBuilder labs sidebar and breadcrumb.
- `Website/Providers`: local navbar providers that assemble DMBEffectBuilder labs menu fragments.
- `Website/Views/Shared/_Layout.cshtml`: local PageBuilder-compatible layout.
- `Website/wwwroot/favicons`: local favicon assets copied from the labs host.
- `Website/wwwroot/logo`: local logo assets copied from the labs host.

## Related projects

- `DMBBootstrapBuilder`: Bootstrap-oriented visual builder package.
- `DMBPageBuilder`: low-level page and HTML builder package.
- `DMBComponentBuilder`: reusable component package used in examples and preview pages.
- `labs_idemobi_com`: publication host for examples, tutorials, information pages, and diagrams.
