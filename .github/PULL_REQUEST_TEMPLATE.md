## PR title

Labels and the draft release notes are generated from the PR title, so please prefix it with the matching type:
`feat:` (new feature) · `fix:` (bugfix) · `test:` · `docs:` · `build:` · `ci:` · `chore:`
Breaking changes must additionally carry `!` or `BREAKING CHANGE:` in the title.

## Related issue

Closes # <!-- issue number, or "n/a" -->

## List of changes

Please provide a briefly described change list that you are going to propose. 
 
## Types of changes

What types of changes are you proposing/introducing to the .NET client?
_Put an `x` in the boxes that apply_

- [ ] Bugfix (non-breaking change which fixes an issue)
- [ ] **New feature** (non-breaking change that adds functionality or value)
- [ ] Breaking change (fix or feature that would cause existing functionality not to work as expected)
- [ ] **New test coverage** (non-breaking change that adds tests for existing, previously untested functionality)
- [ ] Test fix (non-breaking change that improves test stability or correctness)
- [ ] Chore/Maintenance (updates to build scripts, dependencies, or GitHub Actions)

## New feature details

_Fill this in only if you checked **New feature** or **Breaking change** above; otherwise delete the section._

- [ ] Every new public type and member has XML documentation comments (`///`)
- [ ] The new API follows the naming, nullability and sync/async conventions of the surrounding driver code
- [ ] Existing public API is unchanged, or the breaking change is described below together with a migration note

**Appium endpoint / extension command backing this feature:** 

**Server and driver requirements:** _e.g. Appium 2.x, UiAutomator2 x.y.z, XCUITest x.y.z_

**New public API:**

```csharp
// signatures of the classes / methods / properties this PR adds
```

**Usage sample:**

```csharp
// short snippet showing how a user calls it
```

## Tests

_Put an `x` in the boxes that apply_

- [ ] Unit tests
- [ ] Integration tests
- [ ] No automated tests (explain why below)

**How they run:** _e.g. picked up by the existing `FullyQualifiedName~Android` filter, no CI changes needed_

Bugfixes, new features and new test coverage are expected to ship with tests. If the behaviour cannot be covered automatically (real device or cloud only, hardware dependent, inherently flaky), please say so here instead.

## Documentation
- [ ] Have you proposed a file change/ PR with Appium to update documentation? 
- [ ] Not applicable (no user facing behaviour change, e.g. tests, CI or maintenance only)
#### This can be done by navigating to the documentation section on http://appium.io selecting the appropriate command/endpoint and clicking the 'Edit this doc' link to update the C# example

## Details

Please provide more details about changes if necessary. You can provide code samples showing how they work and possible use cases if there are new features. Also, you can create [gists](https://gist.github.com) with pasted C# code samples or put them here using markdown. 
About markdown please read [Mastering markdown](https://guides.github.com/features/mastering-markdown/) and [Writing on GitHub](https://docs.github.com/en/get-started/writing-on-github)
