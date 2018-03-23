using System;
using System.Collections.Generic;
using System.Text;

using Blackjack01.gamepieces;

namespace Blackjack01.plays
{
    class DoubleDown : AbstractPlay
    {
        public override Card execute(Hand hand, Card card, ref bool playOver)
        {
            hand.addCard(card);
            playOver = true;
            return null;
        }
    }
}
