using System;
using System.Collections.Generic;
using System.Text;

namespace Jokenpo
{
    public interface IJokenpoHand
    {
        JokenpoHandType Hand
        {
            get;
            set;
        }
    }
}
