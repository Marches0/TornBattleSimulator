namespace TornBattleSimulator.Core.Thunderdome.Player.Armours;

public class ArmourSetContext : ITickable
{
    public ArmourSetContext(List<ArmourContext> armour)
    {
        Armour = armour;
    }

    public List<ArmourContext> Armour { get; }

    public void FightBegin(ThunderdomeContext context)
    {
        foreach (ArmourContext armour in Armour)
        {
            armour.FightBegin(context);
        }
    }

    public void OpponentActionComplete(ThunderdomeContext context)
    {
        foreach (ArmourContext armour in Armour)
        {
            armour.OpponentActionComplete(context);
        }
    }

    public void OwnActionComplete(ThunderdomeContext context)
    {
        foreach (ArmourContext armour in Armour)
        {
            armour.OwnActionComplete(context);
        }
    }

    public void TurnComplete(ThunderdomeContext context)
    {
        foreach (ArmourContext armour in Armour)
        {
            armour.TurnComplete(context);
        }
    }
}