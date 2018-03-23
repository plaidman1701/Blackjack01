using System;
using System.Collections.Generic;
using System.Text;
using Blackjack01.gamepieces;

namespace Blackjack01.plays
{
    abstract class AbstractPlay : Play
    {
        public abstract Card execute(Hand hand, Card card, ref bool playOver);
    }
}
