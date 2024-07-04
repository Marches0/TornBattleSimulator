using TornBattleSimulator.Battle.Build.Equipment;

namespace TornBattleSimulator.Battle.Thunderdome.Events.Data;
public interface IEventData
{
    string Format();
}

public class AttackHitEvent : IEventData
{
    public AttackHitEvent(WeaponType weapon, int damage)
    {
        Weapon = weapon;
        Damage = damage;
    }

    public WeaponType Weapon { get; }

    public int Damage { get; }

    public string Format()
    {
        return $"{Weapon} dealt {Damage}";
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
    public WeaponModifierType ModifierType { get; }

    public EffectBeginEvent(WeaponModifierType modifierType)
    {
        ModifierType = modifierType;
    }

    public string Format()
    {
        return $"{ModifierType}";
    }
}