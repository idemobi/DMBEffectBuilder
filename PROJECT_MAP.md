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

- `DMBEffectBuilder.csproj`: project file and package metadata.
- `README.md`: package overview and documentation entry point.
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

## Configuration

- `EffectBuilderConfiguration.cs`: default effect builder configuration.
- `EffectBuilderConfigureOptions.cs`: static file options configuration for embedded effect assets.

## Effects/Images

- `BlurToSharpEffect_ImageExtension.cs`: blur-to-sharp image effect.
- `ColorFlickerEffect_ImageExtension.cs`: color flicker image effect.
- `ColorInvertEffect_ImageExtension.cs`: color inversion image effect.
- `DarkenToNormalEffect_ImageExtension.cs`: darken-to-normal image effect.
- `DistortEffect_ImageExtension.cs`: distortion image effect.
- `FadeHoverEffect_ImageExtension.cs`: fade hover image effect.
- `FlipEffect_ImageExtension.cs`: flip image effect.
- `FlipAxis.cs`: supported flip axis enum.
- `FocusSpotEffect_ImageExtension.cs`: focus spot image effect.
- `GhostEffect_ImageExtension.cs`: ghost image effect.
- `GlassRevealEffect_ImageExtension.cs`: glass reveal image effect.
- `GlitchEffect_ImageExtension.cs`: glitch image effect.
- `GlowHoverEffect_ImageExtension.cs`: glow hover image effect.
- `GrayscaleHoverEffect_ImageExtension.cs`: grayscale hover image effect.
- `GrayscaleToColorEffect_ImageExtension.cs`: grayscale-to-color image effect.
- `KenBurnsSlowEffect_ImageExtension.cs`: slow Ken Burns image effect.
- `LiftHoverEffect_ImageExtension.cs`: lift hover image effect.
- `LockedRevealEffect_ImageExtension.cs`: locked reveal image effect.
- `NeonFrameEffect_ImageExtension.cs`: neon frame image effect.
- `PolaroidEffect_ImageExtension.cs`: polaroid frame image effect.
- `PulseEffect_ImageExtension.cs`: pulse image effect.
- `RainbowEffect_ImageExtension.cs`: rainbow image effect.
- `RetroScanlinesEffect_ImageExtension.cs`: retro scanlines image effect.
- `RotateEffect_ImageExtension.cs`: rotation image effect.
- `RotateRandomEffect_ImageExtension.cs`: random rotation image effect.
- `RotateSlightlyEffect_ImageExtension.cs`: slight rotation image effect.
- `SepiaEffect_ImageExtension.cs`: sepia image effect.
- `SepiaToColorEffect_ImageExtension.cs`: sepia-to-color image effect.
- `ShakeEffect_ImageExtension.cs`: shake image effect.
- `ShineSweepEffect_ImageExtension.cs`: shine sweep image effect.
- `SoftShadowEffect_ImageExtension.cs`: soft shadow image effect.
- `TiltParallaxEffect_ImageExtension.cs`: tilt parallax image effect.
- `VignetteEffect_ImageExtension.cs`: vignette image effect.
- `ZoomHoverEffect_ImageExtension.cs`: zoom hover image effect.

## Effects/Sections

- `SectionEffectsDebugHelper.cs`: helper for inspecting or demonstrating section-level effect behavior.

## Managers

- `Managers.cs`: shared manager or registration surface for effect-related behavior.

## Related projects

- `DMBBootstrapBuilder`: Bootstrap-oriented visual builder package.
- `DMBPageBuilder`: low-level page and HTML builder package.
- `DMBComponentBuilder`: reusable component package used in examples and preview pages.
- `labs_idemobi_com`: publication host for examples, tutorials, information pages, and diagrams.
