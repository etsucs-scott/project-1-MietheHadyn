using static WarClassLibrary.Ranks;
using static WarClassLibrary.Suits;

namespace WarClassLibrary
{
    /// <summary>
    /// card, with a suit and rank
    /// </summary>
    public class Card
    {

        /// <summary>
        /// gets the suit of the card
        /// </summary>
        public Suits.Suit Suit { get; }

        /// <summary>
        /// gets the rank of the card
        /// </summary>
        public Ranks.Rank Rank { get; }

        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }
    }
}
