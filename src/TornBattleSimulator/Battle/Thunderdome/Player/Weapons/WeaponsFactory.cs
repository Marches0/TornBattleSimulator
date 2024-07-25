using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Build;
using TornBattleSimulator.Core.Build.Equipment;
using TornBattleSimulator.Core.Thunderdome.Modifiers;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.Battle.Thunderdome.Player.Weapons;
public class WeaponsFactory
{
    private readonly ModifierFactory _modifierFactory;
    private readonly TemporaryWeaponFactory _temporaryWeaponFactory;

    public WeaponsFactory(
        ModifierFactory modifierFactory,
        TemporaryWeaponFactory temporaryWeaponFactory)
    {
        _modifierFactory = modifierFactory;
        _temporaryWeaponFactory = temporaryWeaponFactory;
    }

    public EquippedWeapons Create(BattleBuild build)
    {
        Weapon? temporary = build.Temporary != null
            ? _temporaryWeaponFactory.GetTemporaryWeapon(build.Temporary.Value)
            : null;

        return new EquippedWeapons(
             GetContext(build.Primary, WeaponType.Primary),
             GetContext(build.Secondary, WeaponType.Secondary),
             GetContext(build.Melee, WeaponType.Melee),
             GetContext(temporary, WeaponType.Temporary)
        );
    }

    private WeaponContext? GetContext(Weapon? weapon, WeaponType weaponType)
    {
        var modifiers = weapon.Modifiers
            .Select(m => new
            {
                Percent = m.Percent,
                Modifier = _modifierFactory.GetModifier(m.Type, m.Percent)
            })
            .ToList();

        List<PotentialModifier> chanceModifiers = modifiers
                .Where(m => m.Modifier.ValueBehaviour == ModifierValueBehaviour.Chance)
                .Select(m => new PotentialModifier(m.Modifier, m.Percent / 100))
                .ToList();

        List<IModifier> otherModifiers = modifiers
            .Where(m => m.Modifier.ValueBehaviour != ModifierValueBehaviour.Chance)
            .Select(m => m.Modifier)
            .ToList();

        return weapon is null
            ? null
            : new WeaponContext(
                weapon,
                weaponType,
                chanceModifiers,
                otherModifiers
            );
    }
}