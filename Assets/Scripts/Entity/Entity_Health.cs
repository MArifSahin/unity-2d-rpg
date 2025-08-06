using UnityEngine;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour, IDamageable
{
    private Slider healthBar;
    private Entity_VFX entityVFX;
    private Entity entity;
    private Entity_Stats stats;


    [SerializeField] private float currentHP;
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
        stats = GetComponent<Entity_Stats>();
        healthBar = GetComponentInChildren<Slider>();

        currentHP = stats.GetMaxHP();
        UpdateHealthBar();
    }

    public virtual bool TakeDamage(float damage, float elementalDamage, ElementType element, Transform damageDealer)
    {
        if (isDead) return false;

        if (AttackEvaded())
        {
            return false;
        }

        Entity_Stats attackerStats = damageDealer.GetComponent<Entity_Stats>();
        float armorReduction = attackerStats ? attackerStats.GetArmorReduction() : 0f;

        float mitigation = stats.GetArmorMitigation(armorReduction);
        float physicalDamageTaken = damage * (1 - mitigation);

        float resistance = stats.GetElementalResistance(element);
        float finalElementalDamage = elementalDamage * (1 - resistance);

        TakeKnockback(damageDealer, physicalDamageTaken);
        ReduceHP(physicalDamageTaken + finalElementalDamage);

        return true;
    }

    private void TakeKnockback(Transform damageDealer, float physicalDamageTaken)
    {
        Vector2 knockback = CalculateKnockBack(physicalDamageTaken, damageDealer);
        float duration = CalculateKnockbackDuration(physicalDamageTaken);

        entity?.ReceiveKnockback(knockback, duration);
    }

    private bool AttackEvaded()
    {
        float evasionChance = stats.GetEvasion();
        return Random.Range(0f, 100f) < evasionChance;
    }

    protected void ReduceHP(float damage)
    {
        entityVFX?.PlayOnDamageVFX();
        currentHP -= damage;
        UpdateHealthBar();
        if (currentHP <= 0)
            Die();
    }

    private void Die()
    {
        isDead = true;
        entity.EntityDeath();
    }

    private void UpdateHealthBar()
    {   
        if (healthBar == null) return;
        healthBar.value = currentHP / stats.GetMaxHP();
    }

    private Vector2 CalculateKnockBack(float damage, Transform damageDealer)
    {
        int direction = transform.position.x < damageDealer.position.x ? -1 : 1;
        Vector2 knockBack = IsHeavyDamage(damage) ? heavyDamageKnockbackPower : knockbackPower;
        return knockBack * direction;
    }

    private float CalculateKnockbackDuration(float damage) => IsHeavyDamage(damage) ? heavyKnockbackDuration : knockbackDuration;

    private bool IsHeavyDamage(float damage) => damage / stats.GetMaxHP() >= heavyDamageThreshold;
}
