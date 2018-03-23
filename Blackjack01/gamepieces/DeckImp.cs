using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack01.gamepieces
{
    class DeckImp : Deck
    {
        // assume an infinite deck for now and generate random card
        // maybe later, build a multi-shoe deck for realism

        private static Random rnd = new Random(); // keep same instance going

        public Card dealCard()
        {
            cardElements.Suits suit = (cardElements.Suits)rnd.Next(3, Enum.GetNames(typeof(cardElements.Suits)).Length + 3);
            cardElements.Ranks rank = (cardElements.Ranks)rnd.Next(0, Enum.GetNames(typeof(cardElements.Ranks)).Length + 0);

            Card returnCard = new CardImp(suit, rank);
            return returnCard;
        }

        public void shuffle()
        {
            // do nothing for now
        }
        
    }
}
