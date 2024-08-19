using TornBattleSimulator.Core.Thunderdome.Modifiers;

namespace TornBattleSimulator.Core.Thunderdome.Player.Armours;

public class ArmourContext : ITickable
{
    public ArmourContext(
        double rating,
        List<ArmourCoverage> coverage,
        List<PotentialModifier> modifiers)
    {
        Rating = rating;
        Coverage = coverage;
        PotentialModifiers = modifiers;
    }

    public double Rating { get; set; }

    public List<ArmourCoverage> Coverage { get; set; }
    public List<PotentialModifier> PotentialModifiers { get; set; }

    public ModifierContext Modifiers { get; set; }

    public void FightBegin(ThunderdomeContext context)
    {
        Modifiers.FightBegin(context);
    }

    public void OpponentActionComplete(ThunderdomeContext context)
    {
        Modifiers.OpponentActionComplete(context);
    }

    public void OwnActionComplete(ThunderdomeContext context)
    {
        Modifiers.OwnActionComplete(context);
    }

    public void TurnComplete(ThunderdomeContext context)
    {
        Modifiers.TurnComplete(context);
    }
}