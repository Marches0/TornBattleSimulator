using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;

namespace TornBattleSimulator.Options;

public class RootConfig
{
    public BodyPart? BodyPartHitOverride { get; set; }

    public BodyModifierOptions BodyModifier { get; set; }

    public List<ArmourCoverageOption> ArmourCoverage { get; set; }

    public List<TemporaryWeaponOption> TemporaryWeapons { get; set; }
}