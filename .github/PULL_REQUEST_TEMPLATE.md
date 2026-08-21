## PR title

The title is what gets labelled: `.github/labeler.yml` matches the title and applies a label, and Release Drafter turns that label into a release-notes category and the version bump. Please prefix the title with the matching type:
`feat:` (new feature) · `fix:` (bugfix) · `test:` · `docs:` · `build:` · `ci:` · `chore:` · `github:`
A scope is optional, e.g. `feat(ios):`. Breaking changes use `type!:` (`feat!:`, `feat(ios)!:`) or carry `BREAKING CHANGE:` in the title.

## Related issue

Closes # <!-- issue number, or "n/a" -->

## List of changes

Please provide a briefly described change list that you are going to propose. 
 
## Types of changes

What types of changes are you proposing/introducing to the .NET client?
_Put an `x` in the boxes that apply_

- [ ] Bugfix (non-breaking change which fixes an issue)
- [ ] New feature (non-breaking change that adds functionality or value)
- [ ] Breaking change (fix or feature that would cause existing functionality not to work as expected)
- [ ] **New test coverage** (non-breaking change that adds tests for existing, previously untested functionality)
- [ ] Test fix (non-breaking change that improves test stability or correctness)
- [ ] Chore/Maintenance (updates to build scripts, dependencies, or GitHub Actions)

## Tests

_Put an `x` in the boxes that apply_

- [ ] Unit tests
- [ ] Integration tests
- [ ] No automated tests (explain why below)

**How they run:** _e.g. picked up by the existing `FullyQualifiedName~Android` filter, no CI changes needed_

Bugfixes, new features and new test coverage are expected to ship with tests. If the behaviour cannot be covered automatically (real device or cloud only, hardware dependent, inherently flaky), please say so here instead.

## Documentation
- [ ] Have you proposed a file change/PR with Appium to update documentation?
- [ ] Not applicable (no user-facing behaviour change, e.g. tests, CI or maintenance only)
#### This can be done by navigating to the documentation section on http://appium.io, selecting the appropriate command/endpoint and clicking the 'Edit this doc' link to update the C# example

## Details

Please provide more details about changes if necessary. You can provide code samples showing how they work and possible use cases if there are new features. Also, you can create [gists](https://gist.github.com) with pasted C# code samples or put them here using markdown. 
About markdown please read [Mastering markdown](https://guides.github.com/features/mastering-markdown/) and [Writing on GitHub](https://docs.github.com/en/get-started/writing-on-github)
