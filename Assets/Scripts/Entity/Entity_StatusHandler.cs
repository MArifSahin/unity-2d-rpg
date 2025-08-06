using System.Collections;
using UnityEngine;

public class Entity_StatusHandler : MonoBehaviour
{
    private ElementType currentEffect = ElementType.None;
    private Entity entity;
    private Entity_VFX vfx;
    private Entity_Stats stats;
    private Entity_Health health;

    void Awake()
    {
        entity = GetComponent<Entity>();
        vfx = GetComponent<Entity_VFX>();
        stats = GetComponent<Entity_Stats>();
        health = GetComponent<Entity_Health>();
    }
    
    public void ApplyBurnEffect(float duration, float totalDamage)
    {
        float fireResistance = stats.GetElementalResistance(ElementType.Fire);
        float finalDamage = totalDamage * (1 - fireResistance);

        if (currentEffect != ElementType.None) return; // Prevent applying if another effect is active

        StartCoroutine(BurnEffectCo(duration, finalDamage));
    }

    public IEnumerator BurnEffectCo(float duration, float totalDamage)
    {
        currentEffect = ElementType.Fire;
        vfx.PlayOnStatusVFX(duration, ElementType.Fire);

        int ticksPerSecond = 2;
        int tickCount = Mathf.RoundToInt(duration * ticksPerSecond);

        float damagePerTick = totalDamage / tickCount;
        float tickInterval = 1f / ticksPerSecond;

        for (int i = 0; i < tickCount; i++)
        {
            health.ReduceHP(damagePerTick);
            yield return new WaitForSeconds(tickInterval);
        }

        currentEffect = ElementType.None;
    }

    public void ApplyChilledEffect(float duration, float slowMultiplier)
    {
        float iceResistance = stats.GetElementalResistance(ElementType.Ice);
        float finalDuration = duration * (1 - iceResistance);

        StartCoroutine(ChilledEffectCo(finalDuration, slowMultiplier));
    }

    private IEnumerator ChilledEffectCo(float duration, float slowMultiplier)
    {
        entity.SlowDownEntity(duration, slowMultiplier);
        currentEffect = ElementType.Ice;
        vfx.PlayOnStatusVFX(duration, ElementType.Ice);


        yield return new WaitForSeconds(duration);
        currentEffect = ElementType.None;
    } 

    public bool CanBeApplied(ElementType element)
    {
        return currentEffect == ElementType.None;
    }
}
