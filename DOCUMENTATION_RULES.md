# DMBEffectBuilder Documentation Rules

## Language

- Documentation must be written in English.
- XML documentation comments must be written in English.

## Target audience

- Primary: developers maintaining or integrating `DMBEffectBuilder`.
- Secondary: developers applying visual effects with the PageBuilder ecosystem.
- Tertiary: AI assistants consuming structured project rules and technical context.

Documentation must be useful without private chat context. A reader should understand what each effect applies, which classes or wrappers it generates, how it composes with BootstrapBuilder output, and what visual or accessibility constraints apply before reading the implementation.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBEffectBuilder`
- Primary API families: image effect extension methods, section effect helpers, effect enums, effect configuration, and embedded static assets.
- Important types to reference when relevant: `FlipAxis`, `EffectBuilderConfiguration`, `EffectBuilderConfigureOptions`, `SectionEffectsDebugHelper`, and the image extension classes under `Effects/Images`.
- Publication host: `labs_idemobi_com`
- Documentation generation strategy: DocumentationBuilder-first; AI prepares content, the developer executes generation.

## Strict C# XML documentation policy

- Always write XML HeaderDoc for public classes, public interfaces, public structs, public records, public methods, public constructors, public properties, public fields, public constants, public events, public delegates, public enums, public enum values, and public extension methods.
- Also write XML HeaderDoc for protected members when they are part of an effect composition contract or are expected to be overridden by derived builders.
- Internal and private members do not require XML HeaderDoc unless they explain complex rendering, CSS class mapping, animation behavior, accessibility, localization, or security behavior that would otherwise be difficult to maintain.
- XML documentation must use valid C# XML syntax.
- Prefer `<summary>`, `<param>`, `<typeparam>`, `<returns>`, `<value>`, `<remarks>`, `<exception>`, `<see cref="..."/>`, and `<seealso cref="..."/>`.
- Use `<inheritdoc/>` only when the inherited documentation is accurate for the current member.

## XML documentation quality standard

XML documentation must explain the public contract, not repeat the member name.

For effect extension classes, document:

- the kind of visual effect applied,
- the expected receiver type and returned builder type,
- the relationship with generated CSS classes and embedded static assets,
- accessibility, reduced-motion, contrast, and hover/focus expectations when relevant.

For methods and constructors, document:

- what the member changes in generated output, classes, wrappers, attributes, animation timing, or visual state,
- every parameter and expected format when relevant,
- the returned fluent builder instance when the method supports chaining,
- side effects such as adding classes, replacing effect state, changing hover behavior, changing animation settings, or adding wrappers,
- validation rules and exceptions,
- whether `null`, empty strings, repeated calls, invalid numeric values, or conflicting effects have special behavior.

For properties, fields, and constants, document:

- the meaning of the value,
- the default value when meaningful,
- whether consumers may set it directly,
- how it affects rendering, styling, animation, accessibility, or static assets.

For enums and enum values, document:

- where the enum is used,
- how each value maps to generated classes, transform direction, rendered states, or fallback behavior.

For extension methods, document:

- the receiver type,
- the builder returned,
- the intended Razor usage pattern,
- how the extension composes with existing BootstrapBuilder or PageBuilder output.

## Project API documentation requirements

- Effect APIs must identify the generated effect class or effect family.
- Image effects must document visual impact and expected image content constraints.
- Motion or flashing effects must mention reduced-motion and readability considerations.
- Transform effects must document direction, axis, intensity, and layout impact when relevant.
- Hover effects must document non-hover fallback behavior for touch and keyboard users when relevant.
- Static asset configuration must document embedded asset exposure and consuming application requirements.

## Examples in XML documentation

Use `<example>` when it materially improves understanding of Razor helper entry points, non-obvious fluent chains, effect composition, or realistic image usage.

Examples must be short, realistic, and compile-oriented. Prefer Razor examples for Razor helpers and C# examples for lower-level builder APIs.

## Markdown documentation policy

- Follow PageBuilder markdown conventions in `../MARKDOWN_GUIDELINES.md`.
- Keep this structure where applicable:
  1. Context
  2. Explanation
  3. Example
  4. Notes / constraints

## Draw.io diagrams for conceptual documentation

Information pages, instruction pages, concept pages, architecture pages, effect lifecycle pages, and tutorial pages may use Draw.io diagrams when they clarify a real model or flow.

Draw.io diagrams must follow `DRAWIO_DIAGRAM_RULES.md`.

Do not use Draw.io diagrams in XML documentation comments. XML documentation may reference concepts that are diagrammed on pages, but the diagram artifact belongs to the website documentation layer.

## DocumentationBuilder-first rule

Documentation in this module must be authored with a DocumentationBuilder-first objective.

- Write docs so they can be extracted and rendered without manual rewrite.
- Keep headings deterministic and stable.
- Keep examples self-contained and realistically useful.
- Avoid implicit references to chat history or hidden context.
- Prefer stable type and member names that DocumentationBuilder can cross-reference.
- Use `<see cref="..."/>` and `<seealso cref="..."/>` for related PageBuilder types whenever it improves navigation.

## Separation from examples and tutorials

`EXAMPLES_AND_TUTORIALS_RULES.md` is not a general documentation rule source.

- Use this file for API documentation, XML HeaderDoc, README updates, reference pages, and DocumentationBuilder-ready documentation.
- Use `../MARKDOWN_GUIDELINES.md` for general Markdown formatting rules.
- Use `EXAMPLES_AND_TUTORIALS_RULES.md` only when the task explicitly creates or updates example pages, demo pages, information pages, instruction pages, concept pages, tutorials, or tutorial-like walkthroughs.

## Minimum update policy

If public effect behavior, generated class behavior, extension behavior, static asset behavior, or accessibility behavior changes, update in the same change set:

- local `README.md`,
- relevant XML docs,
- impacted guidance/examples when the task includes pages.

## Review checklist for documentation changes

- The documentation names real `DMBEffectBuilder` concepts, not copied source-project concepts.
- All public and protected-contract API members touched by the change have valid XML documentation.
- Summaries are specific enough to help IntelliSense users choose the right API.
- Parameters, return values, generic parameters, exceptions, and side effects are documented where applicable.
- Examples reflect current code behavior and realistic Razor effect usage.
- Draw.io diagrams, when added, follow `DRAWIO_DIAGRAM_RULES.md`.
- DocumentationBuilder can extract the content without needing hidden context or manual rewrite.
