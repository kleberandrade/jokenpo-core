namespace Jokenpo;

public class JokenpoHandAI : JokenpoHand
{
    private static readonly JokenpoHandType[] Choices = Enum.GetValues<JokenpoHandType>();
    private readonly Random random;

    public JokenpoHandAI() : this(Random.Shared) { }

    /// <summary>Inject a seeded Random for repeatable games on the same runtime.</summary>
    public JokenpoHandAI(Random random)
    {
        ArgumentNullException.ThrowIfNull(random);
        this.random = random;
    }

    public JokenpoHandType Next()
    {
        Hand = Choices[random.Next(Choices.Length)];
        return Hand;
    }
}
