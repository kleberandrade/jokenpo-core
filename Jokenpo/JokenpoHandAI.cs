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
            Hand = (JokenpoHandType)AverageOfNumberRandom(5);
            return Hand;
        }

        private int AverageOfNumberRandom(int maxNumber)
        {
            int average = 0;

            for (int i = 0; i < maxNumber; i++)
                average += m_Random.Next(5);
            
            return average / maxNumber;
        }
    }
}
