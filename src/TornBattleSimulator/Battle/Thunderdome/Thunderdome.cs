using TornBattleSimulator.Battle.Thunderdome.Damage;

namespace TornBattleSimulator.Battle.Thunderdome;

public class Thunderdome
{
    private readonly DamageCalculator _damageCalculator;
    private readonly ThunderdomeContext _context;

    public delegate Thunderdome Create(ThunderdomeContext context);
    
    public Thunderdome(
        DamageCalculator damageCalculator,
        ThunderdomeContext context)
    {
        _damageCalculator = damageCalculator;
        _context = context;
    }

    public void Battle()
    {
        int turnNumber = 0;
        do
        {
            _context.Tick();
        } while (turnNumber < 25);

        // Temps tick
        // Attacker moves
        // Defender moves
        // ?
        var dmg = _damageCalculator.CalculateDamage(_context);
    }
}