public class ScoreSetup
{
    public int Punten { get; private set; }

    public void GeefPunten()
    {
        Punten += 1;
    }

    public void GeefStrafpunten()
    {
        Punten -= 1;
    }
}