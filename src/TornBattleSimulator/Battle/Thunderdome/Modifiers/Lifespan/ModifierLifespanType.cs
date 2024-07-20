namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Lifespan;

public enum ModifierLifespanType
{
    Temporal = 1,
    Turns,
    AfterCurrentAction,
    AfterNextEnemyAction,
    AfterNextOwnAction,
    Indefinite
}