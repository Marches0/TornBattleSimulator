using TornBattleSimulator.Battle.Build;
using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome;
using TornBattleSimulator.Battle.Thunderdome.Modifiers;
using TornBattleSimulator.Battle.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.UnitTests.Thunderdome;

public class PlayerContextBuilder
{
    private BattleStats? _battleStats;
    private uint _health;
    private Weapon? _primary;
    private Weapon? _secondary;
    private Weapon? _melee;

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

    public PlayerContextBuilder WithSecondary(Weapon weapon)
    {
        _secondary = weapon;
        return this;
    }

    public PlayerContextBuilder WithMelee(Weapon weapon)
    {
        _melee = weapon;
        return this;
    }

    public PlayerContextBuilder WithHealth(uint health)
    {
        _health = health;
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
            new EquippedWeapons(
                _primary != null ? new WeaponContext(_primary, WeaponType.Primary, new List<PotentialModifier>()) : null,
                _secondary != null ? new WeaponContext(_secondary, WeaponType.Secondary, new List<PotentialModifier>()) : null,
                _melee != null ? new WeaponContext(_melee, WeaponType.Melee, new List<PotentialModifier>()) : null,
                null),
            null
            );
    }
}