using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage.BodyParts;

namespace TornBattleSimulator.BonusModifiers.Damage.BodyParts;

public class AchillesModifier : BodyPartDamageModifier, IModifier
{
    public AchillesModifier(double damage) : base(BodyPart.Feet, damage)
    {
    }

    /// <inheritdoc/>
    public ModifierType Effect => ModifierType.Achilles;
}