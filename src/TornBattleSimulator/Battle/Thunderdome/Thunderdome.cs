using TornBattleSimulator.Battle.Thunderdome.Damage;

namespace TornBattleSimulator.Battle.Thunderdome;

public class Thunderdome
{
    private readonly ThunderdomeContext _context;
    private readonly IDamageCalculator _damageCalculator;

    public delegate Thunderdome Create(ThunderdomeContext context);
    
    public Thunderdome(
        ThunderdomeContext context,
        IDamageCalculator damageCalculator)
    {
        _context = context;
        _damageCalculator = damageCalculator;
    }

    public void Battle()
    {
        _context.Attacker.CurrentAction = Strategy.BattleAction.AttackPrimary;
        var dmg = _damageCalculator.CalculateDamage(_context, _context.Attacker, _context.Defender);

        int turnNumber = 0;
        do
        {
            _context.Tick();
        } while (turnNumber < 25);

        // Temps tick
        // Attacker moves
        // Defender moves
        // ?
    }
}