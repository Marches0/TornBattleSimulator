using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage.BodyParts;

namespace TornBattleSimulator.BonusModifiers.Damage.BodyParts;

public class RoshamboModifier : BodyPartDamageModifier, IModifier
{
    public RoshamboModifier(double value) : base(BodyPart.Groin, value)
    {
    }

    /// <inheritdoc/>
    public ModifierType Effect { get; } = ModifierType.Roshambo;
}