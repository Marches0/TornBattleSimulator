namespace TornBattleSimulator.Battle.Thunderdome.Action;

public interface IAction
{
    ThunderdomeEvent PerformAction(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other);
}