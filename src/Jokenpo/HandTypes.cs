namespace Jokenpo;

internal static class HandTypes
{
    internal static void Validate(JokenpoHandType hand, string parameterName)
    {
        if (!Enum.IsDefined(hand))
            throw new ArgumentOutOfRangeException(parameterName, hand, "Choose a defined hand.");
    }
}
