namespace TornBattleSimulator.Core.Thunderdome.Chance.Source;

public interface IRandomSource
{
    double Next();
    int Next(int min, int max);
}