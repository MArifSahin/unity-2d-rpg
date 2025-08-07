using System.Collections;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{
    private SpriteRenderer sr;
    private Entity entity;

    [Header("On Taking Damage VFX")]
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] private float onDamageVFXDuration = 0.2f;
    private Material originalMaterial;
    private Coroutine onDamageVFXCoroutine;

    [Header("On Doing Damage VFX")]
    [SerializeField] private Color hitVfxColor = Color.white;
    [SerializeField] private GameObject hitVFXPrefab;
    [SerializeField] private GameObject critHixVFXPrefab;

    [Header("Element Colors")]
    [SerializeField] private Color originalHitVfxColor = Color.white;
    //give hex values for colors
    [SerializeField] private Color burnVFX = Color.red; // Warm orange
    [SerializeField] private Color chillVFX = Color.cyan;  
    [SerializeField] private Color electrifyVFX = Color.yellow;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
        originalHitVfxColor = hitVfxColor; // Store the original color for reset
        entity = GetComponent<Entity>();
    }

    public void PlayOnStatusVFX(float duration, ElementType element)
    {
        if (element == ElementType.Ice)
        {
            StartCoroutine(PlayStatusVFXCo(duration, chillVFX));
        }
        else if (element == ElementType.Fire)
        {
            StartCoroutine(PlayStatusVFXCo(duration, burnVFX));
        }
        else if (element == ElementType.Lightning)
        {
            StartCoroutine(PlayStatusVFXCo(duration, electrifyVFX));
        }
    }

    public void StopAllVFX()
    {
        StopAllCoroutines();
        sr.color = Color.white; // Reset color after stopping all VFX
        sr.material = originalMaterial; // Reset material after stopping all VFX
    }

    private IEnumerator PlayStatusVFXCo(float duration, Color effectColor)
    {
        float tickInterval = 0.25f;
        float timeHasPassed = 0f;

        Color lightColor = effectColor * 1.2f;
        Color darkColor = effectColor * 0.8f;

        bool toggle = false;

        while (timeHasPassed < duration)
        {
            sr.color = toggle ? lightColor : darkColor;
            toggle = !toggle;

            yield return new WaitForSeconds(tickInterval);
            timeHasPassed += tickInterval;
        }

        sr.color = Color.white; // Reset color after effect ends
    }

    public void CreateOnHitVFX(Transform target, bool isCrit)
    {
        GameObject hitPrefab = isCrit ? critHixVFXPrefab : hitVFXPrefab;
        GameObject vfx = Instantiate(hitPrefab, target.position, Quaternion.identity);

        if (!isCrit)
            vfx.GetComponentInChildren<SpriteRenderer>().color = hitVfxColor;

        if (entity.facingDirection == -1 && isCrit)
        {
            vfx.transform.Rotate(0, 180, 0); // Rotate crit VFX for right-facing entities
        }
    }

    public void UpdateOnHitColor(ElementType element)
    {
        switch (element)
        {
            case ElementType.Fire:
                hitVfxColor = burnVFX;
                break;
            case ElementType.Ice:
                hitVfxColor = chillVFX;
                break;
            case ElementType.Lightning:
                hitVfxColor = electrifyVFX;
                break;
            default:
                hitVfxColor = originalHitVfxColor; // Reset to original color for any other
                break;
        }
    }

    public void PlayOnDamageVFX()
    {
        if (onDamageVFXCoroutine != null)
        {
            StopCoroutine(onDamageVFXCoroutine);
        }
        // Start the coroutine to handle the on damage VFX
        onDamageVFXCoroutine = StartCoroutine(OnDamageVFXCo());
    }

    private IEnumerator OnDamageVFXCo()
    {
        // Change the material to the onDamageMaterial
        sr.material = onDamageMaterial;

        // Wait for the specified duration
        yield return new WaitForSeconds(onDamageVFXDuration);

        // Revert back to the original material
        sr.material = originalMaterial;
    }
}
