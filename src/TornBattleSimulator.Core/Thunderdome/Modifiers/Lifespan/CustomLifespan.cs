using TornBattleSimulator.Core.Thunderdome.Damage;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

/// <summary>
///  A <see cref="ModifierLifespanType.Custom"/> <see cref="IModifierLifespan"/>.
/// </summary>
public class CustomLifespan : IModifierLifespan
{
    public bool Expired { get; private set; } = false;

    public float Remaining => 1f;

    public void SetExpiry(AttackResult? attack, IOwnedLifespan modifier)
    {
        Expired = modifier.Expired(attack);
    }

    public void FightBegin(ThunderdomeContext context) { }

    public void OpponentActionComplete(ThunderdomeContext context) { }

    public void OwnActionComplete(ThunderdomeContext context) { }

    public void TurnComplete(ThunderdomeContext context) { }

}