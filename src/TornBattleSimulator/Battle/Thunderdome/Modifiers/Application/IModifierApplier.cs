using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome;
using TornBattleSimulator.Core.Thunderdome.Modifiers;

namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Application;

public interface IModifierApplier
{
    List<ThunderdomeEvent> ApplyModifier(IModifier modifier, AttackContext attack);

    List<ThunderdomeEvent> ApplyOtherHeals(AttackContext attack);

    List<ThunderdomeEvent> ApplyFightStartModifiers(ThunderdomeContext context);

    List<ThunderdomeEvent> ApplyBetweenActionModifiers(ThunderdomeContext context);
}