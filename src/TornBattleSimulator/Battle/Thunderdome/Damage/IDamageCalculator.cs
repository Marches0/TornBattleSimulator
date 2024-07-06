namespace TornBattleSimulator.Battle.Thunderdome.Damage;

public interface IDamageCalculator
{
    DamageResult CalculateDamage(
        ThunderdomeContext context,
        PlayerContext active,
        PlayerContext other);
}