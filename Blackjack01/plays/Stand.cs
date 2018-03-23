using System;
using System.Collections.Generic;
using System.Text;

using Blackjack01.gamepieces;

namespace Blackjack01.plays
{
    class Stand : AbstractPlay
    {
        public override Card execute(Hand hand, Card card, ref bool playOver)
        {
            playOver = true;
            return null;
        }
    }
}
