namespace TornBattleSimulator.Shared.Thunderdome.Player.Armours;

public class ArmourSetContext
{
    public ArmourSetContext(List<ArmourContext> armour)
    {
        Armour = armour;
    }

    public List<ArmourContext> Armour { get; }
}