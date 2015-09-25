using System;

namespace Jokenpo
{
    public class JokenpoHand : IJokenpoHand
    {
        public JokenpoHandType Hand { get; set; }

        public JokenpoHand()
        {
            Hand = JokenpoHandType.Paper;
        }

        public JokenpoHand(JokenpoHandType handType)
        {
            Hand = handType;
        }

        public override string ToString()
        {
            return Hand.ToString();
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            JokenpoHand otherHand = (JokenpoHand)obj;
            return Hand == otherHand.Hand;
        }

        public override int GetHashCode()
        {
            return (int)Hand * 7 + 13;
        }
    }
}
