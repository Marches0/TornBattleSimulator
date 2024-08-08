using TornBattleSimulator.Core.Thunderdome.Damage;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Modifiers.Health;
using TornBattleSimulator.Core.Thunderdome.Player;
using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome.Events.Data;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;

public class HealthModifierApplier : IHealthModifierApplier
{
    public ThunderdomeEvent ModifyHealth(
        ThunderdomeContext context,
        PlayerContext target,
        IHealthModifier healthModifier,
        AttackResult? attackResult)
    {        
        int heal = healthModifier.GetHealthModifier(target, attackResult?.Damage);

        // Don't go above max HP or below 0
        target.Health.CurrentHealth += heal;
        target.Health.CurrentHealth
            = Math.Clamp(target.Health.CurrentHealth, 0, target.Health.MaxHealth);

        return heal >= 0
            ? context.CreateEvent(target, ThunderdomeEventType.Heal, new HealEvent(heal, healthModifier.Effect))
            : context.CreateEvent(target, ThunderdomeEventType.ExtraDamage, new ExtraDamageEvent(-heal, healthModifier.Effect));
    }
}