using TornBattleSimulator.Battle.Thunderdome.Events;

namespace TornBattleSimulator.Battle.Thunderdome;

public class ThunderdomeContext
{
    public ThunderdomeContext(
        PlayerContext attacker,
        PlayerContext defender)
    {
        Attacker = attacker;
        Defender = defender;
    }

    public PlayerContext Attacker { get; }
    public PlayerContext Defender { get; }

    public List<ThunderdomeEvent> Events { get; } = new List<ThunderdomeEvent>();

    // We tick before anyone moves, so start at 0 and consider
    // 1 to be the first turn
    public int Turn { get; private set; } = 0;

    public float AttackInterval { get; } = 1;

    public void Tick()
    {
        Attacker.Tick(this);
        Defender.Tick(this);
        ++Turn;
    }

    public ThunderDomeResult? GetResult()
    {
        if (Defender.Health <= 0)
        {
            return ThunderDomeResult.AttackerWin;
        }

        if (Attacker.Health <= 0)
        {
            return ThunderDomeResult.DefenderWin;
        }

        if (Turn >= 25)
        {
            return ThunderDomeResult.Stalemate;
        }

        return null;
    }
}