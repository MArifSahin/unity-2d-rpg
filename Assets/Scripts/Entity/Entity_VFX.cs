using System.Collections;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("On Damage VFX")]
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] private float onDamageVFXDuration = 0.2f;
    private Material originalMaterial;
    private Coroutine onDamageVFXCoroutine;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
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
