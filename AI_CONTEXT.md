# DMBEffectBuilder AI Context

## Purpose

This file gives AI assistants the minimum project context required to work safely in `DMBEffectBuilder`.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBEffectBuilder`
- Project folder: `DMBEffectBuilder`
- Project role: reusable visual effect package for PageBuilder and BootstrapBuilder applications.
- Main dependency: `DMBBootstrapBuilder`.
- Publication host: `labs_idemobi_com`
- Primary documentation audience: developers applying visual effects to Razor page content.

## What this project is

`DMBEffectBuilder` is a visual effect extension package.

It provides:

- image effect extension methods for blur, color, glow, reveal, flip, rotate, zoom, shake, scanline, vignette, and related visual treatments,
- section effect helpers for debugging and demonstrating section-level effects,
- configuration classes for exposing embedded assets to consuming MVC/Razor applications,
- effect names and generated classes that integrate with the PageBuilder ecosystem.

## What this project is not

This project is not:

- a component gallery package,
- a low-level HTML builder package,
- a general Bootstrap layout package,
- a form builder package,
- an image processing package,
- an ASP.NET middleware package,
- a documentation website.

## Main concepts

- Image extension methods apply visual classes, attributes, or wrappers to BootstrapBuilder image output.
- Effect methods should remain composable with existing BootstrapBuilder and PageBuilder render flows.
- Effect options must preserve predictable CSS class output and avoid surprising layout shifts.
- `FlipAxis` defines the supported flip direction model.
- Configuration classes register embedded effect assets for consuming MVC/Razor applications.

## Change strategy

- Keep changes localized to the relevant effect family.
- Preserve public API names and behavior unless the request explicitly asks for a breaking change.
- Keep generated class names, animation names, timing defaults, and embedded asset paths deterministic.
- Document public API behavior in XML comments when the code is touched.
- Update README and local rule files when project behavior or documentation strategy changes.

## Documentation strategy

- Use `DOCUMENTATION_RULES.md` for XML docs, README/reference docs, and DocumentationBuilder-ready documentation.
- Use `EXAMPLES_AND_TUTORIALS_RULES.md` only for pages, examples, tutorials, and walkthroughs.
- Use `DRAWIO_DIAGRAM_RULES.md` when diagrams clarify effect application, CSS asset flow, reduced-motion behavior, or image rendering flow.
- Keep all generated documentation in English unless the user explicitly requests another language for user-facing website content.
