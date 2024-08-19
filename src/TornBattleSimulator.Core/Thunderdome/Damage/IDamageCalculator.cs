namespace TornBattleSimulator.Core.Thunderdome.Damage;

public interface IDamageCalculator
{
    DamageResult CalculateDamage(AttackContext attack);
}