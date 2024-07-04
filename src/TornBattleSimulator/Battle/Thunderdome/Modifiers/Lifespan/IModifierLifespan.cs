namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;

public interface IModifierLifespan
{
    bool Expired { get; }

    void Tick(ThunderdomeContext thunderdomeContext);

    /*bool AfterNextOpponentAction();

    bool AfterNextOwnAction();*/
}