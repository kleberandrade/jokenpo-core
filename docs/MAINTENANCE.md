# Maintenance log

## 2026-11-16 — implementation

- Cloned the repository; confirmed a clean `master`, existing Git identity,
  original authorship and MIT license. No project-local AGENTS.md was present.
- Inspected all library classes and the interactive sample. Preserved the five
  enum values, namespace, hand interface, default Paper hand, equality semantics,
  and comparison convention. The original target was .NET Framework 2.0.
- Checked Microsoft's support policy and installed official .NET SDK 10.0.404
  in a temporary directory for validation. Selected .NET 10 LTS and pinned the
  SDK; production code has no external package dependencies.
- Moved SDK-style projects into `src`, `tests`, and `examples`; removed obsolete
  assembly metadata, app configuration, and the Visual Studio class diagram.
- Characterized every original ordered pair using an independent expected-result
  matrix. All 25 passed. Before changing behavior, added null and invalid-hand
  tests: 5 failed for the expected missing validation / wrong exception reasons.
- Replaced mutable singleton initialization with one immutable set of ten winning
  pairs and a pure enum comparison overload. Added boundary validation, retaining
  the existing interface overload. The same 30 tests then passed.
- Added per-instance Random injection with Random.Shared as the default; tested
  repeatability on the same runtime, state updates, all reachable choices through
  a controlled generator, null rejection, and valid default random output.
- Replaced the non-terminating-on-EOF interactive sample with an argument-based,
  single-round CLI. Tested win/loss/draw, help, case-insensitive names, random
  opponent, numeric/unknown input, missing arguments and excess arguments.
- Used xUnit v3 4.0.3 with its native Microsoft.Testing.Platform integration and
  a checked-in NuGet lock file. Removed redundant VSTest packages after the first
  test invocation exposed their incompatibility with the new runner.
- Final implementation suite: 51 passing tests, no skipped tests.
- Inspected crapper, dryer and mutator help output. These installed tools do not
  support C#; they cannot provide useful CRAP, duplication or mutation scores here.
  No score or coverage percentage is claimed.
- Updated GitHub About through existing authenticated credentials and read it back:
  "A lightweight C# library for Rock-Paper-Scissors-Lizard-Spock, with deterministic rules and a CLI example."
  Confirmed the existing default branch is `master`; repository visibility unchanged.

## Validation and publication

See the following entries for final checks. CI is configured for three operating
systems; only macOS ARM64 execution was performed locally. No GitHub Actions run,
physical-device testing, package publication, push, or deployment is claimed.

### Final checks — macOS ARM64, SDK 10.0.404

- `dotnet restore Jokenpo.slnx --locked-mode`: passed.
- `dotnet build Jokenpo.slnx --configuration Release --no-restore`: passed,
  zero warnings and zero errors.
- `dotnet test --solution Jokenpo.slnx --configuration Release --no-build`:
  51 passed, zero failed, zero skipped.
- `dotnet format Jokenpo.slnx --verify-no-changes --no-restore`: passed.
- `dotnet pack src/Jokenpo --configuration Release --no-build --output artifacts`:
  created the local `Jokenpo.2.0.0.nupkg`; verified the library DLL, exact README
  and original LICENSE bytes inside the archive. No upload was performed.
- Executed the compiled CLI as a separate process for win, loss, draw, help,
  missing arguments and invalid opponent; checked messages and exit codes.
- Four targeted temporary source mutations were detected by test assertions:
  deleting Rock's win over Scissor, returning a win for draws, accepting invalid
  hand assignments, and failing to update the AI hand. Each original file was
  restored in a finally block; the complete final build and test suite passed
  afterward. These are specific sensitivity checks, not a full mutation score.
- `dotnet list Jokenpo.slnx package --vulnerable --include-transitive`: no known
  vulnerable packages reported by NuGet at validation time. This is a dependency
  advisory check, not a comprehensive security audit.
- Added CI for Linux/macOS/Windows with read-only repository permissions, pinned
  action revisions, locked restore, build, tests, format, CLI smoke and local pack.
  Workflow execution awaits a push authorized by the owner.
- Added English README, rules/API details, migration guidance, accurate language,
  runtime and license badges, plus a CI badge targeting `master` and the workflow
  included here. Original MIT license text is unchanged.
- `git diff --check`: passed. Changes are recorded as local commits; no push,
  deployment, package publication, or unrelated-project edits were performed.

### Authorized publication and branch rename

- At the owner's request, pushed the two modernization commits, then renamed the
  default GitHub branch from `master` to `main` using the branch-rename API.
- Renamed the local branch and updated the workflow push filter and README badge.
  Earlier entries above describe the historical state before publication.

### CI correction — checkout line endings

- The first remote CI runs passed build and tests on all three systems, but the
  Windows formatting step reported ENDOFLINE errors: Git had checked out CRLF
  while .editorconfig requires LF.
- Added .gitattributes to enforce LF at checkout on every platform, fixing the
  source of the mismatch without weakening the formatting check.
