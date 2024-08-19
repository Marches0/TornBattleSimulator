using TornBattleSimulator.Battle.Thunderdome.Target;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Chance;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Battle.Thunderdome.Accuracy;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome.Events.Data;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.BonusModifiers.Target;

namespace TornBattleSimulator.Battle.Thunderdome.Action.Weapon.Usage;

public class DamageProcessor
{
    private readonly IChanceSource _chanceSource;
    private readonly IAccuracyCalculator _accuracyCalculator;
    private readonly IAmmoCalculator _ammoCalculator;
    private readonly IDamageCalculator _damageCalculator;

    public DamageProcessor(
        IChanceSource chanceSource,
        IAccuracyCalculator accuracyCalculator,
        IAmmoCalculator ammoCalculator,
        IDamageCalculator damageCalculator,
        TargetSelector targetSelector)
    {
        _chanceSource = chanceSource;
        _accuracyCalculator = accuracyCalculator;
        _ammoCalculator = ammoCalculator;
        _damageCalculator = damageCalculator;
    }

    public ThunderdomeEvent PerformAttack(AttackContext attack)
    {
        // Non-damaging temps are handled specially, since they don't
        // actually miss and are better with a description of their type.
        if (attack.Weapon.Type == WeaponType.Temporary && attack.Weapon.Description.Damage == 0)
        {
            return attack.Context.CreateEvent(
                attack.Active,
                ThunderdomeEventType.UsedTemporary,
                new UsedTemporaryEvent(attack.Weapon.Description.TemporaryWeaponType!.Value)
            );
        }

        // Make a new context with whoever is actually being targeted (i.e. because of HomeRun)
        (PlayerContext target, PlayerContext source) = GetDamageTarget(attack);
        AttackContext targetContext = new AttackContext(attack.Context, source, target, attack.Weapon, null);

        attack.AttackResult = CalculateAttack(targetContext);
        return MakeHit(target, source, attack);
    }

    private AttackResult CalculateAttack(AttackContext attack)
    {
        // double check all this
        double hitChance = _accuracyCalculator.GetAccuracy(attack.Active, attack.Other, attack.Weapon);
        return _chanceSource.Succeeds(hitChance)
            ? new AttackResult(true, hitChance, _damageCalculator.CalculateDamage(attack))
            : new AttackResult(false, hitChance, null);
    }

    private (PlayerContext target, PlayerContext source) GetDamageTarget(AttackContext attack)
    {
        // This logic is spread in two places, one for damage deflection and one for modifier.
        // Ideally it'd be in one, but would need to turn things around.
        return attack.Weapon.Type == WeaponType.Temporary && attack.Other.Modifiers.Active.OfType<HomeRunModifier>().Any()
            ? (attack.Active, attack.Other)
            : (attack.Other, attack.Active);
    }

    private ThunderdomeEvent MakeHit(PlayerContext target, PlayerContext source, AttackContext attack)
    {
        if (attack.AttackResult!.Hit)
        {
            target.Health.CurrentHealth -= attack.AttackResult.Damage!.DamageDealt;
            return attack.Context.CreateEvent(
                source,
                ThunderdomeEventType.AttackHit,
                new AttackHitEvent(attack.Weapon.Type, attack.AttackResult)
            );
        }
        else
        {
            return attack.Context.CreateEvent(
                attack.Active,
                ThunderdomeEventType.AttackMiss,
                new AttackMissedEvent(attack.Weapon.Type, attack.AttackResult.HitChance)
            );
        }
    }
}