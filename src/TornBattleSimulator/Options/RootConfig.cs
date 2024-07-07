namespace TornBattleSimulator.Options;

public class RootConfig
{
    public BodyModifierOptions BodyModifier { get; set; }

    public List<ArmourCoverageOption> ArmourCoverage { get; set; }

    public List<TemporaryWeaponOption> TemporaryWeapons { get; set; }
}