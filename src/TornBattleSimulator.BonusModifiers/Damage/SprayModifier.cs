using TornBattleSimulator.Battle.Thunderdome.Damage.Targeting;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Ammo;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Conditional;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;

namespace TornBattleSimulator.BonusModifiers.Damage;

public class SprayModifier : IModifier, IDamageModifier, IConditionalModifier, IAmmoModifier
{
    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Fixed(ModifierLifespanType.AfterOwnAction);

    public bool RequiresDamageToApply { get; } = false;

    public ModifierTarget Target { get; } = ModifierTarget.Self;

    public ModifierApplication AppliesAt { get; } = ModifierApplication.BeforeAction;

    public ModifierType Effect { get; } = ModifierType.Spray;

    public ModifierValueBehaviour ValueBehaviour { get; } = ModifierValueBehaviour.Chance;

    public ModificationType Type { get; } = ModificationType.Additive;

    // Must have a full magazine
    public bool CanActivate(AttackContext attack) => attack.Weapon.Ammo!.MagazineAmmoRemaining == attack.Weapon.Ammo.MagazineSize;

    public double GetDamageModifier(AttackContext attack, HitLocation hitLocation) => 2;

    public double GetModifier() => double.MaxValue;
}