using blackjackOOP;

public class ShoeSetup
{
    public int AantalDecks { get; private set; }
    public List<deck> Decks { get; private set; }

    public ShoeSetup(int aantalDecks)
    {
        AantalDecks = aantalDecks;
        Decks = new List<deck>();

        for (int i = 0; i < aantalDecks; i++)
        {
            Decks.Add(new deck(1));
        }
    }

    public int TotaalKaarten()
    {
        return Decks.Sum(d => d.Cards.Count);
    }

    public Card DealKaart()
    {
        deck gekozenDeck = Decks.First(d => d.Cards.Count > 0);
        return gekozenDeck.Deal();
    }

    public void Shuffle()
    {
        List<Card> alleKaarten = Decks.SelectMany(d => d.Cards).ToList();

        if (alleKaarten.Count == 0)
            throw new Exception("Er zijn geen kaarten om te schudden.");

        alleKaarten = alleKaarten.OrderBy(_ => new Random().Next()).ToList();

        int index = 0;
        foreach (deck d in Decks)
        {
            d.Cards = alleKaarten.Skip(index).Take(d.Cards.Count).ToList();
            index += d.Cards.Count;
        }
    }
}