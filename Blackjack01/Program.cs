using Blackjack01.gamepieces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


using Blackjack01.plays;
using Blackjack01.players;

namespace Blackjack01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Set your font to Consolas or Lucida Console");

            List<Player> playerList = new List<Player>();
            // just one player for now
            playerList.Add(new PlayerImp());

            Deck myDeck = new DeckImp();

            int totalBets = 0; // the number of bets from all players

            do
            {
                // get bets
                totalBets = 0;
                for (int i = 0; i < playerList.Count; ++i)
                {
                    getBets(playerList[i], i);
                    totalBets += playerList[i].bets.Count;
                }

                if (totalBets == 0)
                {
                    continue; // game over
                }

                dealPlayers(playerList, myDeck);

                Hand dealerHand = new HandImp(myDeck.dealCard(), 0);
                Console.WriteLine("dealer upcard: {0}", dealerHand);

                // player turns
                for (int j = 0; j < playerList.Count; ++j)
                {
                    Player p = playerList[j];

                    Console.WriteLine("player {0}\'s turn", j + 1);

                    // play each hand
                    int i = 0;
                    while (i < p.getHands().Count)
                    {
                        Hand currentHand = p.getHands()[i];
                        Console.WriteLine("hand {0}, bet ${1}, ${2} available : {3}", i + 1, currentHand.bet, p.money, currentHand);

                        bool playOver = false;
                        while ((!playOver) && (currentHand.handValue < 21))
                        {
                            Dictionary<char, string> legitPlays = currentHand.getLegitPlays(p.money);

                            char inputChar = '0';

                            do
                            {
                                Console.Write("Choose one: ");
                                foreach (char c in legitPlays.Keys)
                                {
                                    Console.Write("({0}){1} ", c, legitPlays[c]);
                                }

                                try
                                {
                                    inputChar = char.Parse(Console.ReadLine());
                                }
                                catch (Exception e)
                                {
                                    inputChar = '0'; // error condition
                                }
                                //Console.WriteLine();
                            }
                            while (!legitPlays.Keys.ToArray().Contains(inputChar));

                            // run play
                            string classToExecute = String.Format("Blackjack01.plays.{0}", legitPlays[inputChar]);
                            Type type = Type.GetType(classToExecute);
                            Play play = (Play)Activator.CreateInstance(type);

                            // check doubledown or split and charge player accordingly
                            if ((play is Blackjack01.plays.DoubleDown) || (play is Blackjack01.plays.Split))
                            {
                                p.chargePlayer(currentHand.bet);
                            }

                            Card returnCard = play.execute(currentHand, myDeck.dealCard(), ref playOver);
                            if (returnCard != null) // split
                            {
                                p.deal(returnCard, myDeck.dealCard(), currentHand.bet, i + 1); // deal new hand at position
                                p.getHands()[i + 1].isSplit = true; // not eligible for blackjack
                            }
                            Console.WriteLine(currentHand);
                        }

                        ++i;
                    }
                }

                Console.Write("Dealer\'s hand: ");
                // all the dealer can do is hit - no decisions
                Play dealerHit = new Hit();
                bool dealerPlayOver = false;

                while (dealerHand.handValue < 17)
                {
                    dealerHit.execute(dealerHand, myDeck.dealCard(), ref dealerPlayOver);
                }
                Console.WriteLine(dealerHand);

                evaluateWinners(playerList, dealerHand);
            }
            while (totalBets > 0);

            Console.WriteLine("Bye!");
            Console.ReadKey();
        }

        private static void getBets(Player p, int i)
        {
            List<uint> newBets = new List<uint>();

            bool gotBets = false;

            while (!gotBets)
            {
                try
                {
                    newBets = new List<uint>();
                    Console.Write("enter new bets for player {0}, or ENTER to skip\nTotal new bets not more than ${1}: ", i + 1, p.money);
                    foreach (uint b in p.bets)
                    {
                        Console.Write("{0} ", b);
                    }

                    string inputString = Console.ReadLine();

                    if (!string.IsNullOrEmpty(inputString))
                    {
                        newBets = inputString.Split(' ').Select(UInt32.Parse).ToList(); // throws exception
                    }

                    // check sum
                    uint sum = 0;
                    foreach (uint b in newBets)
                    {
                        sum += b;
                    }

                    if (sum > p.money)
                    {
                        Console.WriteLine("Bets not accepted, you only have ${0} to bet with.", p.money);
                        //gotBets = false;
                        //continue;
                        throw new Exception();
                    }
                    else
                    {
                        // charge player for their bets
                        p.chargePlayer(sum);
                        gotBets = true;
                    }
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e.Message);
                    gotBets = false;
                }
            }

            p.bets.AddRange(newBets);
        }

        private static void dealPlayers(List<Player> playerList, Deck myDeck)
        {
            Console.WriteLine("players hands:");
            for (int i = 0; i < playerList.Count; ++i)
            {
                playerList[i].getHands().Clear();

                for (int j = 0; j < playerList[i].bets.Count; ++j)
                {
                    playerList[i].deal(myDeck.dealCard(), myDeck.dealCard(), playerList[i].bets[j]);
                }

                foreach (Hand h in playerList[i].getHands())
                {
                    Console.Write("player {0}, bet ${1}: {2}", i + 1, h.bet, h);
                    if (h.isBlackjack)
                    {
                        Console.Write(" Blackjack!");
                    }
                    Console.WriteLine();
                }
            }
        }

        private static void evaluateWinners(List<Player> players, Hand dealerHand)
        {
            Console.WriteLine("Evaluate winners");

            for (int i = 0; i < players.Count; ++i)
            {
                Player p = players[i];
                p.bets = new List<uint>(); // reset bets

                Console.WriteLine("Player {0}:", i + 1);
                foreach (Hand h in p.getHands())
                {
                    uint winnings;

                    Console.Write("{0} bet: ${1} ", h, h.bet);

                    if (h.handValue > 21)
                    {
                        Console.WriteLine(" lose - bust");
                    }
                    else if (h.isBlackjack)
                    {
                        if (dealerHand.isBlackjack)
                        {
                            // push
                            Console.WriteLine(" push bet ${0} to next hand", h.bet);
                            p.bets.Add(h.bet);
                        }
                        else
                        {
                            winnings = (uint)Math.Ceiling(h.bet * 2.5);
                            Console.WriteLine(" Blackjack! Win ${0}", winnings);
                            p.awardPlayer(winnings);
                        }
                    }
                    else if (dealerHand.isBlackjack)
                    {
                        // already checked for player's blackjack
                        Console.WriteLine(" lose - dealer has blackjack");
                    }
                    else if (dealerHand.handValue > 21)
                    {
                        winnings = h.bet * 2;
                        Console.WriteLine(" dealer busts! win ${0}", winnings);
                        p.awardPlayer(winnings);
                    }
                    else if (h.handValue > dealerHand.handValue)
                    {
                        winnings = h.bet * 2;
                        Console.WriteLine(" win ${0}", winnings);
                        p.awardPlayer(winnings);
                    }
                    else if (h.handValue == dealerHand.handValue)
                    {
                        // push
                        Console.WriteLine(" push bet ${0} to next hand", h.bet);
                        p.bets.Add(h.bet);
                    }
                    else // dealer has higher value
                    {
                        Console.WriteLine(" lose to dealer");
                    }

                }

                if (p.bets.Count() > 0)
                {
                    Console.Write("Pushed bets for player {0}: ", i + 1);
                    foreach (uint b in p.bets)
                    {
                        Console.Write("${0} ", b);
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
