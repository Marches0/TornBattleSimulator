using TornBattleSimulator.Core.Build;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Armours;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.UnitTests.Thunderdome;

public class PlayerContextBuilder
{
    private BattleStats _battleStats = new BattleStats() {};
    private uint _health;
    private WeaponContext? _primary;
    private WeaponContext? _secondary;
    private WeaponContext? _melee;
    private WeaponContext? _temporary;
    private EquippedWeapons? _equippedWeapons;
    private List<ArmourContext> _armour = new();

    public PlayerContextBuilder WithStats(BattleStats battleStats)
    {
        _battleStats = battleStats;
        return this;
    }

    public PlayerContextBuilder WithPrimary(WeaponContext weapon)
    {
        _primary = weapon;
        return this;
    }

    public PlayerContextBuilder WithSecondary(WeaponContext weapon)
    {
        _secondary = weapon;
        return this;
    }

    public PlayerContextBuilder WithMelee(WeaponContext weapon)
    {
        _melee = weapon;
        return this;
    }

    public PlayerContextBuilder WithTemporary(WeaponContext weapon)
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
                /*Primary = _primary,
                Secondary = _secondary,
                Melee = _melee*/
            },
            0,
            _equippedWeapons ?? new EquippedWeapons(
                _primary,
                _secondary,
                _melee,
                _temporary),
            new ArmourSetContext(_armour),
            null
            );
    }
}