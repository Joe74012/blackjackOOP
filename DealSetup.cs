using blackjackOOP;

public class DealSetup
{
    public List<Player> Spelers { get; private set; }
    public Card DealerKaart1 { get; private set; }
    public Card DealerKaart2 { get; private set; }

    public DealSetup(PlayerSetup playerSetup, ShoeSetup shoeSetup)
    {
        Spelers = new List<Player>();

        if (!HeeftGenoegKaarten(playerSetup.AantalSpelers, shoeSetup))
            throw new Exception("Niet genoeg kaarten om uit te delen.");

        for (int i = 0; i < playerSetup.AantalSpelers; i++)
        {
            Player speler = new Player(playerSetup.Namen[i]);
            speler.Hand.Add(shoeSetup.DealKaart());
            speler.Hand.Add(shoeSetup.DealKaart());
            Spelers.Add(speler);
        }

        DealerKaart1 = shoeSetup.DealKaart();
        DealerKaart2 = shoeSetup.DealKaart();
    }

    private bool HeeftGenoegKaarten(int aantalSpelers, ShoeSetup shoeSetup)
    {
        int benodigdeKaarten = (aantalSpelers * 2) + 2;
        return shoeSetup.TotaalKaarten() >= benodigdeKaarten;
    }
}