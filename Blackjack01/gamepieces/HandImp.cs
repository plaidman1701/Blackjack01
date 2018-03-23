using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

namespace Blackjack01.gamepieces
{
    class HandImp : Hand
    {
        List<Card> cards;

        public HandImp(uint bet)
        {
            cards = new List<Card>();
            this.bet = bet;
            this.isSplit = false;
        }

        public uint bet
        {
            get;
            private set;
        }

        public bool isSplit
        {
            get;
            set;
        }

        public HandImp(Card firstCard, uint bet) : this(bet)
        {
            this.addCard(firstCard);
        }

        public HandImp(Card firstCard, Card secondCard, uint bet) : this(firstCard, bet)
        {
            this.addCard(secondCard);
        }

        /*
        public void doubleDown()
        {
            bet += bet;
        }

    */
        public bool isBlackjack
        {
            get
            {
                if ((cards.Count == 2) && (handValue == 21) && (!isSplit))
                {
                    return true;
                }
                return false;
            }
        }

        public int handValue
        {
            get
            {
                int returnInt = 0;
                bool acePresent = false;
                foreach (Card c in cards)
                {
                    returnInt += c.value;
                    if (c.rank == cardElements.Ranks.ACE)
                    {
                        acePresent = true;
                    }
                }

                if ((returnInt <= 11) && (acePresent))
                {
                    returnInt += 10;
                }

                return returnInt;
            }
        }

        public void addCard(Card cardToAdd)
        {
            cards.Add(cardToAdd);
        }

        public override string ToString()
        {
            StringBuilder returnString = new StringBuilder();

            if (isSplit)
            {
                returnString.Append("from split - ");
            }

            foreach (Card c in cards)
            {
                returnString.Append(c).Append(" ");
            }
            returnString.Append(string.Format("({0})", handValue));

            return returnString.ToString();
        }

        public Card getLastCard()
        {
            // assume only 2 cards
            if (cards.Count == 2)
            {
                Card tempCard = cards[1];
                cards.RemoveAt(1);
                return tempCard;
            }

            return null;
        }

        public Dictionary<char, string> getLegitPlays(uint bankroll)
        {
            Dictionary<char, string> returnDic = new Dictionary<char, string>
            {
                // every hand can hit and stand
                { 'h', "Hit" },
                { 's', "Stand" }
            };

            if ((cards.Count == 2) && (bet <= bankroll))
            {
                returnDic.Add('d', "DoubleDown");

                if (cards[0].rank == cards[1].rank)
                {
                    returnDic.Add('p', "Split");
                }
            }

            return returnDic;
        }

    }
}
