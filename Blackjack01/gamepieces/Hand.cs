using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack01.gamepieces
{
    interface Hand
    {
        int handValue
        {
            get;
        }

        uint bet
        {
            get;
        }

        //void doubleDown();

        bool isBlackjack
        {
            get;
        }

        Dictionary<char, string> getLegitPlays(uint bankroll);

        string ToString();

        bool isSplit
        {
            get;
            set;
        }

        void addCard(Card cardToAdd);
        Card getLastCard();


    }
}
