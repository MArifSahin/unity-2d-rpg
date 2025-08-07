using UnityEngine;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour, IDamageable
{
    private Slider healthBar;
    private Entity_VFX entityVFX;
    private Entity entity;
    private Entity_Stats stats;


    [SerializeField] private float currentHealth;
    [SerializeField] protected bool isDead = false;
    [Header("Health Regeneration")]
    [SerializeField] private float regenerationInterval = 1f;
    [SerializeField] private bool canRegenerateHealth = true;

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

        currentHealth = stats.GetMaxHP();
        UpdateHealthBar();

        InvokeRepeating(nameof(RegenerateHealth), 0, regenerationInterval);
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
        ReduceHealth(physicalDamageTaken + finalElementalDamage);

        return true;
    }

    private bool AttackEvaded()
    {
        float evasionChance = stats.GetEvasion();
        return Random.Range(0f, 100f) < evasionChance;
    }

    private void RegenerateHealth()
    {
        if (isDead || !canRegenerateHealth) return;

        float regenerationAmount = stats.resources.healthRegeneration.GetValue();
        IncreaseHealth(regenerationAmount);
    }

    public void IncreaseHealth(float healAmount)
    {
        if (isDead) return;

        currentHealth = Mathf.Min(currentHealth + healAmount, stats.GetMaxHP());
        UpdateHealthBar();

    }

    public void ReduceHealth(float damage)
    {
        entityVFX?.PlayOnDamageVFX();
        currentHealth -= damage;
        UpdateHealthBar();
        if (currentHealth <= 0)
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
        healthBar.value = currentHealth / stats.GetMaxHP();
    }

    private void TakeKnockback(Transform damageDealer, float physicalDamageTaken)
    {
        Vector2 knockback = CalculateKnockBack(physicalDamageTaken, damageDealer);
        float duration = CalculateKnockbackDuration(physicalDamageTaken);

        entity?.ReceiveKnockback(knockback, duration);
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
