using blackjackOOP;

public class Dealer
{
    public List<Card> Hand { get; set; }

    public Dealer()
    {
        Hand = new List<Card>();
    }

    public int HandValue()
    {
        int total = 0;
        foreach (Card card in Hand)
            total += card.Value;
        return total;
    }

    public bool MoetHitten()
    {
        return HandValue() < 17;
    }
}