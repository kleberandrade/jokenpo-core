using System;
using System.Collections.Generic;

namespace Jokenpo
{
    internal class JokenpoRulesSingleton : IComparer<IJokenpoHand>
    {
        private static Dictionary<JokenpoHandType, List<JokenpoHandType>> m_Rules = null;

        private static volatile JokenpoRulesSingleton instance;

        private static object syncRoot = new Object();

        public static JokenpoRulesSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new JokenpoRulesSingleton();
                    }
                }

                return instance;
            }
        }

        private JokenpoRulesSingleton()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (m_Rules != null)
                return;

            m_Rules = new Dictionary<JokenpoHandType, List<JokenpoHandType>>();
            m_Rules.Add(JokenpoHandType.Scissor, new List<JokenpoHandType>() { JokenpoHandType.Paper, JokenpoHandType.Lizard });
            m_Rules.Add(JokenpoHandType.Paper, new List<JokenpoHandType>() { JokenpoHandType.Rock, JokenpoHandType.Spock });
            m_Rules.Add(JokenpoHandType.Rock, new List<JokenpoHandType>() { JokenpoHandType.Scissor, JokenpoHandType.Lizard });
            m_Rules.Add(JokenpoHandType.Spock, new List<JokenpoHandType>() { JokenpoHandType.Scissor, JokenpoHandType.Rock });
            m_Rules.Add(JokenpoHandType.Lizard, new List<JokenpoHandType>() { JokenpoHandType.Paper, JokenpoHandType.Spock });
        }

        public int Compare(IJokenpoHand x, IJokenpoHand y)
        {
            if (x.Hand == y.Hand)
                return 0;

            return m_Rules[x.Hand].Contains(y.Hand) ? 1 : -1;
        }        
    }
}