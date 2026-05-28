# DMBEffectBuilder Architecture Decisions

## Purpose

Record durable architecture decisions that AI assistants and maintainers must preserve unless a change request explicitly supersedes them.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBEffectBuilder`
- Project folder: `DMBEffectBuilder`
- Project role: reusable visual effect package for PageBuilder and BootstrapBuilder applications.
- Main dependency: `DMBBootstrapBuilder`.
- Publication host: `labs_idemobi_com`

## Decisions

### Keep effects as additive extensions

Effect APIs should add classes, attributes, or lightweight wrappers to existing builders instead of replacing base image or section rendering.

### Keep effect output deterministic

Generated CSS classes, animation names, and wrapper structures must remain predictable for consumers and documentation examples.

### Keep effect families isolated

Image effects belong under `Effects/Images`. Section-level helpers belong under `Effects/Sections`.

### Respect accessibility and motion preferences

Effects that imply animation, motion, flashing, distortion, focus masking, or reduced readability must document their visual impact and should remain compatible with reduced-motion strategies in consuming CSS.

### Keep styling integrated with BootstrapBuilder

Effects should compose with existing BootstrapBuilder and PageBuilder output instead of introducing an independent UI framework.

### Keep examples outside the package

Example pages, tutorials, diagrams, and explanatory pages are published through `labs_idemobi_com` when requested.

The package should not embed documentation website pages directly.
