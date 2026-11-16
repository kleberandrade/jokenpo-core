# jokenpo-core

[![CI](https://github.com/kleberandrade/jokenpo-core/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/kleberandrade/jokenpo-core/actions/workflows/ci.yml)
[![C#](https://img.shields.io/badge/language-C%23-512BD4)](https://learn.microsoft.com/dotnet/csharp/)
[![.NET 10](https://img.shields.io/badge/runtime-.NET%2010%20LTS-512BD4)](https://dotnet.microsoft.com/download/dotnet/10.0)
[![MIT license](https://img.shields.io/badge/license-MIT-green)](LICENSE)

A small C# library for **Rock–Paper–Scissors–Lizard–Spock**. Compare two hands,
create mutable player hands, or generate computer choices. The library has no
external package dependencies. A single-round CLI demonstrates its use.

Originally created by Kléber de Oliveira Andrade on September 24, 2015, during
Advanced Studies in Digital Games at FATEC-AM.

## Requirements and installation

Install the stable **.NET 10 SDK 10.0.404** specified in [global.json](global.json).
Later servicing patches in that SDK feature band are accepted. A .NET runtime
alone cannot build the project. .NET 10 is an [LTS release supported through
November 2028](https://dotnet.microsoft.com/platform/support/policy).

```sh
git clone https://github.com/kleberandrade/jokenpo-core.git
cd jokenpo-core
dotnet restore Jokenpo.slnx --locked-mode
dotnet build Jokenpo.slnx --configuration Release --no-restore
```

The source supports .NET 10 on Windows, Linux, and macOS. CI is configured for all
three; see the workflow status for remote execution results.

To use the library from another project, add a project reference (adjust the path
for your checkout):

```sh
dotnet add YourApp.csproj reference path/to/jokenpo-core/src/Jokenpo/Jokenpo.csproj
```

No published NuGet version is required or implied by these instructions.

## Quick start

```csharp
using Jokenpo;

int result = JokenpoRules.Compare(JokenpoHandType.Rock, JokenpoHandType.Lizard);
Console.WriteLine(result); // 1: the first hand wins

IJokenpoHand player = new JokenpoHand(JokenpoHandType.Paper);
var computer = new JokenpoHandAI(new Random(42));
computer.Next(); // Updates computer.Hand and returns the selected hand.
Console.WriteLine(JokenpoRules.Compare(player, computer));
```

## CLI example

```sh
# An explicit opponent gives a reproducible round.
dotnet run --project examples/Jokenpo.Cli -- rock scissor
# Player: Rock; Opponent: Scissor
# You win!

# Omit the opponent to generate a random choice.
dotnet run --project examples/Jokenpo.Cli -- spock

dotnet run --project examples/Jokenpo.Cli -- --help
```

Names are case-insensitive: `rock`, `paper`, `scissor`, `lizard`, `spock`.
The singular spelling `Scissor` is retained from the original public API.
Numeric values and unknown names are rejected. The CLI plays one round and exits:
`0` for a completed game (including a loss or draw) or help; `2` for usage errors,
written to standard error. It does not read standard input.

## Rules

Each hand beats two others, loses to two, and draws against itself:

- Rock crushes Scissor and Lizard.
- Paper covers Rock and disproves Spock.
- Scissor cuts Paper and decapitates Lizard.
- Lizard eats Paper and poisons Spock.
- Spock vaporizes Rock and smashes Scissor.

These ten winning relationships live in one immutable rule set. Comparison is
from the **first hand's perspective**: `1` is a win, `0` is a draw, `-1` is a loss.
This is a cyclic game relation, so `Compare` must not be used to sort collections.

## API

- `JokenpoHandType`: `Rock = 0`, `Paper = 1`, `Scissor = 2`, `Lizard = 3`, `Spock = 4`.
- `JokenpoRules.Compare(JokenpoHandType x, JokenpoHandType y)`: pure comparison.
- `JokenpoRules.Compare(IJokenpoHand x, IJokenpoHand y)`: compatible object-based API.
- `IJokenpoHand.Hand`: a mutable hand property.
- `new JokenpoHand()`: starts with Paper; the enum constructor selects another hand.
- `new JokenpoHandAI()`: starts with Paper and uses `Random.Shared` for future choices.
- `new JokenpoHandAI(Random random)`: inject a generator, such as `new Random(42)`.
- `JokenpoHandAI.Next()`: selects a hand, updates `Hand`, and returns it.

Invalid enum values throw `ArgumentOutOfRangeException` at construction,
assignment, or comparison, including values from custom `IJokenpoHand`
implementations. Null hands and a null generator throw `ArgumentNullException`.
A failed assignment leaves the previous hand intact.

Equality retains the original behavior: two hands are equal when their concrete
types and current values match. Hands are mutable: do not change a hand while it
is used as a dictionary key or set member.

A seeded generator is repeatable on the same runtime with the same call order;
sequences are not a cross-runtime compatibility contract. Use a separate seeded
`Random` per independent simulation. Mutable hands and injected generators are
not synchronized; avoid sharing those instances across concurrent writers.
The generator is for games, not cryptographic use.

## Tests and development

```sh
dotnet restore Jokenpo.slnx --locked-mode
dotnet build Jokenpo.slnx --configuration Release --no-restore
dotnet test --solution Jokenpo.slnx --configuration Release --no-build
dotnet format Jokenpo.slnx --verify-no-changes --no-restore
# Create a local package; this does not upload it.
dotnet pack src/Jokenpo --configuration Release --no-build --output artifacts
```

Tests use xUnit v3 and Microsoft.Testing.Platform, selected in `global.json`.
The .NET 10 syntax uses `--solution` or `--project` for test selection. The lock
file fixes transitive test dependencies; update it intentionally when changing
package versions. Warnings fail the build.

The suite checks all 25 ordered pairs against an independent matrix, reverse
symmetry, draws, boundary errors, equality behavior, repeatable random choices,
reachable hands, and CLI outcomes/errors. It does not rely on statistical random
thresholds. No coverage percentage or mutation score is claimed.

```text
src/Jokenpo/              Library and centralized domain rules
examples/Jokenpo.Cli/     Single-round command-line example
tests/Jokenpo.Tests/      Library and CLI behavior tests
docs/                    Migration guide and maintenance log
.github/workflows/       Portable CI checks
```

## Migration and limitations

Version 2 targets .NET 10 and replaces the Visual Studio 2015/.NET Framework 2.0
project format. Rebuild consumers for .NET 10; the old framework is not targeted.
The original namespace, public hand types, numeric enum values, mutable hand API,
and comparison signs remain available. Invalid inputs now fail consistently.
The sample has moved and accepts arguments instead of an interactive menu.
See the [migration guide](docs/MIGRATION.md) and [maintenance log](docs/MAINTENANCE.md).

This library only models one round and random choices. It does not provide a
scoreboard, adaptive AI, networking, or a graphical interface.

## License

[MIT](LICENSE), copyright © 2015 Kléber de Oliveira Andrade. The original license
and authorship are preserved.
