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

    public float AttackInterval { get; } = 1;

    public void Tick()
    {
        Attacker.Tick(this);
        Defender.Tick(this);
    }
}