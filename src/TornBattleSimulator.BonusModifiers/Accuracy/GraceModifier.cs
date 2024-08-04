using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Accuracy;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Damage;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Stats;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.BonusModifiers.Accuracy;

public class GraceModifier : IModifier, IAccuracyModifier, IDamageModifier
{
    private readonly double _accuracyModifier;
    private readonly double _damageModifier;

    public GraceModifier(double value)
    {
        _accuracyModifier = 1 + value;
        _damageModifier = 1 - (value / 2);
    }

    public ModifierLifespanDescription Lifespan { get; } = ModifierLifespanDescription.Fixed(ModifierLifespanType.Indefinite);

    public bool RequiresDamageToApply { get; } = false;

    public ModifierTarget Target { get; } = ModifierTarget.SelfWeapon;

    public ModifierApplication AppliesAt { get; } = ModifierApplication.FightStart;

    public ModifierType Effect { get; } = ModifierType.Grace;

    public ModifierValueBehaviour ValueBehaviour { get; } = ModifierValueBehaviour.Potency;

    public StatModificationType Type { get; } = StatModificationType.Additive;

    public double GetAccuracyModifier(PlayerContext active, PlayerContext other, WeaponContext weapon) => _accuracyModifier;

    public DamageModifierResult GetDamageModifier(PlayerContext active, PlayerContext other, WeaponContext weapon, DamageContext damageContext) => new(_damageModifier);
}