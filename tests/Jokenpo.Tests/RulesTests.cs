using Xunit;

namespace Jokenpo.Tests;

public class RulesTests
{
    // Independent oracle: rows/columns are Rock, Paper, Scissor, Lizard, Spock.
    private static readonly int[][] Outcomes =
    [
        [0, -1, 1, 1, -1],
        [1, 0, -1, -1, 1],
        [-1, 1, 0, 1, -1],
        [-1, 1, -1, 0, 1],
        [1, -1, 1, -1, 0]
    ];

    public static IEnumerable<object[]> AllPairs()
    {
        for (var first = 0; first < 5; first++)
            for (var second = 0; second < 5; second++)
                yield return [(JokenpoHandType)first, (JokenpoHandType)second, Outcomes[first][second]];
    }

    [Theory]
    [MemberData(nameof(AllPairs))]
    public void Every_pair_obeys_the_rules_and_reverse_symmetry(
        JokenpoHandType first, JokenpoHandType second, int expected)
    {
        var x = new JokenpoHand(first);
        var y = new JokenpoHand(second);
        Assert.Equal(expected, JokenpoRules.Compare(x, y));
        Assert.Equal(expected, JokenpoRules.Compare(first, second));
        Assert.Equal(-expected, JokenpoRules.Compare(y, x));
    }

    [Fact]
    public void Null_hands_are_rejected_at_the_boundary()
    {
        Assert.Throws<ArgumentNullException>(() => JokenpoRules.Compare(null!, new JokenpoHand()));
        Assert.Throws<ArgumentNullException>(() => JokenpoRules.Compare(new JokenpoHand(), null!));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(5)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void Invalid_hands_cannot_be_constructed_or_assigned(int value)
    {
        var invalid = (JokenpoHandType)value;
        Assert.Throws<ArgumentOutOfRangeException>(() => new JokenpoHand(invalid));
        var hand = new JokenpoHand();
        Assert.Throws<ArgumentOutOfRangeException>(() => hand.Hand = invalid);
        Assert.Equal(JokenpoHandType.Paper, hand.Hand);
    }
}

public class BoundaryTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(5)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void Both_comparison_boundaries_reject_invalid_values_even_for_draws(int value)
    {
        var bad = (JokenpoHandType)value;
        Assert.Throws<ArgumentOutOfRangeException>(() => JokenpoRules.Compare(bad, JokenpoHandType.Rock));
        Assert.Throws<ArgumentOutOfRangeException>(() => JokenpoRules.Compare(JokenpoHandType.Rock, bad));
        Assert.Throws<ArgumentOutOfRangeException>(() => JokenpoRules.Compare(bad, bad));
        var custom = new CustomHand { Hand = bad };
        Assert.Throws<ArgumentOutOfRangeException>(() => JokenpoRules.Compare(custom, new JokenpoHand()));
        Assert.Throws<ArgumentOutOfRangeException>(() => JokenpoRules.Compare(new JokenpoHand(), custom));
        Assert.Throws<ArgumentOutOfRangeException>(() => JokenpoRules.Compare(custom, custom));
    }

    [Fact]
    public void Hand_value_behavior_remains_compatible()
    {
        var hand = new JokenpoHand();
        Assert.Equal(JokenpoHandType.Paper, hand.Hand);
        Assert.Equal("Paper", hand.ToString());
        Assert.Equal(new JokenpoHand(JokenpoHandType.Paper), hand);
        Assert.Equal(new JokenpoHand().GetHashCode(), hand.GetHashCode());
        Assert.False(hand.Equals(null));
        Assert.False(hand.Equals("Paper"));
        Assert.False(hand.Equals(new JokenpoHandAI()));
        hand.Hand = JokenpoHandType.Spock;
        Assert.Equal("Spock", hand.ToString());
        Assert.NotEqual(new JokenpoHand(), hand);
    }

    private sealed class CustomHand : IJokenpoHand
    {
        public JokenpoHandType Hand { get; set; }
    }
}
