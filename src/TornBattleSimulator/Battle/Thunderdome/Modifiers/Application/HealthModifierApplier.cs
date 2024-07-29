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
        DamageResult? damageResult)
    {

        // Don't allow healing above max.
        int heal = Math.Min(
            healthModifier.GetHealthModifier(target, damageResult),
            target.Health.MaxHealth - target.Health.CurrentHealth
        );

        target.Health.CurrentHealth += heal;

        return heal >= 0
            ? context.CreateEvent(target, ThunderdomeEventType.Heal, new HealEvent(heal, healthModifier.Effect))
            : context.CreateEvent(target, ThunderdomeEventType.ExtraDamage, new ExtraDamageEvent(-heal, healthModifier.Effect));
    }
}