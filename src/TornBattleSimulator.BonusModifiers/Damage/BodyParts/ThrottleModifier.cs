using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage.BodyParts;

namespace TornBattleSimulator.BonusModifiers.Damage.BodyParts;

public class ThrottleModifier : BodyPartDamageModifier, IModifier
{
    public ThrottleModifier(double value) : base(BodyPart.Throat, value)
    {
    }

    /// <inheritdoc/>
    public ModifierType Effect { get; } = ModifierType.Throttle;
}