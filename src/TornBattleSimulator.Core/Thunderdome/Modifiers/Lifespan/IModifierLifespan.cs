namespace TornBattleSimulator.Core.Thunderdome.Modifiers.Lifespan;

/// <summary>
///  How much longer a modifier will be active for.
/// </summary>
public interface IModifierLifespan : ITickable
{
    /// <summary>
    ///  Whether or not the lifespan has expired.
    /// </summary>
    bool Expired { get; }
    
    /// <summary>
    ///  How much time is left in the lifespan.
    /// </summary>
    /// <remarks>
    ///  The units depend on the <see cref="IModifierLifespan"/> implementation.
    /// </remarks>
    // todo? needed? remove?
    float Remaining { get; }
}