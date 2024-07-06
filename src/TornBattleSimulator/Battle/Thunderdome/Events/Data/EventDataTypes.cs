using TornBattleSimulator.Battle.Build.Equipment;
using TornBattleSimulator.Battle.Thunderdome.Damage.Modifiers;

namespace TornBattleSimulator.Battle.Thunderdome.Events.Data;
public interface IEventData
{
    string Format();
}

public class AttackHitEvent : IEventData
{
    public AttackHitEvent(WeaponType weapon, int damage, BodyPart bodyPart)
    {
        Weapon = weapon;
        Damage = damage;
        BodyPart = bodyPart;
    }

    public WeaponType Weapon { get; }

    public int Damage { get; }

    public BodyPart BodyPart { get; }

    public string Format()
    {
        return $"{Weapon} dealt {Damage} ({BodyPart})";
    }
}

public class AttackMissedEvent : IEventData
{
    public AttackMissedEvent(WeaponType weapon)
    {
        Weapon = weapon;
    }

    public WeaponType Weapon { get; }

    public string Format()
    {
        return $"{Weapon}";
    }
}

public class ReloadEvent : IEventData
{
    public WeaponType Weapon { get; }

    public ReloadEvent(WeaponType weapon)
    {
        Weapon = weapon;
    }

    public string Format()
    {
        return $"{Weapon}";
    }
}

public class EffectBeginEvent : IEventData
{
    public ModifierType ModifierType { get; }

    public EffectBeginEvent(ModifierType modifierType)
    {
        ModifierType = modifierType;
    }

    public string Format()
    {
        return $"{ModifierType}";
    }
}