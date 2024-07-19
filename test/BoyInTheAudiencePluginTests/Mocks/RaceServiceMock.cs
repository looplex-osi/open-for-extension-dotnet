namespace BoyInTheAudiencePluginTests.Mocks;

public class RaceService
{
    public void StartRaceAsync(Action a)
    {
        a();
    }
}