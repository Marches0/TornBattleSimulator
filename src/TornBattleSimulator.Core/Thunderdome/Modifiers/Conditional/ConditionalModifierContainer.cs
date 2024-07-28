using TornBattleSimulator.Core.Extensions;
using TornBattleSimulator.Core.Thunderdome.Events;
using TornBattleSimulator.Core.Thunderdome.Events.Data;
using TornBattleSimulator.Core.Thunderdome.Player;

namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Conditional;

/// <summary>
///  Tracks the status of a <see cref="IConditionalModifier"/>.
/// </summary>
public class ConditionalModifierContainer : ITickable
{
    private readonly PlayerContext _owner;

    public ConditionalModifierContainer(
        IConditionalModifier modifier,
        PlayerContext owner)
    {
        Modifier = modifier;
        _owner = owner;
    }

    /// <summary>
    ///  The tracked modifier.
    /// </summary>
    public IConditionalModifier Modifier { get; }

    /// <summary>
    ///  Whether or not <see cref="Modifier"/> is currently active.
    /// </summary>
    public bool IsActive { get; private set; }

    /// <inheritdoc/>
    public void FightBegin(ThunderdomeContext context)
    {
        SetIsActive(context);
    }

    /// <inheritdoc/>
    public void OpponentActionComplete(ThunderdomeContext context)
    {
        SetIsActive(context);
    }

    /// <inheritdoc/>
    public void OwnActionComplete(ThunderdomeContext context)
    {
        SetIsActive(context);
    }

    /// <inheritdoc/>
    public void TurnComplete(ThunderdomeContext context)
    {
        SetIsActive(context);
    }

    private void SetIsActive(ThunderdomeContext context)
    {
        PlayerContext other = context.Attacker == _owner
            ? context.Defender
            : context.Attacker;

        bool newValue = Modifier.IsActive(_owner, other);
        if (newValue != IsActive)
        {
            Emit(context, newValue);
            IsActive = newValue;
        }
    }

    private void Emit(ThunderdomeContext context, bool isNowActive)
    {
        ThunderdomeEvent thunderdomeEvent;
        if (isNowActive)
        {
            thunderdomeEvent = context.CreateEvent(
                _owner,
                ThunderdomeEventType.EffectBegin,
                new EffectBeginEvent(Modifier.Effect)
            );
        }
        else
        {
            thunderdomeEvent = context.CreateEvent(
                _owner,
                ThunderdomeEventType.EffectEnd,
                new EffectEndEvent(Modifier.Effect)
            );
        }

        context.Events.Add(thunderdomeEvent);
    }
}