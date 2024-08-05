using TornBattleSimulator.BonusModifiers.Accuracy;
using TornBattleSimulator.BonusModifiers.Actions;
using TornBattleSimulator.BonusModifiers.Ammo;
using TornBattleSimulator.BonusModifiers.Attacks;
using TornBattleSimulator.BonusModifiers.Damage;
using TornBattleSimulator.BonusModifiers.Damage.BodyParts;
using TornBattleSimulator.BonusModifiers.DamageOverTime;
using TornBattleSimulator.BonusModifiers.Health;
using TornBattleSimulator.BonusModifiers.Stats.Temporary;
using TornBattleSimulator.BonusModifiers.Stats.Temporary.Needles;
using TornBattleSimulator.BonusModifiers.Stats.Weapon;
using TornBattleSimulator.BonusModifiers.Target;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers;

public class ModifierFactory
{
    public PotentialModifier GetModifier(ModifierType modifierType, double percent)
    {
        double value = percent / 100;

        // Was nice when it was using keyed DI registrations, but some modifiers
        // needing to accept the % as part of their data means we need this instead.

        IModifier modifier = modifierType switch
        {
            // Temps
            ModifierType.Blinded => new BlindedModifier(),
            ModifierType.Concussed => new ConcussedModifier(),
            ModifierType.Gassed => new GassedModifier(),
            ModifierType.Maced => new MacedModifier(),
            ModifierType.SevereBurning => new SevereBurningModifier(),
            ModifierType.Smoked => new SmokedModifier(),

            // Needles
            ModifierType.Hardened => new HardenedModifier(),
            ModifierType.Strengthened => new StrengthenedModifier(),
            ModifierType.Hastened => new HastenedModifier(),
            ModifierType.Sharpened => new SharpenedModifier(),

            // Stat Debuffs
            ModifierType.Cripple => new CrippleModifier(),
            ModifierType.Demoralized => new DemoralizedModifier(),
            ModifierType.Freeze => new FreezeModifier(),

            // Stat Buffs
            ModifierType.Empower => new EmpowerModifier(value),
            ModifierType.Motivation => new MotivationModifier(),

            // Stuns
            ModifierType.Shock => new ShockModifier(),
            ModifierType.Paralyzed => new ParalyzedModifier(),

            // Accuracy
            ModifierType.Focus => new FocusModifier(value),
            ModifierType.Grace => new GraceModifier(value),

            // Damage
            ModifierType.Assassinate => new AssassinateModifier(value),
            ModifierType.Beserk => new BeserkModifier(value),
            ModifierType.Blindside => new BlindsideModifier(value),
            ModifierType.Comeback => new ComebackModifier(value),
            ModifierType.Deadeye => new DeadeyeModifier(value),
            ModifierType.Deadly => new DeadlyModifier(),
            ModifierType.DoubleEdged => new DoubleEdgedModifier(),
            ModifierType.Eviscerate => new EviscerateModifier(value),
            ModifierType.Finale => new FinaleModifier(value),
            ModifierType.Frenzy => new FrenzyModifier(value),
            ModifierType.Smash => new SmashModifier(),

            // Body part modifiers
            ModifierType.Achilles => new AchillesModifier(value),
            ModifierType.Crusher => new CrusherModifier(value),
            ModifierType.Cupid => new CupidModifier(value),

            // DoTs
            ModifierType.Bleed => new BleedModifier(),
            ModifierType.Burning => new BurningModifier(),
            ModifierType.Laceration => new LacerationModifier(),
            ModifierType.Poisoned => new PoisonedModifier(),

            // Health
            ModifierType.Bloodlust => new BloodlustModifier(value),
            ModifierType.Execute => new ExecuteModifier(value),

            // Action buffs
            ModifierType.Blindfire => new BlindfireModifier(),
            ModifierType.DoubleTap => new DoubleTapModifier(),
            ModifierType.Fury => new FuryModifier(),
            ModifierType.Rage => new RageModifier(),

            // Ammo
            ModifierType.Conserve => new ConserveModifier(value),

            // Target
            ModifierType.HomeRun => new HomeRunModifier(),

            //  All modifiers in Torn can be created, but those that have no use
            //  (e.g. Pluder) are no-ops so we can support them being added without throwing.
            ModifierType.Backstab => new NoOpModifier(ModifierType.Backstab),

            0 => throw new NotImplementedException($"{modifierType} is not registered in ${nameof(ModifierFactory)}"),
        };

        return ChanceWrapper(modifier, value);
    }

    private PotentialModifier ChanceWrapper(IModifier modifier, double value)
    {
        return new PotentialModifier(
            modifier,
            modifier.ValueBehaviour == ModifierValueBehaviour.Chance ? value : 1);
    }
}