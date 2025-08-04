using UnityEngine;

public class Entity_Health : MonoBehaviour
{
    [SerializeField] public float maxHP = 100f;
    [SerializeField] protected bool isDead = false;

    public virtual void TakeDamage(float damage)
    {
        if (isDead) return;
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
