using System.Collections.Frozen;

namespace Jokenpo;

public class JokenpoRules
{
    // Each winning relationship appears once; reversing it is a loss.
    private static readonly FrozenSet<(JokenpoHandType Winner, JokenpoHandType Loser)> Wins =
        new (JokenpoHandType, JokenpoHandType)[]
        {
            (JokenpoHandType.Rock, JokenpoHandType.Scissor),
            (JokenpoHandType.Rock, JokenpoHandType.Lizard),
            (JokenpoHandType.Paper, JokenpoHandType.Rock),
            (JokenpoHandType.Paper, JokenpoHandType.Spock),
            (JokenpoHandType.Scissor, JokenpoHandType.Paper),
            (JokenpoHandType.Scissor, JokenpoHandType.Lizard),
            (JokenpoHandType.Lizard, JokenpoHandType.Paper),
            (JokenpoHandType.Lizard, JokenpoHandType.Spock),
            (JokenpoHandType.Spock, JokenpoHandType.Rock),
            (JokenpoHandType.Spock, JokenpoHandType.Scissor)
        }.ToFrozenSet();

    /// <summary>Returns 1 when x wins, 0 for a draw, and -1 when y wins.</summary>
    public static int Compare(JokenpoHandType x, JokenpoHandType y)
    {
        HandTypes.Validate(x, nameof(x));
        HandTypes.Validate(y, nameof(y));
        return x == y ? 0 : Wins.Contains((x, y)) ? 1 : -1;
    }

    /// <summary>Compares two hands using the same rules as the enum overload.</summary>
    public static int Compare(IJokenpoHand x, IJokenpoHand y)
    {
        ArgumentNullException.ThrowIfNull(x);
        ArgumentNullException.ThrowIfNull(y);
        return Compare(x.Hand, y.Hand);
    }
}
