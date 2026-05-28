# DMBEffectBuilder Troubleshooting

## Purpose

Collect common issues and investigation paths for `DMBEffectBuilder`.

## Project-specific section

When copying this file to another PageBuilder ecosystem project, update this section first.

- Project name: `DMBEffectBuilder`
- Project folder: `DMBEffectBuilder`
- Project role: reusable visual effect package for PageBuilder and BootstrapBuilder applications.
- Publication host: `labs_idemobi_com`

## Effect class is missing from output

Check:

- the extension method is called on the expected receiver type,
- the effect method returns and chains the same builder instance,
- repeated fluent calls do not remove the effect class,
- generated class names match the embedded CSS selectors.

## Effect styling does not appear

Check:

- consuming applications register `EffectBuilderConfigureOptions`,
- embedded assets are exposed through the host application,
- generated class names match the CSS selectors,
- BootstrapBuilder and PageBuilder dependencies are configured.

## Hover effect does not work

Check:

- the target device supports hover,
- the effect has a non-hover fallback for touch or keyboard users when required,
- the generated wrapper and class structure matches the CSS selector,
- another effect or class is not overriding the transform or opacity.

## Motion or flashing effect is uncomfortable

Check:

- animation duration and intensity,
- reduced-motion support in consuming CSS,
- whether the effect should be replaced by a static variant,
- contrast and readability after the effect is applied.

## Transform effect changes layout unexpectedly

Check:

- the effect uses transform instead of layout-changing properties where possible,
- wrapper overflow and transform origin are appropriate,
- image dimensions are stable,
- rotate, flip, zoom, or tilt parameters are not conflicting.

## Documentation page issues

When pages in `labs_idemobi_com` are wrong or inconsistent:

- read `EXAMPLES_AND_TUTORIALS_RULES.md`,
- use `CodeBlockBuilder` or `Html.CodeBlock(...)` for code examples,
- use `ActionItem` with `ButtonRender` for action links,
- use `DRAWIO_DIAGRAM_RULES.md` for editable diagrams,
- keep DocumentationViewer links targeting `DMBEffectBuilder`.
