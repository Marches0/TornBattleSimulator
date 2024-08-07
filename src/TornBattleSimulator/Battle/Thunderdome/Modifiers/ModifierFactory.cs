using TornBattleSimulator.BonusModifiers.Accuracy;
using TornBattleSimulator.BonusModifiers.Actions;
using TornBattleSimulator.BonusModifiers.Ammo;
using TornBattleSimulator.BonusModifiers.Armour;
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
            ModifierType.Achilles => new AchillesModifier(value),
            ModifierType.Assassinate => new AssassinateModifier(value),

            ModifierType.Beserk => new BeserkModifier(value),
            ModifierType.Bleed => new BleedModifier(),
            ModifierType.Blinded => new BlindedModifier(),
            ModifierType.Blindfire => new BlindfireModifier(),
            ModifierType.Blindside => new BlindsideModifier(value),
            ModifierType.Bloodlust => new BloodlustModifier(value),
            ModifierType.Burning => new BurningModifier(),

            ModifierType.Comeback => new ComebackModifier(value),
            ModifierType.Concussed => new ConcussedModifier(),
            ModifierType.Conserve => new ConserveModifier(value),
            ModifierType.Cripple => new CrippleModifier(),
            ModifierType.Crusher => new CrusherModifier(value),
            ModifierType.Cupid => new CupidModifier(value),

            ModifierType.Deadeye => new DeadeyeModifier(value),
            ModifierType.Deadly => new DeadlyModifier(),
            ModifierType.Demoralized => new DemoralizedModifier(),
            ModifierType.DoubleEdged => new DoubleEdgedModifier(),
            ModifierType.DoubleTap => new DoubleTapModifier(),

            ModifierType.Empower => new EmpowerModifier(value),
            ModifierType.Eviscerate => new EviscerateModifier(value),
            ModifierType.Execute => new ExecuteModifier(value),

            ModifierType.Focus => new FocusModifier(value),
            ModifierType.Finale => new FinaleModifier(value),
            ModifierType.Frenzy => new FrenzyModifier(value),
            ModifierType.Freeze => new FreezeModifier(),
            ModifierType.Fury => new FuryModifier(),

            ModifierType.Gassed => new GassedModifier(),
            ModifierType.Grace => new GraceModifier(value),

            ModifierType.Hardened => new HardenedModifier(),
            ModifierType.Hastened => new HastenedModifier(),
            ModifierType.HomeRun => new HomeRunModifier(),

            ModifierType.Laceration => new LacerationModifier(),

            ModifierType.Maced => new MacedModifier(),
            ModifierType.Motivation => new MotivationModifier(),

            ModifierType.Paralyzed => new ParalyzedModifier(),
            ModifierType.Parry => new ParryModifier(),
            ModifierType.Penetrate => new PenetrateModifier(value),
            ModifierType.Puncture => new PunctureModifier(),
            ModifierType.Poisoned => new PoisonedModifier(),
            ModifierType.Powerful => new PowerfulModifier(value),

            ModifierType.Quicken => new QuickenModifier(value),

            ModifierType.Rage => new RageModifier(),
            ModifierType.Roshambo => new RoshamboModifier(value),

            ModifierType.SevereBurning => new SevereBurningModifier(),
            ModifierType.Sharpened => new SharpenedModifier(),
            ModifierType.Shock => new ShockModifier(),
            ModifierType.Slow => new SlowModifier(),
            ModifierType.Smash => new SmashModifier(),
            ModifierType.Smoked => new SmokedModifier(),
            ModifierType.Smurf => new SmurfModifier(value),
            ModifierType.Specialist => new SpecialistModifier(value),
            ModifierType.Strengthened => new StrengthenedModifier(),
            ModifierType.Stun => new StunModifier(),
            ModifierType.Suppress => new SuppressModifier(),
            ModifierType.SureShot => new SureShotModifier(),

            ModifierType.Throttle => new ThrottleModifier(value),

            //  All modifiers in Torn can be created, but those that have no use
            //  (e.g. Pluder) are no-ops so we can support them being added without throwing.
            ModifierType.Backstab => new NoOpModifier(ModifierType.Backstab),
            ModifierType.Plunder => new NoOpModifier(ModifierType.Plunder),
            ModifierType.Proficience => new NoOpModifier(ModifierType.Proficience),
            ModifierType.Revitalize => new NoOpModifier(ModifierType.Revitalize),
            ModifierType.Stricken => new NoOpModifier(ModifierType.Stricken),
            ModifierType.Warlord => new NoOpModifier(ModifierType.Warlord),

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