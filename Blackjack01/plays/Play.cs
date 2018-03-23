using System;
using System.Collections.Generic;
using System.Text;

using Blackjack01.gamepieces;

namespace Blackjack01.plays
{
    interface Play
    {
        Card execute(Hand hand, Card card, ref bool playOver);
    }
}
