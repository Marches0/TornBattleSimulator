using Autofac;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.BonusModifiers.Actions;
using TornBattleSimulator.BonusModifiers.Attacks;
using TornBattleSimulator.BonusModifiers.Damage;
using TornBattleSimulator.BonusModifiers.DamageOverTime;
using TornBattleSimulator.BonusModifiers.Stats.Temporary.Needles;
using TornBattleSimulator.BonusModifiers.Stats.Temporary;
using TornBattleSimulator.BonusModifiers.Stats.Weapon;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Build.Equipment;

namespace TornBattleSimulator.Modules;

public class ModifierModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ModifierFactory>()
            .As<ModifierFactory>();

        builder.RegisterType<ModifierApplier>()
            .As<ModifierApplier>();

        RegisterTempWeaponModifiers(builder);
        RegisterUniqueWeaponModifiers(builder);
        RegisterWeaponModifiers(builder);
    }

    private void RegisterTempWeaponModifiers(ContainerBuilder builder)
    {
        builder.RegisterType<ConcussedModifier>()
            .Keyed<IModifier>(ModifierType.Concussed);

        builder.RegisterType<StrengthenedModifier>()
            .Keyed<IModifier>(ModifierType.Strengthened);

        builder.RegisterType<BlindedModifier>()
            .Keyed<IModifier>(ModifierType.Blinded);

        builder.RegisterType<HastenedModifier>()
            .Keyed<IModifier>(ModifierType.Hastened);

        builder.RegisterType<SevereBurningModifier>()
            .Keyed<IModifier>(ModifierType.SevereBurning);

        builder.RegisterType<MacedModifier>()
            .Keyed<IModifier>(ModifierType.Maced);

        builder.RegisterType<HardenedModifier>()
            .Keyed<IModifier>(ModifierType.Hardened);

        builder.RegisterType<GassedModifier>()
           .Keyed<IModifier>(ModifierType.Gassed);

        builder.RegisterType<SharpenedModifier>()
            .Keyed<IModifier>(ModifierType.Sharpened);
    }

    private void RegisterUniqueWeaponModifiers(ContainerBuilder builder)
    {
        builder.RegisterType<BlindfireModifier>()
            .Keyed<IModifier>(ModifierType.Blindfire);

        builder.RegisterType<BurningModifier>()
            .Keyed<IModifier>(ModifierType.Burning);

        builder.RegisterType<DemoralizedModifier>()
            .Keyed<IModifier>(ModifierType.Demoralized);

        builder.RegisterType<FreezeModifier>()
            .Keyed<IModifier>(ModifierType.Freeze);

        builder.RegisterType<LacerationModifier>()
            .Keyed<IModifier>(ModifierType.Laceration);

        builder.RegisterType<PoisonedModifier>()
            .Keyed<IModifier>(ModifierType.Poisoned);

        builder.RegisterType<ShockModifier>()
            .Keyed<IModifier>(ModifierType.Shock);

        builder.RegisterType<SmashModifier>()
            .Keyed<IModifier>(ModifierType.Smash);
    }

    private void RegisterWeaponModifiers(ContainerBuilder builder)
    {
        builder.RegisterType<FuryModifier>()
            .Keyed<IModifier>(ModifierType.Fury);

        builder.RegisterType<RageModifier>()
            .Keyed<IModifier>(ModifierType.Rage);
    }
}