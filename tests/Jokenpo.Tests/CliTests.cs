using Jokenpo.Cli;
using Xunit;

namespace Jokenpo.Tests;

public class CliTests
{
    [Theory]
    [InlineData("rock", "scissor", "You win!")]
    [InlineData("Rock", "SPOCK", "You lose!")]
    [InlineData("paper", "paper", "Draw.")]
    public void Explicit_hands_produce_a_repeatable_result(string player, string opponent, string message)
    {
        using var output = new StringWriter();
        using var error = new StringWriter();
        Assert.Equal(0, GameCli.Run([player, opponent], output, error));
        Assert.Contains(message, output.ToString());
        Assert.Empty(error.ToString());
    }

    [Theory]
    [InlineData()]
    [InlineData("0")]
    [InlineData("-1")]
    [InlineData("unknown")]
    [InlineData("rock", "5")]
    [InlineData("rock", "paper", "spock")]
    public void Invalid_arguments_print_usage_and_return_two(params string[] args)
    {
        using var output = new StringWriter();
        using var error = new StringWriter();
        Assert.Equal(2, GameCli.Run(args, output, error));
        Assert.Contains("Usage:", error.ToString());
        Assert.Empty(output.ToString());
    }

    [Theory]
    [InlineData("--help")]
    [InlineData("-h")]
    public void Help_succeeds(string option)
    {
        using var output = new StringWriter();
        using var error = new StringWriter();
        Assert.Equal(0, GameCli.Run([option], output, error));
        Assert.Contains("Usage:", output.ToString());
        Assert.Empty(error.ToString());
    }

    [Fact]
    public void Omitted_opponent_plays_one_random_round()
    {
        using var output = new StringWriter();
        using var error = new StringWriter();
        Assert.Equal(0, GameCli.Run(["rock"], output, error));
        Assert.Contains("Player: Rock; Opponent:", output.ToString());
        Assert.Empty(error.ToString());
    }
}
