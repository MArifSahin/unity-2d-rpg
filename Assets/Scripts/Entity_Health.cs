using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    private Entity_VFX entityVFX;

    [SerializeField] public float maxHP = 100f;
    [SerializeField] protected bool isDead = false;

    protected virtual void Awake()
    {
        entityVFX = GetComponent<Entity_VFX>();
    }

    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead) return;
        entityVFX?.PlayOnDamageVFX();
        ReduceHP(damage);
    }

    protected void ReduceHP(float damage)
    {
        maxHP -= damage;
        if (maxHP <= 0)
            Die();
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Entity has died.");
    }
}
