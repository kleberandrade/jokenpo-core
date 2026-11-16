# Migrating from the 2015 project

## Build and layout

- Install .NET SDK 10.0.404 (or a later patch in the same feature band).
- Open `Jokenpo.slnx` instead of the old `Jokenpo.sln`, or use `dotnet build`.
- Reference `src/Jokenpo/Jokenpo.csproj` instead of `Jokenpo/Jokenpo.csproj`.
- Rebuild your application for .NET 10. This is a breaking framework migration;
  old .NET Framework binaries are not supported.
- The CLI lives in `examples/Jokenpo.Cli`; tests live in `tests/Jokenpo.Tests`.
- SDK-generated assembly metadata replaces the hand-maintained Properties files.

## Existing library usage

The `Jokenpo` namespace and original public types remain. This still works:

```csharp
using Jokenpo;

IJokenpoHand player = new JokenpoHand(); // Paper
player.Hand = JokenpoHandType.Paper;
var computer = new JokenpoHandAI();
computer.Next();
int result = JokenpoRules.Compare(player, computer);
```

`Compare` still returns `1`, `0`, or `-1` from the first hand's perspective.
`Scissor` keeps its original singular spelling and numeric value `2`.
Equality still requires matching concrete types and current hand values.

## New options and deliberate changes

- Compare enums directly when mutable objects are unnecessary:
  `JokenpoRules.Compare(JokenpoHandType.Rock, JokenpoHandType.Spock)` returns `-1`.
- Pass `new Random(seed)` to `JokenpoHandAI` for repeatable simulations on the
  same runtime. The parameterless constructor uses `Random.Shared`; no historic
  random sequence is promised.
- Null arguments throw `ArgumentNullException`. Undefined enum values now throw
  `ArgumentOutOfRangeException`, even when both invalid values are identical.
  Custom `IJokenpoHand` implementations are validated during comparison too.
- The private singleton has been removed. Rules are immutable and comparison has
  no mutable initialization state.
- Run the example with `dotnet run --project examples/Jokenpo.Cli -- rock spock`.
  There is no interactive menu, input loop, screen clearing, or final key press.
  Bad arguments return exit code `2`; a game loss returns `0`.
- Tests run with `dotnet test --solution Jokenpo.slnx`. Microsoft.Testing.Platform
  is configured explicitly; VSTest-specific options are not used.

No package publication or remote branch update is part of this migration.
