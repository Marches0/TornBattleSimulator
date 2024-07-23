namespace TornBattleSimulator.Shared.Thunderdome.Modifiers.Lifespan;

public interface IModifierLifespan : ITickable
{
    bool Expired { get; }
    float Remaining { get; }
}