using TornBattleSimulator.Core.Thunderdome.Modifiers;

namespace TornBattleSimulator.Core.Thunderdome.Player.Armours;

public class ArmourContext
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
}