# AGENTS.md

Guidance for AI coding agents (and humans) working in the Appium .NET client.
See also [CONTRIBUTING.md](CONTRIBUTING.md).

## Repository layout

- `src/Appium.Net/` – the `Appium.WebDriver` library (targets `netstandard2.0`, strong-name signed).
- `test/integration/` – NUnit test project (targets `net48` and `net8.0`). Holds both server-free tests and
  functional tests that need an Appium server plus a device, simulator or emulator.
- `Appium.Net.sln` – solution used by CI.

## Build and test

```bash
dotnet restore Appium.Net.sln
dotnet build Appium.Net.sln --configuration Release
```

Tests that run without a device (the set CI runs on every PR):

```bash
dotnet test ./test/integration/Appium.Net.Integration.Tests.csproj \
  --configuration Release --framework net8.0 \
  --filter "AppiumLocalServerLaunchingTest|DirectConnectTest|AppiumClientConfigTest"
```

Functional tests are selected by namespace, e.g. `--filter "FullyQualifiedName~Android"` or
`"FullyQualifiedName~IOS"`. They need a running Appium server and a configured `test/integration/env.json`
(copy from `env.json.sample`; never commit `env.json`).

`net48` tests only run on Windows. If you cannot run a target framework or a functional suite locally,
say so in the PR rather than claiming it passed.

## Coding guidelines

- Follow the style of the surrounding code and `.editorconfig`.
- Keep the public API backwards compatible unless the change is intentionally breaking; breaking changes
  must be called out in the PR title (see below).
- Build with no new warnings across all target frameworks.
- Bugfixes and new features ship with tests. If behaviour cannot be covered automatically, explain why in the PR.

## Git workflow

- Check the current branch before committing. Start unrelated work on a new branch from `main`; do not
  pile it onto an existing feature branch.
- Stage files explicitly. Avoid `git add .` / `git add -A`, which can pick up local artifacts
  (`env.json`, test results, tool folders).
- Do not commit secrets, credentials, device IDs or local environment files.

## Pull requests

- Open PRs against `appium/dotnet-client`, branch `main`.
- The title drives labelling and release notes (`.github/labeler.yml` + Release Drafter). Prefix it with a
  type: `feat:`, `fix:`, `refactor:`, `test:`, `docs:`, `build:`, `ci:`, `chore:` or `github:`.
  Optional scope, e.g. `feat(ios):`. Breaking changes use `type!:` or include `BREAKING CHANGE:`.
- Fill in [`.github/PULL_REQUEST_TEMPLATE.md`](.github/PULL_REQUEST_TEMPLATE.md) completely, including
  the tests section.
- Keep PRs focused: one logical change per PR.

## Reviewing pull requests

- Do not approve a PR while any CI check is failing, pending or in an unknown state, even if the failure
  looks pre-existing on `main`.
- For a red check: reproduce the build/tests locally, comment with your findings, and request changes if
  the PR caused the failure.

## Addressing review comments

1. Read every unresolved review thread before changing code.
2. Make the requested change, add or update tests for it, and build all target frameworks.
3. Run the relevant tests, e.g. `dotnet test --filter "FullyQualifiedName~<TestClassName>"`.
4. Stage only the files you changed, commit, and push to the PR branch.
5. Reply to each thread (what changed, or why not) and resolve the ones you addressed.
