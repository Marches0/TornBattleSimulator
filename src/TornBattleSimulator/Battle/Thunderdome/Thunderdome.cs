namespace TornBattleSimulator.Battle.Thunderdome;

public class Thunderdome
{
    private readonly ThunderdomeContext _context;

    public delegate Thunderdome Create(ThunderdomeContext context);
    
    public Thunderdome(
        ThunderdomeContext context)
    {
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
    }
}