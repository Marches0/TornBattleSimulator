namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;

public interface IModifierLifespan : ITickable
{
    bool Expired { get; }
    float Remaining { get; }
}