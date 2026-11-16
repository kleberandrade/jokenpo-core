namespace Jokenpo.Cli;

internal static class GameCli
{
    private const string Usage = "Usage: jokenpo <rock|paper|scissor|lizard|spock> [opponent]";

    internal static int Run(string[] args, TextWriter output, TextWriter error)
    {
        if (args is ["--help"] or ["-h"])
        {
            output.WriteLine(Usage);
            return 0;
        }

        if (args.Length is < 1 or > 2 || !TryParse(args[0], out var player))
            return InvalidInput(error);

        JokenpoHandType opponent;
        if (args.Length == 2)
        {
            if (!TryParse(args[1], out opponent))
                return InvalidInput(error);
        }
        else
        {
            opponent = new JokenpoHandAI().Next();
        }

        var result = JokenpoRules.Compare(player, opponent);
        output.WriteLine($"Player: {player}; Opponent: {opponent}");
        output.WriteLine(result switch { 1 => "You win!", -1 => "You lose!", _ => "Draw." });
        return 0;
    }

    private static bool TryParse(string text, out JokenpoHandType hand)
    {
        // Parsing by name deliberately rejects numeric enum strings.
        foreach (var choice in Enum.GetValues<JokenpoHandType>())
        {
            if (string.Equals(text, choice.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                hand = choice;
                return true;
            }
        }

        hand = default;
        return false;
    }

    private static int InvalidInput(TextWriter error)
    {
        error.WriteLine(Usage);
        return 2;
    }
}
