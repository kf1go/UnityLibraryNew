using UnityEngine;

public struct DamageArgument
{
    public readonly float ResultDamage => damage * multiplier;

    public float damage;
    public float multiplier;
    public EDamageType type;
    public DamageArgument(float damage, float multiplier, EDamageType type = EDamageType.None)
    {
        this.damage = damage;
        this.multiplier = multiplier;
        this.type = type;
    }
}
