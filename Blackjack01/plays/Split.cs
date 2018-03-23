using System;
using System.Collections.Generic;
using System.Text;
using Blackjack01.gamepieces;

namespace Blackjack01.plays
{
    class Split : AbstractPlay
    {
        public override Card execute(Hand hand, Card card, ref bool playOver)
        {
            playOver = false;
            Card tempCard = hand.getLastCard();
            hand.addCard(card);
            return tempCard;
        }
    }
}
