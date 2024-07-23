namespace TornBattleSimulator.Core.Thunderdome;

public interface ITickable
{
    void TurnComplete(ThunderdomeContext context);

    void OwnActionComplete(ThunderdomeContext context);

    void OpponentActionComplete(ThunderdomeContext context);
}