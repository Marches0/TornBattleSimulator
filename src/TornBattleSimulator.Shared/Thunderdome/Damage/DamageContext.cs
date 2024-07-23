using TornBattleSimulator.Core.Thunderdome.Damage.Modifiers;

namespace TornBattleSimulator.Core.Thunderdome.Damage;
public class DamageContext
{
    public BodyPart? TargetBodyPart { get; set; }

    public DamageFlags Flags { get; set; }

    public void SetFlag(DamageFlags flag)
    {
        Flags |= flag;
    }
}