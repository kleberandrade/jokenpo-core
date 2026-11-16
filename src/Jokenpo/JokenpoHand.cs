namespace Jokenpo;

public class JokenpoHand : IJokenpoHand
{
    private JokenpoHandType hand;

    public JokenpoHandType Hand
    {
        get => hand;
        set
        {
            HandTypes.Validate(value, nameof(value));
            hand = value;
        }
    }

    public JokenpoHand() : this(JokenpoHandType.Paper) { }

    public JokenpoHand(JokenpoHandType handType)
    {
        Hand = handType;
    }

    public override string ToString() => Hand.ToString();

    public override bool Equals(object? obj) =>
        obj is JokenpoHand other && GetType() == other.GetType() && Hand == other.Hand;

    public override int GetHashCode() => (int)Hand * 7 + 13;
}
