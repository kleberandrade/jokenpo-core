using Xunit;

namespace Jokenpo.Tests;

public class RandomHandTests
{
    [Fact]
    public void Equal_seeds_produce_equal_sequences_and_update_the_hand()
    {
        var first = new JokenpoHandAI(new Random(42));
        var second = new JokenpoHandAI(new Random(42));
        for (var i = 0; i < 100; i++)
        {
            var next = first.Next();
            Assert.Equal(next, second.Next());
            Assert.Equal(next, first.Hand);
            Assert.True(Enum.IsDefined(next));
        }
    }

    [Fact]
    public void Every_choice_is_reachable_without_probability_based_assertions()
    {
        var random = new CyclingRandom();
        var ai = new JokenpoHandAI(random);
        foreach (var hand in Enum.GetValues<JokenpoHandType>())
            Assert.Equal(hand, ai.Next());
    }

    [Fact]
    public void Default_generator_only_returns_defined_hands()
    {
        var ai = new JokenpoHandAI();
        for (var i = 0; i < 100; i++)
            Assert.True(Enum.IsDefined(ai.Next()));
    }

    [Fact]
    public void Null_random_is_rejected() =>
        Assert.Throws<ArgumentNullException>(() => new JokenpoHandAI(null!));

    private sealed class CyclingRandom : Random
    {
        private int next;
        public override int Next(int maxValue) => next++ % maxValue;
    }
}
