using System;
using System.Collections.Generic;
using System.Text;

using Blackjack01.gamepieces;

namespace Blackjack01.players
{
    class PlayerImp : Player
    {
        List<Hand> hands;

        public List<uint> bets
        {
            get;
            set;
        }

        public PlayerImp()
        {
            this.hands = new List<Hand>();
            this.money = 1000;
            this.bets = new List<uint>();
        }

        public void deal(Card card, uint bet)
        {
            hands.Add(new HandImp(card, bet));
        }

        public void deal(Card firstCard, Card secondCard, uint bet)
        {
            hands.Add(new HandImp(firstCard, secondCard, bet));
        }

        public void deal(Card firstCard, Card secondCard, uint bet, int position)
        {
            hands.Insert(position, new HandImp(firstCard, secondCard, bet));
        }

        public List<Hand> getHands()
        {
            return hands;
        }

        public uint money
        {
            get;
            private set;
        }

        public void chargePlayer(uint value)
        {
            money -= value;
        }

        public void awardPlayer(uint winnings)
        {
            money += winnings;
        }
    }
}
