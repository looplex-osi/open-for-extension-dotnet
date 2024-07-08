namespace BoyInTheAudiencePluginTests.Mocks;

public class RaceService
{
    public void StartRace(Action a)
    {
        a();
    }
}