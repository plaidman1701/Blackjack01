using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack01.gamepieces
{
    interface Deck
    {
        Card dealCard();
        void shuffle();
    }
}
