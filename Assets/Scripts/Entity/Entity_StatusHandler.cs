using System.Collections;
using UnityEngine;

public class Entity_StatusHandler : MonoBehaviour
{
    private ElementType currentEffect = ElementType.None;
    private Entity entity;
    private Entity_VFX vfx;
    private Entity_Stats stats;
    private Entity_Health health;

    [Header("Electrify Effect Details")]
    [SerializeField] private GameObject lightningStrikeVFX;
    [SerializeField] private float currentCharge;
    [SerializeField] private float maxCharge = 1f;
    private Coroutine electrifyCoroutine;

    void Awake()
    {
        entity = GetComponent<Entity>();
        vfx = GetComponent<Entity_VFX>();
        stats = GetComponent<Entity_Stats>();
        health = GetComponent<Entity_Health>();
    }

    public void ApplyLightningEffect(float duration, float totalDamage, float charge)
    {
        float lightningResistance = stats.GetElementalResistance(ElementType.Lightning);
        //reduce charge based on resistance
        charge *= 1 - lightningResistance;
        currentCharge += charge;

        if (currentCharge >= maxCharge)
        {
            DoLightningStrike(totalDamage);
            StopElectrifyEffect();
            return;
        }

        if (electrifyCoroutine != null)
        {
            StopCoroutine(electrifyCoroutine);
        }

        electrifyCoroutine = StartCoroutine(ElectrifyEffectCo(duration));
        
    }

    private void StopElectrifyEffect()
    {
        currentEffect = ElementType.None;
        currentCharge = 0;
        vfx.StopAllVFX();
    }

    private void DoLightningStrike(float damage)
    {
        Instantiate(lightningStrikeVFX, transform.position, Quaternion.identity);
        health.ReduceHealth(damage);
    }

    private IEnumerator ElectrifyEffectCo(float duration)
    {
        currentEffect = ElementType.Lightning;
        vfx.PlayOnStatusVFX(duration, ElementType.Lightning);

        yield return new WaitForSeconds(duration);
        StopElectrifyEffect();
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
            health.ReduceHealth(damagePerTick);
            yield return new WaitForSeconds(tickInterval);
        }

        currentEffect = ElementType.None;
    }

    public void ApplyChillEffect(float duration, float slowMultiplier)
    {
        float iceResistance = stats.GetElementalResistance(ElementType.Ice);
        float finalDuration = duration * (1 - iceResistance);

        StartCoroutine(ChillEffectCo(finalDuration, slowMultiplier));
    }

    private IEnumerator ChillEffectCo(float duration, float slowMultiplier)
    {
        entity.SlowDownEntity(duration, slowMultiplier);
        currentEffect = ElementType.Ice;
        vfx.PlayOnStatusVFX(duration, ElementType.Ice);


        yield return new WaitForSeconds(duration);
        currentEffect = ElementType.None;
    } 

    public bool CanBeApplied(ElementType element)
    {
        if (element == ElementType.Lightning && currentEffect == ElementType.Lightning)
            return true;

        return currentEffect == ElementType.None;
    }
}
