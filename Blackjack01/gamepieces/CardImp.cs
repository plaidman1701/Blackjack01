using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack01.gamepieces
{
    class CardImp : Card
    {
        public CardImp(cardElements.Suits suit, cardElements.Ranks rank)
        {
            this.suit = suit;
            this.rank = rank;
        }

        public cardElements.Suits suit
        {
            get;
            private set;
        }

        public cardElements.Ranks rank
        {
            get;
            private set;
        }

        public int value
        {
            get
            {
                return cardElements.CardValues.rankValues[rank];
            }
        }

        public override String ToString()
        {
            string printRank = cardElements.CardValues.rankStrings[rank];
            char printSuit = (char)suit;
            return String.Format("{0}{1}", printSuit, printRank);
        }
    }
}
