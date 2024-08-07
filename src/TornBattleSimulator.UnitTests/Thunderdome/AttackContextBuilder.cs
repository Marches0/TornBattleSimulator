using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Thunderdome.Player.Weapons;

namespace TornBattleSimulator.UnitTests.Thunderdome;

public class AttackContextBuilder
{
    private ThunderdomeContext? _context;
    private PlayerContext? _active;
    private PlayerContext? _other;
    private WeaponContext? _weapon;
    private AttackResult? _attack;

    public AttackContextBuilder WithContext(ThunderdomeContext context)
    {
        _context = context;
        return this;
    }

    public AttackContextBuilder WithActive(PlayerContext player)
    {
        _active = player;
        return this;
    }

    public AttackContextBuilder WithOther(PlayerContext player)
    {
        _other = player;
        return this;
    }

    public AttackContextBuilder WithWeapon(WeaponContext weapon)
    {
        _weapon = weapon;
        return this;
    }

    public AttackContextBuilder WithAttackResult(AttackResult attack)
    {
        _attack = attack;
        return this;
    }

    public AttackContext Build()
    {
        return new AttackContext(
            _context ?? new ThunderdomeContextBuilder().Build(),
            _active ?? new PlayerContextBuilder().Build(),
            _other ?? new PlayerContextBuilder().Build(),
            _weapon ?? new WeaponContextBuilder().Build(),
            _attack
        );
    }
}