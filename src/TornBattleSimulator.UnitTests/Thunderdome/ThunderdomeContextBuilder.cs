using TornBattleSimulator.Shared.Thunderdome;
using TornBattleSimulator.Shared.Thunderdome.Player;

namespace TornBattleSimulator.UnitTests.Thunderdome;

public class ThunderdomeContextBuilder
{
    private PlayerContext _attacker;
    private PlayerContext _defender;

    public ThunderdomeContextBuilder WithParticipants(PlayerContextBuilder attacker, PlayerContextBuilder defender)
    {
        return WithParticipants(attacker.Build(), defender.Build());
    }

    public ThunderdomeContextBuilder WithParticipants(PlayerContext attacker, PlayerContext defender)
    {
        _attacker = attacker;
        _defender = defender;
        return this;
    }

    public ThunderdomeContext Build()
    {
        return new ThunderdomeContext(_attacker, _defender);
    }
}