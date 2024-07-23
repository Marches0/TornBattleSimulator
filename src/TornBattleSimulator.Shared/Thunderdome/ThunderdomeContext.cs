using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.Core.Thunderdome;

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

    public int Turn { get; private set; } = 1;

    public float AttackInterval { get; } = 1;

    public void AttackerActionComplete()
    {
        Attacker.OwnActionComplete(this);
        Defender.OpponentActionComplete(this);
    }

    public void DefenderActionComplete()
    {
        Defender.OwnActionComplete(this);
        Attacker.OpponentActionComplete(this);
    }

    public void TurnComplete()
    {
        Attacker.TurnComplete(this);
        Defender.TurnComplete(this);
        ++Turn;
    }

    public ThunderDomeResult? GetResult()
    {
        if (Defender.Health.CurrentHealth <= 0)
        {
            return ThunderDomeResult.AttackerWin;
        }

        if (Attacker.Health.CurrentHealth <= 0)
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