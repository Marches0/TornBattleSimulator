namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;

public interface IModifierLifespan
{
    bool Expired { get; }

    void Tick(ThunderdomeContext thunderdomeContext);

    float Remaining { get; }

    /*bool AfterNextOpponentAction();

    bool AfterNextOwnAction();*/
}