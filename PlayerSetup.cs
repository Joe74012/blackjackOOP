public class PlayerSetup
{
    public int AantalSpelers { get; private set; }
    public List<string> Namen { get; private set; }

    private static readonly string[] AlleNamen = { "Alice", "Bob", "Charlie", "Diana", "Eve", "Frank", "Grace", "Hank" };

    public PlayerSetup(int aantalSpelers)
    {
        AantalSpelers = aantalSpelers;
        Namen = new List<string>();

        Random rnd = new Random();
        List<string> geshuffled = AlleNamen.OrderBy(_ => rnd.Next()).ToList();

        for (int i = 0; i < aantalSpelers; i++)
        {
            Namen.Add(geshuffled[i]);
        }
    }
}