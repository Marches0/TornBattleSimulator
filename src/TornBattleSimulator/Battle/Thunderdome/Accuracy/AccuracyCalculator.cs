﻿using TornBattleSimulator.Battle.Thunderdome.Accuracy.Modifiers;
using TornBattleSimulator.BonusModifiers.Accuracy;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Accuracy;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Accuracy;

public class AccuracyCalculator : IAccuracyCalculator
{
    private readonly ISpeedDexterityAccuracyModifier _speedDexterityAccuracyModifier;
    private readonly IWeaponAccuracyModifier _weaponAccuracyModifier;

    public AccuracyCalculator(
        ISpeedDexterityAccuracyModifier speedDexterityAccuracyModifier,
        IWeaponAccuracyModifier weaponAccuracyModifier)
    {
        _speedDexterityAccuracyModifier = speedDexterityAccuracyModifier;
        _weaponAccuracyModifier = weaponAccuracyModifier;
    }

    public double GetAccuracy(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon)
    {
        if (active.Modifiers.Active.OfType<SureShotModifier>().Any()) 
        {
            return 1;
        }

        double baseAccuracy = GetBaseAccuracy(active, other, weapon);
        return Math.Clamp(
            GetModifiedAccuracy(active, other, weapon, baseAccuracy),
            0,
            1
        );
    }

    private double GetBaseAccuracy(PlayerContext active,
        PlayerContext other,
        WeaponContext weapon)
    {
        // Chaining a single interface doesn't work as well (as used for damage)
        // since more calculations are linked, so we explicitly line up the components.
        double statChance = _speedDexterityAccuracyModifier.GetHitChance(active, other);
        double totalChance = _weaponAccuracyModifier.GetHitChance(active, other, weapon, statChance);

        return totalChance;
    }

    private double GetModifiedAccuracy(
        PlayerContext active,
        PlayerContext other,
        WeaponContext weapon,
        double baseAccuracy)
    {
        var modifiers = active.Modifiers.Active.OfType<IAccuracyModifier>()
            .Concat(weapon.Modifiers.Active.OfType<IAccuracyModifier>());

        return modifiers.Aggregate(baseAccuracy, (total, modifier) => total * modifier.GetAccuracyModifier(active, other, weapon));
    }
}