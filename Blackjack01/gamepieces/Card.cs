using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack01.gamepieces
{
    interface Card
    {
        int value
        {
            get;
        }

        cardElements.Ranks rank
        {
            get;
        }

        cardElements.Suits suit
        {
            get;
        }

        string ToString();

    }
}
