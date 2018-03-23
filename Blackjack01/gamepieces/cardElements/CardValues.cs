using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Blackjack01.gamepieces.cardElements
{
    abstract class CardValues
    {
        public static readonly Dictionary<Ranks, string> rankStrings = new Dictionary<Ranks, string>
        {
            {Ranks.ACE, "A"},
            {Ranks.TWO, "2"},
            {Ranks.THREE, "3"},
            {Ranks.FOUR, "4" },
            {Ranks.FIVE, "5"},
            {Ranks.SIX, "6"},
            {Ranks.SEVEN, "7"},
            {Ranks.EIGHT, "8"},
            {Ranks.NINE, "9"},
            {Ranks.TEN, "10"},
            {Ranks.JACK, "J"},
            {Ranks.QUEEN, "Q"},
            {Ranks.KING, "K"}
        };

        public static readonly Dictionary<Ranks, int> rankValues = new Dictionary<Ranks, int>
        {
            {Ranks.ACE, 1},
            {Ranks.TWO, 2},
            {Ranks.THREE, 3},
            {Ranks.FOUR, 4},
            {Ranks.FIVE, 5},
            {Ranks.SIX, 6},
            {Ranks.SEVEN, 7},
            {Ranks.EIGHT, 8},
            {Ranks.NINE, 9},
            {Ranks.TEN, 10},
            {Ranks.JACK, 10},
            {Ranks.QUEEN, 10},
            {Ranks.KING, 10}
        };
    }
}