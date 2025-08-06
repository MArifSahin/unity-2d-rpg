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

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
        entity = GetComponent<Entity>();
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
