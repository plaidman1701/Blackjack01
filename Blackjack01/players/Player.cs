using System;
using System.Collections.Generic;
using System.Text;

using Blackjack01.gamepieces;

namespace Blackjack01.players
{
    interface Player
    {
        void deal(Card card, uint bet);
        void deal(Card firstCard, Card secondCard, uint bet);
        void deal(Card firstCard, Card secondCard, uint bet, int position);


        List<Hand> getHands();

        List<uint> bets
        {
            get;
            set;
        }

        uint money
        {
            get;
        }

        void chargePlayer(uint value);
        void awardPlayer(uint winnings);
    }
}
