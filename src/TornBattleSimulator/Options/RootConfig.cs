using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;

namespace TornBattleSimulator.Options;

public class RootConfig
{
    public BodyPart? BodyPartHitOverride { get; set; }

    public Dictionary<ModifierType, double> ArmourSetBonuses { get; set; }

    public BodyModifierOptions BodyModifier { get; set; }

    public List<ArmourCoverageOption> ArmourCoverage { get; set; }

    public List<TemporaryWeaponOption> TemporaryWeapons { get; set; }
}