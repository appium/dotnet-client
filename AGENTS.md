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

### Tests without a device (`unit-test.yml`, every PR)

Prerequisite: Node.js and Appium installed globally (`npm install -g appium`);
`AppiumLocalServerLaunchingTest` starts a local Appium server.

CI runs the same filter for `net8.0` on Windows, Linux and macOS, and additionally for `net48` on Windows:

```bash
dotnet test ./test/integration/Appium.Net.Integration.Tests.csproj \
  --configuration Release --framework net8.0 \
  --filter "AppiumLocalServerLaunchingTest|DirectConnectTest|AppiumClientConfigTest"

# Windows only
dotnet test ./test/integration/Appium.Net.Integration.Tests.csproj \
  --configuration Release --framework net48 \
  --filter "AppiumLocalServerLaunchingTest|DirectConnectTest|AppiumClientConfigTest"
```

### Functional tests (`functional-test.yml`)

These need an Appium server and an Android emulator or iOS simulator. Prerequisites, matching CI:

```bash
npm install -g appium
appium driver install uiautomator2   # Android (CI also installs espresso)
appium driver install xcuitest       # iOS (macOS only)
```

With the local settings below the tests start Appium themselves via `AppiumLocalService`, so the `appium`
CLI and the platform driver must be installed. Against a remote server, that host provides them instead.

CI uses these filters:

```bash
# Android (CustomCommandTests lives in the root namespace, so it is matched separately)
dotnet test ./test/integration/Appium.Net.Integration.Tests.csproj --configuration Release --framework net8.0 \
  --filter "FullyQualifiedName~Android|FullyQualifiedName~CustomCommand"

# iOS
dotnet test ./test/integration/Appium.Net.Integration.Tests.csproj --configuration Release --framework net8.0 \
  --filter "FullyQualifiedName~IOS"
```

Configure `env.json` first (never commit it):

```bash
cp test/integration/env.json.sample test/integration/env.json
```

The sample's defaults do not work as-is:

- Local Appium server: set `"DEV": true` (or the `DEV` environment variable).
- Remote Appium server: set `"isRemoteAppiumServer": true` and replace the `remoteAppiumServerUri`
  placeholder with the real URL.

See [`test/integration/README.md`](test/integration/README.md) for details.

If you cannot run a target framework or a functional suite locally, say so in the PR rather than claiming
it passed.

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
3. Run the relevant tests, e.g.
   `dotnet test ./test/integration/Appium.Net.Integration.Tests.csproj --filter "FullyQualifiedName~<TestClassName>"`.
4. Stage only the files you changed, commit, and push to the PR branch.
5. Reply to each thread (what changed, or why not) and resolve the ones you addressed.
