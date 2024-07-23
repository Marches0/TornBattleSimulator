namespace TornBattleSimulator.Shared.Thunderdome.Chance.Source;

public interface IRandomSource
{
    double Next();
    int Next(int min, int max);
}