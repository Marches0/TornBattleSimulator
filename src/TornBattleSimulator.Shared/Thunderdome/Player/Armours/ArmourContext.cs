using TornBattleSimulator.Battle.Thunderdome.Modifiers;

namespace TornBattleSimulator.Battle.Thunderdome.Player.Armours;

public class ArmourContext
{
    public ArmourContext(
        double rating,
        List<ArmourCoverage> coverage,
        List<PotentialModifier> modifiers)
    {
        Rating = rating;
        Coverage = coverage;
        Modifiers = modifiers;
    }

    public double Rating { get; set; }

    public List<ArmourCoverage> Coverage { get; set; }
    public List<PotentialModifier> Modifiers { get; set; }
}