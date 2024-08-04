namespace TornBattleSimulator.Core.Thunderdome.Modifiers;

public interface IOwnedLifespan : IModifier
{
    bool Expired(AttackContext attack);
}