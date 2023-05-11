namespace Stat
{
    public interface IStats
    {
        int CoffinTimeAdded { get; set; }
        int Stability { get; set; }
        int Vision { get; set; }
        int GrindMiniGameBallSize { get; set; }
        
        int GrindLeniency { get; set; }
        int ScoreMultiplier { get; set; }

        Stats Add(IStats other);
    }
}