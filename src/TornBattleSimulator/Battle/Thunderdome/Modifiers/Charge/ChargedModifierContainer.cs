namespace TornBattleSimulator.Battle.Thunderdome.Modifiers.Charge;

public class ChargedModifierContainer
{
    public ChargedModifierContainer(
        IChargeableModifier modifier)
    {
        Modifier = modifier;
        Charged = modifier.StartsCharged;
    }

    public IChargeableModifier Modifier { get; }

    public bool Charged { get; set; }
}