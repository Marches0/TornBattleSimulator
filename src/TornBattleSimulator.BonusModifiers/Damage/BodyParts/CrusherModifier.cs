using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage.BodyParts;

namespace TornBattleSimulator.BonusModifiers.Damage.BodyParts;

public class CrusherModifier : BodyPartDamageModifier, IModifier
{
    public CrusherModifier(double value) : base(BodyPart.Head, value)
    {
    }

    /// <inheritdoc/>
    public ModifierType Effect => ModifierType.Crusher;
}