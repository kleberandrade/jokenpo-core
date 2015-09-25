using System;

namespace Jokenpo
{
    public class JokenpoHandAI : JokenpoHand
    {
        private static Random m_Random = null;

        public JokenpoHandAI()
        {
            if (m_Random == null)
                m_Random = new Random((int)DateTime.Now.Ticks);
        }

        public JokenpoHandType Next()
        {
            Hand = (JokenpoHandType)AverageOfNumberRandom();
            return Hand;
        }

        private int AverageOfNumberRandom()
        {
            return m_Random.Next(Enum.GetNames(typeof(JokenpoHandType)).Length);
        }
    }
}
