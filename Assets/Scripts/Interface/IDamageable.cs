using UnityEngine;

public interface IDamageable
{
    public bool TakeDamage(float damage, float elementalDamage, Transform damageDealer);
}
