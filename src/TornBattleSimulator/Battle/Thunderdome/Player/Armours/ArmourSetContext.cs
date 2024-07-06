namespace TornBattleSimulator.Battle.Thunderdome.Player.Armours;

public class ArmourSetContext
{
    private readonly List<ArmourContext> _armour;

    public ArmourSetContext(List<ArmourContext> armour)
    {
        _armour = armour;
    }
}