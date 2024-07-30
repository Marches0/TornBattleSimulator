using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage.BodyParts;

namespace TornBattleSimulator.BonusModifiers.Damage.BodyParts;

public class CupidModifier : BodyPartDamageModifier, IModifier
{
    public CupidModifier(double value) : base(BodyPart.Heart, value)
    {
    }

    /// <inheritdoc/>
    public ModifierType Effect { get; } = ModifierType.Cupid;
}