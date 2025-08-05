using UnityEngine;

public class Entity_Health : MonoBehaviour, IDamageable
{
    private Entity_VFX entityVFX;
    private Entity entity;

    [SerializeField] private float currentHP;
    [SerializeField] public float maxHP = 100f;
    [SerializeField] protected bool isDead = false;

    [Header("On Damage Knockback")]
    [SerializeField] protected Vector2 knockbackPower = new Vector2(1.5f, 2.5f);
    [SerializeField] private Vector2 heavyDamageKnockbackPower = new Vector2(7f, 7f);
    [SerializeField] protected float knockbackDuration = .2f;
    [SerializeField] private float heavyKnockbackDuration = .5f;
    [Header("On Heavy Damage Knockback")]
    [SerializeField] private float heavyDamageThreshold = .3f; // Percentage of maxHP to trigger heavy knockback

    protected virtual void Awake()
    {
        entityVFX = GetComponent<Entity_VFX>();
        entity = GetComponent<Entity>();

        currentHP = maxHP;
    }

    public virtual void TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead) return;

        Vector2 knockback = CalculateKnockBack(damage, damageDealer);
        float duration = CalculateKnockbackDuration(damage);

        entity?.ReceiveKnockback(knockback, duration);
        entityVFX?.PlayOnDamageVFX();
        ReduceHP(damage);
    }

    protected void ReduceHP(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
            Die();
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Entity has died.");
        entity.EntityDeath();
    }

    private Vector2 CalculateKnockBack(float damage, Transform damageDealer)
    {
        int direction = transform.position.x < damageDealer.position.x ? -1 : 1;
        Vector2 knockBack = IsHeavyDamage(damage) ? heavyDamageKnockbackPower : knockbackPower;
        return knockBack;
    }

    private float CalculateKnockbackDuration(float damage) => IsHeavyDamage(damage) ? heavyKnockbackDuration : knockbackDuration;

    private bool IsHeavyDamage(float damage) => damage / maxHP >= heavyDamageThreshold;
}
