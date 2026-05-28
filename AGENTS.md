# AI Rules - DMBEffectBuilder

## Scope

- Applies to the `DMBEffectBuilder` folder and descendants.
- This project is autonomous: required rules are defined in local documentation files.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBEffectBuilder`
- Project folder: `DMBEffectBuilder`
- Project role: reusable visual effect package for PageBuilder and BootstrapBuilder applications.
- Primary consumers: Razor views and PageBuilder ecosystem packages that need image, hover, reveal, motion, and section effects.
- Main dependency: `DMBBootstrapBuilder`.
- Publication host: `labs_idemobi_com`
- Documentation generation strategy: DocumentationBuilder-first; AI prepares content, the developer executes generation.

## Module intent

- Provide fluent extension methods that apply visual effects to existing BootstrapBuilder and PageBuilder elements.
- Keep generated classes, CSS expectations, accessibility behavior, and effect names deterministic for consuming pages.
- Avoid moving base component rendering, form behavior, low-level HTML primitives, or ASP.NET middleware responsibilities into this package.

## Key constraints

- Keep public APIs backward compatible unless a change request explicitly allows breakage.
- Prefer additive effect options over structural rewrites.
- Every new visual effect must include a demo or preview page in the publication host when the task includes effect delivery.
- Cover normal state, empty state, error/fallback state, and one realistic data example for new effect demos.
- Treat generated CSS classes, animation timing, reduced-motion behavior, image URLs, and user-provided attributes as behavior-sensitive areas.
- Do not run `dotnet build`, `dotnet test`, `dotnet restore`, or `dotnet format` unless explicitly requested.

## Documentation objective

- Documentation must be authored so it can be extracted and rendered by DocumentationBuilder.
- Publication target is `labs_idemobi_com`.
- Documentation output must serve both developers and AI assistants.
- AI prepares documentation content and structure; the developer runs DocumentationBuilder.
- XML documentation comments must be written in English.
- Public classes, public methods, public constructors, public properties, public fields, public constants, public enums, public enum values, public records, and extension methods must have useful XML documentation.

## Local rule sources

- Use [AI_CONTEXT.md](AI_CONTEXT.md) for the project summary and safe-change strategy.
- Use [DOCUMENTATION_RULES.md](DOCUMENTATION_RULES.md) for XML HeaderDoc, README/reference documentation, and DocumentationBuilder-ready documentation.
- Use [EXAMPLES_AND_TUTORIALS_RULES.md](EXAMPLES_AND_TUTORIALS_RULES.md) only when creating or updating example, demo, information, instruction, concept, or tutorial pages.
- Use [DRAWIO_DIAGRAM_RULES.md](DRAWIO_DIAGRAM_RULES.md) when adding editable Draw.io diagrams to information, instruction, concept, architecture, effect lifecycle, or tutorial pages.
- Use `CodeBlockBuilder` or the local `Html.CodeBlock(...)` helper for code examples when available.
- Use `ActionItem` with `ButtonRender` for page action links when the target publication project exposes those helpers.
- Store editable Draw.io diagrams as enriched `.drawio.svg` files under `labs_idemobi_com/wwwroot/drawio/{Area}/`.

## Localization

- Follow local [LOCALIZATION_NOMENCLATURE.md](LOCALIZATION_NOMENCLATURE.md).
- Do not assume external localization rules unless duplicated here.

## Before delivery

- Update local docs when behavior changes.
- State untested areas explicitly.
- Do not claim build/test or DocumentationBuilder execution when they were not run.
