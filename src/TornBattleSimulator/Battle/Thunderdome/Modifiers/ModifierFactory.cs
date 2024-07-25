using TornBattleSimulator.BonusModifiers.Actions;
using TornBattleSimulator.BonusModifiers.Attacks;
using TornBattleSimulator.BonusModifiers.Damage;
using TornBattleSimulator.BonusModifiers.Damage.BodyParts;
using TornBattleSimulator.BonusModifiers.DamageOverTime;
using TornBattleSimulator.BonusModifiers.Stats.Temporary;
using TornBattleSimulator.BonusModifiers.Stats.Temporary.Needles;
using TornBattleSimulator.BonusModifiers.Stats.Weapon;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers;

public class ModifierFactory
{
    public IModifier GetModifier(ModifierType modifierType, double percent)
    {
        // Was nice when it was using keyed DI registrations, but some modifiers
        // needing to accept the % as part of their data means we need this instead.
        return modifierType switch
        {
            ModifierType.Concussed => new ConcussedModifier(),
            ModifierType.Strengthened => new StrengthenedModifier(),
            ModifierType.Blinded => new BlindedModifier(),
            ModifierType.Hastened => new HastenedModifier(),
            ModifierType.SevereBurning => new SevereBurningModifier(),
            ModifierType.Maced => new MacedModifier(),
            ModifierType.Hardened => new HardenedModifier(),
            ModifierType.Gassed => new GassedModifier(),
            ModifierType.Smoked => new SmokedModifier(),
            ModifierType.Sharpened => new SharpenedModifier(),

            ModifierType.Blindfire => new BlindfireModifier(),
            ModifierType.Burning => new BurningModifier(),
            ModifierType.Demoralized => new DemoralizedModifier(),
            ModifierType.Freeze => new FreezeModifier(),
            ModifierType.Laceration => new LacerationModifier(),
            ModifierType.Poisoned => new PoisonedModifier(),
            ModifierType.Shock => new ShockModifier(),
            ModifierType.Smash => new SmashModifier(),

            ModifierType.Fury => new FuryModifier(),
            ModifierType.Rage => new RageModifier(null), // hmm
            ModifierType.Achilles => new AchillesModifier(percent / 100),

            0 => throw new ArgumentOutOfRangeException()
        };
    }
}