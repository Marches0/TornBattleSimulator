using TornBattleSimulator.Battle.Thunderdome.Player.Armours;

namespace TornBattleSimulator.Options;

public class RootConfig
{
    public BodyModifierOptions BodyModifier { get; set; }

    public List<ArmourCoverageOption> ArmourCoverage { get; set; }
}