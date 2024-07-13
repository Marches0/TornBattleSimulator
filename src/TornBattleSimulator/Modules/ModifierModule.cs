using Autofac;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Stats.Temporary;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Stats.Temporary.Needles;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.DamageOverTime;
using TornBattleSimulator.Battle.Thunderdome.Modifiers.Attacks;

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
    }
}