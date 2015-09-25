using System;
using System.Collections.Generic;
using System.Text;

namespace Jokenpo
{
    public class JokenpoRules
    {
        private static JokenpoRulesSingleton m_Rules = null;

        public static int Compare(IJokenpoHand x, IJokenpoHand y)
        {
            if (m_Rules == null)
                m_Rules = JokenpoRulesSingleton.Instance;

            return m_Rules.Compare(x, y);
        }
    }
}
