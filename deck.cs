using blackjackOOP.Enums;

namespace blackjackOOP
{
    public class deck
    {
        public List<Card> Cards { get; private set; }

        public deck(int aantalDecks)
        {
            Cards = new List<Card>();

            for (int d = 0; d < aantalDecks; d++)
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                    {
                        Cards.Add(new Card(rank, suit));
                    }
                }
            }
        }
    }
}