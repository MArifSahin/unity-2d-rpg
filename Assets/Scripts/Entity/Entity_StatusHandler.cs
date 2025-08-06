using System.Collections;
using UnityEngine;

public class Entity_StatusHandler : MonoBehaviour
{
    private ElementType currentEffect = ElementType.None;
    private Entity entity;
    private Entity_VFX vfx;
    private Entity_Stats stats;

    void Awake()
    {
        entity = GetComponent<Entity>();
        vfx = GetComponent<Entity_VFX>();
        stats = GetComponent<Entity_Stats>();
    }

    public void ApplyChilledEffect(float duration, float slowMultiplier)
    {
        float iceResistance = stats.GetElementalResistance(ElementType.Ice);
        float reducedDuration = duration * (1 - iceResistance);

        StartCoroutine(ChilledEffectCo(reducedDuration, slowMultiplier));
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
