using TornBattleSimulator.Shared.Thunderdome.Damage.Modifiers;

namespace TornBattleSimulator.Shared.Thunderdome.Damage;
public class DamageContext
{
    public BodyPart? TargetBodyPart { get; set; }

    public DamageFlags Flags { get; set; }

    public void SetFlag(DamageFlags flag)
    {
        Flags |= flag;
    }
}