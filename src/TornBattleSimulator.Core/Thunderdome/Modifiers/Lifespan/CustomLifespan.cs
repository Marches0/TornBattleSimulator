using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

/// <summary>
///  A <see cref="ModifierLifespanType.Custom"/> <see cref="IModifierLifespan"/>.
/// </summary>
public class CustomLifespan : IModifierLifespan
{
    public bool Expired { get; private set; } = false;

    public float Remaining => 1f;

    public void SetExpiry(
        PlayerContext owner,
        AttackResult? attack,
        IOwnedLifespan modifier)
    {
        Expired = modifier.Expired(owner, attack);
    }

    public void FightBegin(ThunderdomeContext context) { }

    public void OpponentActionComplete(ThunderdomeContext context) { }

    public void OwnActionComplete(ThunderdomeContext context) { }

    public void TurnComplete(ThunderdomeContext context) { }

}