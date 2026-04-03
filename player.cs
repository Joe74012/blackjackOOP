using blackjackOOP.Enums;

namespace blackjackOOP
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; set; }
        public bool IsStanding { get; set; }

        public Player(string name)
        {
            Name = name;
            Hand = new List<Card>();
            IsStanding = false;
        }

        public int HandValue()
        {
            int total = 0;
            foreach (Card card in Hand)
                total += card.Value;
            return total;
        }

        public bool CanSplit()
        {
            return Hand.Count == 2 && Hand[0].Value == Hand[1].Value;
        }
    }
}