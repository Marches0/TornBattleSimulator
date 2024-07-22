using TornBattleSimulator.Battle.Build;
using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Player.Armours;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.UnitTests.Thunderdome;

public class PlayerContextBuilder
{
    private BattleStats _battleStats = new BattleStats() {};
    private uint _health;
    private Weapon? _primary;
    private WeaponContext? _primaryContext;
    private Weapon? _secondary;
    private WeaponContext? _secondaryContext;
    private Weapon? _melee;
    private Weapon? _temporary;
    private EquippedWeapons? _equippedWeapons;
    private List<ArmourContext> _armour = new();

    public PlayerContextBuilder WithStats(BattleStats battleStats)
    {
        _battleStats = battleStats;
        return this;
    }

    public PlayerContextBuilder WithPrimary(Weapon weapon)
    {
        _primary = weapon;
        return this;
    }

    public PlayerContextBuilder WithPrimaryContext(WeaponContext weaponContext)
    {
        _primaryContext = weaponContext;
        return this;
    }

    public PlayerContextBuilder WithSecondary(Weapon weapon)
    {
        _secondary = weapon;
        return this;
    }

    public PlayerContextBuilder WithSecondaryContext(WeaponContext weaponContext)
    {
        _secondaryContext = weaponContext;
        return this;
    }

    public PlayerContextBuilder WithMelee(Weapon weapon)
    {
        _melee = weapon;
        return this;
    }

    public PlayerContextBuilder WithTemporary(Weapon weapon)
    {
        _temporary = weapon;
        return this;
    }

    public PlayerContextBuilder WithHealth(uint health)
    {
        _health = health;
        return this;
    }

    public PlayerContextBuilder WithWeapons(EquippedWeapons weapons)
    {
        _equippedWeapons = weapons;
        return this;
    }

    public PlayerContextBuilder WithArmour(List<ArmourContext> armour)
    {
        _armour = armour;
        return this;
    }

    public PlayerContext Build()
    {
        return new PlayerContext(
            new BattleBuild()
            {
                BattleStats = _battleStats,
                Health = _health,
                Primary = _primary,
                Secondary = _secondary,
                Melee = _melee
            },
            0,
            _equippedWeapons ?? new EquippedWeapons(
                _primaryContext ?? (_primary != null ? new WeaponContext(_primary, WeaponType.Primary, new List<PotentialModifier>(), new List<IModifier>()) : null),
                _secondaryContext ?? (_secondary != null ? new WeaponContext(_secondary, WeaponType.Secondary, new List<PotentialModifier>(), new List<IModifier>()) : null),
                _melee != null ? new WeaponContext(_melee, WeaponType.Melee, new List<PotentialModifier>(), new List<IModifier>()) : null,
                _temporary != null ? new WeaponContext(_temporary, WeaponType.Temporary, new List<PotentialModifier>(), new List<IModifier>()) : null),
            new ArmourSetContext(_armour),
            null
            );
    }
}