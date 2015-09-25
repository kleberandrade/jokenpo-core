using System;
using System.Collections.Generic;

namespace Jokenpo
{
    internal class JokenpoRulesSingleton : IComparer<IJokenpoHand>
    {
        private static Dictionary<JokenpoHandType, List<JokenpoHandType>> m_Rules = null;

        private static volatile JokenpoRulesSingleton m_Instance;

        private static object m_SyncRoot = new Object();

        public static JokenpoRulesSingleton Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    lock (m_SyncRoot)
                    {
                        if (m_Instance == null)
                            m_Instance = new JokenpoRulesSingleton();
                    }
                }

                return m_Instance;
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